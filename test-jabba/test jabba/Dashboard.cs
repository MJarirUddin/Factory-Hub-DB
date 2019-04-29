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
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Ent_2_Emp form = new Ent_2_Emp();
            form.Show();
            this.Hide();
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Ent1 form = new Ent1();
            form.Show();
            this.Hide();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            Ent_3_Client form = new Ent_3_Client();
            form.Show();
            this.Hide(); 
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            Ent_4_Order form = new Ent_4_Order();
            form.Show();
            this.Hide(); 
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            Form1 form = new Form1();
            form.Show();
            this.Hide();
        }
    }
}
