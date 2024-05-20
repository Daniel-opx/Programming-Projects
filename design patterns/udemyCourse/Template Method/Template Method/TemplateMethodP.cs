using static System.Console;


namespace Template_Method
{
    public abstract class Game
    {
        public void Run()
        {
            Start();
            while(!HaveWinner)
                TakeTurn();
            Console.WriteLine($"Player {winningPlayer} is winning");
        }

        protected Game(int numberOfPlayers)
        {
            this.numberOfPlayers = numberOfPlayers; 
        }

        protected int currentPalyer;
        protected readonly int numberOfPlayers;
        protected abstract void Start();
        protected abstract void TakeTurn();
        protected abstract bool HaveWinner { get;}
        protected abstract int winningPlayer {  get; }

    }
    public class Chess : Game
    {
        public Chess() : base(2)
        {
            
        }
        protected override bool HaveWinner => turn == maxTurns;

        protected override int winningPlayer => currentPalyer;

        protected override void Start()
        {
            Console.WriteLine($"statring a game of chess with {numberOfPlayers}");
        }

        protected override void TakeTurn()
        {
            Console.WriteLine($"Turn {turn++} taken by player {currentPalyer}");
            currentPalyer = (currentPalyer +1) % numberOfPlayers;
        }

        private int turn = 1;
        private readonly int maxTurns = 10;
    }

    internal class TemplateMethodP
    {
        static void Main(string[] args)
        {
            var chess = new Chess();
            chess.Run();
        }
    }
}
