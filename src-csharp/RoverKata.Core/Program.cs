using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoverKata.Core
{
    class Program
    {
        static void Main(string[] args)
        {
        }
    }

    public class Rover
    {
        public char direction { get; set; }
        public Location location { get; set; }

        public Grid grid { get; set; }

        private IList<char> _directionMap
        {
            get { return new List<char> {'N', 'E', 'S', 'W'}; }
        }

        public Rover(Grid startGrid, Location loc, char dir)
        {
            location = loc;
            direction = dir;
            grid = startGrid;
        }

        public void ProcessCommands(char[] commands)
        {
            foreach (var c in commands)
            {
                ProcessCommand(c);
            }
        }

        public void ProcessCommand(char command)
        {

            switch (command)
            {
                case 'f':
                    ProcessMotion(1);
                    break;
                case 'b':
                    ProcessMotion(-1);
                    break;
                case 'r':
                    if (direction == _directionMap.Last())
                    {
                        direction = _directionMap.First();
                    }
                    direction = _directionMap[_directionMap.IndexOf(direction) + 1];
                    break;
                case 'l':
                    if (direction == _directionMap.First())
                    {
                        direction = _directionMap.Last();
                    }
                    direction = _directionMap[_directionMap.IndexOf(direction) - 1];
                    break;
                default:
                    throw new ArgumentOutOfRangeException("command", "You can't give that command.");
            }
        }

        private void ProcessMotion(decimal magnitude)
        {
            var potentialLocation = location;
            switch (direction)
            {
                case 'N':
                    potentialLocation.latitude = ((potentialLocation.latitude + magnitude) % grid.maxSize.latitude) - 1;
                    break;
                case 'E':
                    potentialLocation.longitude += magnitude;
                    break;
                case 'S':
                    potentialLocation.latitude -= magnitude;
                    break;
                case 'W':
                    potentialLocation.longitude -= magnitude;
                    break;
            }

            if(grid.IsAvailableLocation(potentialLocation))
            {
                location = potentialLocation;
            }

        }
 
    }

    public class Grid
    {
        public Location maxSize { get; set; }

        public Grid(decimal maxLat, decimal maxLong)
        {
            maxSize = new Location(maxLat, maxLong);
        }

        public bool IsAvailableLocation(Location testLoc)
        {
            return !(testLoc.latitude > maxSize.latitude 
                || testLoc.longitude > maxSize.longitude 
                || testLoc.latitude < 0 
                || testLoc.longitude < 0);
        }


    }

    public class Location
    {
        public decimal latitude;
        public decimal longitude;

        public Location(decimal x, decimal y)
        {
            latitude = x;
            longitude = y;
        }
    }
}
