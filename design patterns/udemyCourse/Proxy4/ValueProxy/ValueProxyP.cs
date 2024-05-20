using System.Reflection.Metadata.Ecma335;

namespace ValueProxy // proxy that is usually constructed over primitive types like int , bool etc...
{
    public struct Precentage
    {
        private readonly float value;

        internal Precentage(float value)
        {
            this.value = value;
        }

        public static implicit operator Precentage(float value)
        {
            return new Precentage(value);
        }

        public static float operator *(float f, Precentage p)
        {
            return f * p.value;
        }

        public static Precentage operator +(Precentage a, Precentage b)
        {
            return new Precentage(a.value + b.value);
        }

        public static implicit operator Precentage(int value)
        {
            return value.Percent();
        }

        public bool Equals(Precentage other)
        {
            return value.Equals(other.value);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Precentage other && Equals(other);
        }

        public override int GetHashCode()
        {
            return value.GetHashCode();
        }

        public override string ToString()
        {
            return $"{value * 100}%";
        }
    }

    public static class PrecentageExtensions
    {
        public static Precentage Percent(this float value)
        {
            return new Precentage(value / 100.0f);
        }
        public static Precentage Percent(this int value)
        {
            return new Precentage(value / 100.0f);
        }
    }
    internal class ValueProxyP
    {
       
        static void Main(string[] args)
        {
            Console.WriteLine(
                10f * 500.Percent());
        }
    }
}
