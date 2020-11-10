using System;
using Android;
using Android.App;
using Android.Content;
using Android.InputMethodServices;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Keycode = Android.Views.Keycode;
using View = Android.Views.View;

namespace GamePadKeyboard.Droid
{
    [Service(Name = "GamePadKeyboard.Droid.GamePadInputMethodService", Label = "S-Duo GamePad",
        Permission = Manifest.Permission.BindInputMethod)]
    [MetaData("android.view.im", Resource = "@xml/method")]
    [IntentFilter(new[] {"android.view.InputMethod"})]
    public class GamePadInputMethodService : InputMethodService, View.IOnClickListener
    {
        public GamePadInputMethodService()
        {
            _keyMap = new AndroidKeyMap();
        }

        public void OnClick(View v)
        {
        }

        public override void OnCreate()
        {
            base.OnCreate();
            SetNotification(true);
        }


        public override View OnCreateInputView()
        {
            Forms.Init(this, null);
            var gamepadView = new GamePad();
            var screenWidthPx = Resources.DisplayMetrics.WidthPixels;
            var screeHeightPx = Resources.DisplayMetrics.HeightPixels / 3;

            if (IsDeviceSurfaceDuo(ApplicationContext))
            {
                var manager = ApplicationContext.GetSystemService(WindowService).JavaCast<IWindowManager>();
                var surfaceOrientation = manager.DefaultDisplay.Rotation;
                if (surfaceOrientation == SurfaceOrientation.Rotation90 ||
                    surfaceOrientation == SurfaceOrientation.Rotation270)
                    screeHeightPx = Resources.DisplayMetrics.HeightPixels / 2;
            }

            var androidView = ConvertFormsToNative(gamepadView,
                new Rectangle(0, 0, screenWidthPx, screeHeightPx));

            gamepadView.KeyPressed += OnGamePadKeyPressed;
            gamepadView.KeyReleased += OnGamePadKeyReleased;
            gamepadView.SettingsPressed += OnSettingsPressed;
            return androidView;
        }

        private void OnSettingsPressed(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(MainActivity));
            intent.SetFlags(ActivityFlags.NewTask);
            StartActivity(intent);
            RequestShowSelf(ShowFlags.Forced);
        }


        public bool IsDeviceSurfaceDuo(Context context, string feature = "com.microsoft.device.display.displaymask")
        {
            return context?.PackageManager?.HasSystemFeature(feature) ?? false;
        }


        private void OnGamePadKeyPressed(object sender, KeyPressEventArgs e)
        {
            var ic = CurrentInputConnection;
            var formsKey = e.KeyValue;
            var keyCode = _keyMap.GetNativeKeyCode(formsKey) as Keycode?;
            if (keyCode == null) return;
            ic.SendKeyEvent(new KeyEvent(KeyEventActions.Down, keyCode.Value));
        }


        private void OnGamePadKeyReleased(object sender, KeyPressEventArgs e)
        {
            var ic = CurrentInputConnection;
            var formsKey = e.KeyValue;
            var keyCode = _keyMap.GetNativeKeyCode(formsKey) as Keycode?;
            if (keyCode == null) return;
            ic.SendKeyEvent(new KeyEvent(KeyEventActions.Up, keyCode.Value));
        }


        private View ConvertFormsToNative(Xamarin.Forms.View formsView, Rectangle size)
        {
            var layout = LayoutInflater.Inflate(Resource.Layout.keyboard_view, null) as LinearLayout;
            var vRenderer = Platform.CreateRendererWithContext(formsView, ApplicationContext);
            var nativeView = vRenderer.View;
            vRenderer.Tracker.UpdateLayout();
            var layoutParams = new ViewGroup.LayoutParams((int) size.Width, (int) size.Height);
            nativeView.LayoutParameters = layoutParams;
            var density = Resources.DisplayMetrics.Density;
            formsView.Layout(new Rectangle(0, 0, size.Width / density, size.Height / density));
            layout.AddView(nativeView);
            return layout;
        }


        private void CreateNotificationChannel()
        {
            if (Build.VERSION.SdkInt < BuildVersionCodes.O)
                // Notification channels are new in API 26 (and not a part of the
                // support library). There is no need to create a notification
                // channel on older versions of Android.
                return;

            var channelName = "GamePadKeyboard";
            var channelDescription = "THis is the notification";
            var channel = new NotificationChannel(NotificationChannelId, channelName, NotificationImportance.Default)
            {
                Description = channelDescription
            };

            var notificationManager = (NotificationManager) GetSystemService(NotificationService);
            notificationManager.CreateNotificationChannel(channel);
        }

        private void SetNotification(bool visible)
        {
            var notificationManager = NotificationManager.FromContext(ApplicationContext);

            if (visible && _notificationReceiver == null)
            {
                CreateNotificationChannel();

                var text = "Keyboard notification enabled.";

                _notificationReceiver = new NotificationReceiver(this);
                var pFilter = new IntentFilter(NotificationReceiver.ActionShow);
                pFilter.AddAction(NotificationReceiver.ActionSettings);
                RegisterReceiver(_notificationReceiver, pFilter);

                var notificationIntent = new Intent(NotificationReceiver.ActionShow);
                var contentIntent = PendingIntent.GetBroadcast(ApplicationContext, 1, notificationIntent, 0);
                
                var title = "Show GamePad Keyboard";
                var body = "Tap here to open the gamepad. Disable in settings.";

                var mBuilder = new NotificationCompat.Builder(this, NotificationChannelId)
                    .SetSmallIcon(Resource.Drawable.notify_panel_notification_icon_bg)
                    .SetAutoCancel(
                        false) //Make this notification automatically dismissed when the user touches it -> false.
                    .SetTicker(text)
                    .SetContentTitle(title)
                    .SetContentText(body)
                    .SetContentIntent(contentIntent)
                    .SetOngoing(true)
                    .SetPriority((int) NotificationPriority.Default);

                var notificationManagerCompat = NotificationManagerCompat.From(this);

                // notificationId is a unique int for each notification that you must define
                notificationManagerCompat.Notify(NotificationOngoingId, mBuilder.Build());
            }
            else if (_notificationReceiver != null)
            {
                notificationManager.Cancel(NotificationOngoingId);
                UnregisterReceiver(_notificationReceiver);
                _notificationReceiver = null;
            }
        }

        private const string NotificationChannelId = "GamePadKeyboard";
        private const int NotificationOngoingId = 1001;
        private const int OpenSettingsNotificationId = 1002;
        private readonly AndroidKeyMap _keyMap;
        private NotificationReceiver _notificationReceiver;
    }
}