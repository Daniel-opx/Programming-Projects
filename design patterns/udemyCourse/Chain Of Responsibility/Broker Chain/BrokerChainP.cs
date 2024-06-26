﻿using System.Xml.Linq;

namespace Broker_Chain
{
    public class Game
    {
        public event EventHandler<Query> Queries;

        public void PreformQuery(object sender, Query e)
        {
            Queries?.Invoke(sender, e);
        }

    }
    public class Query
    {
        public string CreatureName;
        public enum Argument
        {
            Attack, Defense
        }
        public Argument WhatToQuery;
        public int Value;

        public Query(string creatureName, Argument whatToQuery, int value)
        {
            this.CreatureName = creatureName ?? throw new ArgumentNullException(nameof(creatureName));
            this.WhatToQuery = whatToQuery;
            this.Value = value;
        }

    }
    public class Creature
    {
        private Game game;
        public string Name;
        private int attack, defense;

        public Creature(Game game, string name, int attack, int defense)
        {
            this.game = game ?? throw new ArgumentNullException(nameof(game));
            this.Name = name ?? throw new ArgumentNullException(nameof(name));
            this.attack = attack;
            this.defense = defense;
        }

        public int Attack
        {
            get
            {
                var q = new Query(Name, Query.Argument.Attack, attack);
                game.PreformQuery(this, q);
                return q.Value;
            }
        }
        public int Defense
        {
            get
            {
                var q = new Query(Name, Query.Argument.Defense, defense);
                game.PreformQuery(this, q);
                return q.Value;
            }
        }
        public override string ToString() // no game
        {
            return $"{nameof(Name)}: {Name}, {nameof(attack)}: {Attack}, {nameof(defense)}: {Defense}";
            //                                                 ^^^^^^^^ using a property    ^^^^^^^^^
        }

    }

   
   
    public abstract class CreatureModifier : IDisposable
    {
        protected Game game;
        protected Creature creature;

        protected CreatureModifier(Game game, Creature creature)
        {
            this.game = game;
            this.creature = creature;
            game.Queries += Handle;
        }

        protected abstract void Handle(object sender, Query q);

        public void Dispose()
        {
            game.Queries -= Handle;
        }
    }

    public class DoubleAttackModifier : CreatureModifier
    {
        public DoubleAttackModifier(Game game, Creature creature) : base(game, creature)
        {
        }

        protected override void Handle(object sender, Query q)
        {
            if (q.CreatureName == creature.Name &&
                q.WhatToQuery == Query.Argument.Attack)
                q.Value *= 2;
        }
    }
    public class IncreaseDefenseModifier : CreatureModifier
    {
        public IncreaseDefenseModifier(Game game, Creature creature) : base(game, creature)
        {
        }

        protected override void Handle(object sender, Query q)
        {
            if (q.CreatureName == creature.Name &&
                q.WhatToQuery == Query.Argument.Defense)
                q.Value += 2;
        }
    }



    internal class BrokerChainP
    {
        

        public static void Main()
        {
           var game = new Game();
            var goblin = new Creature(game, "Goblin", 2, 2);
            Console.WriteLine(goblin);

            using (new DoubleAttackModifier(game, goblin))
            {
                Console.WriteLine(goblin);
                using (new IncreaseDefenseModifier(game, goblin))
                {
                    Console.WriteLine(goblin);
                }
            }
            Console.WriteLine(goblin);

        }



    }

    
}
