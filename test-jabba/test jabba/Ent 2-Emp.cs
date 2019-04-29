using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace test_jabba
{
    public partial class Ent_2_Emp : Form
    {
        public Ent_2_Emp()
        {
            InitializeComponent();

            //DATAGRIDVIEW PROPERTIES
            dataGridView1.ColumnCount = 9;
            dataGridView1.Columns[0].Name = "ID";
            dataGridView1.Columns[1].Name = "Name";
            dataGridView1.Columns[2].Name = "Salary";
            dataGridView1.Columns[3].Name = "Joining Date";
            dataGridView1.Columns[4].Name = "Phone No:";
            dataGridView1.Columns[5].Name = "E-mail";
            dataGridView1.Columns[6].Name = "Nationality";
            dataGridView1.Columns[7].Name = "Emp Type";
            dataGridView1.Columns[8].Name = "Dept Id";

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            //SELECTION MODE
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;

        }

        //SOME EXTRA THINGS        
        private void label3_Click(object sender, EventArgs e)
        {

        }
        private void Ent_2_Emp_Load(object sender, EventArgs e)
        {

        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

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
        private void add(string Id, string Name, string Salary, string Date, string Contact, string Email, string Nationality, string EmpTypeId, string DeptId  )
        {
            //sql stmt

            string sql = "Insert into employee(Emp_Id, Emp_Name, Emp_Salary, Emp_Joining_date, Emp_Phone_no, Emp_E_mail, Emp_Nationality, Emp_Type_id, Dept_Id) values(@Id, @Name, @Salary, @Date, @Contact, @Email, @Nationality, @EmpTypeId, @DeptId)";
            cmd = new MySqlCommand(sql, con);

            //ADD PARAMETERS
            cmd.Parameters.AddWithValue("@Id", Id);
            cmd.Parameters.AddWithValue("@Name", Name);
            cmd.Parameters.AddWithValue("@Salary", Salary);
            cmd.Parameters.AddWithValue("@Date", Date);
            cmd.Parameters.AddWithValue("@Contact", Contact);
            cmd.Parameters.AddWithValue("@Email", Email);
            cmd.Parameters.AddWithValue("@Nationality", Nationality);
            cmd.Parameters.AddWithValue("@EmpTypeId", EmpTypeId);
            cmd.Parameters.AddWithValue("@DeptId", DeptId);
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

            string sql = "Select * from employee";
            cmd = new MySqlCommand(sql, con);
            try
            {
                con.Open();

                adapter = new MySqlDataAdapter(cmd);

                adapter.Fill(dt);

                // loop thru dt

                foreach (DataRow row in dt.Rows)
                {
                    populate(row[0].ToString(), row[1].ToString(), row[2].ToString(), row[3].ToString(), row[4].ToString(), row[5].ToString(), row[6].ToString(), row[7].ToString(), row[8].ToString());

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
        private void update(string Id, string Name, string Salary, string Date, string Contact, string Email, string Nationality, string EmpTypeId, string DeptId)
        {           
            string sql = "Update employee Set Emp_Name='" + Name + "', Emp_Salary ='" + Salary + "', Emp_Joining_date = '" + Date + "' ,Emp_Phone_no='" + Contact + "', Emp_E_mail='"+ Email + "',Emp_Nationality='" + Nationality + "',Emp_Type_id='" + EmpTypeId + "',Dept_Id='" + DeptId + " 'Where Emp_Id = " + Id + "";

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
            string sql = "Delete From employee where Emp_Id = " + Id + "";
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
            textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();
            textBox8.Clear();
            textBox9.Clear();

        }
        //POPULATE DATAGRID
        private void populate(string Id, string Name, string Salary, string Date, string Contact, string Email, string Nationality, string EmpTypeId, string DeptId)
        {
            dataGridView1.Rows.Add(Id, Name, Salary, Date, Contact, Email, Nationality, EmpTypeId, DeptId);
        }



        //JUST SOME FORMS' THINGS
        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void DataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            textBox1.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();          
            textBox4.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            textBox5.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
            textBox6.Text = dataGridView1.SelectedRows[0].Cells[5].Value.ToString();
            textBox7.Text = dataGridView1.SelectedRows[0].Cells[6].Value.ToString();
            textBox8.Text = dataGridView1.SelectedRows[0].Cells[7].Value.ToString();
            textBox9.Text = dataGridView1.SelectedRows[0].Cells[8].Value.ToString();
        }

        private void Button1_Click(object sender, EventArgs e)//RETREIVE btn
        {
            retrieve();
        }

        private void Button3_Click(object sender, EventArgs e)//DELETE btn
        {
            delete(textBox1.Text);
        }

        private void Button4_Click(object sender, EventArgs e)//UPDATE btn
        {
            update(textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text, textBox6.Text, textBox7.Text, textBox8.Text, textBox9.Text);
        }

        private void Button5_Click(object sender, EventArgs e)//BACK btn
        {
           
            Dashboard form = new Dashboard();
            form.Show();
            this.Hide();
        }

        private void Button2_Click(object sender, EventArgs e)//INSERT btn
        {
            add(textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text, textBox6.Text, textBox7.Text, textBox8.Text, textBox9.Text);
        }
    }
}
