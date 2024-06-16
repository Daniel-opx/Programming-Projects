using System.Text;

namespace ConditionTree
{


    /*
     * Visitor pattern -  double dispatch 
     * https://refactoring.guru/design-patterns/visitor-double-dispatch
     */

    
    public abstract class WherePredicate
    {
        public abstract void Accept(IPredicateVisitor visitor);
    }

    public class SinglePerdicate : WherePredicate
    {
        public readonly string columnName;
        public readonly string @operator;
        public readonly string value;

        public SinglePerdicate(string columnName, string @operator, string value)
        {
            this.columnName = columnName ?? throw new ArgumentNullException(nameof(columnName));
            this.@operator = @operator ?? throw new ArgumentNullException(nameof(value));
            this.value = value ?? throw new ArgumentNullException(nameof(value));
        }

        public override void Accept(IPredicateVisitor visitor) => visitor.Visit(this);

    }

    public class AndPredicate : WherePredicate
    {
        public WherePredicate Left;
        public WherePredicate Right;

        public AndPredicate(WherePredicate left, WherePredicate right)
        {
            Left = left;
            Right = right;
        }

        public override void Accept(IPredicateVisitor visitor) => visitor.Visit(this);

    }

    public interface IPredicateVisitor
    {
        void Visit(SinglePerdicate sp);
        void Visit(AndPredicate ap);
    }

    public class PredicatePrinter : IPredicateVisitor
    {

        StringBuilder sb = new StringBuilder();

        public PredicatePrinter()
        {
        }

        public void Visit(SinglePerdicate sp)
        {
            sb.Append($@"{sp.columnName} {sp.@operator} ""{sp.value}""");
        }

        public void Visit(AndPredicate ab)
        {
            sb.Append('(');
            ab.Left.Accept(this);
            sb.Append(" AND ");
            ab.Right.Accept(this);
            sb.Append(")");

        }
        public override string ToString() => sb.ToString();
    }
}
