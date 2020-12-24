using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GamePadKeyboard.TouchApi;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GamePadKeyboard
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GamePad : ContentView
    {
        private readonly bool _isSurfaceDuo;

        public GamePad(bool isSurfaceDuo)
        {
            _isSurfaceDuo = isSurfaceDuo;
            InitializeComponent();
            BindingContext = this;
            SizeChanged += SetLayout;
            MessagingCenter.Subscribe<MainPage>(this, "Refresh", RefreshLayout);

            _buttonKeys = new Dictionary<View, FormsKey>
            {
                {LeftButton, FormsKey.A},
                {RightButton, FormsKey.D},
                {UpButton, FormsKey.W},
                {DownButton, FormsKey.S},
                {AButton, FormsKey.ButtonA},
                {BButton, FormsKey.ButtonB},
                {XButton, FormsKey.ButtonX},
                {YButton, FormsKey.ButtonY},
                {LeftLookButton, FormsKey.Left},
                {RightLookButton, FormsKey.Right},
                {UpLookButton, FormsKey.Up},
                {DownLookButton, FormsKey.Down},
                {DLeftButton, FormsKey.DpadLeft},
                {DRightButton, FormsKey.DpadRight},
                {DUpButton, FormsKey.DpadUp},
                {DDownButton, FormsKey.DpadDown},
                {L1Button, FormsKey.ButtonL1},
                {L2Button, FormsKey.ButtonL2},
                {R1Button, FormsKey.ButtonR1},
                {R2Button, FormsKey.ButtonR2},
                {StartButton, FormsKey.ButtonStart},
                {SelectButton, FormsKey.ButtonSelect}
            };

            var dPadTouchEffect = new TouchEffect();
            dPadTouchEffect.TouchAction += OnTouchEffectAction;
            var lookPadTouchEffect = new TouchEffect();
            lookPadTouchEffect.TouchAction += OnTouchEffectAction;
            LeftPadFrame.Effects.Add(dPadTouchEffect);
            LookPadFrame.Effects.Add(lookPadTouchEffect);
        }

        public event EventHandler<KeyPressEventArgs> KeyPressed;
        public event EventHandler<KeyPressEventArgs> KeyReleased;
        public event EventHandler SettingsPressed;

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


        public Constraint ButtonsY
        {
            get => _buttonsY;
            set
            {
                if (_buttonsY != value)
                {
                    _buttonsY = value;
                    OnPropertyChanged();
                }
            }
        }


        public Constraint LookX
        {
            get => _lookX;
            set
            {
                if (_lookX != value)
                {
                    _lookX = value;
                    OnPropertyChanged();
                }
            }
        }


        public Constraint LookY
        {
            get => _lookY;
            set
            {
                if (_lookY != value)
                {
                    _lookY = value;
                    OnPropertyChanged();
                }
            }
        }

        public Thickness PadMargin
        {
            get => _padMargin;
            set
            {
                if (_padMargin != value)
                {
                    _padMargin = value;
                    OnPropertyChanged();
                }
            }
        }

        public double Scale
        {
            get => _scale;
            set
            {
                if (_scale != value)
                {
                    _scale = value;
                    OnPropertyChanged();
                }
            }
        }

        public Constraint SettingsX
        {
            get => _settingsX;
            set
            {
                if (_settingsX != value)
                {
                    _settingsX = value;
                    OnPropertyChanged();
                }
            }
        }


        public Constraint SettingsY
        {
            get => _settingsY;
            set
            {
                if (_settingsY != value)
                {
                    _settingsY = value;
                    OnPropertyChanged();
                }
            }
        }

        private void RefreshLayout<TSender>(TSender obj) where TSender : class
        {
            SetLayout(this, EventArgs.Empty);
        }

        private void SetLayout(object sender, EventArgs e)
        {
            try
            {
                var isLandscape = DeviceDisplay.MainDisplayInfo.Rotation == DisplayRotation.Rotation90 ||
                                  DeviceDisplay.MainDisplayInfo.Rotation == DisplayRotation.Rotation270;
                Scale = isLandscape ? Width / 720.0 : Width / 540;

                switch (UserSettings.GetControllerLayout())
                {
                    case ControllerLayout.PlayStyle:
                        LeftPadGrid.TranslationX = 40 * Scale;
                        LeftPadGrid.TranslationY = Height - (LeftPadGrid.Height + 140);
                        LeftPadGrid.IsVisible = isLandscape;
                        DirectionalPadFrame.TranslationX = 0;
                        DirectionalPadFrame.TranslationY = 0;
                        DirectionalPadFrame.IsVisible = true;
                        break;
                    default:
                        LeftPadGrid.TranslationX = 0;
                        LeftPadGrid.TranslationY = 0;
                        LeftPadGrid.IsVisible = true;
                        DirectionalPadFrame.TranslationX = 40 * Scale;
                        DirectionalPadFrame.TranslationY = Height - (DirectionalPadFrame.Height + 100);
                        DirectionalPadFrame.IsVisible = isLandscape;
                        break;
                }

                if (_isSurfaceDuo)
                {
                    ButtonsX = Constraint.Constant(Width - (ButtonsFrame.Width + (40 * Scale)));
                    SettingsX = Constraint.Constant(Width / 2 - (40 * Scale));
                    LeftPadGrid.TranslationY += 20;
                    DirectionalPadFrame.TranslationY += 20;
                }
                else
                {
                    ButtonsX = Constraint.Constant(Width - (ButtonsFrame.Width + (20 * Scale)));
                    SettingsX = Constraint.Constant(Width / 2 - (60 * Scale));
                    LeftPadGrid.TranslationX = LeftPadGrid.TranslationX - 28;
                }

                ButtonsY = Constraint.Constant(0);
                LookX = Constraint.Constant(Width - (ButtonsFrame.Width + 100));
                LookY = Constraint.Constant(Height - (LookPadFrame.Height + 120));

                SettingsY = Constraint.Constant(Height - (SettingsButton.Height + 10));

                LookPadGrid.IsVisible = isLandscape;
                L1Button.IsVisible = isLandscape;
                L2Button.IsVisible = isLandscape;
                R1Button.IsVisible = isLandscape;
                R2Button.IsVisible = isLandscape;
                SelectButton.Margin = isLandscape
                    ? new Thickness(-80, 80, 0, 0)
                    : new Thickness(-10, 120, 0, 0);
                StartButton.Margin = isLandscape
                    ? new Thickness(60, 80, 0, 0)
                    : new Thickness(-10, 166, 0, 0);
                PadMargin = isLandscape ? new Thickness(0, 0, 0, (50 * Scale)) : new Thickness(0);
                InvalidateLayout();
            }
            catch (Exception ex)
            {
                Task.Run(() => ExceptionHandler.Handle(ex));
            }
        }


        private void OnTouchEffectAction(object sender, TouchActionEventArgs args)
        {
            try
            {
                if (!(sender is View view)) return;
                switch (args.Type)
                {
                    case TouchActionType.Pressed:
                        // Don't allow a second touch on an already touched BoxView
                        if (!_dragDictionary.ContainsKey(view))
                        {
                            _dragDictionary.Add(view, new DragInfo(args.Id, args.Location));

                            // Set Capture property to true
                            var touchEffect = (TouchEffect) view.Effects.FirstOrDefault(e => e is TouchEffect);
                            touchEffect.Capture = true;
                            if (sender == LeftPadFrame)
                            {
                                SetDPadTouch(args.Location);
                            }
                            else if (sender == LookPadFrame)
                            {
                                _startLookPoint = args.Location;
                                SetLookPadTouch(args.Location);
                            }
                        }

                        break;

                    case TouchActionType.Moved:
                        if (_dragDictionary.ContainsKey(view) && _dragDictionary[view].Id == args.Id)
                        {
                            if (sender == LeftPadFrame)
                                SetDPadTouch(args.Location);
                            else if (sender == LookPadFrame) SetLookPadTouch(args.Location);
                        }

                        break;

                    case TouchActionType.Released:
                        if (_dragDictionary.ContainsKey(view) && _dragDictionary[view].Id == args.Id)
                            _dragDictionary.Remove(view);

                        if (sender == LeftPadFrame)
                            ReleaseDPadTouch();
                        else if (sender == LookPadFrame) ReleaseLookPadTouch();

                        break;
                }
            }
            catch (Exception ex)
            {
                Task.Run(() => ExceptionHandler.Handle(ex));
            }
        }


        private void SetDPadTouch(Point point)
        {
            try
            {
                ThumbLeft.IsVisible = true;
                ThumbLeft.TranslationX = point.X - 60;
                ThumbLeft.TranslationY = point.Y - 60;

                var width = LeftPadFrame.Width;
                var height = LeftPadFrame.Height;
                if (point.X < width / 3)
                {
                    OnButtonPressed(LeftButton, EventArgs.Empty);
                    if (_buttonsDown.Contains(RightButton)) OnButtonReleased(RightButton, EventArgs.Empty);
                }
                else if (point.X > width / 3 * 2)
                {
                    OnButtonPressed(RightButton, EventArgs.Empty);
                    if (_buttonsDown.Contains(LeftButton)) OnButtonReleased(LeftButton, EventArgs.Empty);
                }
                else
                {
                    if (_buttonsDown.Contains(LeftButton)) OnButtonReleased(LeftButton, EventArgs.Empty);

                    if (_buttonsDown.Contains(RightButton)) OnButtonReleased(RightButton, EventArgs.Empty);
                }

                if (point.Y < height / 3)
                {
                    OnButtonPressed(UpButton, EventArgs.Empty);
                    if (_buttonsDown.Contains(DownButton)) OnButtonReleased(DownButton, EventArgs.Empty);
                }
                else if (point.Y > height / 3 * 2)
                {
                    OnButtonPressed(DownButton, EventArgs.Empty);
                    if (_buttonsDown.Contains(UpButton)) OnButtonReleased(UpButton, EventArgs.Empty);
                }
                else
                {
                    if (_buttonsDown.Contains(UpButton)) OnButtonReleased(UpButton, EventArgs.Empty);
                    if (_buttonsDown.Contains(DownButton)) OnButtonReleased(DownButton, EventArgs.Empty);
                }
            }
            catch (Exception ex)
            {
                Task.Run(() => ExceptionHandler.Handle(ex));
            }
        }


        private void ReleaseDPadTouch()
        {
            ThumbLeft.IsVisible = false;
            if (_buttonsDown.Contains(LeftButton)) OnButtonReleased(LeftButton, EventArgs.Empty);
            if (_buttonsDown.Contains(RightButton)) OnButtonReleased(RightButton, EventArgs.Empty);
            if (_buttonsDown.Contains(UpButton)) OnButtonReleased(UpButton, EventArgs.Empty);
            if (_buttonsDown.Contains(DownButton)) OnButtonReleased(DownButton, EventArgs.Empty);
        }


        private void SetLookPadTouch(Point point)
        {
            try
            {
                ThumbRight.IsVisible = true;
                ThumbRight.TranslationX = point.X - 60;
                ThumbRight.TranslationY = point.Y - 60;
                var width = LookPadFrame.Width;
                var height = LookPadFrame.Height;
                var x = point.X - (_startLookPoint.X - width / 2);
                var y = point.Y - (_startLookPoint.Y - height / 2);
                if (x < width / 3)
                {
                    OnButtonPressed(LeftLookButton, EventArgs.Empty);
                    if (_buttonsDown.Contains(RightLookButton)) OnButtonReleased(RightLookButton, EventArgs.Empty);
                }
                else if (x > width / 3 * 2)
                {
                    OnButtonPressed(RightLookButton, EventArgs.Empty);
                    if (_buttonsDown.Contains(LeftLookButton)) OnButtonReleased(LeftLookButton, EventArgs.Empty);
                }
                else
                {
                    if (_buttonsDown.Contains(LeftLookButton)) OnButtonReleased(LeftLookButton, EventArgs.Empty);

                    if (_buttonsDown.Contains(RightLookButton)) OnButtonReleased(RightLookButton, EventArgs.Empty);
                }

                if (y < height / 3)
                {
                    OnButtonPressed(UpLookButton, EventArgs.Empty);
                    if (_buttonsDown.Contains(DownLookButton)) OnButtonReleased(DownLookButton, EventArgs.Empty);
                }
                else if (y > height / 3 * 2)
                {
                    OnButtonPressed(DownLookButton, EventArgs.Empty);
                    if (_buttonsDown.Contains(UpLookButton)) OnButtonReleased(UpLookButton, EventArgs.Empty);
                }
                else
                {
                    if (_buttonsDown.Contains(UpLookButton)) OnButtonReleased(UpLookButton, EventArgs.Empty);
                    if (_buttonsDown.Contains(DownLookButton)) OnButtonReleased(DownLookButton, EventArgs.Empty);
                }
            }
            catch (Exception ex)
            {
                Task.Run(()=> ExceptionHandler.Handle(ex));
            }
        }


        private void ReleaseLookPadTouch()
        {
            ThumbRight.IsVisible = false;
            if (_buttonsDown.Contains(LeftLookButton)) OnButtonReleased(LeftLookButton, EventArgs.Empty);
            if (_buttonsDown.Contains(RightLookButton)) OnButtonReleased(RightLookButton, EventArgs.Empty);
            if (_buttonsDown.Contains(UpLookButton)) OnButtonReleased(UpLookButton, EventArgs.Empty);
            if (_buttonsDown.Contains(DownLookButton)) OnButtonReleased(DownLookButton, EventArgs.Empty);
        }


        private void OnButtonPressed(object sender, EventArgs e)
        {
            try
            {
                if (_buttonsDown.Contains(sender)) return;

                var key = GetKeyFromSender(sender);

                if (key == null) return;
                var args = new KeyPressEventArgs(key.Value);
                _buttonsDown.Add(sender as View);
                KeyPressed?.Invoke(this, args);
            }
            catch (Exception ex)
            {
                Task.Run(() => ExceptionHandler.Handle(ex));
            }
        }

        private void OnButtonReleased(object sender, EventArgs e)
        {
            try
            {
                if (!_buttonsDown.Contains(sender)) return;

                var key = GetKeyFromSender(sender);

                if (key == null) return;
                var args = new KeyPressEventArgs(key.Value);
                _buttonsDown.Remove(sender as View);
                KeyReleased?.Invoke(this, args);
            }
            catch (Exception ex)
            {
                Task.Run(() => ExceptionHandler.Handle(ex));
            }
        }

        private void OnSettingsPressed(object sender, EventArgs e)
        {
            SettingsPressed?.Invoke(this, e);
        }

        private FormsKey? GetKeyFromSender(object sender)
        {
            if (sender is View v && _buttonKeys.ContainsKey(v)) return UserSettings.GetMappedKey(_buttonKeys[v]);

            return null;
        }

        private readonly Dictionary<View, FormsKey> _buttonKeys;
        private readonly List<View> _buttonsDown = new List<View>();
        private readonly Dictionary<View, DragInfo> _dragDictionary = new Dictionary<View, DragInfo>();
        private Constraint _buttonsX;
        private Constraint _buttonsY;
        private Constraint _lookX;
        private Constraint _lookY;
        private Thickness _padMargin;
        private double _scale;
        private Constraint _settingsX;
        private Constraint _settingsY;
        private Point _startLookPoint;

        private class DragInfo
        {
            public DragInfo(long id, Point pressPoint)
            {
                Id = id;
                PressPoint = pressPoint;
            }

            public long Id { get; }

            public Point PressPoint { get; }
        }
    }
}