﻿using System.Security.Cryptography.X509Certificates;

namespace Composite_Proxy
{
    class Creature
    {
        public byte Age;
        public int X, Y;
    }
    public class Creatures
    {
        private readonly int size;
        private byte[] age;
        private int[] x, y;

        public Creatures(int size)
        {
            this.size = size;
            age  = new byte[size];
            x = new int[size];
            y = new int[size];
        }

        public struct CreatureProxy
        {
            private readonly Creatures creatures;
            private readonly int index;
            public CreatureProxy(Creatures creatures, int index)
            {
                this.creatures = creatures;
                this.index = index;
            }

            public ref byte Age => ref creatures.age[index];
            public ref int X => ref creatures.x[index];
            public ref int Y => ref creatures.y[index];
        }
        public IEnumerator<CreatureProxy> GetEnumerator()
        {
            for (int pos = 0; pos < size; pos++)
            {
                yield return new CreatureProxy(this, pos);
            }

        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            // Array Of Structures - AOS
            var creatures = new Creature[100];
            foreach (var c in creatures)
            {
                c.X++;
            }

            //Structure OF Arrys
            var creatures2 = new Creatures(100);
            foreach (var c in creatures2)
            {
                c.X++;
            }

        }
    }
}
