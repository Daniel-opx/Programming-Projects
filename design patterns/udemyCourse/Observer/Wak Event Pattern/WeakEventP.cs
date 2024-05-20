
namespace Wak_Event_Pattern
{
    public class Button
    {
        public event EventHandler Clicked;
        public void Fire() => Clicked?.Invoke(this, EventArgs.Empty);
    }
    public class Window
    {
        public Window(Button button)
        {
            button.Clicked += ButtonOnClicked;
            
        }

        private void ButtonOnClicked(object? sender, EventArgs e)
        {
            Console.WriteLine("Button Clicked (window Handler)");
        }

        ~Window() { Console.WriteLine("Window Finalized"); }

    }
    internal class WeakEventP
    {
        static void Main(string[] args)
        {
            var button  =  new Button();
            var window = new Window(button);
            button.Fire();

            Console.WriteLine("setting window to null");
            window = null;

        }
    }
}
