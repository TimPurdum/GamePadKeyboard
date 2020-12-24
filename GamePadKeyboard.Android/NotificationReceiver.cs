using System.Threading.Tasks;
using Android.Content;
using Android.Views.InputMethods;
using Java.Lang;

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
            try
            {
                var action = intent.Action;

                if (action == ActionShow)
                {
                    _imeService.RequestShowSelf(ShowFlags.Forced);
                }
            }
            catch (Exception ex)
            {
                Task.Run(() => ExceptionHandler.Handle(ex));
            }
        }

        public const string ActionShow = "com.cedarrivertech.gamepadkeyboard.SHOW";
        public const string ActionSettings = "com.cedarrivertech.gamepadkeyboard.SETTINGS";
        private readonly GamePadInputMethodService _imeService;
    }
}