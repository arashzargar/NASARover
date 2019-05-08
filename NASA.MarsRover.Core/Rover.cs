using NASA.MarsRover.Core.Enums;
using System;
using System.Collections.Generic;

namespace NASA.MarsRover.Core
{
    public class Rover : IRover
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        public CardinalDirection Direction { get; private set; }
        public List<Command> Instruction { get; private set; }
        
        public void Init(int x, int y, string direction, string instruction)
        {
            (X, Y) = GetLocation(x, y);
            Direction = GetDirection(direction);
            Instruction = GetInstruction(instruction);
        }
        
        public void Move(Plateau plateau)
        {
            foreach (var item in Instruction)
            {
                if (item != Command.Move)
                {
                    UpdateDirection(item);
                    continue;
                }
                else
                {
                    switch (Direction)
                    {
                        case CardinalDirection.North: Y++; break;
                        case CardinalDirection.South: Y--; break;
                        case CardinalDirection.East: X++; break;
                        case CardinalDirection.West: X--; break;
                    }

                    if (plateau.TopRightX < X || plateau.TopRightY < Y || X < plateau.ButtomLeftX || Y < plateau.ButtomLeftY)
                        throw new ArgumentOutOfRangeException("The Rover will be out of the plateau using this instructions.");
                }
            }
        }

        private (int X, int Y) GetLocation(int x, int y)
        {
            if (x < 0 || y < 0)
                throw new ArgumentOutOfRangeException("Invalid rover initial location. location must be positive integer. Please try again.");

            return (x, y);
        }

        private List<Command> GetInstruction(string instruction)
        {
            var commands = new List<Command>();

            if (string.IsNullOrWhiteSpace(instruction))
                throw new ArgumentException("Invalid instruction. Please try again.");

            foreach (var c in instruction.ToCharArray())
            {
                if (Enum.IsDefined(typeof(Command), (int)c))
                    commands.Add((Command)c);
                else
                    throw new ArgumentException("Invalid instruction. Instruction must be combination of {R, L, M}. PLease try again.");
            }

            return commands;
        }

        private CardinalDirection GetDirection(string direction)
        {
            CardinalDirection initialDirection;

            if (string.IsNullOrWhiteSpace(direction) || direction.Length > 1)
                throw new ArgumentException("Invalid direction. Direction must be single character. Please try again.");

            if (Enum.IsDefined(typeof(CardinalDirection), (int)direction[0]))
                initialDirection = (CardinalDirection)direction[0];
            else
                throw new ArgumentException("Invalid direction. Direction must be one of these: {N, S, E, W}. Please try again.");

            return initialDirection;
        }

        private void UpdateDirection(Command inst)
        {
            if ((Direction == CardinalDirection.North && inst == Command.Right) ||
                (Direction == CardinalDirection.South && inst == Command.Left))
            {
                Direction = CardinalDirection.East;
                return;
            }

            if ((Direction == CardinalDirection.North && inst == Command.Left) ||
                (Direction == CardinalDirection.South && inst == Command.Right))
            {
                Direction = CardinalDirection.West;
                return;
            }

            if ((Direction == CardinalDirection.East && inst == Command.Right) ||
                (Direction == CardinalDirection.West && inst == Command.Left))
            {
                Direction = CardinalDirection.South;
                return;
            }

            if ((Direction == CardinalDirection.East && inst == Command.Left) ||
                (Direction == CardinalDirection.West && inst == Command.Right))
            {
                Direction = CardinalDirection.North;
                return;
            }
        }
    }
}
