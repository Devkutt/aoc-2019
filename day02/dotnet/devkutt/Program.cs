using System;
using System.IO;
using System.Linq;

namespace devkutt
{
    class Program
    {
        private static readonly int TARGET_OUTPUT = 19690720;

        static void Main(string[] args)
        {
            int[] program = ReadProgram("input.txt");
            CalculateNounAndVerb(program, TARGET_OUTPUT);
        }

        static int[] ReadProgram(string inputFile)
        {
            FileStream fileStream = new FileStream(inputFile, FileMode.Open);
            using (var reader = new StreamReader(fileStream))
            {
                var line = reader.ReadLine();
                return line.Split(",").Select(x => int.Parse(x)).ToArray();
            }
        }

        static void CalculateNounAndVerb(int[] program, int targetOutput)
        {
            for (int noun = 0; noun < 100; noun++)
            {
                for (int verb = 0; verb < 100; verb++)
                {
                    var output = Compute(program, noun, verb);
                    if (output == targetOutput)
                    {
                        Console.WriteLine(100 * noun + verb);
                        return;
                    }
                }
            }
        }

        static int Compute(int[] program, int noun, int verb)
        {
            int instructionPointer = 0;
            int[] computedProgram = new int[program.Length];
            program.CopyTo(computedProgram, 0);
            computedProgram[1] = noun;
            computedProgram[2] = verb;

            while (true)
            {
                int opCode = computedProgram[instructionPointer];
                if (opCode == 99)
                {
                    return computedProgram[0];
                }
                int parameterAddress1 = computedProgram[instructionPointer + 1];
                int parameterAddress2 = computedProgram[instructionPointer + 2];
                int parameterAddress3 = computedProgram[instructionPointer + 3];
                int parameter1 = computedProgram[parameterAddress1];
                int parameter2 = computedProgram[parameterAddress2];

                switch (opCode)
                {
                    case 1:
                        computedProgram[parameterAddress3] = parameter1 + parameter2;
                        break;
                    case 2:
                        computedProgram[parameterAddress3] = parameter1 * parameter2;
                        break;
                    default:
                        throw new Exception("Something went wrong");
                }
                instructionPointer += 4;
            }
        }
    }
}
