using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GamePadKeyboard
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DPadControl : ContentView
    {
        private Constraint _buttonsY;

        public DPadControl()
        {
            InitializeComponent();
        }


        public Constraint ButtonsY
        {
            get => _buttonsY;
            set => _buttonsY = value;
        }
    }
}