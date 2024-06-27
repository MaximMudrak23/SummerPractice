using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab4
{
    public partial class Form1 : Form
    {

        DataBase database = new DataBase();

        public Form1()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;

            textBox2.PasswordChar = '*';
            textBox1.MaxLength = 20;
            textBox2.MaxLength = 20;

            this.Activated += new EventHandler(Form1_Activated);
        }

        private void Form1_Activated(object sender, EventArgs e)
        {
            pictureBox1.Visible = true;
            pictureBox2.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var login = textBox1.Text;
            var password = textBox2.Text;

            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();

            string queryString = $"select id, acclogin, accpassword from accounts where acclogin = '{login}' and accpassword = '{password}'";

            SqlCommand command = new SqlCommand(queryString, database.getConnection());

            adapter.SelectCommand = command;
            adapter.Fill(table);

            if (table.Rows.Count == 1 )
            {
                pictureBox2.Visible = true;
                pictureBox1.Visible = false;

                MessageBox.Show("Успіх!", "Успіх!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Form4 frm4 = new Form4();
                this.Hide();
                frm4.ShowDialog();
                this.Show();
            }
            else
                MessageBox.Show("Такого аккаунту не існує!", "Помилка!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form2 frm2 = new Form2();
            frm2.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form3 frm3 = new Form3();
            frm3.ShowDialog();
        }
    }
}