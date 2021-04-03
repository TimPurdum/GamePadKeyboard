using Xamarin.Forms;

namespace GamePadKeyboard
{
    internal class DragInfo
    {
        public DragInfo(long id, Point pressPoint)
        {
            Id = id;
            PressPoint = pressPoint;
        }

        public long Id { get; }

        public Point PressPoint { get; }
    }
}