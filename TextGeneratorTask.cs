using System.Text;

namespace TextAnalysis;

static class TextGeneratorTask
{
    public static string ContinuePhrase(
        Dictionary<string, string> nextWords,
        string phraseBeginning,
        int wordsCount)
    {
        var phrase = new StringBuilder();
        phrase.Append(phraseBeginning);
        
        char[] wordsSeparators = {'.', '!', '?', ';', ':', '(', ')', ',', '\t', '\n', '\r', ' '};
        string[] t = phraseBeginning.Split(wordsSeparators, StringSplitOptions.RemoveEmptyEntries);
        string w1 = string.Empty;
        string w2 = string.Empty;
        
        if (t.Length > 0) w2 = t[t.Length - 1];
        if (t.Length > 1) w1 = t[t.Length - 2];
        
        for (int i = 0; i < wordsCount; i++)
        {
            string keyFirst = w1 + " " + w2;
            string keySecond = w2;
            string key = "";
            
            if (nextWords.ContainsKey(keyFirst)) key = keyFirst;
            else if (nextWords.ContainsKey(keySecond)) key = keySecond;
            else break;

            phrase.Append(" ");
            phrase.Append(nextWords[key]);
            w1 = w2;
            w2 = nextWords[key];
        }
        
        return phrase.ToString();
    }
}