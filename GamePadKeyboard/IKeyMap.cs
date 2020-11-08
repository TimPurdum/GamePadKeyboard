namespace GamePadKeyboard
{
    public interface IKeyMap
    {
        object GetNativeKeyCode(FormsKey formsKey);
    }
}