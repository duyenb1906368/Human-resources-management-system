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
    public partial class fHSThuViec : Form
    {
        public SqlConnection conn;
        public fHSThuViec()
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
            query = "  select NVTV_MA, b.BP_TEN, c.PB_TEN, NVTV_TEN, NVTV_NGAYSINH, NVTV_GIOITINH, NVTV_DIACHI, NVTV_CHUYENMON, NVTV_VITRITHUVIEC, NVTV_NGAYTV, NVTV_SOTHANGTV from NHANVIENTHUVIEC a, bophan b, phongban c where(a.PB_MA = c.PB_MA)";
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

        private void fHSThuViec_Load(object sender, EventArgs e)
        {
            ketnoi();

            HienThiDLLenDataGV("select * from NHANVIENTHUVIEC", dataGridView1);

            HienThiLenCB("select * from PHONGBAN", cbbPB, "PB_TEN", "PB_MA");
            HienThiLenCB("select * from BOPHAN", cbbBP, "BP_TEN", "BP_MA");
        }

        private void ShowDataForm(object sender, DataGridViewCellEventArgs e)
        {
            txbMa.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            cbbBP.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            cbbPB.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            txbHT.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            txbNS.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            txbGT.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
            txbDC.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
            txbCM.Text = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
            txbVTTV.Text = dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();
            txbNTV.Text = dataGridView1.Rows[e.RowIndex].Cells[9].Value.ToString();
            txbSTTV.Text = dataGridView1.Rows[e.RowIndex].Cells[10].Value.ToString();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            txbCM.Text = "";
            txbDC.Text = "";
            txbGT.Text = "";
            txbHT.Text = "";
            txbMa.Text = "";
            txbNS.Text = "";
            txbNTV.Text = "";
            txbSTTV.Text = "";
            txbVTTV.Text = "";
            cbbBP.Text = "";
            cbbPB.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string them_sql = "insert into NHANVIENTHUVIEC values " + "('" + txbMa.Text + "','"+ cbbBP.SelectedValue.ToString() + "','"+cbbPB.SelectedValue.ToString() + "', '" + txbHT.Text + "', '" + txbNS.Text + "', '" + txbGT.Text + "', '" + txbDC.Text + "', '" + txbCM.Text + "', '" + txbVTTV.Text + "', '" + txbNTV.Text + "', '" + txbSTTV.Text + "')";
            SqlCommand cmd = new SqlCommand(them_sql, conn);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Them NVTV thanh cong!!");
            HienThiDLLenDataGV("select * from NHANVIENTHUVIEC", dataGridView1);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string NVTV_MA = txbMa.Text;

            if (NVTV_MA == "")
            {
                MessageBox.Show("Bạn chưa chọn Nhân Viên!!");
            }
            else
            {
                string sua_sql = "update NHANVIENTHUVIEC set BP_MA='" + cbbBP.SelectedValue.ToString() + "', PB_MA = '" + cbbPB.SelectedValue.ToString() + "', NVTV_TEN = '" + txbHT.Text + "', NVTV_NGAYSINH = '" + txbNS.Text + "', NVTV_GIOITINH = '" + txbGT.Text + "'," +
                    "NVTV_DIACHI = '" + txbDC.Text + "', NVTV_CHUYENMON = '" + txbCM.Text + "', NVTV_VITRITHUVIEC = '" + txbVTTV.Text + "', NVTV_NGAYTV = '" + txbNTV.Text + "', NVTV_SOTHANGTV = '" + txbSTTV.Text + "' where NVTV_MA  = '" + NVTV_MA + "' ";

                Console.WriteLine(sua_sql);
                SqlCommand comd = new SqlCommand(sua_sql, conn);
                comd.ExecuteNonQuery();
                MessageBox.Show("Sửa Nhân Viên thành công!!");
                HienThiDLLenDataGV("select * from NHANVIENTHUVIEC", dataGridView1);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string xoa_sql = "delete from NHANVIENTHUVIEC where NVTV_MA= '" + txbMa.Text + "' ";
            SqlCommand comd = new SqlCommand(xoa_sql, conn);
            comd.BeginExecuteNonQuery();
            MessageBox.Show("Xóa Nhân Viên thành công !!");
            HienThiDLLenDataGV("select * from NHANVIENTHUVIEC", dataGridView1);
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
                string timkiem = "select * from NHANVIENTHUVIEC where NVTV_MA = '" + word + "' OR NVTV_TEN like N'%" + word + "%' ";
                HienThiTimKiem(timkiem, dataGridView1);
            }
        }
    }
}
