const fs = require('fs');
const util = require('util');
const readFile = util.promisify(fs.readFile);

async function getOriginalInput() {
    const textInput = await readFile('./input.txt', 'utf8');
    return textInput
        .split(',')
        .filter(value => value !== '')
        .map(value => parseInt(value));
}

function processIntcode(intcode) {
    const validOpCodes = [1, 2, 99];

    for (let i = 0; i < intcode.length; i += 4) {
        const opcode = intcode[i];
        if (!validOpCodes.includes(opcode)) {
            console.error(`Non-valid opcode ${opcode}`);
            break;
        }
        const input1Position = intcode[i + 1];
        const input1 = intcode[input1Position];
        const input2Position = intcode[i + 2];
        const input2 = intcode[input2Position];
        const resultStoragePosition = intcode[i + 3];
        if (opcode === 99) {
            break;
        } else if (opcode === 1) {
            intcode[resultStoragePosition] = input1 + input2;
        } else if (opcode === 2) {
            intcode[resultStoragePosition] = input1 * input2;
        }
    }

    return intcode[0];
}

function replaceNoun(intcode, noun) {
    intcode[1] = noun;
}

function replaceVerb(intcode, verb) {
    intcode[2] = verb;
}

(async () => {
    const outputGoal = 19690720;
    let output;

    for (let noun = 0; noun < 100; noun++) {
        for (let verb = 0; verb < 100; verb++) {
            const intcode = await getOriginalInput();
            replaceNoun(intcode, noun);
            replaceVerb(intcode, verb);
            output = processIntcode(intcode);
            if (output === outputGoal) {
                console.log(100 * noun + verb);
                break;
            }
        }
        if (output === outputGoal) {
            break;
        }
    }
})();
