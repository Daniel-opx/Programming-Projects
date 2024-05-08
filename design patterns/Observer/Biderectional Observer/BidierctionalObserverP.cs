using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Biderectional_Observer
{
    public class Product : INotifyPropertyChanged
    {
        private string name;

        public string Name
        {
            get => name;
            set
            {
                if (value == name) return; // critical
                name = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        
        protected virtual void OnPropertyChanged(
          [CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this,
              new PropertyChangedEventArgs(propertyName));
        }

        public override string ToString()
        {
            return $"Product: {Name}";
        }

    }
    public class Window : INotifyPropertyChanged
    {
        private string productName;

        public string ProductName
        {
            get => productName;
            set
            {
                if (value == productName) return; // critical- prevent infinuty recursion back and forth from the product object setter to the window object setter
                productName = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public Window()
        {
            
        }
        public Window(Product product)
        {
            ProductName = product.Name;
        }

       
        protected virtual void OnPropertyChanged(
          [CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this,
              new PropertyChangedEventArgs(propertyName));
        }

        public override string ToString()
        {
            return $"Window: {ProductName}";
        }

    }

    public sealed class BidirectionalBinding : IDisposable
    {
        private bool disposed;
        //first , second
        //firstProp, secondProp

        public BidirectionalBinding(
            INotifyPropertyChanged first,
            Expression<Func<object>> firstProperty,
            INotifyPropertyChanged second,
             Expression<Func<object>> secondProperty
            )
        {
            if (firstProperty.Body is MemberExpression firstExpr
          && secondProperty.Body is MemberExpression secondExpr)
            {
                if (firstExpr.Member is PropertyInfo firstProp
                    && secondExpr.Member is PropertyInfo secondProp)
                {
                    first.PropertyChanged += (sender, args) =>
                    {
                        if (!disposed)
                        {
                            secondProp.SetValue(second, firstProp.GetValue(first));
                        }
                    };
                    second.PropertyChanged += (sender, args) =>
                    {
                        if (!disposed)
                        {
                            firstProp.SetValue(first, secondProp.GetValue(second));
                        }
                    };
                }
            }

        }

        public void Dispose()
        {
            disposed = true;
        }
    }
    internal class BidierctionalObserverP
    {
        static void Main(string[] args)
        {
            var product = new Product() { Name = "Book"};
            var window = new Window() { ProductName = "Book" };

            product.PropertyChanged += (sender, eventArgs) =>
            {
                if(eventArgs.PropertyName == "Name")
                {
                    Console.WriteLine("Name changed in product");
                    window.ProductName = product.Name;
                }
            };

            window.PropertyChanged += (sender, eventArgs) =>
            {
                if (eventArgs.PropertyName == "ProductName")
                {
                    Console.WriteLine("Name is cahnged is window");
                    product.Name = window.ProductName;
                }
            };

            product.Name = "Smart Book";
            Console.WriteLine($"{product}\n " +
                $"{window}");


            //second approach to this bidirectional binding
            Console.WriteLine("\n\nnow using second approach with {0} class\n" +
                "============================================================",nameof(BidirectionalBinding) );
            var product2 = new Product() { Name = "Book" };
            var window2 = new Window() { ProductName = "Book" };

            using var binding = new BidirectionalBinding(
        product2,
        () => product.Name,
        window2,
        () => window.ProductName);

            product2.Name = "Smart Book";
            window2.ProductName = "Realty smart book";
            Console.WriteLine($"{product2}\n{window2}");

        }
    }
}
