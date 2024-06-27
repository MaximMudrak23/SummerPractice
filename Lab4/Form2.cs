using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Lab4
{
    public partial class Form2 : Form
    {
        DataBase dataBase = new DataBase();
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var login = textBox1.Text;
            var password = textBox2.Text;
            int startCount;
            int endCount;

            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();

            string addString = $"insert into accounts(acclogin, accpassword) values('{login}','{password}')";
            string startCountString = $"select count(*) from accounts";
            string checkDuplicateString = $"select * from accounts where acclogin = '{login}'";

            SqlCommand addCommand = new SqlCommand(addString, dataBase.getConnection());
            SqlCommand countCommand = new SqlCommand(startCountString, dataBase.getConnection());
            SqlCommand checkDuplicateCommand = new SqlCommand(checkDuplicateString, dataBase.getConnection());

            dataBase.getConnection().Open();

            startCount = (int)countCommand.ExecuteScalar();

            adapter.SelectCommand = checkDuplicateCommand;
            adapter.Fill(table);

            if(table.Rows.Count > 0)
            {
                MessageBox.Show("Аккаунт з таким логіном вже існує!", "Помилка!");
            }
            else
            {
                adapter.SelectCommand = addCommand;
                adapter.Fill(table);
                endCount = (int)(countCommand.ExecuteScalar());

                if (startCount < endCount)
                {
                    MessageBox.Show("Аккаунт успішно доданий!", "Успіх!");
                }
                else
                {
                    MessageBox.Show("Здається, число аккаунтів не змінилось!", "Помилка!");
                }
            }

            dataBase.getConnection().Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
        }
    }
}
