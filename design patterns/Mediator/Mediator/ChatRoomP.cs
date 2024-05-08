using static System.Console;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks.Dataflow;
namespace Mediator
{
    public class Person
    {
        public string Name;
        public ChatRoom Room;
        private List<string> chatLog = new List<string>();

      

        public Person(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }
        public void Say(string msg)
        {
            Room.Broadcast(Name, msg);
        }
        public void PrivateMassage(string who, string msg)
        {
            Room.Message(Name, who, msg);
        }

        public void Receive(string sender, string message)
        {
            string s = $"{sender} : '{message}'";
            chatLog.Add(s);
            WriteLine($"[{Name}]'s chat session] {s}");
        }

        public class ChatRoom
        {
            private readonly List<Person> people = new List<Person>();

            public void Join(Person p)
            {
                string joinMsg = $"{p.Name} joins the chat";
                Broadcast("room", joinMsg);
                p.Room = this;
                people.Add(p);
            }
           

            public void Broadcast(string src, string msg)
            {
                foreach (var p in people)
                {
                    if (p.Name != src) p.Receive(src, msg);
                }
            }

            public void Message(string src, string dst, string msg)
            {
                people.FirstOrDefault(p => p.Name == dst)
                    ?.Receive(src, msg);
            }
        }

        internal class ChatRoomP
        {
            static void Main(string[] args)
            {
                var room = new ChatRoom();

                var John = new Person("John");
                var Jane = new Person("Jane");

                room.Join(John);
                room.Join(Jane);
               

                John.Say("Hi");
                Jane.Say("Hi John");

                var simon = new Person("Simon");
                room.Join(simon);

                simon.Say("Hi guys");

            }
        }
    }
}
