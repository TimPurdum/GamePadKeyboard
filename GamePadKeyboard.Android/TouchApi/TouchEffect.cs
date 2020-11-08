using System;
using System.Collections.Generic;
using System.Linq;
using Android.Views;
using GamePadKeyboard.TouchApi;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using TouchEffect = GamePadKeyboard.Droid.TouchApi.TouchEffect;
using View = Android.Views.View;

[assembly: ResolutionGroupName("XamarinDocs")]
[assembly: ExportEffect(typeof(TouchEffect), "TouchEffect")]

namespace GamePadKeyboard.Droid.TouchApi
{
    public class TouchEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            // Get the Android View corresponding to the Element that the effect is attached to
            view = Control == null ? Container : Control;

            // Get access to the TouchEffect class in the .NET Standard library
            var touchEffect =
                (GamePadKeyboard.TouchApi.TouchEffect) Element.Effects.FirstOrDefault(e =>
                    e is GamePadKeyboard.TouchApi.TouchEffect);

            if (touchEffect != null && view != null)
            {
                viewDictionary.Add(view, this);

                formsElement = Element;

                libTouchEffect = touchEffect;

                // Save fromPixels function
                fromPixels = view.Context.FromPixels;

                // Set event handler on View
                view.Touch += OnTouch;
            }
        }

        protected override void OnDetached()
        {
            if (viewDictionary.ContainsKey(view))
            {
                viewDictionary.Remove(view);
                view.Touch -= OnTouch;
            }
        }

        private void OnTouch(object sender, View.TouchEventArgs args)
        {
            // Two object common to all the events
            var senderView = sender as View;
            var motionEvent = args.Event;

            // Get the pointer index
            var pointerIndex = motionEvent.ActionIndex;

            // Get the id that identifies a finger over the course of its progress
            var id = motionEvent.GetPointerId(pointerIndex);


            senderView.GetLocationOnScreen(twoIntArray);
            var screenPointerCoords = new Point(twoIntArray[0] + motionEvent.GetX(pointerIndex),
                twoIntArray[1] + motionEvent.GetY(pointerIndex));


            // Use ActionMasked here rather than Action to reduce the number of possibilities
            switch (args.Event.ActionMasked)
            {
                case MotionEventActions.Down:
                case MotionEventActions.PointerDown:
                    FireEvent(this, id, TouchActionType.Pressed, screenPointerCoords, true);

                    idToEffectDictionary.Add(id, this);

                    capture = libTouchEffect.Capture;
                    break;

                case MotionEventActions.Move:
                    // Multiple Move events are bundled, so handle them in a loop
                    for (pointerIndex = 0; pointerIndex < motionEvent.PointerCount; pointerIndex++)
                    {
                        id = motionEvent.GetPointerId(pointerIndex);

                        if (capture)
                        {
                            senderView.GetLocationOnScreen(twoIntArray);

                            screenPointerCoords = new Point(twoIntArray[0] + motionEvent.GetX(pointerIndex),
                                twoIntArray[1] + motionEvent.GetY(pointerIndex));

                            FireEvent(this, id, TouchActionType.Moved, screenPointerCoords, true);
                        }
                        else
                        {
                            CheckForBoundaryHop(id, screenPointerCoords);

                            if (idToEffectDictionary[id] != null)
                                FireEvent(idToEffectDictionary[id], id, TouchActionType.Moved, screenPointerCoords,
                                    true);
                        }
                    }

                    break;

                case MotionEventActions.Up:
                case MotionEventActions.Pointer1Up:
                    if (capture)
                    {
                        FireEvent(this, id, TouchActionType.Released, screenPointerCoords, false);
                    }
                    else
                    {
                        CheckForBoundaryHop(id, screenPointerCoords);

                        if (idToEffectDictionary[id] != null)
                            FireEvent(idToEffectDictionary[id], id, TouchActionType.Released, screenPointerCoords,
                                false);
                    }

                    idToEffectDictionary.Remove(id);
                    break;

                case MotionEventActions.Cancel:
                    if (capture)
                    {
                        FireEvent(this, id, TouchActionType.Cancelled, screenPointerCoords, false);
                    }
                    else
                    {
                        if (idToEffectDictionary[id] != null)
                            FireEvent(idToEffectDictionary[id], id, TouchActionType.Cancelled, screenPointerCoords,
                                false);
                    }

                    idToEffectDictionary.Remove(id);
                    break;
            }
        }

        private void CheckForBoundaryHop(int id, Point pointerLocation)
        {
            TouchEffect touchEffectHit = null;

            foreach (var view in viewDictionary.Keys)
            {
                // Get the view rectangle
                try
                {
                    view.GetLocationOnScreen(twoIntArray);
                }
                catch // System.ObjectDisposedException: Cannot access a disposed object.
                {
                    continue;
                }

                var viewRect = new Rectangle(twoIntArray[0], twoIntArray[1], view.Width, view.Height);

                if (viewRect.Contains(pointerLocation)) touchEffectHit = viewDictionary[view];
            }

            if (touchEffectHit != idToEffectDictionary[id])
            {
                if (idToEffectDictionary[id] != null)
                    FireEvent(idToEffectDictionary[id], id, TouchActionType.Exited, pointerLocation, true);
                if (touchEffectHit != null)
                    FireEvent(touchEffectHit, id, TouchActionType.Entered, pointerLocation, true);
                idToEffectDictionary[id] = touchEffectHit;
            }
        }

        private void FireEvent(TouchEffect touchEffect, int id, TouchActionType actionType, Point pointerLocation,
            bool isInContact)
        {
            // Get the method to call for firing events
            Action<Element, TouchActionEventArgs> onTouchAction = touchEffect.libTouchEffect.OnTouchAction;

            // Get the location of the pointer within the view
            touchEffect.view.GetLocationOnScreen(twoIntArray);
            var x = pointerLocation.X - twoIntArray[0];
            var y = pointerLocation.Y - twoIntArray[1];
            var point = new Point(fromPixels(x), fromPixels(y));

            // Call the method
            onTouchAction(touchEffect.formsElement,
                new TouchActionEventArgs(id, actionType, point, isInContact));
        }

        private static readonly Dictionary<View, TouchEffect> viewDictionary =
            new Dictionary<View, TouchEffect>();

        private static readonly Dictionary<int, TouchEffect> idToEffectDictionary =
            new Dictionary<int, TouchEffect>();

        private bool capture;
        private Element formsElement;
        private Func<double, double> fromPixels;
        private GamePadKeyboard.TouchApi.TouchEffect libTouchEffect;
        private readonly int[] twoIntArray = new int[2];
        private View view;
    }
}