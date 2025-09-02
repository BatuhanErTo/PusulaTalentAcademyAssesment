using System.Linq;
using System.Text.Json;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using System.Collections.Generic;
using System.Globalization;
public class Question2
{

    private const string TURKISH_VOWEL = "aeıioöuü";
    private const string TURKISH_CULTURE_INFO_CODE = "tr-TR";
     
    public static string LongestVowelSubsequenceAsJson(List<string> words)
    {

        if (words == null) throw new ArgumentNullException(nameof(words));

        if (words.Count == 0) return JsonSerializer.Serialize(Array.Empty<VowelRecord>());

        var tr = CultureInfo.GetCultureInfo(TURKISH_CULTURE_INFO_CODE);
        List<VowelRecord> result = new List<VowelRecord>();

        foreach (var word in words)
        {
            if (word == null) throw new ArgumentNullException("List cannot contain null values!!");

            char[] wordToChar = word.ToCharArray();

            BestSequenceData bestSequenceData = new BestSequenceData();
            SequenceTracker sequenceTracker = new SequenceTracker();

            for (int i = 0; i < wordToChar.Length; i++)
            {
                if (IsVowel(char.ToLower(wordToChar[i], tr)))
                {
                    if (!sequenceTracker.IsSequenceStarted)
                    {
                        sequenceTracker.IsSequenceStarted = true;
                        sequenceTracker.CurrentSequenceStartIndex = i;
                    }
                    sequenceTracker.CurrentSequenceCount++;
                }
                else
                {
                    if (sequenceTracker.IsSequenceStarted)
                    {
                        sequenceTracker.IsSequenceStarted = false;
                        bestSequenceData.EvalAndUpdateBestSequence(
                                            sequenceTracker.CurrentSequenceCount,
                                            sequenceTracker.CurrentSequenceStartIndex,
                                            i - 1
                                        );
                        sequenceTracker.CurrentSequenceCount = 0;
                    }
                }
            }
            if (sequenceTracker.IsSequenceStarted)
            {
                bestSequenceData.EvalAndUpdateBestSequence(
                                        sequenceTracker.CurrentSequenceCount,
                                        sequenceTracker.CurrentSequenceStartIndex,
                                        word.Length - 1
                                );
            }
            result.Add(new VowelRecord(
                                word,
                                GetSequenceOfVowelCharacters(word, bestSequenceData.BestSequenceStartIndex, bestSequenceData.BestSequenceEndIndex),
                                bestSequenceData.MaxSequenceCount)
                        );
        }


        return JsonSerializer.Serialize(result, new JsonSerializerOptions {Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)});
    }

    private static bool IsVowel(char character) => TURKISH_VOWEL.Contains(character);
    private static string GetSequenceOfVowelCharacters(string word, int startIndex, int endIndex)
    {
        return startIndex == -1 ? "" : word.Substring(startIndex, endIndex - startIndex + 1);
    }

    private record VowelRecord(string word, string sequence, int length);
    private class SequenceTracker
    {
        public int CurrentSequenceCount { get; set; } = 0;
        public int CurrentSequenceStartIndex { get; set; } = -1;
        public bool IsSequenceStarted { get; set; } = false;
    }

    private class BestSequenceData
    {
        public int MaxSequenceCount { get; set; } = 0;
        public int BestSequenceStartIndex { get; set; } = -1;
        public int BestSequenceEndIndex { get; set; } = -1;

        public void EvalAndUpdateBestSequence(int count, int startIndex, int endIndex)
        {
            if (count > MaxSequenceCount)
            {
                MaxSequenceCount = count;
                BestSequenceStartIndex = startIndex;
                BestSequenceEndIndex = endIndex;
            }
        }
    }
}