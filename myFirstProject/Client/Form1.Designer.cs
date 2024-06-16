namespace Client
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DataGrid = new DataGridView();
            GetAllPeople = new Button();
            richTextBox1 = new RichTextBox();
            textBox1 = new TextBox();
            lastNameFilter = new TextBox();
            richTextBox2 = new RichTextBox();
            ((System.ComponentModel.ISupportInitialize)DataGrid).BeginInit();
            SuspendLayout();
            // 
            // DataGrid
            // 
            DataGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DataGrid.Location = new Point(25, 55);
            DataGrid.Margin = new Padding(3, 4, 3, 4);
            DataGrid.Name = "DataGrid";
            DataGrid.RowHeadersWidth = 51;
            DataGrid.Size = new Size(416, 523);
            DataGrid.TabIndex = 0;
            // 
            // GetAllPeople
            // 
            GetAllPeople.Location = new Point(25, 13);
            GetAllPeople.Margin = new Padding(3, 4, 3, 4);
            GetAllPeople.Name = "GetAllPeople";
            GetAllPeople.Size = new Size(198, 31);
            GetAllPeople.TabIndex = 2;
            GetAllPeople.Text = "see all people";
            GetAllPeople.UseVisualStyleBackColor = true;
            GetAllPeople.Click += GetAllPeople_Click;
            // 
            // richTextBox1
            // 
            richTextBox1.Location = new Point(493, 51);
            richTextBox1.Margin = new Padding(3, 4, 3, 4);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new Size(161, 65);
            richTextBox1.TabIndex = 3;
            richTextBox1.Text = "enetr first name here if you want to filter by it otherwise leave empty";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(505, 17);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(125, 27);
            textBox1.TabIndex = 6;
            // 
            // lastNameFilter
            // 
            lastNameFilter.Location = new Point(664, 17);
            lastNameFilter.Name = "lastNameFilter";
            lastNameFilter.Size = new Size(125, 27);
            lastNameFilter.TabIndex = 7;
            // 
            // richTextBox2
            // 
            richTextBox2.Location = new Point(664, 55);
            richTextBox2.Margin = new Padding(3, 4, 3, 4);
            richTextBox2.Name = "richTextBox2";
            richTextBox2.Size = new Size(161, 65);
            richTextBox2.TabIndex = 8;
            richTextBox2.Text = "enetr last name here if you want to filter by it otherwise leave empty";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(914, 600);
            Controls.Add(richTextBox2);
            Controls.Add(lastNameFilter);
            Controls.Add(textBox1);
            Controls.Add(richTextBox1);
            Controls.Add(GetAllPeople);
            Controls.Add(DataGrid);
            Margin = new Padding(3, 4, 3, 4);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)DataGrid).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView DataGrid;
        private Button GetAllPeople;
        private RichTextBox richTextBox1;
        private TextBox textBox1;
        private TextBox lastNameFilter;
        private RichTextBox richTextBox2;
    }
}
