using System;
using Android;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.InputMethodServices;
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
    [Service (Name = "GamePadKeyboard.Droid.GamePadInputMethodService", Label = "GamePad Keyboard", 
        Permission = Manifest.Permission.BindInputMethod)]
    [MetaData("android.view.im", Resource = "@xml/method")]
    [IntentFilter(new []{"android.view.InputMethod"})]
    public class GamePadInputMethodService: InputMethodService, View.IOnClickListener
    {
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

            var androidView = ConvertFormsToNative(gamepadView, new Rectangle(0, 0, screenWidthPx, gamepadView.HeightRequest));
            
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


        private double PxToDp(double px)
        {
            var dm = Resources.DisplayMetrics;
            return Math.Round(px / (dm.Xdpi / dm.Density));
        }


        private View ConvertFormsToNative(Xamarin.Forms.View formsView, Rectangle size)
        {
            var vRenderer = Platform.CreateRendererWithContext(formsView, ApplicationContext);
            var nativeView = vRenderer.View;
            vRenderer.Tracker.UpdateLayout();
            var layoutParams = new ViewGroup.LayoutParams((int)size.Width, (int)size.Height);
            nativeView.LayoutParameters = layoutParams;
            formsView.Layout(size);
            nativeView.Layout(0, 0, (int)formsView.WidthRequest, (int)formsView.HeightRequest);
            var layout = LayoutInflater.Inflate(Resource.Layout.keyboard_view, null) as LinearLayout;
            layout.AddView(nativeView);
            return layout;
        }
    }
}