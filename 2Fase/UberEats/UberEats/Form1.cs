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
using System.Text.RegularExpressions;

namespace UberEats
{
    public partial class Form1 : Form
    {
        private SqlConnection cn;
        ArrayList panels = new ArrayList();
        ArrayList produtos_checked = new ArrayList();        

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
            //listaEstabelecimentos();
            listaClientes();
            listaMarcasVeiculos();

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

            //DataGridViewRow row = tabela_encomendas.Rows[0];
            //row.Cells["status"].Value = "ativa";
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
            DataGridViewRow row = tabela_encomendas.Rows[0];
            row.Cells["preco_total"].Value = 12;
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

        // Function to list Estabelecimentos
        private void listaEstabelecimentos()
        {
            if (!verifySGBDConnection())
                return;

            SqlCommand cmd = new SqlCommand("select * from ServEntr.ListEstabelecimentos();", cn);
            SqlDataReader reader = cmd.ExecuteReader();
            while(reader.Read())
            {
                comboBox2.Items.Add(reader["nome"].ToString());
            }

            reader.Close();
        }

        // Function to list Clientes
        private void listaClientes()
        {
            if (!verifySGBDConnection())
                return;

            SqlCommand cmd = new SqlCommand("select nome from ServEntr.ListClientes();", cn);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                comboBox1.Items.Add(reader["nome"].ToString());
            }

            reader.Close();

        }

        // Function to list MarcaVeiculos
        private void listaMarcasVeiculos()
        {
            if (!verifySGBDConnection())
                return;

            SqlCommand cmd = new SqlCommand("select * from ServEntr.ListMarcas();", cn);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                comboBox5.Items.Add(reader["marcaveiculo"].ToString());
                comboBox6.Items.Add(reader["marcaveiculo"].ToString());
            }

