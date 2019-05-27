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
            CenterToScreen();
            MaximizeBox = false;
            FormBorderStyle = FormBorderStyle.FixedSingle;  // lock resize
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

            this.label7.Font = new Font("Arial", 14, FontStyle.Bold);
            this.label8.Font = new Font("Arial", 10);
            this.label9.Font = new Font("Arial", 10);
            this.label10.Font = new Font("Arial", 10);
            this.label11.Font = new Font("Arial", 10);
            this.label12.Font = new Font("Arial", 10);

            this.label13.Font = new Font("Arial", 10);
            this.label14.Font = new Font("Arial", 10);
            this.label15.Font = new Font("Arial", 10);
            this.label16.Font = new Font("Arial", 10);
            this.label17.Font = new Font("Arial", 10);
            this.label18.Font = new Font("Arial", 14, FontStyle.Bold);
            this.label19.Font = new Font("Arial", 14, FontStyle.Bold);

            this.b_encomendas_lateral.Font = new Font("Arial", 12);
            this.button2.Font = new Font("Arial", 12);
            this.button3.Font = new Font("Arial", 12);
            this.exit_button.Font = new Font("Arial", 14);
            this.b_add_encomenda.Font = new Font("Arial", 13);
            this.b_edit_encomenda.Font = new Font("Arial", 13);
            this.elim_encomenda.Font = new Font("Arial", 13);
            this.b_cancel_add_encomenda.Font = new Font("Arial", 13);
            this.b_ok_addencomenda.Font = new Font("Arial", 13);
            this.b_cancel_edit_encomenda.Font = new Font("Arial", 13);
            this.b_ok_editencomenda.Font = new Font("Arial", 13);
        }

        private void exit_button_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void b_cancel_add_encomenda_Click(object sender, EventArgs e)
        {
            this.painel_addencomenda.Visible = false;
            this.painel_encomendas.Visible = true;
        }

        private void b_add_encomenda_Click(object sender, EventArgs e)
        {
            this.painel_editencomenda.Visible = false;
            this.painel_addencomenda.Visible = true;
        }

        private void b_edit_encomenda_Click(object sender, EventArgs e)
        {
            this.painel_addencomenda.Visible = false;
            this.painel_editencomenda.Visible = true;
        }

        private void b_cancel_add_encomenda_Click_1(object sender, EventArgs e)
        {
            this.painel_addencomenda.Visible = false;
            this.painel_encomendas.Visible = true;
        }

        private void b_cancel_edit_encomenda_Click(object sender, EventArgs e)
        {
            this.painel_editencomenda.Visible = false;
            this.painel_encomendas.Visible = true;
        }

        private void b_encomendas_lateral_Click(object sender, EventArgs e)
        {
            this.painel_addencomenda.Visible = false;
            this.painel_editencomenda.Visible = false;
            this.painel_encomendas.Visible = true;
        }
    }
}
