using System;
using System.Collections.Generic;
using System.Text;

namespace GamePadKeyboard
{
    public static class CurrentKeyMap
    {
        public static KeyMap Map => new KeyMap
        {
            {Key.Up, "DpadUp"},
            {Key.Down, "DpadDown"},
            {Key.Left, "DpadLeft"},
            {Key.Right, "DpadRight"}
        };
    }

    public class KeyMap: Dictionary<Key, string>
    {
    }


    public enum Key
    {
        None,
        Up,
        Down,
        Left,
        Right
    }
}
