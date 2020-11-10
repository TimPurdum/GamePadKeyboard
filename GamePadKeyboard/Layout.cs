using System.Collections.Generic;
using System.Runtime.Serialization;

namespace GamePadKeyboard
{
    public class Layout
    {
        public string Name { get; set; } = "New Layout";
        public Dictionary<FormsKey, FormsKey> KeyMap { get; } = new Dictionary<FormsKey, FormsKey>();
        public bool Current { get; set; }

        public ControllerLayout ControllerLayout { get; set; } = ControllerLayout.XStyle;
    }
}