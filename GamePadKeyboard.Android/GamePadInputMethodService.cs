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
using View = Android.Views.View;
using Keycode = Android.Views.Keycode;

namespace GamePadKeyboard.Droid
{
    [Service(Name = "GamePadKeyboard.Droid.GamePadInputMethodService", Label = "GamePad Keyboard",
        Permission = Manifest.Permission.BindInputMethod)]
    [MetaData("android.view.im", Resource = "@xml/method")]
    [IntentFilter(new[] {"android.view.InputMethod"})]
    public class GamePadInputMethodService : InputMethodService, View.IOnClickListener
    {
        public override void OnCreate()
        {
            base.OnCreate();
            SetNotification(true);
        }

        public void OnClick(View v)
        {
            
        }


        public override View OnCreateInputView()
        {
            Forms.Init(this, null);
            var gamepadView = new GamePad();
            var screenWidthPx = Resources.DisplayMetrics.WidthPixels;
            var screeHeightPx = Resources.DisplayMetrics.HeightPixels / 3;

            if (IsDeviceSurfaceDuo(ApplicationContext))
            {
                var manager = ApplicationContext.GetSystemService(Context.WindowService).JavaCast<IWindowManager>();
                var surfaceOrientation = manager.DefaultDisplay.Orientation;
                if (surfaceOrientation == (int)SurfaceOrientation.Rotation90 ||
                    surfaceOrientation == (int)SurfaceOrientation.Rotation270)
                {
                    screeHeightPx = Resources.DisplayMetrics.HeightPixels;
                }
            }
            var androidView = ConvertFormsToNative(gamepadView,
                new Rectangle(0, 0, screenWidthPx, screeHeightPx));

            gamepadView.KeyPressed += OnGamePadKeyPressed;
            gamepadView.KeyReleased += OnGamePadKeyReleased;
            return androidView;
        }


        public bool IsDeviceSurfaceDuo(Context context, string feature = "com.microsoft.device.display.displaymask")
            => context?.PackageManager?.HasSystemFeature(feature) ?? false;


        private void OnGamePadKeyPressed(object sender, KeyPressEventArgs e)
        {
            var ic = CurrentInputConnection;
            var clickedKeyText = e.KeyValue;
            Enum.TryParse<Keycode>(clickedKeyText, out var keyCode);
            ic.SendKeyEvent(new KeyEvent(KeyEventActions.Down, keyCode));
            ic.CommitText(clickedKeyText, 1);
        }


        private void OnGamePadKeyReleased(object sender, KeyPressEventArgs e)
        {
            var ic = CurrentInputConnection;
            var clickedKeyText = e.KeyValue;
            Enum.TryParse<Keycode>(clickedKeyText, out var keyCode);
            ic.SendKeyEvent(new KeyEvent(KeyEventActions.Up, keyCode));
            ic.CommitText(clickedKeyText, 1);
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
            {
                // Notification channels are new in API 26 (and not a part of the
                // support library). There is no need to create a notification
                // channel on older versions of Android.
                return;
            }

            var channelName = "GamePadKeyboard";
            var channelDescription = "THis is the notification";
            var channel = new NotificationChannel(NotificationChannelId, channelName, NotificationImportance.Default)
            {
                Description = channelDescription
            };

            var notificationManager = (NotificationManager)GetSystemService(NotificationService);
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

                var configIntent = new Intent(NotificationReceiver.ActionSettings);
                var configPendingIntent =
                    PendingIntent.GetBroadcast(ApplicationContext, 2, configIntent, 0);

                var title = "Show GamePad Keyboard";
                var body = "Select this to open the game pad. Disable in settings.";

                var mBuilder = new NotificationCompat.Builder(this, NotificationChannelId)
                    .SetSmallIcon(Resource.Drawable.notify_panel_notification_icon_bg)
                    .SetAutoCancel(false) //Make this notification automatically dismissed when the user touches it -> false.
                    .SetTicker(text)
                    .SetContentTitle(title)
                    .SetContentText(body)
                    .SetContentIntent(contentIntent)
                    .SetOngoing(true)
                    .AddAction(Resource.Drawable.notification_action_background, "Settings",
                        configPendingIntent)
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
        private NotificationReceiver _notificationReceiver;
    }
}