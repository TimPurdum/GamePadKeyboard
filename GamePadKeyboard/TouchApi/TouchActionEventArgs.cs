using System;
using Xamarin.Forms;

namespace GamePadKeyboard.TouchApi
{
    public class TouchActionEventArgs : EventArgs
    {
        public TouchActionEventArgs(long id, TouchActionType type, Point location, bool isInContact)
        {
            Id = id;
            Type = type;
            Location = location;
            IsInContact = isInContact;
        }

        public long Id { get; }

        public bool IsInContact { get; }

        public Point Location { get; }

        public TouchActionType Type { get; }
    }
}