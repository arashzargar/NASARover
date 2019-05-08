using System;
using System.Collections.Generic;
using System.Text;

namespace NASA.MarsRover.App
{
    public class RoverMoveRequest
    {
        public int X { get; set; }
        public int Y { get; set; }
        public string Position { get; set; }
        public string Instructions { get; set; }
    }
}
