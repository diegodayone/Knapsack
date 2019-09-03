using System;
using System.Linq;

namespace Knapsack
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
             * Creating all the 2^N combinations that we might want to check ==> 2^N value
             * Go through all the combinations                               ==> To binary
             * Sum all the elements value and weight for the combination     ==> binary 1 to select items
             * if Weight > Capacity => skip                                  ==> if else statement
             * else OK
             */

            //  00101
            //  0 => do nothing
            //  0 => do nothing
            //  1 => do I have some space for item #3? 
            //           if so, I'm adding it, increase the totalWeight and totalAmount variables
            //  0 => do nothing again
            //  1 => do I have some space for item #5? 
            //           if so, I'm adding it, increase the totalWeight and totalAmount variables
            var capacity = 100;

            var stealableItems = new Item[]
            {
                new Item() { Weight= 5, Value = 5},
                new Item() { Weight= 20, Value = 15},
                new Item() { Weight= 50, Value = 76},
                new Item() { Weight= 100, Value = 80},
                new Item() { Weight= 1, Value = 2},
                new Item() { Weight= 14, Value = 20},
                new Item() { Weight= 11, Value = 11},
                new Item() { Weight= 25, Value = 30},
                new Item() { Weight= 20, Value = 27},
                new Item() { Weight= 10, Value = 20},
                new Item() { Weight= 3, Value = 10},
            };

            var bestCaseValue = -1;
            var bestCaseWeight = -1;
            string winningCombination = "";

            var iterationCount = Math.Pow(2, stealableItems.Length); // 2^(items.Length)
            for(var i = 0; i < iterationCount; i++)
            {
                var combination = Convert.ToString(i, 2).PadLeft(stealableItems.Length, '0');

                var totalWeight = 0;
                var totalAmount = 0;

                for(var selectedIndex = 0; selectedIndex < stealableItems.Length; selectedIndex++)
                {
                    if (combination[selectedIndex].Equals('1')) //if in our combination the item #selectedIndex is 1, let's try to steal it
                    {
                        var currentItemToBeStealed = stealableItems[selectedIndex];

                        if (totalWeight + currentItemToBeStealed.Weight <= capacity) //if we have space
                        {
                            totalWeight += currentItemToBeStealed.Weight; //we add the element to our bag
                            totalAmount += currentItemToBeStealed.Value;
                        }
                        else
                        {
                            //this combination is not valid, therefore we are assigning the lowest "score" possible
                            totalWeight = int.MaxValue;
                            totalAmount = int.MinValue;
                            break;
                        }
                    }
                }

                if (totalAmount > bestCaseValue)
                {
                    bestCaseValue = totalAmount;
                    bestCaseWeight = totalWeight;
                    winningCombination = combination;

                    Console.WriteLine("Possible winning combination = " + winningCombination);
                    Console.WriteLine("With a value of " + bestCaseValue + " - " + bestCaseWeight);
                }
            }
        
        }

        static bool[] ToBits(int input, int numberOfBits)
        {
            return Enumerable.Range(0, numberOfBits)
            .Select(bitIndex => 1 << bitIndex)
            .Select(bitMask => (input & bitMask) == bitMask)
            .ToArray();
        }
    }
}
