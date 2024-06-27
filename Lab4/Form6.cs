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
using System.Data.Common;

namespace Lab4
{
    public partial class Form6 : Form
    {
        DataBase dataBase = new DataBase();
        public Form6()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int id = int.Parse(textBox6.Text);

            string selectQuery = $"select mebel_type, mebel_brand, mebel_creator, mebel_supplier, mebel_price from mebel_table where id = '{id}'";

            SqlCommand command = new SqlCommand(selectQuery, dataBase.getConnection());

            command.Parameters.AddWithValue("@id", id);

            dataBase.getConnection().Open();

            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    textBox1.Text = reader["mebel_type"].ToString();
                    textBox2.Text = reader["mebel_brand"].ToString();
                    textBox3.Text = reader["mebel_creator"].ToString();
                    textBox4.Text = reader["mebel_supplier"].ToString();
                    textBox5.Text = reader["mebel_price"].ToString();
                }
                else
                {
                    MessageBox.Show("No record found with the specified ID.");
                }
            }

            dataBase.getConnection().Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int id = int.Parse(textBox6.Text);

            var mebel_type = textBox1.Text;
            var mebel_brand = textBox2.Text;
            var mebel_creator = textBox3.Text;
            var mebel_supplier = textBox4.Text;
            var mebel_price = textBox5.Text;
            String[] textBoxes = { textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text };

            string editString = $"update mebel_table set mebel_type = '{mebel_type}', mebel_brand = '{mebel_brand}', mebel_creator = '{mebel_creator}', mebel_supplier = '{mebel_supplier}', mebel_price = '{mebel_price}' where id = '{id}'";

            SqlCommand editCommand = new SqlCommand(editString, dataBase.getConnection());

            dataBase.getConnection().Open();

            bool hasError = false;

            foreach( var textBox in textBoxes)
            {
                if (string.IsNullOrEmpty(textBox))
                {
                    MessageBox.Show("Одне з полів пусте!");
                    hasError = true;
                }
            }

            if(int.TryParse(textBox6.Text, out _) && hasError == false)
            {
                editCommand.ExecuteNonQuery();
                MessageBox.Show("Дані успішно оновлені!");
            }
            else
            {
                MessageBox.Show("Дані не вдалося оновити!");
            }

            dataBase.getConnection().Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int id = int.Parse(textBox6.Text);
            int startCount;
            int endCount;

            string deleteString = $"delete from mebel_table where id = '{id}'";
            string startCountString = $"select count(*) from mebel_table";

            SqlCommand deleteCommand = new SqlCommand(deleteString, dataBase.getConnection());
            SqlCommand countCommand = new SqlCommand(startCountString, dataBase.getConnection());

            dataBase.getConnection().Open();

            startCount = (int)countCommand.ExecuteScalar();
            deleteCommand.ExecuteNonQuery();
            endCount = (int)(countCommand.ExecuteScalar());

            if (startCount > endCount)
            {
                MessageBox.Show("Аккаунт успішно видалений!");
            }
            else
            {
                MessageBox.Show("Аккаунт не був видалений!");
            }

            dataBase.getConnection().Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            dataBase.getConnection().Open();

            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();

            string selectString = "select * from mebel_table";

            SqlCommand selectCommand = new SqlCommand(selectString, dataBase.getConnection());

            adapter.SelectCommand = selectCommand;
            adapter.Fill(table);

            dataGridView1.DataSource = table;

            dataBase.getConnection().Close();
            
            
        }
    }
}
