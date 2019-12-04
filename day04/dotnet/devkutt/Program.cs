using System;

namespace devkutt
{
    class Program
    {
        private static readonly int Min = 372037;

        private static readonly int Max = 905157;

        static void Main(string[] args)
        {
            Console.WriteLine(PasswordCount(Min, Max));
        }

        static int PasswordCount(int min, int max)
        {
            int count = 0;
            for (int i = min; i < max; i++)
            {
                var numberAsString = i.ToString();
                if (numberAsString.Length != 6)
                {
                    continue;
                }
                var error = false;
                var foundDouble = false;
                for (int j = 1; j < numberAsString.Length; j++)
                {
                    char? numberBeforeThat = null;
                    if (j - 2 >= 0)
                    {
                        numberBeforeThat = numberAsString[j - 2];
                    }
                    var numberBefore = numberAsString[j - 1];
                    var number = numberAsString[j];
                    char? numberAfter = null;
                    if (j + 1 < numberAsString.Length)
                    {
                        numberAfter = numberAsString[j + 1];
                    }

                    if (number == numberBefore)
                    {
                        if (numberBeforeThat != number && numberAfter != number)
                        {
                            foundDouble = true;
                        }
                    }
                    else if (numberBefore > number)
                    {
                        error = true;
                    }
                }
                if (error)
                {
                    continue;
                }
                if (!foundDouble)
                {
                    continue;
                }

                count++;
            }

            return count;
        }
    }
}
