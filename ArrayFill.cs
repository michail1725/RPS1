using System;

namespace Lab1
{
    class ArrayFill
    {
        public void Fill(int len, int[] arr)
        {
            Random rnd = new Random();
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = rnd.Next(-10000000,10000000);
            }
        }
    }
}
