using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace GamePadKeyboard
{
    public static class UserSettings
    {
        static UserSettings()
        {
            if (Application.Current.Properties.ContainsKey("LayoutMaps"))
            {
                var savedJson = Application.Current.Properties["LayoutMaps"]?.ToString();
                if (!string.IsNullOrEmpty(savedJson)) Layouts = JsonConvert.DeserializeObject<List<Layout>>(savedJson);
            }

            if (Layouts == null)
                Layouts = new List<Layout>
                {
                    new Layout {Name = "Layout 1", Current = true}
                };
            _currentLayout = Layouts.First(l => l.Current) ?? Layouts.First();
        }

        public static FormsKey GetMappedKey(FormsKey inputKey)
        {
            if (!_currentLayout.KeyMap.ContainsKey(inputKey)) _currentLayout.KeyMap[inputKey] = inputKey;

            return _currentLayout.KeyMap[inputKey];
        }

        public static ControllerLayout GetControllerLayout()
        {
            return _currentLayout.ControllerLayout;
        }

        public static async Task SetControllerLayout(ControllerLayout controllerLayout)
        {
            if (_currentLayout.ControllerLayout != controllerLayout)
            {
                _currentLayout.ControllerLayout = controllerLayout;
                await SaveLayouts();
            }
        }

        public static async Task SetMappedKey(FormsKey inputKey, FormsKey mapKey)
        {
            if (mapKey != FormsKey.Unknown && mapKey != _currentLayout.KeyMap[inputKey])
            {
                _currentLayout.KeyMap[inputKey] = mapKey;
                await SaveLayouts();
            }
        }

        public static string[] GetLayoutMapNames()
        {
            return Layouts?.Select(l => l.Name).ToArray();
        }


        public static async Task NewLayoutMap(string name)
        {
            if (Layouts.Any(l => l.Name == name)) return;
            _currentLayout.Current = false;
            var newLayout = new Layout {Name = name, Current = true};
            _currentLayout = newLayout;
            Layouts.Add(newLayout);
            await SaveLayouts();
        }


        public static async Task SetCurrentLayout(string name)
        {
            if (string.IsNullOrEmpty(name)) return;
            var oldLayout = _currentLayout;

            var newLayout = Layouts.FirstOrDefault(l => l.Name == name);
            if (newLayout != null)
            {
                oldLayout.Current = false;
                _currentLayout = newLayout;
                _currentLayout.Current = true;
                await SaveLayouts();
            }
        }


        public static async Task RenameCurrentLayout(string name)
        {
            if (string.IsNullOrEmpty(name)) return;
            _currentLayout.Name = name;
            await SaveLayouts();
        }


        public static string GetCurrentMapName()
        {
            return _currentLayout.Name;
        }

        public static async Task DeleteLayoutMap()
        {
            Layouts.Remove(_currentLayout);
            if (!Layouts.Any()) Layouts.Add(new Layout {Name = "Layout 1", Current = true});

            await SaveLayouts();
        }


        private static async Task SaveLayouts()
        {
            Application.Current.Properties["LayoutMaps"] = JsonConvert.SerializeObject(Layouts);
            await Application.Current.SavePropertiesAsync();
        }

        private static Layout _currentLayout;
        private static readonly List<Layout> Layouts;
    }
}