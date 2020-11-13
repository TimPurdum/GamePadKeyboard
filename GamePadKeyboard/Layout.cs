using System.Collections.Generic;

namespace GamePadKeyboard
{
    public class Layout
    {
        public Layout()
        {
            KeyMap = DefaultKeys.CreateDefaultMap();
        }

        public ControllerLayout ControllerLayout { get; set; } = ControllerLayout.XStyle;
        public bool Current { get; set; }
        public Dictionary<FormsKey, FormsKey> KeyMap { get; }
        public string Name { get; set; } = "New Layout";
    }
}