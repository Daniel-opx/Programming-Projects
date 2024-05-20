using System;
using System.Collections;
using System.Text;

namespace Coding.Exercise
{
    public class Code
    {
        private readonly string codeBlock;

        public Code(string codeBlock)
        {
            this.codeBlock = codeBlock;
        }

        public override string ToString()
        {
            return codeBlock;
        }
    }


    public class CodeBuilder
    {


        private readonly StringBuilder sb = new StringBuilder();

        public CodeBuilder(string name)
        {
            sb.AppendFormat("public class {0}", name)
                .AppendLine()
                .AppendLine("{");
        }

        public CodeBuilder AddField(string fieldName, string fieldDataType)
        {
            sb.AppendFormat("  public {0} {1};", fieldDataType, fieldName).AppendLine();

            return this;
        }

        public Code Build()
        {
            return new Code(sb.ToString() + '}');

        }

        public static void Main()
        {
            var bla = new CodeBuilder("foofoo")
                .AddField("wow!", "int")
                .AddField("superWow!", "string")
                .Build();
            Console.WriteLine(bla);
        }
    }


}
