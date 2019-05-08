using NASA.MarsRover.Core;
using NASA.MarsRover.Core.Enums;
using System;
using Xunit;

namespace NASA.MarsRover.Test
{
    public class RoverControllerUnitTest
    {
        [Theory]
        [InlineData(5, 5, 1, 2, "N", "LMLMLMLMM", 1, 3, CardinalDirection.North)]
        [InlineData(5, 5, 3, 3, "E", "MMRMMRMRRM", 5, 1, CardinalDirection.East)]
        [InlineData(7, 7, 2, 0, "W", "MRRMMLMMRLMMLL", 3, 4, CardinalDirection.South)]
        public void Rover_Move_Success_Test(int plateauX, int plateauY, int x, int y, string direction, string instuction, int expectedX, int expectedY, CardinalDirection expectedDirection)
        {
            var rover = new Rover();
            var plateau = new Plateau(plateauX, plateauY);

            rover.Init(x, y, direction, instuction);
            rover.Move(plateau);

            Assert.Equal(expectedX, rover.X);
            Assert.Equal(expectedY, rover.Y);
            Assert.Equal(expectedDirection, rover.Direction);
        }

        [Theory]
        [InlineData(5, 5, 1, 2, "N", "RRMMM")]
        public void Rover_Move_Failed_Test(int plateauX, int plateauY, int x, int y, string direction, string instuction)
        {
            // arrang
            var rover = new Rover();
            rover.Init(x, y, direction, instuction);
            var plateau = new Plateau(plateauX, plateauY);

            // act
            var exception = Record.Exception(() => rover.Move(plateau));

            // assert
            Assert.NotNull(exception);
            Assert.IsType<ArgumentOutOfRangeException>(exception);
        }

        [Theory]
        [InlineData(1, -1, 2, 2, "N", "LMLMLMLMM")]
        [InlineData(-1, 1, 1, 2, "N", "LMLMLMLMM")]
        [InlineData(-1, -1, 1, 2, "N", "LMLMLMLMM")]
        public void Rover_Plateau_Failed_Test(int plateauX, int plateauY, int x, int y, string direction, string instuction)
        {
            // arrang
            var rover = new Rover();

            // act
            var exception = Record.Exception(() => new Plateau(plateauX, plateauY));

            // assert
            Assert.NotNull(exception);
            Assert.IsType<ArgumentOutOfRangeException>(exception);
        }

        [Theory]
        [InlineData(1, 1, -1, 2, "N", "LMLMLMLMM")]
        public void Rover_Location_Failed_Test(int plateauX, int plateauY, int x, int y, string direction, string instuction)
        {
            // arrang
            var plateau = new Plateau(plateauX, plateauY);
            var rover = new Rover();

            // act
            var exception = Record.Exception(() => rover.Init(x, y, direction, instuction));

            // assert
            Assert.NotNull(exception);
            Assert.IsType<ArgumentOutOfRangeException>(exception);
        }

        [Theory]
        [InlineData(10, 10, 1, 2, "", "LMLMLMLMM")]
        [InlineData(10, 10, 1, 2, "K", "LMLMLMLMM")]
        [InlineData(10, 10, 1, 2, "N", "")]
        [InlineData(10, 10, 1, 2, "S", "W")]
        public void Rover_Direction_Instruction_Failed_Test(int plateauX, int plateauY, int x, int y, string direction, string instuction)
        {
            // arrang
            var plateau = new Plateau(plateauX, plateauY);
            var rover = new Rover();

            // act
            var exception = Record.Exception(() => rover.Init(x, y, direction, instuction));

            // assert
            Assert.NotNull(exception);
            Assert.IsType<ArgumentException>(exception);
        }
    }
}
