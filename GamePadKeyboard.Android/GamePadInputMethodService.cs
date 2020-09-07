using Android;
using Android.App;
using Android.Content;
using Android.InputMethodServices;
using Android.OS;
using Android.Support.V4.App;
using Android.Util;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Button = Android.Widget.Button;
using View = Android.Views.View;

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
            OnSetNotification(true);
        }

        private NotificationReceiver _notificationReceiver;

        public void OnClick(View v)
        {
            var ic = CurrentInputConnection;
            if (ic != null && v is Button b)
            {
                var clickedKeyText = b.Tag.ToString();
                ic.CommitText(clickedKeyText, 1);
            }
        }


        public override View OnCreateInputView()
        {
            Forms.Init(this, null);
            var gamepadView = new GamePad();
            var screenWidthPx = Resources.DisplayMetrics.WidthPixels;

            var androidView = OnConvertFormsToNative(gamepadView,
                new Rectangle(0, 0, screenWidthPx, gamepadView.HeightRequest));

            Log.Debug("AndroidView", $"Height: {androidView.Height}");
            gamepadView.KeyPressed += OnGamePadKeyPressed;
            return androidView;
        }


        private void OnGamePadKeyPressed(object sender, KeyPressEventArgs e)
        {
            var button = new Button(ApplicationContext);
            button.Tag = e.KeyValue;
            OnClick(button);
        }


        private View OnConvertFormsToNative(Xamarin.Forms.View formsView, Rectangle size)
        {
            var vRenderer = Platform.CreateRendererWithContext(formsView, ApplicationContext);
            var nativeView = vRenderer.View;
            vRenderer.Tracker.UpdateLayout();
            var layoutParams = new ViewGroup.LayoutParams((int) size.Width, (int) size.Height);
            nativeView.LayoutParameters = layoutParams;
            formsView.Layout(size);
            nativeView.Layout(0, 0, (int) formsView.WidthRequest, (int) formsView.HeightRequest);
            var layout = LayoutInflater.Inflate(Resource.Layout.keyboard_view, null) as LinearLayout;
            layout.AddView(nativeView);
            return layout;
        }


        private void OnCreateNotificationChannel()
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

        private void OnSetNotification(bool visible)
        {
            var ns = NotificationService;
            var mNotificationManager = NotificationManager.FromContext(ApplicationContext);

            if (visible && _notificationReceiver == null)
            {
                OnCreateNotificationChannel();

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

                var title = "Show Hacker's Keyboard";
                var body = "Select this to open the keyboard. Disable in settings.";

                var mBuilder = new NotificationCompat.Builder(this, NotificationChannelId)
                    .SetSmallIcon(Resource.Drawable.notify_panel_notification_icon_bg)
                    .SetAutoCancel(
                        false) //Make this notification automatically dismissed when the user touches it -> false.
                    .SetTicker(text)
                    .SetContentTitle(title)
                    .SetContentText(body)
                    .SetContentIntent(contentIntent)
                    .SetOngoing(true)
                    .AddAction(Resource.Drawable.notification_action_background, "NotificationActionSetting",
                        configPendingIntent)
                    .SetPriority((int) NotificationPriority.Default);

                var notificationManager = NotificationManagerCompat.From(this);

                // notificationId is a unique int for each notification that you must define
                notificationManager.Notify(NotificationOngoingId, mBuilder.Build());
            }
            else if (_notificationReceiver != null)
            {
                mNotificationManager.Cancel(NotificationOngoingId);
                UnregisterReceiver(_notificationReceiver);
                _notificationReceiver = null;
            }
        }

        private const string NotificationChannelId = "GamePadKeyboard";
        private const int NotificationOngoingId = 1001;
        private const string PrefKeyboardNotification = "keyboard_notification";
    }
}