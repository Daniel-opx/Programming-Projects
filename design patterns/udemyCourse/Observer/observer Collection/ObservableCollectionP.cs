using System.ComponentModel;

namespace observer_Collection
{
    public class Market
    {
        public BindingList<float> Prices = new BindingList<float>();
        public void AddPrice(float price)
        {
            Prices.Add(price);
        }


        public event EventHandler<PriceAddedEventArgs> PriceAdded;



    }
    public class PriceAddedEventArgs
    {
        public float Price;
    }

    internal class ObservableCollectionP
    {
        static void Main(string[] args)
        {
            Market market = new Market();
            market.PriceAdded += (sender, eventArgs) =>
                  {
                      Console.WriteLine($"Added price {eventArgs.Price}");
                  };
            market.Prices.ListChanged += (sender, args) =>
            {
                if (args.ListChangedType == ListChangedType.ItemAdded)
                {
                    float price = ((BindingList<float>)sender)[args.NewIndex];
                    Console.WriteLine($"Binding list got a price of{price}");
                }
            };
            market.Prices.Add(5);


        }
    }
}
