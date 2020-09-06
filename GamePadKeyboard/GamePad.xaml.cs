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
    public partial class GamePad : ContentView
    {
        public GamePad()
        {
            InitializeComponent();
        }


        public event EventHandler<KeyPressEventArgs> KeyPressed;

        private void OnButtonClicked(object sender, EventArgs e)
        {
            Key key = Key.Up;
            if (sender == UpButton)
            {
                key = Key.Up;
            }
            else if (sender == DownButton)
            {
                key = Key.Down;
            }
            else if (sender == LeftButton)
            {
                key = Key.Left;
            }
            else if (sender == RightButton)
            {
                key = Key.Right;
            }
            
            var args = new KeyPressEventArgs(CurrentKeyMap.Map[key], sender as Button);
            KeyPressed?.Invoke(this, args);
        }
    }

    public class KeyPressEventArgs: EventArgs
    {
        public KeyPressEventArgs(string keyValue, Button button)
        {
            KeyValue = keyValue;
            Button = button;
        }

        public string KeyValue { get; }
        public Button Button { get; }
    }
}