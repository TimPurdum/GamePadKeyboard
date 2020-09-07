using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.InputMethodServices;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;

namespace GamePadKeyboard.Droid
{
    public class NotificationReceiver: BroadcastReceiver
    {
        private readonly GamePadInputMethodService _imeService;
        public static string ActionShow = "com.cedarrivertech.gamepadkeyboard.SHOW";
        public static string ActionSettings = "com.cedarrivertech.gamepadkeyboard.SETTINGS";

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
    }
}