function areTwoAdjacentDigitsEqual(number) {
    const numberAsString = number.toString();
    for (let i = 0; i < numberAsString.length - 1; i++) {
        if (numberAsString[i] === numberAsString[i + 1]) {
            return true;
        }
    }
    return false;
}

function hasOnlyIncreasingOrEqualDigits(number) {
    const numberAsString = number.toString();
    for (let i = 0; i < numberAsString.length - 1; i++) {
        const digit1 = parseInt(numberAsString[i]);
        const digit2 = parseInt(numberAsString[i + 1]);
        if (digit1 > digit2) {
            return false;
        }
    }
    return true;
}

// For part 2
function hasExactlyTwoEqualAdjacentDigits(number) {
    const numberAsString = number.toString();
    let hasExactly = false;
    const digits = [];
    for (let i = 0; i < 10; i++) {
        digits.push(i);
    }
    for (let i = 0; i < numberAsString.length - 1; i++) {
        const current = numberAsString[i];
        const next =
            i !== numberAsString.length - 1 ? numberAsString[i + 1] : '';
        const previous = i !== 0 ? numberAsString[i - 1] : '';
        const afterNext =
            i !== numberAsString.length - 2 ? numberAsString[i + 2] : '';
        if (current === next && current !== previous && next !== afterNext) {
            return true;
        }
    }
    return false;
}

const range = '248345-746315'; // puzzle-input
const [startString, endString] = range.split('-');
const start = parseInt(startString);
const end = parseInt(endString);

let current = start;
let possiblePasswordCount = 0;

while (current !== end) {
    if (
        areTwoAdjacentDigitsEqual(current) &&
        hasOnlyIncreasingOrEqualDigits(current) &&
        hasExactlyTwoEqualAdjacentDigits(current)
    ) {
        possiblePasswordCount++;
    }
    current++;
}

console.log(`Amount of possible passwords: ${possiblePasswordCount}`);
