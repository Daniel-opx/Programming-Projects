using System.ComponentModel.DataAnnotations;

namespace State
{
    public class Switch
    {
        public State state = new OffState();
        public void On() => state.On(this);
        public void Off() => state.Off(this);

    }
    public abstract class State
    {
        public virtual void On(Switch sw)
        {
            Console.WriteLine("light is already on");
        }
        public virtual void Off(Switch sw)
        {
            Console.WriteLine("Light is already off");
        }
    }
    public class OnState : State
    {
        public OnState()
        {
            Console.WriteLine("light turned on -ctor");
        }
        public override void Off(Switch sw)
        {
            Console.WriteLine("turning the light off...");
            sw.state = new OffState();
        }
    }
    public class OffState : State 
    {
        public OffState()
        {
            Console.WriteLine("light turned of -ctor.");
        }
        public override void On(Switch sw)
        {
            Console.WriteLine("turning light on....");
            sw.state = new OnState();
        }
    }

     
    internal class ClassicImpl
    {
        static void Main(string[] args)
        {
            var ls = new Switch();
            ls.On();
            ls.Off();
            ls.Off();
        }
    }
}
