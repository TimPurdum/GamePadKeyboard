using System;
using System.Collections.Generic;
using System.Text;

namespace GamePadKeyboard
{
    public static class CurrentKeyMap
    {
        public static KeyMap Map => new KeyMap
        {
            {Key.Up, "w"},
            {Key.Down, "s"},
            {Key.Left, "a"},
            {Key.Right, "d"}
        };
    }

    public class KeyMap: Dictionary<Key, string>
    {
    }


    public enum Key
    {
        Up,
        Down,
        Left,
        Right
    }
}
