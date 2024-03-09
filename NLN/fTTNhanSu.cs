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
    public partial class fTTNhanSu : Form
    {
        public SqlConnection conn;
        public fTTNhanSu()
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
            query = "select nvct_ma, b.bp_ten, c.pb_ten, d.blct_lcb, NVCT_HOTEN, NVCT_NGAYSINH, NVCT_GIOITINH, NVCT_TTHN, NVCT_CCCD, NVCT_CHUCVU, NVCT_SDT, NVCT_CHUYENMON, NVCT_TGTUYENDUNG from NHANVIENCHINHTHUC a, bophan b, phongban c, BANGLUONGCTY d where(a.BLCT_MALUONG = d.BLCT_MALUONG)";
            SqlDataAdapter dta = new SqlDataAdapter(query, conn);
            DataSet ds = new DataSet();
            dta.Fill(ds, "QLNS");
            dg.DataSource = ds;
            dg.DataMember = "QLNS";
        }

        private void fTTNhanSu_Load(object sender, EventArgs e)
        {
            ketnoi();

            HienThiDLLenDataGV("select * from NHANVIENCHINHTHUC",  dataGridView1);
            
            HienThiLenCB("select * from PHONGBAN", cbbPB, "PB_TEN", "PB_MA");
            HienThiLenCB("select * from BOPHAN", cbbBP, "BP_TEN", "BP_MA");
            HienThiLenCB("select * from BANGLUONGCTY", cbbLuong, "BLCT_LCB", "BLCT_MALUONG");
        }

        private void ShowDataForm(object sender, DataGridViewCellEventArgs e)
        {
            txbMaNV.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            cbbBP.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            cbbPB.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            cbbLuong.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            txbHT.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            txbNS.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
            txbGT.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
            txbTTHN.Text = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
            txbCCCD.Text = dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString(); 
            txbCV.Text = dataGridView1.Rows[e.RowIndex].Cells[9].Value.ToString();
            txbSDT.Text = dataGridView1.Rows[e.RowIndex].Cells[10].Value.ToString();
            txbCM.Text = dataGridView1.Rows[e.RowIndex].Cells[11].Value.ToString();
            txbTGTD.Text = dataGridView1.Rows[e.RowIndex].Cells[12].Value.ToString();


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

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            txbCCCD.Text = "";
            txbCM.Text = "";
            txbCV.Text = "";
            txbGT.Text = "";
            txbHT.Text = "";
            txbMaNV.Text = "";
            txbNS.Text = "";
            txbSDT.Text = "";
            txbTGTD.Text = "";
            txbTTHN.Text = "";
            cbbBP.Text = "";
            cbbLuong.Text = "";
            cbbPB.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string them_sql = "insert into NHANVIENCHINHTHUC values " + "('" + txbMaNV.Text + "', '" + cbbBP.SelectedValue.ToString() + "', '" + cbbPB.SelectedValue.ToString() + "', '" + cbbLuong.SelectedValue.ToString() + "', '" + txbHT.Text + "', '"+txbNS.Text+"', '"+txbGT.Text+"', '"+txbTTHN.Text+"', '"+txbCCCD.Text+"', '"+txbCV.Text+"', '"+txbSDT.Text+"', '"+txbCM.Text+"', '"+txbTGTD.Text+"')";
            SqlCommand cmd = new SqlCommand(them_sql, conn);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Them NV thanh cong!!");
            HienThiDLLenDataGV("select * from NHANVIENCHINHTHUC", dataGridView1);
        }

        private void button3_Click(object sender, EventArgs e)
        {

            string NVCT_MA = txbMaNV.Text;

            if (NVCT_MA == "")
            {
                MessageBox.Show("Bạn chưa chọn Nhân Viên!!");
            }
            else
            {
                string sua_sql = "update NHANVIENCHINHTHUC set BP_MA='" + cbbBP.SelectedValue.ToString() + "', PB_MA = '" + cbbPB.SelectedValue.ToString() + "', BLCT_MALUONG = '" + cbbLuong.SelectedValue.ToString() + "', NVCT_HOTEN = '" + txbHT.Text + "', NVCT_NGAYSINH = '" + txbNS.Text + "', NVCT_GIOITINH = '" + txbGT.Text + "'," +
                    "NVCT_TTHN = '" + txbTTHN.Text + "', NVCT_CCCD = '" + txbCCCD.Text + "', NVCT_CHUCVU = '" + txbCV.Text + "', NVCT_SDT = '" + txbSDT.Text+"', NVCT_CHUYENMON = '"+txbCM.Text+"', NVCT_TGTUYENDUNG = '"+txbTGTD.Text+ "' where NVCT_MA  = '" +NVCT_MA + "' ";

                Console.WriteLine(sua_sql);
                SqlCommand comd = new SqlCommand(sua_sql, conn);
                comd.ExecuteNonQuery();
                MessageBox.Show("Sửa Nhân Viên thành công!!");
                HienThiDLLenDataGV("select * from NHANVIENCHINHTHUC", dataGridView1);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            

            string xoa_sql = "delete from NHANVIENCHINHTHUC where NVCT_MA= '" + txbMaNV.Text + "' ";
            SqlCommand comd = new SqlCommand(xoa_sql, conn);
            comd.BeginExecuteNonQuery();
            MessageBox.Show("Xóa Nhân Viên thành công !!");
            HienThiDLLenDataGV("select * from NHANVIENCHINHTHUC", dataGridView1);
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

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            String word = textBox1.Text;
            if (e.KeyCode == Keys.Enter)
            {
                string timkiem = "select * from NHANVIENCHINHTHUC a where a.NVCT_MA = '" + word + "' OR a.NVCT_HOTEN like N'%" + word + "%' ";
                HienThiTimKiem(timkiem, dataGridView1);
            }
        }
    }
}
