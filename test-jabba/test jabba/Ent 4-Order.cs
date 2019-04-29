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
    public partial class Ent_4_Order : Form
    {
        public Ent_4_Order()
        {
            InitializeComponent();

            //DATAGRIDVIEW PROPERTIES
            dataGridView1.ColumnCount = 6;
            dataGridView1.Columns[0].Name = "ID";
            dataGridView1.Columns[1].Name = "Price";
            dataGridView1.Columns[2].Name = "Quantity";
            dataGridView1.Columns[3].Name = "Placed at";
            dataGridView1.Columns[4].Name = "Dispatched on";
            dataGridView1.Columns[5].Name = "Dept Id";

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
        private void add(string Id, string Price, string Quantity, string Placed_at, string Dispatched_on, string Dept_Id)
        {
            //sql stmt

            string sql = "Insert into orders(Orders_Id, Orders_Price, Orders_Quantity, Orders_placed, Orders_dispatch, Dept_Id) values(@Id, @Price, @Quantity, @Placed_at, @Dispatched_on, @Dept_Id)";
            cmd = new MySqlCommand(sql, con);

            //ADD PARAMETERS
            cmd.Parameters.AddWithValue("@Id", Id);
            cmd.Parameters.AddWithValue("@Price", Price);
            cmd.Parameters.AddWithValue("@Quantity", Quantity);
            cmd.Parameters.AddWithValue("@Placed_at", Placed_at);
            cmd.Parameters.AddWithValue("@Dispatched_on", Dispatched_on);
            cmd.Parameters.AddWithValue("@Dept_Id", Dept_Id);
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

            string sql = "Select * from orders";
            cmd = new MySqlCommand(sql, con);
            try
            {
                con.Open();

                adapter = new MySqlDataAdapter(cmd);

                adapter.Fill(dt);

                // loop thru dt

                foreach (DataRow row in dt.Rows)
                {
                    populate(row[0].ToString(), row[1].ToString(), row[2].ToString(), row[3].ToString(), row[4].ToString(), row[5].ToString());

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
        private void update(string Id, string Price, string Quantity, string Placed_at, string Dispatched_on, string Dept_Id)
        {            
            string sql = "Update orders Set Orders_Id ='" + Id + "', Orders_Price ='" + Price + "', Orders_Quantity= '" + Quantity + "' ,Orders_placed ='" + Placed_at + "', Orders_dispatch ='" + Dispatched_on + "',Dept_Id='" + Dept_Id + " 'Where Orders_Id = " + Id + "";

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
            string sql = "Delete From orders where Orders_Id = " + Id + "";
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
            textBox6.Clear();
            textBox4.Clear();
            textBox7.Clear();          

        }
        //POPULATE DATAGRID
        private void populate(string Id, string Price, string Quantity, string Placed_at, string Dispatched_on, string Dept_Id)
        {
            dataGridView1.Rows.Add(Id, Price, Quantity, Placed_at, Dispatched_on, Dept_Id);
        }





        private void Button2_Click(object sender, EventArgs e)//INSERT btn
        {
            add(textBox1.Text, textBox2.Text, textBox3.Text, textBox6.Text, textBox4.Text, textBox7.Text );
        }
        private void Button1_Click(object sender, EventArgs e)//RETRIEVE btn
        {
            retrieve();
        }
        private void Button4_Click(object sender, EventArgs e)//UPDATE btn
        {
            update(textBox1.Text, textBox2.Text, textBox3.Text, textBox6.Text, textBox4.Text, textBox7.Text);
        }
        private void Button5_Click(object sender, EventArgs e)//BACK btn
        {
            Dashboard form = new Dashboard();
            form.Show();
            this.Hide();
        }       
        private void Button3_Click(object sender, EventArgs e)//DELETE btn
        {
            delete(textBox1.Text);
        }
        private void Ent_4_Order_Load(object sender, EventArgs e)
        {

        }
    }
}
