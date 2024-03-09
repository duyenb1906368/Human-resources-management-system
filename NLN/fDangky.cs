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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace NLN
{
    public partial class fDangky : Form
    {
        public SqlConnection conn;
        ClassDatabase cls = new ClassDatabase();

        public fDangky()
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
            SqlDataAdapter dta = new SqlDataAdapter(query, conn);
            DataSet ds = new DataSet();
            dta.Fill(ds, "QLNS");
            dg.DataSource = ds;
            dg.DataMember = "QLNS";
        }

        private void btnNhapLai_Click(object sender, EventArgs e)
        {
            txbDN.Text = "";
            txbHT.Text = "";
            txbMK.Text = "";
            txbQuyen.Text = "";
            txbNS.Text = "";
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string them_sql = "insert into TAIKHOAN values " + "('" + txbDN.Text + "', '" + txbMK.Text + "', '" + txbQuyen.Text + "', '" + txbHT.Text + "', '" + txbNS.Text + "')";
            SqlCommand cmd = new SqlCommand(them_sql, conn);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Them NV thanh cong!!");
            HienThiDLLenDataGV("select USERNAME, PASSWORD, QUYEN, TEN, NGAYSINH from TAIKHOAN", dataGridView1);
        }

        private void fDangky_Load(object sender, EventArgs e)
        {

            ketnoi();
            HienThiDLLenDataGV("select USERNAME, PASSWORD, QUYEN, TEN, NGAYSINH from TAIKHOAN", dataGridView1);
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string sua_sql = "Update TAIKHOAN set PASSWORD = '" + txbMK.Text + "', QUYEN = '" + txbQuyen.Text + "', TEN = '" + txbHT.Text + "', NGAYSINH = '" + txbNS.Text + "' where USERNAME = '" + txbDN.Text + "' ";
            SqlCommand cmd = new SqlCommand(sua_sql, conn);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Sua NV thanh cong!!");
            HienThiDLLenDataGV("select USERNAME, PASSWORD, QUYEN, TEN, NGAYSINH from TAIKHOAN", dataGridView1);
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ShowDataForm(object sender, DataGridViewCellEventArgs e)
        {
            txbDN.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            txbMK.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            txbQuyen.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            txbHT.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            txbNS.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string xoa_sql = "delete from TAIKHOAN where USERNAME = '" + txbDN.Text + "'";
            SqlCommand cmd = new SqlCommand(xoa_sql, conn);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Xoa NV thanh cong!!");
            HienThiDLLenDataGV("select USERNAME, PASSWORD, QUYEN, TEN, NGAYSINH from TAIKHOAN", dataGridView1);
        }
    }
}
