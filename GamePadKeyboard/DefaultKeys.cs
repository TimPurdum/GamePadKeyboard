using System.Collections.Generic;

namespace GamePadKeyboard
{
    public static class DefaultKeys
    {
        public static Dictionary<FormsKey, FormsKey> CreateDefaultMap()
        {
            return new Dictionary<FormsKey, FormsKey>
            {
                {FormsKey.A, FormsKey.A},
                {FormsKey.D, FormsKey.D},
                {FormsKey.W, FormsKey.W},
                {FormsKey.S, FormsKey.S},
                {FormsKey.ButtonA, FormsKey.ButtonA},
                {FormsKey.ButtonB, FormsKey.ButtonB},
                {FormsKey.ButtonX, FormsKey.ButtonX},
                {FormsKey.ButtonY, FormsKey.ButtonY},
                {FormsKey.Left, FormsKey.Left},
                {FormsKey.Right, FormsKey.Right},
                {FormsKey.Up, FormsKey.Up},
                {FormsKey.Down, FormsKey.Down},
                {FormsKey.DpadLeft, FormsKey.DpadLeft},
                {FormsKey.DpadRight, FormsKey.DpadRight},
                {FormsKey.DpadUp, FormsKey.DpadUp},
                {FormsKey.DpadDown, FormsKey.DpadDown},
                {FormsKey.ButtonL1, FormsKey.ButtonL1},
                {FormsKey.ButtonL2, FormsKey.ButtonL2},
                {FormsKey.ButtonR1, FormsKey.ButtonR1},
                {FormsKey.ButtonR2, FormsKey.ButtonR2},
                {FormsKey.ButtonStart, FormsKey.ButtonStart},
                {FormsKey.ButtonSelect, FormsKey.ButtonSelect}
            };
        }
    }
}