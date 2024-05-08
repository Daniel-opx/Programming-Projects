using System.Text;

namespace Section_2___Builder
{
    public class HtmlElement
    {
        public string Name, Text;
        public List<HtmlElement> Elements = new List<HtmlElement>();
        private const int indentSize = 2;

        public HtmlElement() { }
        public HtmlElement(string name, string text)
        {
            if (name == null) throw new ArgumentNullException(paramName: nameof(name));
            if (text == null) throw new ArgumentNullException(paramName: nameof(text));
            Name = name;
            Text = text;
        }
        private string ToStringImpl(int indent)
        {
            var sb = new StringBuilder();
            var i = new string(' ', indentSize * indent);
            sb.AppendLine($"{i}<{Name}>");

            if (!string.IsNullOrWhiteSpace(Text))
            {
                sb.Append(new string(' ', indentSize * (indent + 1)));
                sb.AppendLine(Text);
            }
            foreach (var e in Elements)
            {
                sb.Append(e.ToStringImpl(indent + 1));
            }
            sb.AppendLine($"{i}</{Name}>");
            return sb.ToString();
        }
        public override string ToString()
        {
            return ToStringImpl(0);
        }
    }
    public class HtmlBuilder
    {
        private readonly string rootName;
        HtmlElement root = new HtmlElement();

        public HtmlBuilder(string rootName)
        {
            this.rootName = rootName;
            root.Name = rootName;
        }
        //now istead of this -  public void AddChild(string childName, string childText)
        //what if we return a refence to the htmlbulder adn that way we can chain addChild
        /*old method:
         * public void AddChild(string childName, string childText)
        {
            var e = new HtmlElement(childName, childText);
            root.Elements.Add(e);
        }
         */
        //new method
        public HtmlBuilder AddChild(string childName, string childText)
        {
            var e = new HtmlElement(childName, childText);
            root.Elements.Add(e);
            return this;
        }
        public override string ToString()
        {
            return root.ToString();
        }
        public void Clear()
        {
            root = new HtmlElement { Name = rootName };
        }
    }
    internal class Program
    /*Builder is a creational design pattern that lets you 
     * construct complex objects step by step. The pattern 
     * allows you to produce different types and representations of 
     * an object using the same construction code.*/
    {
        static void Main(string[] args)
        {
            //all the lines above is constructing a complicated object 
            //without builder design pattern
            var hello = "hello";
            var sb = new StringBuilder();
            sb.Append(" <p> ");
            sb.Append(hello);
            sb.Append(" <p> ");
            Console.WriteLine(sb);

            var words = new[] { "Hello", "world" };
            sb.Clear();
            sb.Append("<ul>");
            foreach (var word in words)
            {
                sb.AppendFormat($"<li>{word}<li>");
            }
            sb.Append("<ul>");
            Console.WriteLine(sb);


            //now using the html builder
            var builder = new HtmlBuilder("ul");
            //now istead of this: 
            /*
             * builder.AddChild("li", "hello");
            builder.AddChild("li", "world");
             */
            //we do this:
            builder.AddChild("li", "hello").AddChild("li", "world"); // fluent interface- chianig append methods to return a reference to the htmlbuilder block that is being worked on


            Console.WriteLine(builder);


        }
    }
}

