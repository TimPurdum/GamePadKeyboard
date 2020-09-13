using System;
using System.Collections.Generic;
using System.ComponentModel;
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
            BindingContext = this;
            SizeChanged += (sender, args) =>
            {
                ButtonsX = Constraint.Constant(Width - (ButtonsFrame.Width + 40));
            };
        }

        public Constraint ButtonsX
        {
            get => _buttonsX;
            set
            {
                if (_buttonsX != value)
                {
                    _buttonsX = value;
                    OnPropertyChanged();
                }
            }
        }

        public event EventHandler<KeyPressEventArgs> KeyPressed;
        public event EventHandler<KeyPressEventArgs> KeyReleased;

        private void OnButtonPressed(object sender, EventArgs e)
        {
            var key = GetKeyFromSender(sender);
            
            var args = new KeyPressEventArgs(CurrentKeyMap.Map[key]);
            KeyPressed?.Invoke(this, args);
        }

        private void OnButtonReleased(object sender, EventArgs e)
        {
            var key = GetKeyFromSender(sender);
            
            var args = new KeyPressEventArgs(CurrentKeyMap.Map[key]);
            KeyReleased?.Invoke(this, args);
        }

        private Key GetKeyFromSender(object sender)
        {
            if (sender == UpButton)
            {
                return Key.Up;
            }
            if (sender == DownButton)
            {
                return Key.Down;
            }

            if (sender == LeftButton)
            {
                return Key.Left;
            }
            if (sender == RightButton)
            {
                return Key.Right;
            }

            return Key.None;
        }

        private Constraint _buttonsX;
    }

    public class KeyPressEventArgs: EventArgs
    {
        public KeyPressEventArgs(string keyValue)
        {
            KeyValue = keyValue;
        }

        public string KeyValue { get; }
    }
}