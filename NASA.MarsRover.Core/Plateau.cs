using System;

namespace NASA.MarsRover.Core
{
    public class Plateau
    {
        public int TopRightX { get; private set; }
        public int TopRightY { get; private set; }

        public int ButtomLeftX = 0;

        public int ButtomLeftY = 0;

        private Plateau()
        {
        }

        public Plateau(int x, int y)
        {
            if (x < 1 || y < 1)
                throw new ArgumentOutOfRangeException("Invalid plateau. Please provide positive plateau x, y with minimum of 1. Please try again.");

            TopRightX = x;
            TopRightY = y;
        }
    }
}
