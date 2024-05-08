using Autofac;

namespace Adapter_in_Dependency_Injection
{

    public interface ICommand
    {
        void Execute();
    }

    public class SaveCommand : ICommand
    {
        public void Execute()
        {
            Console.WriteLine("Saving a file");
        }
    }

    public class OpenCommand : ICommand
    {
        public void Execute()
        {
            Console.WriteLine("opening a file");
        }
    }

    public class Button
    {
        private ICommand command;

        public Button(ICommand command)
        {
            this.command = command;
        }

        public void Click()
        {
            command.Execute();
        }
    }

    public class Editor
    {
        private IEnumerable<Button> buttons;

        public Editor(IEnumerable<Button> buttons)
        {
            this.buttons = buttons;
        }
        public void ClickAll()
        {
            foreach (var button in buttons)
            {
                button.Click();
            }
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            var cb = new ContainerBuilder();
            cb.RegisterType<OpenCommand>().As<ICommand>();
            cb.RegisterType<SaveCommand>().As<ICommand>();
            cb.RegisterType<Button>();
            cb.RegisterType<Editor>();

            using(var c = cb.Build())
            {
                var editor = c.Resolve<Editor>();
                editor.ClickAll();
            }

        }
    }
}
