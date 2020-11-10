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
                if (!string.IsNullOrEmpty(savedJson))
                {
                    _layouts = JsonConvert.DeserializeObject<List<Layout>>(savedJson);
                }
            }
            if (_layouts == null)
            {
                _layouts = new List<Layout>
                {
                    new Layout {Name = "Layout 1", Current = true}
                };
            }
            _currentLayout = _layouts?.FirstOrDefault(l => l.Current) ?? new Layout();
        }
        
        public static FormsKey GetMappedKey(FormsKey inputKey)
        {
            if (!_currentLayout.KeyMap.ContainsKey(inputKey))
            {
                _currentLayout.KeyMap[inputKey] = inputKey;
            }

            return _currentLayout.KeyMap[inputKey];
        }

        public static ControllerLayout GetControllerLayout()
        {
            return _currentLayout.ControllerLayout;
        }

        public static async Task SetControllerLayout(ControllerLayout controllerLayout)
        {
            _currentLayout.ControllerLayout = controllerLayout;
            await SaveLayouts();
        }

        public static async Task SetMappedKey(FormsKey inputKey, FormsKey mapKey)
        {
            _currentLayout.KeyMap[inputKey] = mapKey;
            await SaveLayouts();
        }

        public static string[] GetLayoutMapNames()
        {
            return _layouts?.Select(l => l.Name).ToArray();
        }


        public static async Task NewLayoutMap(string name)
        {
            _currentLayout.Current = false;
            var newLayout = new Layout {Name = name, Current = true};
            _currentLayout = newLayout;
            _layouts.Add(newLayout);
            await SaveLayouts();
        }


        public static async Task SetCurrentLayout(string name)
        {
            if (string.IsNullOrEmpty(name)) return;
            var oldLayout = _currentLayout;
            oldLayout.Current = false;
            _currentLayout = _layouts.FirstOrDefault(l => l.Name == name) ?? new Layout();
            _currentLayout.Current = true;
            await SaveLayouts();
        }
        
        
        public static string GetCurrentMapName()
        {
            return _currentLayout.Name;
        }


        private static async Task SaveLayouts()
        {
            Application.Current.Properties["LayoutMaps"] = JsonConvert.SerializeObject(_layouts);
            await Application.Current.SavePropertiesAsync();
        }

        private static Layout _currentLayout;
        private static readonly List<Layout> _layouts;
    }
}