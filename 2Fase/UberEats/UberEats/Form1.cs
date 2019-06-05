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
using System.Data.SqlClient;

namespace UberEats
{
    public partial class Form1 : Form
    {
        private SqlConnection cn;
        ArrayList panels = new ArrayList();
        

        public Form1()
        {

            InitializeComponent();
            CenterToScreen();
            MaximizeBox = false;
            FormBorderStyle = FormBorderStyle.FixedSingle;  // lock resize
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cn = getSGBDConnection();
            loadTabelaEncomendas();

            this.panels.Add(this.painel_encomendas);
            this.panels.Add(this.painel_addencomenda);
            this.panels.Add(this.painel_editencomenda);
            this.panels.Add(this.painel_motoristas);
            this.panels.Add(this.painel_addmotorista);
            this.panels.Add(this.painel_edit_motorista);
            this.panels.Add(this.painel_clientes);
            this.panels.Add(this.painel_add_cliente);
            this.panels.Add(this.painel_edit_cliente);

            this.b_encomendas_lateral.Text = "Encomendas: " + num_encomendas();
            this.label4.Text = "Ativas: " + num_encomendas_ativas();
            this.label5.Text = "Terminadas: " + num_encomendas_terminadas();
            this.label6.Text = "Canceladas: " + num_encomendas_canceladas();
        }

        // Auxiliar function to manage panels's visibility
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

        // Function to load table Encomenda
        private void loadTabelaEncomendas()
        {
            if (!verifySGBDConnection())
                return;

            SqlCommand cmd = new SqlCommand("select * from ServEntr.ListEncomendas();", cn);
            SqlDataAdapter data = new SqlDataAdapter(cmd);
            DataTable table_encomendas = new DataTable();
            data.Fill(table_encomendas);
            this.tabela_encomendas.DataSource = table_encomendas;
        }

        // Function to load table Motorista
        private void loadTabelaMotoristas()
        {
            if (!verifySGBDConnection())
                return;

            SqlCommand cmd = new SqlCommand("select * from ServEntr.ListMotoristas();", cn);
            SqlDataAdapter data = new SqlDataAdapter(cmd);
            DataTable table_motoristas = new DataTable();
            data.Fill(table_motoristas);
            this.tabela_motoristas.DataSource = table_motoristas;
        }

        // Function to load table Cliente
        private void loadTabelaClientes()
        {
            if (!verifySGBDConnection())
                return;

            SqlCommand cmd = new SqlCommand("select * from ServEntr.ListClientes();", cn);
            SqlDataAdapter data = new SqlDataAdapter(cmd);
            DataTable table_clientes = new DataTable();
            data.Fill(table_clientes);
            this.tabela_clientes.DataSource = table_clientes;
        }

        private void exit_button_Click(object sender, EventArgs e)
        {
            cn.Close();
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
            loadTabelaMotoristas();
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
            loadTabelaClientes();
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

        public SqlConnection getSGBDConnection()
        {
            return new SqlConnection("data source=tcp:mednat.ieeta.pt\\SQLSERVER,8101; initial catalog=p3g1; uid=p3g1; password=@0det3nosso");
        }

        public bool verifySGBDConnection()
        {
            if (cn == null)
                cn = getSGBDConnection();

            if (cn.State != ConnectionState.Open)
                cn.Open();

            return cn.State == ConnectionState.Open;
        }

        public int num_encomendas()
        {
            if (!verifySGBDConnection())
                return 0;

            SqlCommand cmd = new SqlCommand("select ServEntr.Num_Encomendas()", cn);
            return (int)cmd.ExecuteScalar();
        }

        public int num_encomendas_ativas()
        {
            if (!verifySGBDConnection())
                return 0;

            SqlCommand cmd = new SqlCommand("select ServEntr.Num_Encomendas_Ativas()", cn);
            return (int)cmd.ExecuteScalar();
        }

        public int num_encomendas_terminadas()
        {
            if (!verifySGBDConnection())
                return 0;

            SqlCommand cmd = new SqlCommand("select ServEntr.Num_Encomendas_Terminadas()", cn);
            return (int)cmd.ExecuteScalar();
        }

        public int num_encomendas_canceladas()
        {
            if (!verifySGBDConnection())
                return 0;

            SqlCommand cmd = new SqlCommand("select ServEntr.Num_Encomendas_Canceladas()", cn);
            return (int)cmd.ExecuteScalar();
        }
    }
}
