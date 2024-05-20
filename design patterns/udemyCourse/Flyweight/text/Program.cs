using System.ComponentModel.DataAnnotations;
using System.Text;

namespace text
{
    public class Sentence
    {
        private List<WordToken> tokens = new List<WordToken>();
        public Sentence(string plainText)
        {
            foreach (string w in plainText.Split(' '))
            {
                tokens.Add(new WordToken(w));
            }
            
        }

        public WordToken this[int index]
        {
            get
            {
                if(index > tokens.Count - 1) throw new IndexOutOfRangeException();
                return tokens[index];
            }
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            foreach(WordToken w in tokens)
            {
                sb.Append(w.Capitalize ? w.word.ToUpperInvariant() :  w.word);
                sb.Append(" ");
            }
            return sb.ToString();
        }

        public class WordToken
        {
            internal string word;
            public bool Capitalize;
            public WordToken(string s)
            {
                this.word = s;
            }
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            var sentence = new Sentence("hello world ss ss");
            sentence[2].Capitalize = true;
            Console.WriteLine(sentence); // writes "hello WORLD"

        }
    }
}
