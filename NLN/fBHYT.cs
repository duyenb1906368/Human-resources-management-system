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
    public partial class fBHYT : Form
    {
        public SqlConnection conn;
        public fBHYT()
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
            query = " select BH_MASO, b.BLCT_MALUONG, c.NVCT_MA, BH_NGAYCAP, BH_NOICAP, BH_GHICHU from BAOHIEM a, BANGLUONGCTY b, NHANVIENCHINHTHUC c where (a.NVCT_MA = c.NVCT_MA)";
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

        private void fBHYT_Load(object sender, EventArgs e)
        {
            ketnoi();

            HienThiDLLenDataGV("select * from BAOHIEM", dataGridView1);

            HienThiLenCB("select * from NHANVIENCHINHTHUC", cbbHT, "NVCT_HOTEN", "NVCT_MA");
            HienThiLenCB("select * from BANGLUONGCTY", cbbLuong, "BLCT_LCB", "BLCT_MALUONG");
        }

        private void ShowDataForm(object sender, DataGridViewCellEventArgs e)
        {
            txbMa.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            cbbLuong.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            cbbHT.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            txbNgayCap.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            txbNoiCap.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            txbGC.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString(); 
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            txbGC.Text = "";
            txbMa.Text = "";
            txbNgayCap.Text = "";
            txbNoiCap.Text = "";
            cbbHT.Text = "";
            cbbLuong.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string them_sql = "insert into BAOHIEM values " + "('" + txbMa.Text + "', '" + cbbLuong.SelectedValue.ToString() + "', '" + cbbHT.SelectedValue.ToString() + "', '" + txbNgayCap.Text + "', '" + txbNoiCap.Text + "', '" + txbGC.Text + "')";
            SqlCommand cmd = new SqlCommand(them_sql, conn);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Them thanh cong!!");
            HienThiDLLenDataGV("select * from BAOHIEM", dataGridView1);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string ma = txbMa.Text;

            if (ma == "")
            {
                MessageBox.Show("Bạn chưa chọn Nhân Viên!!");
            }
            else
            {
                string sua_sql = "update BAOHIEM set BLCT_MALUONG='" + cbbLuong.SelectedValue.ToString() + "', NVCT_MA = '" + cbbHT.SelectedValue.ToString() + "',BH_NGAYCAP = '" + txbNgayCap.Text + "', BH_NOICAP = '" + txbNoiCap.Text + "', BH_GHICHU = '" + txbGC.Text + "' where BH_MASO  = '" + ma + "' ";

                Console.WriteLine(sua_sql);
                SqlCommand comd = new SqlCommand(sua_sql, conn);
                comd.ExecuteNonQuery();
                MessageBox.Show("Sửa thành công!!");
                HienThiDLLenDataGV("select * from BAOHIEM", dataGridView1);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string xoa_sql = "delete from BAOHIEM where BH_MASO= '" + txbMa.Text + "' ";
            SqlCommand comd = new SqlCommand(xoa_sql, conn);
            comd.BeginExecuteNonQuery();
            MessageBox.Show("Xóa thành công !!");
            HienThiDLLenDataGV("select * from BAOHIEM", dataGridView1);
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
