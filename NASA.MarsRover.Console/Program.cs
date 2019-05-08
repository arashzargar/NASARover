using NASA.MarsRover.App;
using NASA.MarsRover.Core;
using System;
using System.Collections.Generic;

namespace NASA.MarsRover.APP
{
    class Program
    {
        static void Main(string[] args)
        {
            var hasAtLeastOneRequest = false;
            var requests = new List<RoverMoveRequest>();
            Plateau plateau;

            Console.WriteLine("Welcome to NASA Mars Rover Controller system.\n\n");

            while (true)
            {
                Console.WriteLine("Please Key in upper-right coordinates of the plateau:");
                var keyInCoordinate = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(keyInCoordinate))
                {
                    Console.WriteLine("Invalid upper-right coordinates of the plateau. It should not be empty!");
                    continue;
                }

                var coordinates = keyInCoordinate.Split(" ");

                if (coordinates.Length != 2 ||
                    !int.TryParse(coordinates[0], out int topRightx) ||
                    !int.TryParse(coordinates[1], out int topRighty))
                {
                    Console.WriteLine("Invalid upper-right coordinates of the plateau.");
                    continue;
                }

                plateau = new Plateau(topRightx, topRighty);
                break;
            }

            while (true)
            {
                Console.WriteLine($"PLease key in {(hasAtLeastOneRequest == true ? "Next " : "")}Rover's position{(hasAtLeastOneRequest == true ? " or press enter to print output " : "")}:");

                var keyInPosition = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(keyInPosition))
                {
                    if (hasAtLeastOneRequest)
                        break;

                    Console.WriteLine("Invalid key in Rover position. It should not be empty.");
                    continue;
                }

                var position = keyInPosition.Split(" ");

                if (position.Length != 3 ||
                    !int.TryParse(position[0], out int x) ||
                    !int.TryParse(position[1], out int y) ||
                    string.IsNullOrWhiteSpace(position[2]))
                {
                    Console.WriteLine("Invalid key in Rover position. It should not be empty.");
                    continue;
                }

                Console.WriteLine($"PLease key in {(hasAtLeastOneRequest == true ? "Next " : "")}Rover's instructions:");
                var keyInInstructions = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(keyInInstructions))
                {
                    Console.WriteLine("Invalid key in Rover instructions. It should not be empty.");
                    continue;
                }

                requests.Add(new RoverMoveRequest()
                {
                    X = x,
                    Y = y,
                    Position = position[2].ToUpper(),
                    Instructions = keyInInstructions.ToUpper()
                });

                hasAtLeastOneRequest = true;
            }

            try
            {
                foreach (var item in requests)
                {
                    Rover rover = new Rover();

                    rover.Init(item.X, item.Y, item.Position, item.Instructions);
                    rover.Move(plateau);
                    Console.WriteLine($"{rover.X} {rover.Y} {(char)rover.Direction}");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}\n");
            }

            Console.ReadLine();
        }
    }
}
