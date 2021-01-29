namespace Main
{
    public interface IInputService
    {
        bool IsJumpDown();
        bool IsJumpUp();
        bool IsRightArrowDown();
        bool IsRightArrowUp();
        bool IsLeftArrowDown();
        bool IsLeftArrowUp();
    }
}