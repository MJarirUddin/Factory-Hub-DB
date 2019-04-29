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
    public partial class Ent1 : Form
    {
        public Ent1()
        {
            InitializeComponent();

            //DATAGRIDVIEW PROPERTIES
            dataGridView1.ColumnCount = 3;
            dataGridView1.Columns[0].Name = "Department ID";
            dataGridView1.Columns[1].Name = "Department Name";
            dataGridView1.Columns[2].Name = "Department Location";

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            //SELECTION MODE
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;

        }


        private void Ent1_Load(object sender, EventArgs e)
        {

        } //Form_Load_btn
     
        private void button1_Click(object sender, EventArgs e)
        {
            //add(textBox1.Text, textBox2.Text, textBox4.Text);             
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
           // textBox1.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
           // textBox2.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();          
           // textBox4.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            string selected = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            int Id = Convert.ToInt32(selected);
            //update(textBox1.Text, textBox2.Text, textBox4.Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
           retrieve();
        }


        //MYSQL CONNECTION STRING 
        public static string _conStr = "server= localhost; user id = root; password = 0331 ; database=factory_hub; port=3306";
        MySqlConnection con = new MySqlConnection(_conStr);
        MySqlCommand cmd;
        MySqlDataAdapter adapter;
        DataTable dt = new DataTable();

        //METHODS FOR USING SQL

        //ADD
        private void add(string Id, string Name, string loc)
        {
            //sql stmt

            string sql = "Insert into department(Dept_Id, Dept_Name, Dept_loc) values(@Id, @Name, @loc)";
            cmd = new MySqlCommand(sql, con);

            //ADD PARAMETERS
            cmd.Parameters.AddWithValue("@Id", Id);
            cmd.Parameters.AddWithValue("@Name", Name);            
            cmd.Parameters.AddWithValue("@loc", loc);
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

            string sql = "Select * from department";
            cmd = new MySqlCommand(sql, con);
            try
            {
                con.Open();

                adapter = new MySqlDataAdapter(cmd);

                adapter.Fill(dt);

                // loop thru dt

                foreach (DataRow row in dt.Rows)
                {
                    populate(row[0].ToString(), row[1].ToString(), row[2].ToString());

                }


                con.Close();
                //clear dt
                dt.Rows.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                con.Close();
            }

        }
        //UPDATE
        private void update(string Id, string Name, string loc)
        {
            string sql = "Update department Set Dept_Name='" + Name + "', Dept_loc ='" + loc + " 'Where ID = " + Id + "";

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
        private void delete(int id)
        {
            //sqlstmt
            string sql = "Delete From department where Dept_Id = " + id + "";
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
                        MessageBox.Show("Succeessfullyyyy HOGYA");
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
           // textBox1.Clear();
          //  textBox2.Clear();         
           // textBox4.Clear();

        }
        //POPULATE DATAGRID
        private void populate(string Id, string Name, string loc)
        {
            dataGridView1.Rows.Add(Id, Name, loc);
        }

        private void Button1_Click_1(object sender, EventArgs e)
        {
            Dashboard form = new Dashboard();
            form.Show();
            this.Hide();
            
        }
    }
}
