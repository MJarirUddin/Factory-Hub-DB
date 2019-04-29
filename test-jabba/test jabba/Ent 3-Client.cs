using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;


namespace test_jabba
{
    public partial class Ent_3_Client : Form
    {
        public Ent_3_Client()
        {
            InitializeComponent();

            //DATAGRIDVIEW PROPERTIES
            dataGridView1.ColumnCount = 8;
            dataGridView1.Columns[0].Name = "ID";
            dataGridView1.Columns[1].Name = "Name";
            dataGridView1.Columns[2].Name = "Contact";
            dataGridView1.Columns[3].Name = "Address";
            dataGridView1.Columns[4].Name = "Nationality";
            dataGridView1.Columns[5].Name = "Order Status";
            dataGridView1.Columns[6].Name = "Order Id";
            dataGridView1.Columns[7].Name = "Clients TypeId";
            
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            //SELECTION MODE
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
        }

        //MYSQL SECTION:

        //MySQL Connection String
        public static string _conStr = "server= localhost; user id = root; password = 0331 ; database=factory_hub; port=3306";
        MySqlConnection con = new MySqlConnection(_conStr);
        MySqlCommand cmd;
        MySqlDataAdapter adapter;
        DataTable dt = new DataTable();
        //Methods for using SQL
        //ADD
        private void add(string Id, string Name, string Contact, string Address, string Nationality, string Order_Status, string Order_Id, string Clients_TypeId )
        {
            //sql stmt

            string sql = "Insert into clients(Clients_Id, Clients_Name, Clients_Contact, Clients_Address, Clients_Nationality, Order_Status, Order_Id, Clients_Type_Id) values(@Id, @Name, @Contact, @Address, @Nationality, @Order_Status, @Order_Id, @Clients_TypeId)";
            cmd = new MySqlCommand(sql, con);

            //ADD PARAMETERS
            cmd.Parameters.AddWithValue("@Id", Id);
            cmd.Parameters.AddWithValue("@Name", Name);
            cmd.Parameters.AddWithValue("@Contact", Contact);
            cmd.Parameters.AddWithValue("@Address", Address);
            cmd.Parameters.AddWithValue("@Nationality", Nationality);
            cmd.Parameters.AddWithValue("@Order_Status", Order_Status);
            cmd.Parameters.AddWithValue("@Order_Id", Order_Id);
            cmd.Parameters.AddWithValue("@Clients_TypeId", Clients_TypeId);
            try
            {
                con.Open();
                if (cmd.ExecuteNonQuery() > 0)
                {
                    clear();
                    MessageBox.Show("Data Inserted Successfully");
                }
                con.Close();
                retrieve();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                con.Close();
            }
        }
        //RETRIEVE
        private void retrieve()
        {
            dataGridView1.Rows.Clear();

            string sql = "Select * from clients";
            cmd = new MySqlCommand(sql, con);
            try
            {
                con.Open();

                adapter = new MySqlDataAdapter(cmd);

                adapter.Fill(dt);

                // loop thru dt

                foreach (DataRow row in dt.Rows)
                {
                    populate(row[0].ToString(), row[1].ToString(), row[2].ToString(), row[3].ToString(), row[4].ToString(), row[5].ToString(), row[6].ToString(), row[7].ToString());

                }
                con.Close();
                dt.Rows.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                con.Close();
            }
            clear();

        }
        //UPDATE
        private void update(string Id, string Name, string Contact, string Address, string Nationality, string Order_Status, string Order_Id, string Clients_TypeId)
        {
            
            string sql = "Update clients Set Clients_Name='" + Name + "', Clients_Contact ='" + Contact + "', Clients_Address = '" + Address + "' ,Clients_Nationality ='" + Nationality + "', Order_Status ='" + Order_Status + "',Order_Id='" + Order_Id + "',Clients_Type_Id ='" + Clients_TypeId + " 'Where Clients_Id = " + Id + "";

            cmd = new MySqlCommand(sql, con);

            // open con u r d
            try
            {
                con.Open();
                adapter = new MySqlDataAdapter(cmd);

                adapter.UpdateCommand = con.CreateCommand();
                adapter.UpdateCommand.CommandText = sql;

                if (adapter.UpdateCommand.ExecuteNonQuery() > 0)
                {
                    clear();
                    MessageBox.Show("Successfully Updated");
                }
                con.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
                con.Close();

            }
        }
        // DELETE 
        private void delete(string Id)
        {
            //sqlstmt
            string sql = "Delete From clients where Clients_Id = " + Id + "";
            cmd = new MySqlCommand(sql, con);

            try
            {
                con.Open();
                adapter = new MySqlDataAdapter(cmd);

                adapter.DeleteCommand = con.CreateCommand();

                adapter.DeleteCommand.CommandText = sql;

                if (MessageBox.Show("Sure ?", "Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                {
                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        clear();
                        MessageBox.Show("Data Deleted!");
                    }
                }

                con.Close();
                retrieve();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                con.Close();

            }
        }
        // CLEAR
        private void clear()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();          
            textBox6.Clear();
            textBox7.Clear();
            textBox8.Clear();
            textBox9.Clear();

        }
        //POPULATE DATAGRID
        private void populate(string Id, string Name, string Contact, string Address, string Nationality, string Order_Status, string Order_Id, string Clients_TypeId)
        {
            dataGridView1.Rows.Add(Id, Name, Contact, Address, Nationality,Order_Status,Order_Id,Clients_TypeId);
        }

        private void Button1_Click(object sender, EventArgs e)//RETRIEVE btn
        {
            retrieve();
        }

        private void Button2_Click(object sender, EventArgs e)//INSERT btn
        {
            add(textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, textBox6.Text, textBox7.Text, textBox8.Text, textBox9.Text);
        }

        private void Button3_Click(object sender, EventArgs e)//DELETE btn
        {
            delete(textBox1.Text);
        }

        private void Button4_Click(object sender, EventArgs e)//UPDATE btn
        {
            update(textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, textBox6.Text, textBox7.Text, textBox8.Text, textBox9.Text);
        }

        private void Button5_Click(object sender, EventArgs e)//BACK btn
        {
            Dashboard form = new Dashboard();
            form.Show();
            this.Hide();
        }
    }
}
