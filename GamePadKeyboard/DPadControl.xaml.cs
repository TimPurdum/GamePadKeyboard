using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GamePadKeyboard
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DPadControl : ContentView
    {
        public DPadControl()
        {
            InitializeComponent();
        }


        public Constraint ButtonsY { get; set; }
    }
}