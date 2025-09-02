using System.Linq;
using System.Text.Json;
using System.Collections.Generic;

public class Question1
{
    public static string MaxIncreasingSubArrayAsJson(List<int> numbers)
    {
        if (numbers == null)
            throw new ArgumentNullException(nameof(numbers));

        if (numbers.Count == 0 || numbers.Count == 1)
            return JsonSerializer.Serialize(numbers);

        int maxSum = int.MinValue;
        int bestStartIndex = 0;
        int bestEndIndex = 0;
        int currentStartIndex = 0;
        int currentSum = 0;

        int endIndexOfNumbersList = numbers.Count - 1;

        for (int i = 0; i <= endIndexOfNumbersList; i++)
        {
            currentSum += numbers[i];

            if (i != endIndexOfNumbersList && numbers[i] >= numbers[i + 1])
            {
                if (maxSum < currentSum)
                {
                    maxSum = currentSum;
                    bestStartIndex = currentStartIndex;
                    bestEndIndex = i;
                }
                currentSum = 0;
                currentStartIndex = i + 1;
            }
            else if (i == endIndexOfNumbersList)
            {
                if (maxSum < currentSum)
                {
                    maxSum = currentSum;
                    bestStartIndex = currentStartIndex;
                    bestEndIndex = i;
                }
            }
        }

        return JsonSerializer.Serialize(numbers.GetRange(bestStartIndex, bestEndIndex - bestStartIndex + 1));
    }
}