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
   
    public partial class fCĐThaiSan : Form
    { 
        public SqlConnection conn;
        public fCĐThaiSan()
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
            query = "select ma, b.nvct_hoten, c.pb_ten, d.bp_ten, ts_ngaynghi, ts_ngaylamlai, ts_trocap, ts_ghichu from THAISAN a, bophan d, phongban c, NHANVIENCHINHTHUC b where (a.NVCT_MA = b.NVCT_MA)";
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

        private void fCĐThaiSan_Load(object sender, EventArgs e)
        {
            ketnoi();

            HienThiDLLenDataGV("select * from THAISAN", dataGridView1);

            HienThiLenCB("select * from NHANVIENCHINHTHUC", cbbHT, "NVCT_HOTEN", "NVCT_MA");
            HienThiLenCB("select * from BOPHAN", cbbBP, "BP_TEN", "BP_MA");
            HienThiLenCB("select * from PHONGBAN", cbbPB, "PB_TEN", "PB_MA");
        }
        string ma;
        private void ShowDataForm(object sender, DataGridViewCellEventArgs e)
        {
            cbbHT.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            cbbBP.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            cbbPB.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            txbNN.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            txbNLL.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
            txbTC.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
            txbGC.Text = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
            ma = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ma = "";
            txbNLL.Text = "";
            txbNN.Text = "";
            txbTC.Text = "";
            txbGC.Text = "";
            cbbBP.Text = "";
            cbbHT.Text = "";
            cbbPB.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlCommand cm = new SqlCommand("SELECT MAX(MA +1) AS MA FROM THAISAN", conn);
            DataTable tb = new DataTable();
            SqlDataReader read = cm.ExecuteReader();
            tb.Load(read);
            string mas = tb.Rows[0]["MA"].ToString();
            string them_sql = "insert into THAISAN values " + "( '" + mas +"', '" + cbbHT.SelectedValue.ToString() + "', '" + cbbBP.SelectedValue.ToString() + "', '" + cbbPB.SelectedValue.ToString() + "', '" + txbNN.Text + "', '" + txbNLL.Text + "', '" + txbTC.Text + "', '" + txbGC.Text + "')";
            SqlCommand cmd = new SqlCommand(them_sql, conn);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Them thanh cong!!");
            HienThiDLLenDataGV("select * from THAISAN", dataGridView1);
        }

        private void button3_Click(object sender, EventArgs e)
        {
           // string ma = txbMa.Text;

            if (ma == "")
            {
                MessageBox.Show("Bạn chưa chọn Nhân Viên!!");
            }
            else
            {
                string sua_sql = "update THAISAN set NVCT_MA='" + cbbHT.SelectedValue.ToString() + "', BP_MA = '" + cbbBP.SelectedValue.ToString() + "', PB_MA = '" + cbbPB.SelectedValue.ToString() + "', TS_NGAYNGHI = '" + txbNN.Text + "', TS_NGAYLAMLAI = '" + txbNLL.Text + "', TS_TROCAP = '" + txbTC.Text + "'," +
                    "TS_GHICHU = '" + txbGC.Text + "' where ts_ma  = '" + ma + "' ";

                Console.WriteLine(sua_sql);
                SqlCommand comd = new SqlCommand(sua_sql, conn);
                comd.ExecuteNonQuery();
                MessageBox.Show("Sửa thành công!!");
                HienThiDLLenDataGV("select * from THAISAN", dataGridView1);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string xoa_sql = "delete from THAISAN where ts_ma= '" + ma + "' ";
            SqlCommand comd = new SqlCommand(xoa_sql, conn);
            comd.BeginExecuteNonQuery();
            MessageBox.Show("Xóa thành công !!");
            HienThiDLLenDataGV("select * from THAISAN", dataGridView1);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            fManager manager = new fManager();
            manager.Show();
            this.Hide();
        }
    }
}
