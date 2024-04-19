using System.Diagnostics.Tracing;
using System.Text;

namespace TextAnalysis;

static class SentencesParserTask
{
    public static List<List<string>> ParseSentences(string text)
    {
        var sentencesList = new List<List<string>>();
        char[] sentencesSeparators = {'.', '!', '?', ';', ':', '(', ')'};
        char[] wordsSeparators = {',', '\t', '\n', '\r', ' '};
        string[] sentences = text.Split(sentencesSeparators, StringSplitOptions.RemoveEmptyEntries);
        foreach (var sen in sentences)
        {
            List<string> words = new List<string>();
            foreach (var word in sen.Split(wordsSeparators, StringSplitOptions.TrimEntries))
                foreach (var el in CheckWord(word))
                    if (el.Length != 0) words.Add(el.ToLower());
            if (words.Count != 0) sentencesList.Add(words);
        }
        
        return sentencesList;
    }

    private static string[] CheckWord(string word)
    {
        var ans = new StringBuilder();
        ans.Append(word);
        for (int i = 0; i < ans.Length; i++)
        {
            if (!char.IsLetter(ans[i]) && ans[i] != '\'')
                ans[i] = ' ';
        }

        return ans.ToString().Split(' ');
    }
}