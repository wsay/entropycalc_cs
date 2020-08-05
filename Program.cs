using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace entropycs {
    class Program {
        static void Main(string[] args) {
            // First argument is min entropy length
            // Second argument is path

            //byte minEntropyLenth = Convert.ToByte(args[0]);
            byte minEntropyLength = Convert.ToByte("2");
            var uniqueCombinations = new Dictionary<string, long>();

            try {
                //using (var sr = new StreamReader(args[1])) {
                using (var sr = new StreamReader("../../test.txt")) {
                    string line = sr.ReadToEnd();
                    line = line.Replace(System.Environment.NewLine, null);
                    string lineExtra = line + line[..minEntropyLength];

                    for (int offset = 0; offset < line.Length; offset++) {
                        string currentCombination = lineExtra[offset..(offset + minEntropyLength)];
                        if (uniqueCombinations.ContainsKey(currentCombination)) {
                            uniqueCombinations[currentCombination] += 1;
                        } else {
                            uniqueCombinations[currentCombination] = 1;
                        }
                    }

                    // There are more elegant ways to do this, but apparently
                    // a straight foreach loop is actually the fastest.
                    var mostCommon = uniqueCombinations.First();
                    foreach (var kvp in uniqueCombinations) {
                        if (kvp.Value > mostCommon.Value) {
                            mostCommon = kvp;
                        }
                    }

                    Console.WriteLine($"Entropy is {(double)uniqueCombinations[mostCommon.Key] / line.Length}");
                    Console.WriteLine($"Most common combination is: {mostCommon.Key} and it is repeated {mostCommon.Value} tiimes.");
                }
            } catch (IOException e) {
                Console.WriteLine("The file could not be read.");
                Console.WriteLine(e.Message);
                throw;
            }
        }
    }
}
