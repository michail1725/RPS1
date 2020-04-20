using System; 
namespace Lab1
{
    public class ArrayCalc
    {
        public double AverageValue(int[] arr) {
            double avg = 0;
            int sum = 0;
            for (int i = 0; i < arr.Length; i++)
            {
                try
                {
                    checked
                    {
                        if (arr[i] > 0) sum += arr[i];
                        else sum += Math.Abs(arr[i]);
                    }
                }
                catch(OverflowException) {
                    Console.WriteLine("Невозможно точно посчитать среднее значение этого массива, так как происходит переполнение");
                    return -1.0;
                }
            }
            avg = (double)sum / arr.Length;
            return avg;
        }
    }
}
