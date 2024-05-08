namespace SwitchExpression
{
    enum Chest
    {
        Opened, 
        Closed,
        Locked
    }
    enum Action
    {
        Open,
        Close
    }

    internal class SwitvhExpression
    {
        static Chest Manipulate
            (Chest chest, Action action, bool isHaveKey) =>
            (chest, action, isHaveKey) switch
            {
                (Chest.Locked, Action.Open,  true) => Chest.Opened,
                (Chest.Closed, Action.Open,_) => Chest.Opened,
                (Chest.Opened,Action.Close,true) => Chest.Locked,
                (Chest.Opened, Action.Close,false) => Chest.Closed,

                _ => chest // default case in the switch

            };

        private static void PrintChestState(Chest chest)
        {
            Console.WriteLine($"the chest is {chest}");
        }
        
        static void Main(string[] args)
        {
            var chest = Chest.Locked;
            PrintChestState(chest);

            chest = Manipulate(chest, Action.Open, true);
            PrintChestState(chest);

            chest = Manipulate(chest,Action.Close,true);
            PrintChestState(chest);

            chest = Manipulate(chest, Action.Close, false);
            PrintChestState(chest);
        }

        
    }
}
