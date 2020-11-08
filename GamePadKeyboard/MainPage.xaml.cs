using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GamePadKeyboard
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            BindingContext = this;
            _upKey = UserKeyMap.GetMappedKey(FormsKey.Up);
            _downKey = UserKeyMap.GetMappedKey(FormsKey.Down);
            _leftKey = UserKeyMap.GetMappedKey(FormsKey.Left);
            _rightKey = UserKeyMap.GetMappedKey(FormsKey.Right);
            _dUpKey = UserKeyMap.GetMappedKey(FormsKey.Forward);
            _dDownKey = UserKeyMap.GetMappedKey(FormsKey.Back);
            _dLeftKey = UserKeyMap.GetMappedKey(FormsKey.ShiftLeft);
            _dRightKey = UserKeyMap.GetMappedKey(FormsKey.ShiftRight);
            _aKey = UserKeyMap.GetMappedKey(FormsKey.A);
            _bKey = UserKeyMap.GetMappedKey(FormsKey.B);
            _xKey = UserKeyMap.GetMappedKey(FormsKey.X);
            _yKey = UserKeyMap.GetMappedKey(FormsKey.Y);
            _lookUpKey = UserKeyMap.GetMappedKey(FormsKey.PageUp);
            _lookDownKey = UserKeyMap.GetMappedKey(FormsKey.PageDown);
            _lookLeftKey = UserKeyMap.GetMappedKey(FormsKey.SoftLeft);
            _lookRightKey = UserKeyMap.GetMappedKey(FormsKey.SoftRight);
            _l1Key = UserKeyMap.GetMappedKey(FormsKey.ButtonL1);
            _l2Key = UserKeyMap.GetMappedKey(FormsKey.ButtonL2);
            _r1Key = UserKeyMap.GetMappedKey(FormsKey.ButtonR1);
            _r2Key = UserKeyMap.GetMappedKey(FormsKey.ButtonR2);

            InitializeComponent();
        }


        public string AKey
        {
            get => _aKey.ToString();
            set
            {
                if (value != _aKey.ToString())
                {
                    Enum.TryParse(value, out _aKey);
                    OnPropertyChanged();
                    Task.Run(() => UserKeyMap.SetMappedKey(FormsKey.A, _aKey));
                }
            }
        }

        public IList<string> AllKeys => Enum.GetValues(typeof(FormsKey)).Cast<FormsKey>()
            .Select(k => k.ToString()).ToList();


        public string BKey
        {
            get => _bKey.ToString();
            set
            {
                if (value != _bKey.ToString())
                {
                    Enum.TryParse(value, out _bKey);
                    OnPropertyChanged();
                    Task.Run(() => UserKeyMap.SetMappedKey(FormsKey.B, _bKey));
                }
            }
        }


        public string DDownKey
        {
            get => _dDownKey.ToString();
            set
            {
                if (value != _dDownKey.ToString())
                {
                    Enum.TryParse(value, out _dDownKey);
                    OnPropertyChanged();
                    Task.Run(() => UserKeyMap.SetMappedKey(FormsKey.Back, _dDownKey));
                }
            }
        }


        public string DLeftKey
        {
            get => _dLeftKey.ToString();
            set
            {
                if (value != _dLeftKey.ToString())
                {
                    Enum.TryParse(value, out _dLeftKey);
                    OnPropertyChanged();
                    Task.Run(() => UserKeyMap.SetMappedKey(FormsKey.ShiftLeft, _dLeftKey));
                }
            }
        }


        public string DownKey
        {
            get => _downKey.ToString();
            set
            {
                if (value != _downKey.ToString())
                {
                    Enum.TryParse(value, out _downKey);
                    OnPropertyChanged();
                    Task.Run(() => UserKeyMap.SetMappedKey(FormsKey.Down, _downKey));
                }
            }
        }


        public string DRightKey
        {
            get => _dRightKey.ToString();
            set
            {
                if (value != _dRightKey.ToString())
                {
                    Enum.TryParse(value, out _dRightKey);
                    OnPropertyChanged();
                    Task.Run(() => UserKeyMap.SetMappedKey(FormsKey.ShiftRight, _dRightKey));
                }
            }
        }


        public string DUpKey
        {
            get => _dUpKey.ToString();
            set
            {
                if (value != _dUpKey.ToString())
                {
                    Enum.TryParse(value, out _dUpKey);
                    OnPropertyChanged();
                    Task.Run(() => UserKeyMap.SetMappedKey(FormsKey.Forward, _dUpKey));
                }
            }
        }


        public string L1Key
        {
            get => _l1Key.ToString();
            set
            {
                if (value != _l1Key.ToString())
                {
                    Enum.TryParse(value, out _l1Key);
                    OnPropertyChanged();
                    Task.Run(() => UserKeyMap.SetMappedKey(FormsKey.ButtonL1, _l1Key));
                }
            }
        }


        public string L2Key
        {
            get => _l2Key.ToString();
            set
            {
                if (value != _l2Key.ToString())
                {
                    Enum.TryParse(value, out _l2Key);
                    OnPropertyChanged();
                    Task.Run(() => UserKeyMap.SetMappedKey(FormsKey.ButtonL2, _l2Key));
                }
            }
        }


        public string LeftKey
        {
            get => _leftKey.ToString();
            set
            {
                if (value != _leftKey.ToString())
                {
                    Enum.TryParse(value, out _leftKey);
                    OnPropertyChanged();
                    Task.Run(() => UserKeyMap.SetMappedKey(FormsKey.Left, _leftKey));
                }
            }
        }


        public string LookDownKey
        {
            get => _lookDownKey.ToString();
            set
            {
                if (value != _lookDownKey.ToString())
                {
                    Enum.TryParse(value, out _lookDownKey);
                    OnPropertyChanged();
                    Task.Run(() => UserKeyMap.SetMappedKey(FormsKey.PageDown, _lookDownKey));
                }
            }
        }


        public string LookLeftKey
        {
            get => _lookLeftKey.ToString();
            set
            {
                if (value != _lookLeftKey.ToString())
                {
                    Enum.TryParse(value, out _lookLeftKey);
                    OnPropertyChanged();
                    Task.Run(() => UserKeyMap.SetMappedKey(FormsKey.SoftLeft, _lookLeftKey));
                }
            }
        }


        public string LookRightKey
        {
            get => _lookRightKey.ToString();
            set
            {
                if (value != _lookRightKey.ToString())
                {
                    Enum.TryParse(value, out _lookRightKey);
                    OnPropertyChanged();
                    Task.Run(() => UserKeyMap.SetMappedKey(FormsKey.SoftRight, _lookRightKey));
                }
            }
        }


        public string LookUpKey
        {
            get => _lookUpKey.ToString();
            set
            {
                if (value != _lookUpKey.ToString())
                {
                    Enum.TryParse(value, out _lookUpKey);
                    OnPropertyChanged();
                    Task.Run(() => UserKeyMap.SetMappedKey(FormsKey.PageUp, _lookUpKey));
                }
            }
        }


        public string R1Key
        {
            get => _r1Key.ToString();
            set
            {
                if (value != _r1Key.ToString())
                {
                    Enum.TryParse(value, out _r1Key);
                    OnPropertyChanged();
                    Task.Run(() => UserKeyMap.SetMappedKey(FormsKey.ButtonR1, _r1Key));
                }
            }
        }


        public string R2Key
        {
            get => _r2Key.ToString();
            set
            {
                if (value != _r2Key.ToString())
                {
                    Enum.TryParse(value, out _r2Key);
                    OnPropertyChanged();
                    Task.Run(() => UserKeyMap.SetMappedKey(FormsKey.ButtonR2, _r2Key));
                }
            }
        }


        public string RightKey
        {
            get => _rightKey.ToString();
            set
            {
                if (value != _rightKey.ToString())
                {
                    Enum.TryParse(value, out _rightKey);
                    OnPropertyChanged();
                    Task.Run(() => UserKeyMap.SetMappedKey(FormsKey.Right, _rightKey));
                }
            }
        }

        public string UpKey
        {
            get => _upKey.ToString();
            set
            {
                if (value != _upKey.ToString())
                {
                    Enum.TryParse(value, out _upKey);
                    OnPropertyChanged();
                    Task.Run(() => UserKeyMap.SetMappedKey(FormsKey.Up, _upKey));
                }
            }
        }


        public string XKey
        {
            get => _xKey.ToString();
            set
            {
                if (value != _xKey.ToString())
                {
                    Enum.TryParse(value, out _xKey);
                    OnPropertyChanged();
                    Task.Run(() => UserKeyMap.SetMappedKey(FormsKey.X, _xKey));
                }
            }
        }


        public string YKey
        {
            get => _yKey.ToString();
            set
            {
                if (value != _yKey.ToString())
                {
                    Enum.TryParse(value, out _yKey);
                    OnPropertyChanged();
                    Task.Run(() => UserKeyMap.SetMappedKey(FormsKey.Y, _yKey));
                }
            }
        }

        private FormsKey _aKey;

        private FormsKey _bKey;

        private FormsKey _dDownKey;

        private FormsKey _dLeftKey;

        private FormsKey _downKey;

        private FormsKey _dRightKey;

        private FormsKey _dUpKey;

        private FormsKey _l1Key;

        private FormsKey _l2Key;

        private FormsKey _leftKey;

        private FormsKey _lookDownKey;

        private FormsKey _lookLeftKey;

        private FormsKey _lookRightKey;

        private FormsKey _lookUpKey;

        private FormsKey _r1Key;

        private FormsKey _r2Key;

        private FormsKey _rightKey;

        private FormsKey _upKey;

        private FormsKey _xKey;

        private FormsKey _yKey;
    }
}