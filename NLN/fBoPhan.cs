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
    public partial class fBoPhan : Form
    {
        public SqlConnection conn;
        public fBoPhan()
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
            //query = "  select * from BOPHAN";
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

        private void fBoPhan_Load(object sender, EventArgs e)
        {
            ketnoi();

            HienThiDLLenDataGV("select * from BOPHAN", dataGridView1);
        }
        string ma;
        private void ShowDataForm(object sender, DataGridViewCellEventArgs e)
        {
            txbBP.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            ma = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            txbNTL.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            txbNTL.Text = "";
            ma = "";
            txbBP.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlCommand cm = new SqlCommand("SELECT MAX(BP_MA +1) AS BP_MA FROM BOPHAN", conn);
            DataTable tb = new DataTable();
            SqlDataReader read = cm.ExecuteReader();
            tb.Load(read);
            string mas = tb.Rows[0]["BP_MA"].ToString();
            string them_sql = "insert into BOPHAN values " + "('" + mas + "',  '" + txbBP.Text + "', '" + txbNTL.Text + "')";
            SqlCommand cmd = new SqlCommand(them_sql, conn);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Them bo phan thanh cong!!");
            HienThiDLLenDataGV("select * from BOPHAN", dataGridView1);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            

            if (ma == "")
            {
                MessageBox.Show("Bạn chưa chọn Bộ Phận!!");
            }
            else
            {
                string sua_sql = "update BOPHAN set BP_TEN = '" + txbBP.Text + "', BP_NGAYTHANHLAP = '" + txbNTL.Text + "' where BP_MA  = '" + ma + "' ";

                Console.WriteLine(sua_sql);
                SqlCommand comd = new SqlCommand(sua_sql, conn);
                comd.ExecuteNonQuery();
                MessageBox.Show("Sửa Bộ Phận thành công!!");
                HienThiDLLenDataGV("select * from BOPHAN", dataGridView1);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string xoa_sql = "delete from BOPHAN where BP_MA= '" + ma + "' ";
            SqlCommand comd = new SqlCommand(xoa_sql, conn);
            comd.BeginExecuteNonQuery();
            MessageBox.Show("Xóa Phòng Ban thành công !!");
            HienThiDLLenDataGV("select * from BOPHAN", dataGridView1);
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

        public void HienThiTimKiem(string cautruyvan, DataGridView dg)
        {
            SqlDataAdapter ad = new SqlDataAdapter(cautruyvan, conn);
            DataSet dt = new DataSet();
            ad.Fill(dt, "DS");
            dg.DataSource = dt;
            dg.DataMember = "DS";
        }

        private void txtTim_KeyDown(object sender, KeyEventArgs e)
        {
            String word = txtTim.Text;
            if (e.KeyCode == Keys.Enter)
            {
                string timkiem = "select * from PHONGBAN where PB_TEN like N'%" + word + "%' ";
                HienThiTimKiem(timkiem, dataGridView1);
            }
        }
    }
}
