const fs = require('fs');
const util = require('util');
const readFile = util.promisify(fs.readFile);

class Coordinate {
    constructor(x, y) {
        this.x = x;
        this.y = y;
    }

    // Starting point is (0,0)
    determineDistanceToStartingPoint() {
        return Math.abs(this.x) + Math.abs(this.y);
    }

    equals(coordinate) {
        return this.x === coordinate.x && this.y === coordinate.y;
    }

    goUp(distance) {
        this.y += distance;
    }

    goDown(distance) {
        this.y -= distance;
    }

    goRight(distance) {
        this.x += distance;
    }

    goLeft(distance) {
        this.x -= distance;
    }

    isStartPoint() {
        return this.x === 0 && this.y === 0;
    }

    performInstruction(instruction) {
        const direction = instruction.substring(0, 1);
        const distance = parseInt(instruction.substring(1, instruction.length));
        switch (direction) {
            case 'U':
                this.goUp(distance);
                break;
            case 'D':
                this.goDown(distance);
                break;
            case 'R':
                this.goRight(distance);
                break;
            case 'L':
                this.goLeft(distance);
                break;
            default:
                console.warn(`Wrong instruction code ${instruction}`);
                break;
        }
    }
}

async function getWirePaths() {
    const textInput = await readFile('./input.txt', 'utf8');
    const wirePathsAsString = textInput.split('\n');
    const wirePaths = [];
    wirePathsAsString.forEach(wirePathString => {
        if (wirePathString) {
            wirePaths.push(wirePathString.split(','));
        }
    });
    return wirePaths;
}

(async () => {
    const wirePaths = await getWirePaths();

    const wireCorners = [];

    // Determine all corner-coordinates for each wire
    for (let i = 0; i < wirePaths.length; i++) {
        wireCorners.push([]);
        wireCorners[i].push(new Coordinate(0, 0));
        const wireCorner = new Coordinate(0, 0);
        for (const instruction of wirePaths[i]) {
            wireCorner.performInstruction(instruction);
            wireCorners[i].push(new Coordinate(wireCorner.x, wireCorner.y));
        }
    }

    // Find all intersection points
    const intersectionPoints = [];
    let wire1Steps = 0;
    const stepsTaken = [];

    for (let i = 0; i < wireCorners.length - 1; i++) {
        for (let j = 0; j < wireCorners[i].length - 1; j++) {
            const startWire1Part = wireCorners[i][j];
            const endWire1Part = wireCorners[i][j + 1];
            const currentWire1PartLocation = new Coordinate(
                startWire1Part.x,
                startWire1Part.y
            );

            while (!currentWire1PartLocation.equals(endWire1Part)) {
                let wire2Steps = 0;
                for (let k = 0; k < wireCorners[i + 1].length - 1; k++) {
                    const startWire2Part = wireCorners[i + 1][k];
                    const endWire2Part = wireCorners[i + 1][k + 1];
                    const currentWire2PartLocation = new Coordinate(
                        startWire2Part.x,
                        startWire2Part.y
                    );

                    while (!currentWire2PartLocation.equals(endWire2Part)) {
                        if (
                            currentWire1PartLocation.equals(
                                currentWire2PartLocation
                            ) &&
                            !currentWire1PartLocation.isStartPoint()
                        ) {
                            intersectionPoints.push(
                                new Coordinate(
                                    currentWire1PartLocation.x,
                                    currentWire1PartLocation.y
                                )
                            );
                            stepsTaken.push(wire1Steps + wire2Steps);
                        }

                        if (startWire2Part.x === endWire2Part.x) {
                            if (startWire2Part.y < endWire2Part.y) {
                                currentWire2PartLocation.goUp(1);
                            } else if (startWire2Part.y > endWire2Part.y) {
                                currentWire2PartLocation.goDown(1);
                            }
                        } else if (startWire2Part.y === endWire2Part.y) {
                            if (startWire2Part.x < endWire2Part.x) {
                                currentWire2PartLocation.goRight(1);
                            } else if (startWire2Part.x > endWire2Part.x) {
                                currentWire2PartLocation.goLeft(1);
                            }
                        }
                        wire2Steps++;
                    }
                }

                if (startWire1Part.x === endWire1Part.x) {
                    if (startWire1Part.y < endWire1Part.y) {
                        currentWire1PartLocation.goUp(1);
                    } else if (startWire1Part.y > endWire1Part.y) {
                        currentWire1PartLocation.goDown(1);
                    }
                } else if (startWire1Part.y === endWire1Part.y) {
                    if (startWire1Part.x < endWire1Part.x) {
                        currentWire1PartLocation.goRight(1);
                    } else if (startWire1Part.x > endWire1Part.x) {
                        currentWire1PartLocation.goLeft(1);
                    }
                }
                wire1Steps++;
            }
        }
    }

    // Find smallest distance
    console.log(
        `Closest distance: ${Math.min(
            ...intersectionPoints.map(intersectionPoint =>
                intersectionPoint.determineDistanceToStartingPoint()
            )
        )}`
    );

    // Find smallest amount of steps taken
    console.log(`Minimal amount of steps: ${Math.min(...stepsTaken)}`);
})();
