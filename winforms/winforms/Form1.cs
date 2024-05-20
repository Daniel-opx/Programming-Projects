using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace winforms
{
    public partial class Form1 : Form
    {
        int[,] doubleDimensionMatrix {  get; set; }
        public Form1()
        {
            
            InitializeComponent();
        }

        private void Populate2dMAtrix()
        {
            doubleDimensionMatrix = new[,] { { 1, 2, 3,},
           {4,5,6 },
           {7,8,9 }};
            
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Populate2dMAtrix();
            dataGridView1.Show();
            DataTable dt = new DataTable();

            for (int i = 0; i < doubleDimensionMatrix.Length; i++)
            {
                dt.Columns.Add(i.ToString());
                dt.Rows.Add(i.ToString());
            }
            DataRow dr;
            for(int i = 0; i < doubleDimensionMatrix.GetLength(0); i++)
            {
                dr = dt.NewRow();
                for(int j = 0; j < doubleDimensionMatrix.GetLength(0); j++)
                {
                    dr[j] = doubleDimensionMatrix[j,i];
                }
                dt.Rows.Add(dr);
            }
            
            dataGridView1.DataSource = dt;
        }
    }
}
