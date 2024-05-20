using System.Text;

namespace VisitorExercise
{
    public abstract class ExpressionVisitor
    {
        public abstract void Visit(Value v);
        public abstract void Visit(AdditionExpression ae);
        public abstract void Visit(MultiplicationExpression me);
    }

    public abstract class Expression
    {
        public abstract void Accept(ExpressionVisitor ev);
    }

    public class Value : Expression
    {
        public readonly int TheValue;

        public Value(int value)
        {
            TheValue = value;
        }
        public override void Accept(ExpressionVisitor ev)
        {
            ev.Visit(this);
        }

    }

    public class AdditionExpression : Expression
    {
        public readonly Expression LHS, RHS;

        public AdditionExpression(Expression lhs, Expression rhs)
        {
            LHS = lhs;
            RHS = rhs;
        }

        // todo
        public override void Accept(ExpressionVisitor ev)
        {
            ev.Visit(this);
        }
    }

    public class MultiplicationExpression : Expression
    {
        public readonly Expression LHS, RHS;

        public MultiplicationExpression(Expression lhs, Expression rhs)
        {
            LHS = lhs;
            RHS = rhs;
        }

        // todo
        public override void Accept(ExpressionVisitor ev)
        {
            ev.Visit(this);
        }
    }

    public class ExpressionPrinter : ExpressionVisitor
    {
        private StringBuilder sb = new StringBuilder();

        public override void Visit(Value v)
        {
            sb.Append(v.TheValue);
        }

        public override void Visit(AdditionExpression ae)
        {
            sb.Append("(");
            ae.LHS.Accept(this);
            sb.Append("+");
            ae.RHS.Accept(this);
            sb.Append(")");

        }

        public override void Visit(MultiplicationExpression me)
        {
            // todo :  public readonly Expression LHS, RHS;
            me.LHS.Accept(this);
            sb.Append('*');
            me.RHS.Accept(this);


        }

        private void ClearSb()=> sb.Clear();
        public void Reset()
        {
            this.ClearSb();
        }

        public override string ToString()
        {
            return sb.ToString();
        }
    }
    internal class VisitorTest
    {
        static void Main(string[] args)
        {
            
            var printer = new ExpressionPrinter();
            

            var ae = new AdditionExpression(new Value(10),new Value(21));
            ae.Accept(printer);
            Console.WriteLine(printer);

            Console.WriteLine("\\\\===============================================================\\\\");

            printer.Reset();

            var me = new MultiplicationExpression(new MultiplicationExpression(new Value(5),new Value(10)),new AdditionExpression(new Value(5),new Value(5)));
            me.Accept(printer);
            Console.WriteLine(printer);



        }
    }
}
