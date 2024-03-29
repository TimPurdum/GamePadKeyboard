﻿using Xamarin.Forms;

namespace GamePadKeyboard.TouchApi
{
    public class TouchEffect : RoutingEffect
    {
        public TouchEffect() : base("XamarinDocs.TouchEffect")
        {
        }

        public event TouchActionEventHandler TouchAction;

        public bool Capture { set; get; }

        public void OnTouchAction(Element element, TouchActionEventArgs args)
        {
            TouchAction?.Invoke(element, args);
        }
    }
}