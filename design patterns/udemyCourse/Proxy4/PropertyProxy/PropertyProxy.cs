namespace PropertyProxy
{
    //the property proxy notion is using an object as a property instead of a literal value.

    public class Property<T>
        where T : new() //  generics cpnstraint  - T  has to have empty constructor
    {
        private T value;

        public Property() : this(default(T))
        {
            
        }

        public Property(T value)
        {
            this.value = value;
        }

        public T Value
        {
            get => value;
            set
            {
                if (Equals(value, this.value)) return;
                Console.WriteLine("Assigning value to {0}",value);
                this.value = value;
            }
        }
        public static implicit operator T(Property<T> property)
        {
            return property.value; // int n = Property<int> p_int
        }

        public static implicit operator Property<T>(T value)
        {
            return new Property<T>(value); // Property<int> p = 123;
        }
        public bool Equals(Property<T> other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return EqualityComparer<T>.Default.Equals(value, other.value);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Property<T>)obj);
        }

        public override int GetHashCode()
        {
            return value.GetHashCode();
        }

        public static bool operator ==(Property<T> left, Property<T> right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Property<T> left, Property<T> right)
        {
            return !Equals(left, right);
        }

    }

    class Creature
    {
        private Property<int> agility = new Property<int>();
        public int Agility { get => agility.Value; set => agility.Value = value; }
    }
    internal class PropertyProxy
    {
        static void Main(string[] args)
        {
            var c = new Creature();
            c.Agility = 10;
            c.Agility = 10;
        }
    }
}
