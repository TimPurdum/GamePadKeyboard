using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views.InputMethods;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Platform = Xamarin.Essentials.Platform;

namespace GamePadKeyboard.Droid
{
    [Activity(Label = "S-Duo GamePad", Icon = "@mipmap/sDuoGamePad", Theme = "@style/MainTheme", MainLauncher = true,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode |
                               ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize)]
    public class MainActivity : FormsAppCompatActivity
    {
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions,
            [GeneratedEnum] Permission[] grantResults)
        {
            Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            Platform.Init(this, savedInstanceState);
            Forms.Init(this, savedInstanceState);
            LoadApplication(new App());
            MessagingCenter.Subscribe<MainPage>(this, "Show GamePad", _ =>
            {
                var imm = (InputMethodManager)ApplicationContext.GetSystemService(Service.InputMethodService);
                if (imm.CurrentInputMethodSubtype.ExtraValue != "S-Duo GamePad")
                {
                    imm.ShowInputMethodPicker();
                }
                imm.ToggleSoftInput(ShowFlags.Forced, HideSoftInputFlags.None);
            });
        }
    }
}