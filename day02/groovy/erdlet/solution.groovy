def initialMemory = new File('input.txt').text.tokenize(',').collect { elem -> elem as int }

def task1Memory = resetWithMemory(initialMemory)
task1Memory[1] = 12
task1Memory[2] = 2

def task1Output = calculateOutput(task1Memory)
println("Task 1 result: ${task1Output}")

def solutionFound = false
for (int i = 0; i <= 99; i++) {
    if (solutionFound) {
        break
    }
    
    for (int j = 99; j >= 0; j--) {
        def task2Memory = resetWithMemory(initialMemory)
        task2Memory[1] = i
        task2Memory[2] = j

        def task2Output = calculateOutput(task2Memory)
        if (task2Output == 19690720) {
            println("Task 2 result: ${100 * i + j}")
            break
        }
    }
}

class Instruction {
    final int OPCODE_ADD = 1
    final int OPCODE_MULTIPLY = 2
    final int OPCODE_END = 99

    int opCode
    int operand1Address
    int operand2Address
    int targetIndex

    def isAddInstruction() {
        return opCode == OPCODE_ADD
    }

    def isMultiplyInstruction() {
        return opCode == OPCODE_MULTIPLY
    }

    def isEndInstruction() {
        return opCode == OPCODE_END
    }

    @java.lang.Override
    java.lang.String toString() {
        return "(${opCode}, ${operand1Address}, ${operand2Address}, ${targetIndex})";
    }
}

static def calculateOutput(memory) {
    def instructionPointer = 0
    while (true) {
        def instruction = new Instruction(opCode: memory[instructionPointer], operand1Address: memory[instructionPointer + 1],
                operand2Address: memory[instructionPointer + 2], targetIndex: memory[instructionPointer + 3])

        if (instruction.isAddInstruction()) {
            handleOpcodeAdd(instruction, memory)
        } else if (instruction.isMultiplyInstruction()) {
            handleOpCodeMultiply(instruction, memory)
        } else if (instruction.isEndInstruction()) {
            return memory[0]
        } else {
            throw new GroovyRuntimeException("Ooooops... encountered unknown opcode ${instruction.opCode}")
        }

        instructionPointer += 4
    }
}

static def handleOpcodeAdd(instruction, intCode) {
    int result = intCode[instruction.operand1Address] + intCode[instruction.operand2Address]
    intCode[instruction.targetIndex] = result
}

static def handleOpCodeMultiply(instruction, intCode) {
    int result = intCode[instruction.operand1Address] * intCode[instruction.operand2Address]
    intCode[instruction.targetIndex] = result
}

static def resetWithMemory(initialMemory) {
    return initialMemory.collect { elem -> elem }
}