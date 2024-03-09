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
    public partial class fLuongThuViec : Form
    {
        public SqlConnection conn;
        public fLuongThuViec()
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
            query = "  select MA, a.NVTV_MA, BCTV_SONGAYCONG, BCTV_SONGAYNGHI, BCTV_SOGIOLAMTHEM, BCTV_LUONG, BCTV_GHICHU from NHANVIENTHUVIEC a, BANGCONGTHUVIEC c where(a.NVTV_MA = c.NVTV_MA)";
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

        private void fLuongThuViec_Load(object sender, EventArgs e)
        {
            ketnoi();

            HienThiDLLenDataGV("select * from BANGCONGTHUVIEC", dataGridView1);

            HienThiLenCB("select * from NHANVIENTHUVIEC", comboBox1, "NVTV_TEN", "NVTV_MA");
        }
        string ma;
        private void ShowDataForm(object sender, DataGridViewCellEventArgs e)
        {
            ma = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            comboBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            textBox4.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
            textBox5.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ma = "";
            comboBox1.Text = "";
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlCommand cm = new SqlCommand("SELECT MAX(MA +1) AS MA FROM BANGCONGTHUVIEC", conn);
            DataTable tb = new DataTable();
            SqlDataReader read = cm.ExecuteReader();
            tb.Load(read);
            string mas = tb.Rows[0]["MA"].ToString();
            string them_sql = "insert into BANGCONGTHUVIEC values " + "('"+mas+"', '" + comboBox1.SelectedValue.ToString() + "',  '" + textBox1.Text + "', '" + textBox2.Text + "', '" + textBox3.Text + "', '" + textBox4.Text + "', '" + textBox5.Text + "')";
            SqlCommand cmd = new SqlCommand(them_sql, conn);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Them luong nhan vien thu viec thanh cong!!");
            HienThiDLLenDataGV("select * from BANGCONGTHUVIEC", dataGridView1);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (ma == "")
            {
                MessageBox.Show("Bạn chưa chọn nhân viên!!");
            }
            else
            {
                string sua_sql = "update BANGCONGTHUVIEC set NVTV_MA='" + comboBox1.SelectedValue.ToString() + "', BCTV_SONGAYCONG = '" + textBox1.Text + "', BCTV_SONGAYNGHI = '" + textBox2.Text + "', BCTV_SOGIOLAMTHEM = '"+textBox3+ "', BCTV_LUONG = '"+textBox4+"', BCTV_GHICHU = '"+textBox5+"' where MA  = '" + ma + "' ";

                Console.WriteLine(sua_sql);
                SqlCommand comd = new SqlCommand(sua_sql, conn);
                comd.ExecuteNonQuery();
                MessageBox.Show("Sửa lương nhân viên thử việc thành công!!");
                HienThiDLLenDataGV("select * from BANGCONGTHUVIEC", dataGridView1);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string xoa_sql = "delete from BANGCONGTHUVIEC where MA= '" + ma + "' ";
            SqlCommand comd = new SqlCommand(xoa_sql, conn);
            comd.BeginExecuteNonQuery();
            MessageBox.Show("Xóa lương nhân viên thử việc thành công !!");
            HienThiDLLenDataGV("select * from BANGCONGTHUVIEC", dataGridView1);
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

        private void button7_Click(object sender, EventArgs e)
        {
           
     
            int snc = Convert.ToInt32(textBox1.Text);
            int snn = Convert.ToInt32(textBox1.Text);
            int sglt = Convert.ToInt32(textBox3.Text);
            int luongtv = Convert.ToInt32(textBox4.Text);

            float luong = ((luongtv / 26) * snc + (sglt * 40000) - (snn * 40000) );
            txbTong.Text = luong.ToString();
        }
    }
}
