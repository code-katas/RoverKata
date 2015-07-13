using System;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RoverKata.Core;
using Should;

namespace RoverKata.Tests
{
    [TestClass]
    public class RoverTests
    {
        [TestMethod]
        public void Can_initialize_rover()
        {
            var startingLoc = new Location(2.0m, 3.0m);
            var startingDir = 'N';
            var grid = new Grid(3, 3);
            var rover = new Rover(grid, startingLoc, startingDir);
            rover.ShouldNotBeNull();

        }

        [TestMethod]
        public void Can_receive_a_move_command()
        {
            var loc = new Location(1.0m, 1.0m);
            var startingDir = 'N';
            var grid = new Grid(3, 3);
            var rover = new Rover(grid, loc, startingDir);

            rover.ProcessCommand('b');
            rover.location.latitude.ShouldEqual(0.0m);
        }

        [TestMethod]
        public void Can_receive_a_turn_command()
        {
            var loc = new Location(1.0m, 1.0m);
            var startingDir = 'N';
            var grid = new Grid(3, 3);
            var rover = new Rover(grid, loc, startingDir);

            rover.ProcessCommand('r');
            rover.direction.ShouldEqual('E');
        }

        [TestMethod]
        public void Can_receive_array_of_commands()
        {
            var loc = new Location(1.0m, 1.0m);
            var startingDir = 'N';
            var grid = new Grid(3, 3);
            var rover = new Rover(grid, loc, startingDir);

            rover.ProcessCommands(new []{'r', 'f', 'l', 'f', 'f'});
            rover.location.latitude.ShouldEqual(3.0m);
            rover.location.longitude.ShouldEqual(2.0m);
            rover.direction.ShouldEqual('N');
        }

        [TestMethod]
        public void Can_move_from_any_direction()
        {
            var loc = new Location(1.0m, 1.0m);
            var grid = new Grid(3, 3);
            var rover1 = new Rover(grid, loc, 'S');
            
            rover1.ProcessCommand('f');
            rover1.location.latitude.ShouldEqual(0.0m);

            var rover2 = new Rover(grid, loc, 'W');
            rover2.ProcessCommand('b');
            rover2.location.longitude.ShouldEqual(2.0m);
        }

        [TestMethod]
        public void Can_Initialize_Grid()
        {
            var grid = new Grid(3, 3);
            grid.ShouldNotBeNull();
        }

        [TestMethod]
        public void Can_determine_if_outside_grid()
        {
            var grid = new Grid(3, 3);
            var location1 = new Location(2, 1);
            grid.IsAvailableLocation(location1).ShouldBeTrue();

            var location2 = new Location(4,2);
            grid.IsAvailableLocation(location2).ShouldBeFalse();
        }

        [TestMethod]
        public void Grid_wraps_movement_when_outside_bounds()
        {
            var loc = new Location(3.0m, 3.0m);
            var grid = new Grid(3, 3);

            var rover = new Rover(grid, loc, 'N');

            rover.ProcessCommand('f');
            rover.location.latitude.ShouldEqual(0.0m);
            rover.location.longitude.ShouldEqual(3.0m);
        }
    }
}
