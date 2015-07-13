export default class Rover {
  constructor(position, grid){
    this.position = position;
    this.grid = grid;
    this.movements = {
      n: {
        axis: "y"
        ,forward: 1
        ,right: "e"
        ,left: "w"
      }
      ,e: {
        axis: "x"
        ,forward: 1
        ,right: "s"
        ,left: "n"
      }
      ,s: {
        axis: "y"
        ,forward: -1
        ,right: "w"
        ,left: "e"
      }
      ,w: {
        axis: "x"
        ,forward: -1
        ,right: "n"
        ,left: "s"
      }
    };
  }

  getPosition() {
    return this.position;
  }

  rotateLeft() {
    let move = this.movements[this.position.direction];
    this.position.direction = move.left;
  }

  rotateRight() {
    let move = this.movements[this.position.direction];
    this.position.direction = move.right;
  }

  go(delta) {
    const move = this.movements[this.position.direction];
    this.position[move.axis] = this.position[move.axis] + (move.forward * delta);
  }

  detectEdge() {
    const edge = this.grid.gridSize / 2;
    const move = this.movements[this.position.direction];
    if (Math.abs(this.position[move.axis]) > (edge)) {
      this.position[move.axis] = (this.position[move.axis] * -1); //wrap around
      this.go(1); //get back onto the grid
    }
  }

  move(commands) {
    switch (commands) {
      case "f":
        this.go(1);
        break;
      case "b":
        this.go(-1);
        break;
      case "r":
        this.rotateRight();
        break;
      case "l":
        this.rotateLeft();
        break;
      default:

    }
    this.detectEdge();
  }

}
