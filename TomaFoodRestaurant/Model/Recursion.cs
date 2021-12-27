using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TomaFoodRestaurant.Model
{
    public class Recursion
    {
        private int elementLevel = -1;
        private int numberOfElements;
        private int[] permutationValue = new int[0];

        private char[] inputSet;
        public char[] InputSet
        {
            get { return inputSet; }
            set { inputSet = value; }
        }

        private int permutationCount = 0;
        public int PermutationCount
        {
            get { return permutationCount; }
            set { permutationCount = value; }
        }
        private List<string> permutatedlist = new List<string>();

        public char[] MakeCharArray(string InputString)
        {
            char[] charString = InputString.ToCharArray();
            Array.Resize(ref permutationValue, charString.Length);
            numberOfElements = charString.Length;
            return charString;
        }

        public List<string> CalcPermutation(int k)
        {
            elementLevel++;
            permutationValue.SetValue(elementLevel, k);

            if (elementLevel == numberOfElements)
            {
                OutputPermutation(permutationValue);
            }
            else
            {
                for (int i = 0; i < numberOfElements; i++)
                {
                    if (permutationValue[i] == 0)
                    {
                        CalcPermutation(i);
                    }
                }
            }
            elementLevel--;
            permutationValue.SetValue(0, k);
            return permutatedlist;
        }

        private void OutputPermutation(int[] value)
        {
            string sr = string.Empty;
            foreach (int i in value)
            {
                sr += (inputSet.GetValue(i - 1));
            }
            permutatedlist.Add(sr);
            PermutationCount++;
        }

    }
}