            reader.Close();
        }

        // Function to list Produtos
        private void listaProdutos()
        {
            if (!verifySGBDConnection())
                return;

            string estabelecimento = comboBox2.GetItemText(comboBox2.SelectedItem);
            SqlCommand cmd = new SqlCommand("select nome, preco from ServEntr.ListProdutos(@estabelecimento);", cn);
            cmd.Parameters.AddWithValue("@estabelecimento", estabelecimento);
            SqlDataReader reader = cmd.ExecuteReader();
            
            while (reader.Read())
            {
                checkedListBox1.Items.Add(reader["nome"].ToString() + ": " + reader["preco"].ToString());
            }
            reader.Close();
        }

        // Function to show Clientes
        private void showCliente(String nome)
        {
            SqlCommand cmd = new SqlCommand("select * from ServEntr.List1Cliente(@nome);", cn);
            cmd.Parameters.AddWithValue("@nome", nome);
            SqlDataReader reader = cmd.ExecuteReader();

            while(reader.Read())
            {
                textBox30.Text = reader["nome"].ToString();
                textBox24.Text = reader["morada"].ToString();
                textBox27.Text = reader["nr_tel"].ToString();
                textBox28.Text = reader["email"].ToString();
                textBox29.Text = reader["nif"].ToString();
            }

            reader.Close();
        }

        // Function to show motoristas
        private void showMotorista(String nome)
        {
            SqlCommand cmd = new SqlCommand("select * from ServEntr.List1Motorista(@nome);", cn);
            cmd.Parameters.AddWithValue("@nome", nome);
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                textBox16.Text = reader["nome"].ToString();
                textBox11.Text = reader["morada"].ToString();
                textBox13.Text = reader["nr_tel"].ToString();
                textBox14.Text = reader["email"].ToString();
                textBox15.Text = reader["nif"].ToString();
                textBox12.Text = reader["matricula"].ToString();
                comboBox6.Text = reader["marcaveiculo"].ToString();
            }

            reader.Close();
        }

        // Function to show encomendas
        private void showEncomenda(String id)
        {
            SqlCommand cmd = new SqlCommand("select * from ServEntr.List1Encomenda(@id);", cn);
            cmd.Parameters.AddWithValue("@id", id);
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                textBox25.Text = reader["cliente"].ToString();
                checkedListBox2.Items.Add(reader["produtos"]);
                textBox2.Text = reader["obs"].ToString();
                textBox26.Text = reader["metodo"].ToString();
            }

            reader.Close();
        }

        // Function to create Encomendas
        private void addEncomenda()
        {
            string estabelecimento = comboBox2.GetItemText(comboBox2.SelectedItem);
            string cliente = comboBox1.GetItemText(comboBox1.SelectedItem);
            string produtos = "";
            string obs = textBox9.Text;

            //foreach (object item in checkedListBox1.CheckedItems)
            //{
            //    string[] line = item.ToString().Split(':');
            //    produtos += line;
            //}
            string[] line = null;

            for (int i = 0; i < checkedListBox1.CheckedItems.Count; i++)
            {
                line = checkedListBox1.CheckedItems.IndexOf(i).ToString().Split(':');
                Console.WriteLine(line[0]);
                //Console.WriteLine(line[i]);
                produtos += line[0].ToString();
            }

            for(int i = 0; i < line.Length; i++)
            {
                Console.WriteLine(line[i].ToString());
            }

            
            Console.WriteLine(produtos);

            //// metodo 
            //string metodo = "";
            //if (radioButton1.Checked)
            //    metodo = radioButton1.Text;
            //else (radioButton2.Checked)
            //    metodo = radioButton2.Text;



            //SqlCommand cmd = new SqlCommand("ServEntr.newEncomenda", cn);
            //cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.AddWithValue("@estabelecimento", estabelecimento);
            //cmd.Parameters.AddWithValue("@cliente", cliente);
            //cmd.Parameters.AddWithValue("@produtos", produtos);
            //cmd.Parameters.AddWithValue("@obs", obs);
            //cmd.Parameters.AddWithValue("@metodo", metodo);
            //cmd.ExecuteNonQuery();

        }

        // Function to edit Encomendas
        private void editEncomenda()
        {
            int selectedrowindex = tabela_encomendas.SelectedCells[0].RowIndex;
            DataGridViewRow selectedRow = tabela_encomendas.Rows[selectedrowindex];
            string id = Convert.ToString(selectedRow.Cells["id"].Value);

            string status = "";
            if (radioButton3.Checked)
                status = "terminada";
            else if (radioButton4.Checked)
                status = "cancelada";
            else if (radioButton5.Checked)
                status = "ativa";
            
            SqlCommand cmd = new SqlCommand("ServEntr.editEncomenda", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@status", status);
            cmd.ExecuteNonQuery();
        }

        // Function to remove Encomendas
        private void deleteEncomenda()
        {

            int selectedrowindex = tabela_encomendas.SelectedCells[0].RowIndex;
            DataGridViewRow selectedRow = tabela_encomendas.Rows[selectedrowindex];
            string id = Convert.ToString(selectedRow.Cells["id"].Value);

            SqlCommand cmd = new SqlCommand("ServEntr.deleteEncomenda", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
        }

        // Function to create Motoristas
        private void addMotorista()
        {
            string nome = textBox8.Text;
            string nif = textBox7.Text;
            string email = textBox6.Text;
            string nr_tel = textBox5.Text;
            string morada = textBox3.Text;
            string matricula = textBox4.Text;
            string marcaveiculo = comboBox5.GetItemText(comboBox5.SelectedItem);

            SqlCommand cmd = new SqlCommand("ServEntr.newMotorista", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@nome", nome);
            cmd.Parameters.AddWithValue("@nif", nif);
            cmd.Parameters.AddWithValue("@nr_tel", nr_tel);
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@morada", morada);
            cmd.Parameters.AddWithValue("@matricula", matricula);
            cmd.Parameters.AddWithValue("@marcaveiculo", marcaveiculo);
            cmd.ExecuteNonQuery();
        }

        // Function to edit Motoristas
        private void editMotorista()
        {
            string nome = textBox16.Text;
            string nif = textBox15.Text;
            string email = textBox14.Text;
            string nr_tel = textBox13.Text;
            string morada = textBox11.Text;
            string matricula = textBox12.Text;
            string marcaveiculo = comboBox6.GetItemText(comboBox6.SelectedItem);

            SqlCommand cmd = new SqlCommand("ServEntr.editMotorista", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@nome", nome);
            cmd.Parameters.AddWithValue("@nif", nif);
            cmd.Parameters.AddWithValue("@nr_tel", nr_tel);
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@morada", morada);
            cmd.Parameters.AddWithValue("@matricula", matricula);
            cmd.Parameters.AddWithValue("@marcaveiculo", marcaveiculo);
            cmd.ExecuteNonQuery();
        }

        // Function to remove Motoristas
        private void deleteMotorista()
        {

            int selectedrowindex = tabela_motoristas.SelectedCells[0].RowIndex;
            DataGridViewRow selectedRow = tabela_motoristas.Rows[selectedrowindex];
            string nome = Convert.ToString(selectedRow.Cells["nome"].Value);

            SqlCommand cmd = new SqlCommand("ServEntr.deleteMotorista", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@nome", nome);
            cmd.ExecuteNonQuery();
        }

        // Function to create Clientes
        private void addCliente()
        {
            string nome = textBox22.Text;
            string nif = textBox21.Text;
            string email = textBox20.Text;
            string nr_tel = textBox18.Text;
            string morada = textBox17.Text;

            SqlCommand cmd = new SqlCommand("ServEntr.newCliente", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@nome", nome);
            cmd.Parameters.AddWithValue("@nif", nif);
            cmd.Parameters.AddWithValue("@nr_tel", nr_tel);
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@morada", morada);
            cmd.ExecuteNonQuery();
        }

        // Function to edit Clientes
        private void editCliente()
        {
            string nome = textBox30.Text;
            string nif = textBox29.Text;
            string email = textBox28.Text;
            string nr_tel = textBox27.Text;
            string morada = textBox24.Text;

            SqlCommand cmd = new SqlCommand("ServEntr.editCliente", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@nome", nome);
            cmd.Parameters.AddWithValue("@nif", nif);
            cmd.Parameters.AddWithValue("@nr_tel", nr_tel);
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@morada", morada);
            cmd.ExecuteNonQuery();
        }

        // Function to edit Clientes
        private void deleteCliente()
        {
            int selectedrowindex = tabela_clientes.SelectedCells[0].RowIndex;
            DataGridViewRow selectedRow = tabela_clientes.Rows[selectedrowindex];
            string nome = Convert.ToString(selectedRow.Cells["nome"].Value);

            SqlCommand cmd = new SqlCommand("ServEntr.deleteCliente", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@nome", nome);
            cmd.ExecuteNonQuery();
        }

        private void exit_button_Click(object sender, EventArgs e)
        {
            cn.Close();
            Application.Exit();
        }

        private void b_cancel_add_encomenda_Click(object sender, EventArgs e)
        {
            showpanel(this.painel_encomendas);
            clear_fields();
        }

        private void b_add_encomenda_Click(object sender, EventArgs e)
        {
            showpanel(this.painel_addencomenda);
            listaEstabelecimentos();
            clear_fields();
        }

        private void b_edit_encomenda_Click(object sender, EventArgs e)
        {
            int selectedrowindex = tabela_encomendas.SelectedCells[0].RowIndex;
            DataGridViewRow selectedRow = tabela_encomendas.Rows[selectedrowindex];
            string id = Convert.ToString(selectedRow.Cells["id"].Value);

            showpanel(this.painel_editencomenda);
            showEncomenda(id);
        }

        private void b_cancel_add_encomenda_Click_1(object sender, EventArgs e)
        {
            showpanel(this.painel_encomendas);
            clear_fields();
        }

        private void b_cancel_edit_encomenda_Click(object sender, EventArgs e)
        {
            showpanel(this.painel_encomendas);
            clear_fields();
        }

        private void b_encomendas_lateral_Click(object sender, EventArgs e)
        {
            showpanel(this.painel_encomendas);
            clear_fields();
        }

        private void b_motoristas_lateral_Click(object sender, EventArgs e)
        {
            showpanel(this.painel_motoristas);
            loadTabelaMotoristas();
            clear_fields();
        }

        private void b_add_motorista_Click(object sender, EventArgs e)
        {
            showpanel(this.painel_addmotorista);
            clear_fields();
        }

        private void b_cancel_add_motorista_Click(object sender, EventArgs e)
        {
            showpanel(this.painel_motoristas);
            clear_fields();
        }

        private void b_edit_motorista_Click(object sender, EventArgs e)
        {
            int selectedrowindex = tabela_motoristas.SelectedCells[0].RowIndex;
            DataGridViewRow selectedRow = tabela_motoristas.Rows[selectedrowindex];
            string nome = Convert.ToString(selectedRow.Cells["nome"].Value);

            showpanel(this.painel_edit_motorista);
            showMotorista(nome);
            clear_fields();
        }

        private void b_cancel_edit_motorista_Click(object sender, EventArgs e)
        {
            showpanel(this.painel_motoristas);
            clear_fields();
        }

        private void b_lateral_clientes_Click(object sender, EventArgs e)
        {
            showpanel(this.painel_clientes);
            loadTabelaClientes();
            clear_fields();
        }

        private void b_add_cliente_Click(object sender, EventArgs e)
        {
            showpanel(this.painel_add_cliente);
            clear_fields();
        }

        private void b_edit_cliente_Click(object sender, EventArgs e)
        {
            int selectedrowindex = tabela_clientes.SelectedCells[0].RowIndex;
            DataGridViewRow selectedRow = tabela_clientes.Rows[selectedrowindex];
            string nome = Convert.ToString(selectedRow.Cells["nome"].Value);

            showpanel(this.painel_edit_cliente);
            showCliente(nome);
        }

        private void b_cancel_add_cliente_Click(object sender, EventArgs e)
        {
            showpanel(this.painel_clientes);
            clear_fields();
        }

        private void b_cancel_edit_cliente_Click(object sender, EventArgs e)
        {
            showpanel(this.painel_clientes);
            clear_fields();
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

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            checkedListBox1.Items.Clear();
            listaProdutos();
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            checkedListBox2.Items.Clear();
            listaProdutos();
        }

        private void b_ok_addencomenda_Click(object sender, EventArgs e)
        {
            addEncomenda();
            showpanel(this.painel_encomendas);
            loadTabelaEncomendas();
            clear_fields();
        }

        private void b_ok_add_motorista_Click(object sender, EventArgs e)
        {
            addMotorista();
            showpanel(this.painel_motoristas);
            loadTabelaMotoristas();
            clear_fields();
        }

        private void b_ok_edit_motorista_Click(object sender, EventArgs e)
        {
            editMotorista();
            showpanel(this.painel_motoristas);
            loadTabelaMotoristas();
            clear_fields();
        }

        private void b_elim_motorista_Click(object sender, EventArgs e)
        {
            deleteMotorista();
            showpanel(this.painel_motoristas);
            loadTabelaMotoristas();
            clear_fields();
        }

        private void b_ok_add_cliente_Click(object sender, EventArgs e)
        {
            addCliente();
            showpanel(this.painel_clientes);
            loadTabelaClientes();
            clear_fields();
        }

        private void b_ok_edit_cliente_Click(object sender, EventArgs e)
        {
            editCliente();
            showpanel(this.painel_clientes);
            loadTabelaClientes();
            clear_fields();
        }

        private void b_elim_cliente_Click(object sender, EventArgs e)
        {
            deleteCliente();
            showpanel(this.painel_clientes);
            loadTabelaClientes();
            clear_fields();
        }
        
        private void b_ok_editencomenda_Click(object sender, EventArgs e)
        {
            editEncomenda();
            showpanel(this.painel_encomendas);
            loadTabelaEncomendas();
            clear_fields();
        }

        private void elim_encomenda_Click(object sender, EventArgs e)
        {
            clear_fields();
            deleteEncomenda();
            loadTabelaEncomendas();
        }

        private void clear_fields()
        {
            textBox1.Text = ""; textBox2.Text = ""; textBox3.Text = ""; textBox4.Text = "";
            textBox5.Text = ""; textBox6.Text = ""; textBox7.Text = ""; textBox8.Text = "";
            textBox9.Text = ""; textBox11.Text = ""; textBox12.Text = ""; textBox13.Text = "";
            textBox14.Text = ""; textBox15.Text = ""; textBox16.Text = ""; textBox17.Text = "";
            textBox18.Text = ""; textBox19.Text = ""; textBox20.Text = ""; textBox21.Text = "";
            textBox22.Text = ""; textBox24.Text = ""; textBox25.Text = ""; textBox26.Text = "";
            textBox27.Text = ""; textBox28.Text = ""; textBox29.Text = ""; textBox30.Text = "";

            checkedListBox1.Items.Clear();
            checkedListBox2.Items.Clear();
        }
    }
}
