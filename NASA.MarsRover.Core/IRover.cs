namespace NASA.MarsRover.Core.Enums
{
    public interface IRover
    {
        void Init(int x, int y, string direction, string instruction);
        void Move(Plateau plateau);
    }
}
