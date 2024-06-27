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
    public partial class Form3 : Form
    {
        DataBase dataBase = new DataBase();
        public Form3()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var login = textBox1.Text;

            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();

            string checkDuplicateString = $"select * from accounts where acclogin = '{login}'";
            string deleteString = $"delete from accounts where acclogin = '{login}'";

            SqlCommand checkDuplicateCommand = new SqlCommand(checkDuplicateString, dataBase.getConnection());
            SqlCommand deleteCommand = new SqlCommand(deleteString, dataBase.getConnection());

            dataBase.getConnection().Open();

            adapter.SelectCommand = checkDuplicateCommand;
            adapter.Fill(table);

            if(table.Rows.Count > 0 )
            {
                deleteCommand.ExecuteNonQuery();
                MessageBox.Show("Аккаунт успішно видалений!");
            }
            else
            {
                MessageBox.Show("Такого аккаунту не існує!");
            }

            dataBase.getConnection().Close();
        }
    }
}
