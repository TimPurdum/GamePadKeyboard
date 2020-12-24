using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace GamePadKeyboard
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            BindingContext = this;
            Layouts = UserSettings.GetLayoutMapNames();
            InitializeComponent();
            SetMappedKeysAndController();
            LayoutPicker.SelectedItem = UserSettings.GetCurrentMapName();
            EditButton.Text = "✏️";
            AddButton.Text = "➕";
            DeleteButton.Text = "🗑️";
            SendCrashData = UserSettings.GetSendCrashData();
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
                    Task.Run(() => UserSettings.SetMappedKey(FormsKey.ButtonA, _aKey));
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
                    Task.Run(() => UserSettings.SetMappedKey(FormsKey.ButtonB, _bKey));
                }
            }
        }

        public string[] Controllers => Enum.GetNames(typeof(ControllerLayout));


        public string DDownKey
        {
            get => _dDownKey.ToString();
            set
            {
                if (value != _dDownKey.ToString())
                {
                    Enum.TryParse(value, out _dDownKey);
                    OnPropertyChanged();
                    Task.Run(() => UserSettings.SetMappedKey(FormsKey.DpadDown, _dDownKey));
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
                    Task.Run(() => UserSettings.SetMappedKey(FormsKey.DpadLeft, _dLeftKey));
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
                    Task.Run(() => UserSettings.SetMappedKey(FormsKey.S, _downKey));
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
                    Task.Run(() => UserSettings.SetMappedKey(FormsKey.DpadRight, _dRightKey));
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
                    Task.Run(() => UserSettings.SetMappedKey(FormsKey.DpadUp, _dUpKey));
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
                    Task.Run(() => UserSettings.SetMappedKey(FormsKey.ButtonL1, _l1Key));
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
                    Task.Run(() => UserSettings.SetMappedKey(FormsKey.ButtonL2, _l2Key));
                }
            }
        }


        public string[] Layouts
        {
            get => _layouts;
            set
            {
                if (_layouts != value)
                {
                    _layouts = value;
                    OnPropertyChanged();
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
                    Task.Run(() => UserSettings.SetMappedKey(FormsKey.A, _leftKey));
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
                    Task.Run(() => UserSettings.SetMappedKey(FormsKey.Down, _lookDownKey));
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
                    Task.Run(() => UserSettings.SetMappedKey(FormsKey.Left, _lookLeftKey));
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
                    Task.Run(() => UserSettings.SetMappedKey(FormsKey.Right, _lookRightKey));
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
                    Task.Run(() => UserSettings.SetMappedKey(FormsKey.Up, _lookUpKey));
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
                    Task.Run(() => UserSettings.SetMappedKey(FormsKey.ButtonR1, _r1Key));
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
                    Task.Run(() => UserSettings.SetMappedKey(FormsKey.ButtonR2, _r2Key));
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
                    Task.Run(() => UserSettings.SetMappedKey(FormsKey.D, _rightKey));
                }
            }
        }

        public string SelectKey
        {
            get => _selectKey.ToString();
            set
            {
                if (value != _selectKey.ToString())
                {
                    Enum.TryParse(value, out _selectKey);
                    OnPropertyChanged();
                    Task.Run(() => UserSettings.SetMappedKey(FormsKey.ButtonSelect, _selectKey));
                }
            }
        }


        public bool SendCrashData
        {
            get => _sendCrashData;
            set
            {
                if (_sendCrashData != value)
                {
                    _sendCrashData = value;
                    OnPropertyChanged();
                    UserSettings.SetSendCrashData(value);
                }
            }
        }


        public string StartKey
        {
            get => _startKey.ToString();
            set
            {
                if (value != _startKey.ToString())
                {
                    Enum.TryParse(value, out _startKey);
                    OnPropertyChanged();
                    Task.Run(() => UserSettings.SetMappedKey(FormsKey.ButtonStart, _startKey));
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
                    Task.Run(() => UserSettings.SetMappedKey(FormsKey.W, _upKey));
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
                    Task.Run(() => UserSettings.SetMappedKey(FormsKey.ButtonX, _xKey));
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
                    Task.Run(() => UserSettings.SetMappedKey(FormsKey.ButtonY, _yKey));
                }
            }
        }

        private async void OnSetupGuidePressed(object sender, EventArgs e)
        {
            await Launcher.OpenAsync("https://cedarrivertech.com/s-duo-gamepad");
        }

        private async void OnLayoutSelectionChanged(object sender, EventArgs e)
        {
            if (LayoutPicker.SelectedItem != null)
            {
                await UserSettings.SetCurrentLayout(LayoutPicker.SelectedItem?.ToString());
                SetMappedKeysAndController();
            }
        }

        private async void OnNewLayoutPressed(object sender, EventArgs e)
        {
            try
            {
                var dialog = new ContentPage
                {
                    BackgroundColor = Color.FromHex("#D9000000"),
                    Padding = new Thickness(20, 20, 20, 20)
                };

                var entry = new Entry();
                var createButton = new Button {Text = "Create", IsEnabled = false};

                entry.TextChanged += (o, args) => { createButton.IsEnabled = !string.IsNullOrEmpty(entry.Text); };

                var cancelButton = new Button {Text = "Cancel"};
                createButton.Pressed += async (o, args) =>
                {
                    await UserSettings.NewLayoutMap(entry.Text);

                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        Layouts = UserSettings.GetLayoutMapNames();
                        SetMappedKeysAndController();
                        LayoutPicker.SelectedItem = entry.Text;
                        await Navigation.PopModalAsync(false);
                    });
                };

                cancelButton.Pressed += async (o, args) =>
                {
                    Device.BeginInvokeOnMainThread(async () => await Navigation.PopModalAsync(false));
                };

                var contentLayout = new StackLayout
                {
                    VerticalOptions = LayoutOptions.CenterAndExpand,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    Orientation = StackOrientation.Vertical,
                    Children =
                    {
                        new Label {Text = "New Layout Name"},
                        entry,
                        new StackLayout
                        {
                            Orientation = StackOrientation.Horizontal,
                            Children =
                            {
                                createButton, cancelButton
                            }
                        }
                    }
                };

                dialog.Content = contentLayout;

                await Navigation.PushModalAsync(dialog, false);
            }
            catch (Exception ex)
            {
                await ExceptionHandler.Handle(ex);
            }
        }


        private void OnEditLayoutPressed(object sender, EventArgs e)
        {
            LayoutEntry.Text = LayoutPicker.SelectedItem?.ToString();
            LayoutEntry.IsVisible = true;
            LayoutPicker.IsVisible = false;
            LayoutEntry.Focus();
        }

        private async void OnControllerSelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (ControllerPicker?.SelectedItem != null)
                {
                    await UserSettings.SetControllerLayout((ControllerLayout) Enum.Parse(typeof(ControllerLayout),
                        ControllerPicker.SelectedItem.ToString()));
                    MessagingCenter.Send(this, "Refresh");
                }
            }
            catch (Exception ex)
            {
                await ExceptionHandler.Handle(ex);
            }
        }

        private void SetMappedKeysAndController()
        {
            try
            {
                _upKey = UserSettings.GetMappedKey(FormsKey.W);
                _downKey = UserSettings.GetMappedKey(FormsKey.S);
                _leftKey = UserSettings.GetMappedKey(FormsKey.A);
                _rightKey = UserSettings.GetMappedKey(FormsKey.D);
                _dUpKey = UserSettings.GetMappedKey(FormsKey.DpadUp);
                _dDownKey = UserSettings.GetMappedKey(FormsKey.DpadDown);
                _dLeftKey = UserSettings.GetMappedKey(FormsKey.DpadLeft);
                _dRightKey = UserSettings.GetMappedKey(FormsKey.DpadRight);
                _aKey = UserSettings.GetMappedKey(FormsKey.ButtonA);
                _bKey = UserSettings.GetMappedKey(FormsKey.ButtonB);
                _xKey = UserSettings.GetMappedKey(FormsKey.ButtonX);
                _yKey = UserSettings.GetMappedKey(FormsKey.ButtonY);
                _lookUpKey = UserSettings.GetMappedKey(FormsKey.Up);
                _lookDownKey = UserSettings.GetMappedKey(FormsKey.Down);
                _lookLeftKey = UserSettings.GetMappedKey(FormsKey.Left);
                _lookRightKey = UserSettings.GetMappedKey(FormsKey.Right);
                _l1Key = UserSettings.GetMappedKey(FormsKey.ButtonL1);
                _l2Key = UserSettings.GetMappedKey(FormsKey.ButtonL2);
                _r1Key = UserSettings.GetMappedKey(FormsKey.ButtonR1);
                _r2Key = UserSettings.GetMappedKey(FormsKey.ButtonR2);
                _startKey = UserSettings.GetMappedKey(FormsKey.ButtonStart);
                _selectKey = UserSettings.GetMappedKey(FormsKey.ButtonSelect);

                foreach (var prop in GetType().GetProperties().Where(p => p.Name.EndsWith("Key")))
                    OnPropertyChanged(prop.Name);

                ControllerPicker.SelectedItem = UserSettings.GetControllerLayout().ToString();
                InvalidateMeasure();
                MessagingCenter.Send(this, "Refresh");
            }
            catch (Exception ex)
            {
                Task.Run(() => ExceptionHandler.Handle(ex));
            }
        }

        private async void OnLayoutEntryCompleted(object sender, EventArgs e)
        {
            try
            {
                LayoutEntry.IsVisible = false;
                LayoutPicker.IsVisible = true;
                if (!string.IsNullOrEmpty(LayoutEntry.Text))
                {
                    await UserSettings.RenameCurrentLayout(LayoutEntry.Text);
                    Layouts = UserSettings.GetLayoutMapNames();
                    LayoutPicker.SelectedItem = LayoutEntry.Text;
                }
            }
            catch (Exception ex)
            {
                await ExceptionHandler.Handle(ex);
            }
        }

        private async void OnDeleteLayoutPressed(object sender, EventArgs e)
        {
            try
            {
                if (LayoutPicker.SelectedItem == null) return;
                var dialog = new ContentPage
                {
                    BackgroundColor = Color.FromHex("#D9000000"),
                    Padding = new Thickness(20, 20, 20, 20)
                };

                var deleteButton = new Button {Text = "Delete", BackgroundColor = Color.Red};

                var cancelButton = new Button {Text = "Cancel"};
                deleteButton.Pressed += async (o, args) =>
                {
                    await UserSettings.DeleteLayoutMap();

                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        Layouts = UserSettings.GetLayoutMapNames();
                        SetMappedKeysAndController();
                        LayoutPicker.SelectedIndex = 0;
                        await Navigation.PopModalAsync(false);
                    });
                };

                cancelButton.Pressed += (o, args) =>
                {
                    Device.BeginInvokeOnMainThread(async () => await Navigation.PopModalAsync(false));
                };

                var contentLayout = new StackLayout
                {
                    VerticalOptions = LayoutOptions.CenterAndExpand,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    Orientation = StackOrientation.Vertical,
                    Children =
                    {
                        new Label {Text = $"Delete Layout {LayoutPicker.SelectedItem}? This action cannot be undone."},
                        new StackLayout
                        {
                            Orientation = StackOrientation.Horizontal,
                            Children =
                            {
                                deleteButton, cancelButton
                            }
                        }
                    }
                };

                dialog.Content = contentLayout;

                await Navigation.PushModalAsync(dialog, false);
            }
            catch (Exception ex)
            {
                await ExceptionHandler.Handle(ex);
            }
        }

        private void OnShowGamePadPressed(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "Show GamePad");
        }

        private async void OnSendFeedback(object sender, EventArgs e)
        {
            try
            {
                var body = "";
                var dialog = new ContentPage
                {
                    BackgroundColor = Color.FromHex("#D9000000"),
                    Padding = new Thickness(20, 20, 20, 20)
                };

                var yesButton = new Button {Text = "Yes"};

                var noButton = new Button {Text = "No"};
                yesButton.Pressed += async (o, args) =>
                {
                    body = $"{Environment.NewLine}{Environment.NewLine}{UserSettings.FetchCrashLogs()}";
                    Device.BeginInvokeOnMainThread(async () => await Navigation.PopModalAsync(false));
                };

                noButton.Pressed += (o, args) =>
                {
                    Device.BeginInvokeOnMainThread(async () => await Navigation.PopModalAsync(false));
                };

                var contentLayout = new StackLayout
                {
                    VerticalOptions = LayoutOptions.CenterAndExpand,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    Orientation = StackOrientation.Vertical,
                    Children =
                    {
                        new Label {Text = "Include crash log data in email message?"},
                        new StackLayout
                        {
                            Orientation = StackOrientation.Horizontal,
                            Children =
                            {
                                yesButton, noButton
                            }
                        }
                    }
                };

                dialog.Content = contentLayout;

                await Email.ComposeAsync("S-Duo GamePad Feedback", body, "tim@cedarrivertech.com");
            }
            catch (Exception ex)
            {
                await ExceptionHandler.Handle(ex);
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

        private string[] _layouts;

        private FormsKey _leftKey;

        private FormsKey _lookDownKey;

        private FormsKey _lookLeftKey;

        private FormsKey _lookRightKey;

        private FormsKey _lookUpKey;

        private FormsKey _r1Key;

        private FormsKey _r2Key;

        private FormsKey _rightKey;
        private FormsKey _selectKey;
        private bool _sendCrashData;

        private FormsKey _startKey;

        private FormsKey _upKey;

        private FormsKey _xKey;

        private FormsKey _yKey;
    }
}