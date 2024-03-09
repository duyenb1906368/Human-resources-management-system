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

namespace NLN
{
    public partial class fBangLuong : Form
    {
        public SqlConnection conn;
        public fBangLuong()
        {
            InitializeComponent();
        }

        public void ketnoi()
        {
            string cauketnoi = "Server = DESKTOP-KMQ5NLH; database = qlns1; integrated security = true; MultipleActiveResultSets = true";
            conn = new SqlConnection(cauketnoi);
            conn.Open();
        }

        public void HienThiDLLenDataGV(string query, DataGridView dg)
        {
            //query = "  select * from BANGLUONGCTY";
            SqlDataAdapter dta = new SqlDataAdapter(query, conn);
            DataSet ds = new DataSet();
            dta.Fill(ds, "QLNS");
            dg.DataSource = ds;
            dg.DataMember = "QLNS";
        }

        public void HienThiLenCB(string query, ComboBox cb, string ShowVal, string HideVal)
        {
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader rd = cmd.ExecuteReader();
            DataTable dtable = new DataTable();
            dtable.Load(rd);
            cb.DataSource = dtable;
            cb.DisplayMember = ShowVal;
            cb.ValueMember = HideVal;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string xoa_sql = "delete from BANGLUONGCTY where BLCT_MALUONG= '" + textBox1.Text + "' ";
            SqlCommand comd = new SqlCommand(xoa_sql, conn);
            comd.BeginExecuteNonQuery();
            MessageBox.Show("Xóa Lương thành công !!");
            HienThiDLLenDataGV("select * from BANGLUONGCTY", dataGridView1);
        }

        private void fBangLuong_Load(object sender, EventArgs e)
        {
            ketnoi();

            HienThiDLLenDataGV("select * from BANGLUONGCTY", dataGridView1);
        }

        private void ShowDataForm(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            textBox4.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            textBox5.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            textBox6.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
            textBox7.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
            textBox8.Text = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
            textBox9.Text = dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();
            textBox10.Text = dataGridView1.Rows[e.RowIndex].Cells[9].Value.ToString();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            textBox8.Text = "";
            textBox9.Text = "";
            textBox10.Text = "";

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string them_sql = "insert into BANGLUONGCTY values " + "('" + textBox1.Text + "',  '" + textBox2.Text + "', '" + textBox3.Text + "', '" + textBox4.Text + "', '" + textBox5.Text + "', '" + textBox6.Text + "', '" + textBox7.Text + "', '" + textBox8.Text + "', '" + textBox9.Text + "', '" + textBox10.Text + "')";
            SqlCommand cmd = new SqlCommand(them_sql, conn);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Them luong thanh cong!!");
            HienThiDLLenDataGV("select * from BANGLUONGCTY", dataGridView1);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string ma = textBox1.Text;

            if (ma == "")
            {
                MessageBox.Show("Bạn chưa chọn Lương!!");
            }
            else
            {
                string sua_sql = "update BANGLUONGCTY set BLCT_LCB = '" + textBox2.Text + "', BLCT_PCCVU = '" + textBox3.Text + "', BLCT_LCBMOI = '" + textBox4.Text + "' , BLCT_NGAYLAP = '" + textBox5.Text + "' , BLCT_NGAYSUA = '" + textBox6.Text + "' , BLCT_LYDO = '" + textBox7.Text + "' , BLCT_PCCVUMOI = '" + textBox8.Text + "' , BLCT_NGAYSUAPCCVU = '" + textBox9.Text + "' , BLCT_GHICHU = '" + textBox10.Text + "' where BLCT_MALUONG  = '" + ma + "' ";
                Console.WriteLine(sua_sql);
                SqlCommand comd = new SqlCommand(sua_sql, conn);
                comd.ExecuteNonQuery();
                MessageBox.Show("Sửa Lương thành công!!");
                HienThiDLLenDataGV("select * from BANGLUONGCTY", dataGridView1);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            fManager manager = new fManager();
            manager.Show();
            this.Hide();
        }
    }
}
