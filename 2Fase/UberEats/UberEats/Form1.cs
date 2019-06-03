using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;

namespace UberEats
{
    public partial class Form1 : Form
    {
        ArrayList panels = new ArrayList();
        public void showpanel(Panel p)
        {
            foreach(Panel panel in panels)
            {
                if (panel != p)
                {
                    panel.Visible = false;
                }
            }
            p.Visible = true;
        }

        public Form1()
        {
            InitializeComponent();
            CenterToScreen();
            MaximizeBox = false;
            FormBorderStyle = FormBorderStyle.FixedSingle;  // lock resize
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.panels.Add(this.painel_encomendas);
            this.panels.Add(this.painel_addencomenda);
            this.panels.Add(this.painel_editencomenda);
            this.panels.Add(this.painel_motoristas);
            this.panels.Add(this.painel_addmotorista);
            this.panels.Add(this.painel_edit_motorista);
            this.panels.Add(this.painel_clientes);
            this.panels.Add(this.painel_add_cliente);
            this.panels.Add(this.painel_edit_cliente);

            /* Zona Geral */
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
            this.label20.Font = new Font("Arial", 14, FontStyle.Bold);
            this.label26.Font = new Font("Arial", 14, FontStyle.Bold);
            this.label38.Font = new Font("Arial", 14, FontStyle.Bold);
            this.label39.Font = new Font("Arial", 14, FontStyle.Bold);
            this.label48.Font = new Font("Arial", 14, FontStyle.Bold);
            this.label57.Font = new Font("Arial", 14, FontStyle.Bold);


            this.b_encomendas_lateral.Font = new Font("Arial", 12);
            this.b_motoristas_lateral.Font = new Font("Arial", 12);
            this.b_lateral_clientes.Font = new Font("Arial", 12);
            this.exit_button.Font = new Font("Arial", 14);

            /* Botões das encomendas */
            this.b_add_encomenda.Font = new Font("Arial", 13);
            this.b_edit_encomenda.Font = new Font("Arial", 13);
            this.elim_encomenda.Font = new Font("Arial", 13);
            this.b_cancel_add_encomenda.Font = new Font("Arial", 13);
            this.b_ok_addencomenda.Font = new Font("Arial", 13);
            this.b_cancel_edit_encomenda.Font = new Font("Arial", 13);
            this.b_ok_editencomenda.Font = new Font("Arial", 13);

            /* Botões dos motoristas */
            this.b_add_motorista.Font = new Font("Arial", 13);
            this.b_edit_motorista.Font = new Font("Arial", 13);
            this.b_elim_motorista.Font = new Font("Arial", 13);
            this.b_cancel_add_motorista.Font = new Font("Arial", 13);
            this.b_ok_add_motorista.Font = new Font("Arial", 13);
            this.b_cancel_edit_motorista.Font = new Font("Arial", 13);
            this.b_ok_edit_motorista.Font = new Font("Arial", 13);

            /* Botões dos clientes */
            this.b_add_cliente.Font = new Font("Arial", 13);
            this.b_edit_cliente.Font = new Font("Arial", 13);
            this.b_elim_cliente.Font = new Font("Arial", 13);
            this.b_cancel_add_cliente.Font = new Font("Arial", 13);
            this.b_ok_add_cliente.Font = new Font("Arial", 13);
            this.b_cancel_edit_cliente.Font = new Font("Arial", 13);
            this.b_ok_edit_cliente.Font = new Font("Arial", 13);
        }

        private void exit_button_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void b_cancel_add_encomenda_Click(object sender, EventArgs e)
        {
            showpanel(this.painel_encomendas);
        }

        private void b_add_encomenda_Click(object sender, EventArgs e)
        {
            showpanel(this.painel_addencomenda);
        }

        private void b_edit_encomenda_Click(object sender, EventArgs e)
        {
            showpanel(this.painel_editencomenda);
        }

        private void b_cancel_add_encomenda_Click_1(object sender, EventArgs e)
        {
            showpanel(this.painel_encomendas);
        }

        private void b_cancel_edit_encomenda_Click(object sender, EventArgs e)
        {
            showpanel(this.painel_encomendas);
        }

        private void b_encomendas_lateral_Click(object sender, EventArgs e)
        {
            showpanel(this.painel_encomendas);
        }

        private void b_motoristas_lateral_Click(object sender, EventArgs e)
        {
            showpanel(this.painel_motoristas);
        }

        private void b_add_motorista_Click(object sender, EventArgs e)
        {
            showpanel(this.painel_addmotorista);
        }

        private void b_cancel_add_motorista_Click(object sender, EventArgs e)
        {
            showpanel(this.painel_motoristas);
        }

        private void b_edit_motorista_Click(object sender, EventArgs e)
        {
            showpanel(this.painel_edit_motorista);
        }

        private void b_cancel_edit_motorista_Click(object sender, EventArgs e)
        {
            showpanel(this.painel_motoristas);
        }

        private void b_lateral_clientes_Click(object sender, EventArgs e)
        {
            showpanel(this.painel_clientes);
        }

        private void b_add_cliente_Click(object sender, EventArgs e)
        {
            showpanel(this.painel_add_cliente);
        }

        private void b_edit_cliente_Click(object sender, EventArgs e)
        {
            showpanel(this.painel_edit_cliente);
        }

        private void b_cancel_add_cliente_Click(object sender, EventArgs e)
        {
            showpanel(this.painel_clientes);
        }

        private void b_cancel_edit_cliente_Click(object sender, EventArgs e)
        {
            showpanel(this.painel_clientes);
        }
    }
}
