using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GamePadKeyboard
{
    public static class UserKeyMap
    {
        public static FormsKey GetMappedKey(FormsKey inputKey)
        {
            if (!_currentMap.ContainsKey(inputKey))
            {
                _currentMap[inputKey] = inputKey;
                if (Application.Current.Properties.ContainsKey(inputKey.ToString()))
                {
                    var keyString = Application.Current.Properties[inputKey.ToString()] as string;
                    if (Enum.TryParse<FormsKey>(keyString, out var savedKey)) _currentMap[inputKey] = savedKey;
                }
            }

            return _currentMap[inputKey];
        }

        public static async Task SetMappedKey(FormsKey inputKey, FormsKey mapKey)
        {
            _currentMap[inputKey] = mapKey;
            Application.Current.Properties[inputKey.ToString()] = mapKey.ToString();
            await Application.Current.SavePropertiesAsync();
        }


        private static readonly Dictionary<FormsKey, FormsKey> _currentMap = new Dictionary<FormsKey, FormsKey>();
    }
}