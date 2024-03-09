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

namespace NLN
{
    public partial class flogin : Form
    {
        
        public flogin()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-KMQ5NLH; Initial Catalog=qlns1; Integrated Security=True");
            try
            {
                con.Open();
                string username = textBox1.Text;
                string password = textBox2.Text;

                string sql = "select * from TAIKHOAN where USERNAME = '" + username + "' and PASSWORD = '" + password + "' ";
                SqlCommand cmd = new SqlCommand(sql, con);
                SqlDataReader read = cmd.ExecuteReader();
                if (read.Read() == true)
                {
                    MessageBox.Show("Dang nhap thanh cong!!", "Thong bao", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    fManager manager = new fManager();
                    fManager.quyen = "admin";
                    manager.Show();
                    this.Hide();
                }
               

                else
                {
                    MessageBox.Show("Dang nhap that bai!!", "Thong bao", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
              
            }
            catch (Exception)
            {
                MessageBox.Show("Loi ket noi!!");
            }
        }
        

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
