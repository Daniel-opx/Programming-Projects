using System.Text;

namespace Static_Strategy
{
    public enum OutputFormat
    {
        Markdown,
        Html
    }

    public interface IListStartegy
    {
        void start(StringBuilder sb);
        void End(StringBuilder sb);
        void AddListItem(StringBuilder sb, string item);
    }
    public class HtmlStrstegy : IListStartegy
    {
        public void AddListItem(StringBuilder sb, string item)
        {
            sb.AppendLine($" <il>{item}</il>");
        }

        public void End(StringBuilder sb)
        {
            sb.AppendLine("</ul>");
        }

        public void start(StringBuilder sb)
        {
            sb.AppendLine("<ul>");
        }
    }
    public class MarkdownListStrategy : IListStartegy
    {
        public void AddListItem(StringBuilder sb, string item)
        {
            sb.AppendLine($" * {item}");
        }

        public void End(StringBuilder sb)
        {

        }

        public void start(StringBuilder sb)
        {

        }
    }
    public class TextProccessor<ls>
        where ls : IListStartegy, new()
    {
        private StringBuilder sb = new StringBuilder();
        private IListStartegy strategy = new ls();
        public void SetOutputFormat(OutputFormat format)
        {
            switch (format)
            {
                case OutputFormat.Html:
                    strategy = new HtmlStrstegy();
                    break;
                case OutputFormat.Markdown:
                    strategy = new MarkdownListStrategy();
                    break;
                default:
                    throw new ArgumentException();
            }
        }
        public void AppendList(IEnumerable<string> list)
        {
            strategy.start(sb);
            foreach (string item in list)
            {
                strategy.AddListItem(sb, item);
            }
            strategy.End(sb);
        }
        public override string ToString()
        {
            return sb.ToString();
        }
        public StringBuilder Clear()
        {
            return sb.Clear();
        }
    }
    internal class StaticStrategy
    {
        static void Main(string[] args)
        {
            var tp = new TextProccessor<HtmlStrstegy>();
            tp.AppendList(new[] { "foo", "bar", "lala" });
            Console.WriteLine(tp);
        }
    }
}
