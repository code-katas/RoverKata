import Rover from '../rover.js';

describe('Rover', function() {
  let rover;

  beforeEach(function(){
    rover = new Rover({x: 0, y: 0, direction: "n"}, {gridSize: 10});
  });

  const moveMany = (direction, times) => {
    for(let i = 0; i < times; i++) {
      rover.move(direction);
    }
  };

  const expectPosition = (x, y, direction) => {
    const position = rover.getPosition();
    expect(position.x).to.equal(x);
    expect(position.y).to.equal(y);
    expect(position.direction).to.equal(direction);
  };

  it("should return position", function() {
    expectPosition(0, 0, "n");
  });

  it("should move forward", function() {
    rover.move("f");
    expectPosition(0, 1, "n");
  });

  it("should move backward", function() {
    rover.move("b");
    expectPosition(0, -1, "n");
  });

  it("should rotate counter-clockwise", function() {
    rover.move("l");
    expectPosition(0, 0, "w");
    rover.move("l");
    expectPosition(0, 0, "s");
    rover.move("l");
    expectPosition(0, 0, "e");
    rover.move("l");
    expectPosition(0, 0, "n");
  });

  it("should rotate clockwise", function() {
    rover.move("r");
    expectPosition(0, 0, "e");
    rover.move("r");
    expectPosition(0, 0, "s");
    rover.move("r");
    expectPosition(0, 0, "w");
    rover.move("r");
    expectPosition(0, 0, "n");
  });

  it("should rotate and move", function() {
    rover.move("f");
    rover.move("f");
    rover.move("r");
    rover.move("f");
    rover.move("f");
    expectPosition(2, 2, "e");
  });

  it("should wrap around grid horizontally", function() {
    rover.move("l");
    moveMany("f", 6);
    expectPosition(5, 0, "w");
  });

  it("should wrap around grid vertically", function() {
    moveMany("f", 6);
    expectPosition(0, -5, "n");
  });

});
