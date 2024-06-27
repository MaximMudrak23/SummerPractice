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
    public partial class Form5 : Form
    {
        DataBase dataBase = new DataBase();
        public Form5()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var mebel_type = textBox1.Text;
            var mebel_brand = textBox2.Text;
            var mebel_creator = textBox3.Text;
            var mebel_supplier = textBox4.Text;
            var mebel_price = textBox5.Text;

            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();

            string addMebel = $"insert into mebel_table(mebel_type, mebel_brand, mebel_creator, mebel_supplier, mebel_price) values('{mebel_type}','{mebel_brand}','{mebel_creator}','{mebel_supplier}','{mebel_price}')";

            SqlCommand addCommand = new SqlCommand(addMebel, dataBase.getConnection());

            dataBase.getConnection().Open();

            adapter.SelectCommand = addCommand;
            adapter.Fill(table);
            MessageBox.Show("Аккаунт успішно доданий!", "Успіх!");

            dataBase.getConnection().Close();
        }
    }
}
