﻿using static System.Console;
namespace Chain_Of_Responsibility
    
{
    public class Creature
    {
        public string Name;
        public int Attack, Defense;

        public Creature(string name, int attack, int defense)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Attack = attack;
            Defense = defense;
        }
        public override string ToString()
        {
            return $"{nameof(Name)}: {Name}, {nameof(Attack)}: {Attack}, {nameof(Defense)}: {Defense}";
        }
    }

    public class CreatureModifier
    {
        protected Creature creature;
        protected CreatureModifier next; // Linked List

        public CreatureModifier(Creature creature)
        {
            this.creature = creature ?? throw new ArgumentNullException(paramName: nameof(creature));
        }

        public void Add(CreatureModifier cm)
        {
            if (next != null) next.Add(cm); // if the pointer to the next link in the list is nt null we jump to the next pointer
                                            //adding the item to the end of the list
            else next = cm;
        }

        public virtual void Handle() => next?.Handle();
    }

    public class NoBonusesModifier : CreatureModifier
    {
        public NoBonusesModifier(Creature creature) : base(creature)
        {
        }

        public override void Handle()
        {
            // nothing
            WriteLine("No bonuses for you!");
        }
    }



    public class DoubleAttackModifier : CreatureModifier
    {
        public DoubleAttackModifier(Creature creature) : base(creature)
        {

        }
        public override void Handle()
        {
            Console.WriteLine($"doubling {creature.Name}'s attack!");
            creature.Attack *= 2;
            base.Handle();
        }
    }

    public class IncreaseDefenseModifier : CreatureModifier
    {
        public IncreaseDefenseModifier(Creature creature) : base(creature)
        {
        }

        public override void Handle()
        {
            WriteLine("Increasing goblin's defense");
            creature.Defense += 3;
            base.Handle();
        }
    }



    internal class MethodChainP
    {
     


        static void Main(string[] args)
        {
            var goblin = new Creature("Goblin", 2, 2);
            WriteLine(goblin);

            var root = new CreatureModifier(goblin);

            root.Add(new NoBonusesModifier(goblin));

            WriteLine("Let's double goblin's attack...");
            root.Add(new DoubleAttackModifier(goblin));

            WriteLine("Let's increase goblin's defense");
            root.Add(new IncreaseDefenseModifier(goblin));

            // eventually...
            root.Handle();
            WriteLine(goblin);

        }
    }
}
