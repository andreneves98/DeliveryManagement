using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UberEats
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.label1.Font = new Font("Arial", 20, FontStyle.Bold);

            this.label2.Font = new Font("Arial", 20, FontStyle.Bold);
            this.label2.ForeColor = Color.FromArgb(0, 204, 0);

            this.label3.Font = new Font("Arial", 12);
            int x = (panel2.Size.Width - label3.Size.Width) / 2;
            label3.Location = new Point(x, label3.Location.Y);

            this.label4.Font = new Font("Arial", 12);
            int x1 = (panel3.Size.Width - label4.Size.Width) / 2;
            label4.Location = new Point(x1, label4.Location.Y);

            this.label5.Font = new Font("Arial", 12);
            int x2 = (panel4.Size.Width - label5.Size.Width) / 2;
            label5.Location = new Point(x2, label5.Location.Y);

            this.label6.Font = new Font("Arial", 12);
            int x3 = (panel5.Size.Width - label6.Size.Width) / 2;
            label6.Location = new Point(x3, label6.Location.Y);

            this.button1.Font = new Font("Arial", 12);
            this.button2.Font = new Font("Arial", 12);
            this.button3.Font = new Font("Arial", 12);
            this.exit_button.Font = new Font("Arial", 14);
        }

        private void exit_button_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
