using Android.Content;
using Android.Views.InputMethods;
using Xamarin.Forms;

namespace GamePadKeyboard.Droid
{
    public class NotificationReceiver : BroadcastReceiver
    {
        public NotificationReceiver(GamePadInputMethodService imeService)
        {
            _imeService = imeService;
        }

        public override void OnReceive(Context context, Intent intent)
        {
            var action = intent.Action;

            if (action == ActionShow)
            {
                _imeService.RequestShowSelf(ShowFlags.Forced);
            }
        }

        public const string ActionShow = "com.cedarrivertech.gamepadkeyboard.SHOW";
        public const string ActionSettings = "com.cedarrivertech.gamepadkeyboard.SETTINGS";
        private readonly GamePadInputMethodService _imeService;
    }
}