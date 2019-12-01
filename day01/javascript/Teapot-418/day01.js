const fs = require('fs');
const util = require('util');
const readFile = util.promisify(fs.readFile);

async function getInput() {
    const textInput = await readFile('./input.txt', 'utf8');
    return textInput
        .split('\n')
        .filter(value => value !== '')
        .map(value => parseInt(value));
}

function calculateFuelForModule(mass, totalFuel = 0) {
    const fuel = Math.floor(mass / 3) - 2;
    if (fuel < 0) {
        return totalFuel;
    }
    return calculateFuelForModule(fuel, totalFuel + fuel);
}

(async () => {
    const moduleMasses = await getInput();
    let fuelRequirement = 0;
    moduleMasses.forEach(mass => {
        fuelRequirement += calculateFuelForModule(mass);
    });
    console.log(fuelRequirement);
})();
