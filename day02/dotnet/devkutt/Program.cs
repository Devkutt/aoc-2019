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
            FileStream fileStream = new FileStream("input.txt", FileMode.Open);
            int[] program;
            using (var reader = new StreamReader(fileStream))
            {
                var line = reader.ReadLine();
                program = line.Split(",").Select(x => int.Parse(x)).ToArray();
            }
            CalculateNounAndVerb(program, TARGET_OUTPUT);
        }

        static void CalculateNounAndVerb(int[] program, int targetOutput)
        {
            for (int noun = 0; noun < 100; noun++)
            {
                for (int verb = 0; verb < 100; verb++)
                {
                    program[1] = noun;
                    program[2] = verb;
                    var computedProgram = Compute(program);
                    var output = computedProgram[0];
                    if (output == targetOutput)
                    {
                        Console.WriteLine(100 * noun + verb);
                        return;
                    }
                }
            }
        }

        static int[] Compute(int[] program)
        {
            int currentPointer = 0;
            int[] computedProgram = new int[program.Length];
            program.CopyTo(computedProgram, 0);

            while (true)
            {
                int opCode = computedProgram[currentPointer];
                if (opCode == 99)
                {
                    return computedProgram;
                }
                int position1 = computedProgram[currentPointer + 1];
                int position2 = computedProgram[currentPointer + 2];
                int position3 = computedProgram[currentPointer + 3];

                switch (opCode)
                {
                    case 1:
                        computedProgram[position3] = computedProgram[position1] + computedProgram[position2];
                        break;
                    case 2:
                        computedProgram[position3] = computedProgram[position1] * computedProgram[position2];
                        break;
                    default:
                        throw new Exception("Something went wrong");
                }
                currentPointer += 4;
            }
        }
    }
}
