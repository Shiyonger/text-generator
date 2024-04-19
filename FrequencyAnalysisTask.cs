namespace TextAnalysis;

static class FrequencyAnalysisTask
{
    public static Dictionary<string, string> GetMostFrequentNextWords(List<List<string>> text)
    {   
        var result = new Dictionary<string, string>();
        var frequency = new Dictionary<string, int>();

        for (int i = 0; i < text.Count; i++)
        {
            for (int j = 0; j < text[i].Count - 1; j++)
            {
                string hram = text[i][j];
                string dhram = hram + " " + text[i][j + 1];
                CheckHram(result, frequency, hram, text[i][j+1]);
                if (j + 2 < text[i].Count) CheckHram(result, frequency, dhram, text[i][j+2]);
            }
        }
        
        return result;
    }

    private static void CheckHram(Dictionary<string, string> result, Dictionary<string, int> frequency, string start,
        string end)
    {
        string dhram = start + " " + end;
        
        if (!frequency.ContainsKey(dhram)) frequency[dhram] = 0;
        frequency[dhram]++;

        if (!result.ContainsKey(start)) result[start] = end;
        else
        {
            string compare = CompareEnds(result, frequency, start, end);
            result[start] = compare;
        }
    }

    private static string CompareEnds(Dictionary<string, string> result, Dictionary<string, int> frequency, string start,
        string end)
    {
        int frequencyCompare = frequency[start + " " + end] - frequency[start + " " + result[start]];
        int stringCompare = string.CompareOrdinal(end, result[start]);
        
        if (frequencyCompare > 0) return end;
        if (frequencyCompare < 0) return result[start];
        
        if (stringCompare < 0) return end;
        if (stringCompare > 0) return result[start];

        return end;
    }
}