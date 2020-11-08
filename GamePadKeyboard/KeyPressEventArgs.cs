using System;

namespace GamePadKeyboard
{
    public class KeyPressEventArgs : EventArgs
    {
        public KeyPressEventArgs(FormsKey keyValue)
        {
            KeyValue = keyValue;
        }

        public FormsKey KeyValue { get; }
    }
}