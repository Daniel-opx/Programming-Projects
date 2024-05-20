using GameLogic;

namespace form
{
    public partial class Form1 : Form
    {
        Board Game = new Board();

        public Form1()
        {
            InitializeComponent();

            foreach (Button item in board.Controls)
            {
                item.Text = "";
            }
            Form1_Load();
            

        }

        private void Form1_Load()
        {
            foreach (Button item in board.Controls)
                item.Click += Button_clicked;
        }
        private void Button_clicked(object sender, EventArgs e)
        {
            Button button = (Button)sender;

            var num = int.Parse(button.Tag.ToString());

            var c = Coordinate.GetCoordinateFromTag(button.Tag);
            if (Game.SetCell(c.x, c.y))
            {
                button.Text = Game.GetValueAt(c);
                button.Enabled = false;
                if (Game.isWinner())
                {
                    textBox1.Text = Game.Player1Turn ? "Player2 won" : "plater1 won";
                    textBox1.Visible = true;
                    MessageBox.Show("My message here");
                    DisableAllButtons();
                }
               

            }


        }


        private void DisableAllButtons()
        {
            foreach (Button item in board.Controls)
                item.Enabled = false;
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
