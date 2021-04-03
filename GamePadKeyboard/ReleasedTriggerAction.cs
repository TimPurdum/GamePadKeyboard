using Xamarin.Forms;

namespace GamePadKeyboard
{
    public class ReleasedTriggerAction: TriggerAction<Button>
    {
        protected override void Invoke(Button sender)
        {
            (sender.BindingContext as BaseViewModel)?.ButtonReleasedCommand?.Execute(sender);
        }
    }
}