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
    public partial class fLuongNV : Form
    {
        public SqlConnection conn;
        public fLuongNV()
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
            query = "select distinct a.BLCT_LCB, b.BP_TEN, c.PB_TEN, d.NVCT_HOTEN, BCNV_MA, BCNV_PCCVIEC, BCNV_PCKHAC, BCNV_KT, BCNV_KL, BCNV_SNC, BCNV_SNN, BCNV_SNLT, BCNV_NGAYLAP, BCNV_GHICHU from BANGLUONGCTY a, bophan b, phongban c, NHANVIENCHINHTHUC d, BANGCONGNHANVIEN e where(a.BLCT_MALUONG = e.BLCT_MALUONG)";
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

        private void fLuongNV_Load(object sender, EventArgs e)
        {
            ketnoi();

            HienThiDLLenDataGV("select * from BANGCONGNHANVIEN", dataGridView1);

            HienThiLenCB("select * from PHONGBAN", comboBox4, "PB_TEN", "PB_MA");
            HienThiLenCB("select * from BOPHAN", comboBox3, "BP_TEN", "BP_MA");
            HienThiLenCB("select * from BANGLUONGCTY", comboBox2, "BLCT_LCB", "BLCT_MALUONG");
            HienThiLenCB("select * from NHANVIENCHINHTHUC", comboBox1, "NVCT_HOTEN", "NVCT_MA");

        }
        string luong_ma;
       // string pccv;
      //  string pck;
      //  string kt, kl, snc, snn, snlt;
        private void ShowDataForm(object sender, DataGridViewCellEventArgs e)
        {
            comboBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            comboBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            comboBox4.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            comboBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            luong_ma = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
            textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
            textBox4.Text = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
            textBox5.Text = dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();
            textBox6.Text = dataGridView1.Rows[e.RowIndex].Cells[9].Value.ToString();
            textBox7.Text = dataGridView1.Rows[e.RowIndex].Cells[10].Value.ToString();
            textBox8.Text = dataGridView1.Rows[e.RowIndex].Cells[11].Value.ToString();
            textBox9.Text = dataGridView1.Rows[e.RowIndex].Cells[12].Value.ToString();
            textBox10.Text = dataGridView1.Rows[e.RowIndex].Cells[13].Value.ToString();


        }

        private void button1_Click(object sender, EventArgs e)
        {
            comboBox1.Text = "";
            comboBox2.Text = "";
            comboBox3.Text = "";
            comboBox4.Text = "";
            txbTong.Text = "";
            textBox10.Text = "";
            textBox11.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = ""; 
            textBox6.Text = ""; 
            textBox7.Text = ""; 
            textBox8.Text = ""; 
            textBox9.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlCommand cm = new SqlCommand("SELECT MAX(BCNV_MA +1) AS BCNV_MA FROM BANGCONGNHANVIEN", conn);
            DataTable tb = new DataTable();
            SqlDataReader read = cm.ExecuteReader();
            tb.Load(read);
            string luong_mas = tb.Rows[0]["BCNV_MA"].ToString();
            string them_sql = "insert into BANGCONGNHANVIEN values " + "('" + comboBox2.SelectedValue.ToString() + "', '" + comboBox3.SelectedValue.ToString() + "', '" + comboBox4.SelectedValue.ToString() + "', '" + comboBox1.SelectedValue.ToString() + "', '"+luong_mas+"' , '" + textBox2.Text + "', '" + textBox3.Text + "', '" + textBox4.Text + "', '" + textBox5.Text + "', '" + textBox6.Text + "', '" + textBox7.Text + "', '" + textBox8.Text + "', '" + textBox9.Text + "', '"+textBox10.Text+"')";
            SqlCommand cmd = new SqlCommand(them_sql, conn);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Them luong NV thanh cong!!");
            HienThiDLLenDataGV("select * from BANGCONGNHANVIEN", dataGridView1);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string ma = comboBox1.SelectedValue.ToString();

            if (ma == "")
            {
                MessageBox.Show("Bạn chưa chọn Nhân Viên!!");
            }
            else
            {
                string sua_sql = "update BANGCONGNHANVIEN set BLCT_MALUONG='" + comboBox2.SelectedValue.ToString() + "', BP_MA = '" + comboBox3.SelectedValue.ToString() + "', PB_MA = '" + comboBox4.SelectedValue.ToString() + "', NVCT_MA = '" + comboBox1.SelectedValue.ToString() + "', BCNV_PCCVIEC = '" + textBox2.Text + "', BCNV_PCKHAC = '" + textBox3.Text + "'," +
                    "BCNV_KT = '" + textBox4.Text + "', BCNV_KL = '" + textBox5.Text + "', BCNV_SNC = '" + textBox6.Text + "', BCNV_SNN = '" + textBox7.Text + "', BCNV_SNLT = '" + textBox8.Text + "', BCNV_NGAYLAP = '" + textBox9.Text + "', BCNV_GHICHU = '"+ textBox10.Text+ "' where BCNV_MA  = '" + luong_ma + "' ";

                Console.WriteLine(sua_sql);
                SqlCommand comd = new SqlCommand(sua_sql, conn);
                comd.ExecuteNonQuery();
                MessageBox.Show("Sửa lương Nhân Viên thành công!!");
                HienThiDLLenDataGV("select * from BANGCONGNHANVIEN", dataGridView1);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
           // string NV_MA = comboBox1.SelectedValue.ToString();

            string xoa_sql = "delete from BANGCONGNHANVIEN where BCNV_MA= '" + luong_ma + "' ";
            SqlCommand comd = new SqlCommand(xoa_sql, conn);
            comd.BeginExecuteNonQuery();
            MessageBox.Show("Xóa LƯƠNG Nhân Viên thành công !!");
            HienThiDLLenDataGV("select * from BANGCONGNHANVIEN", dataGridView1);
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
            int lcb = Convert.ToInt32(comboBox2.Text);
            int pccv = Convert.ToInt32(textBox2.Text);
            int pck = Convert.ToInt32(textBox3.Text);
            int khthuong = Convert.ToInt32(textBox4.Text);
            int kyluat = Convert.ToInt32(textBox5.Text);
            int snc = Convert.ToInt32(textBox6.Text);
            int snn = Convert.ToInt32(textBox7.Text);
            int snlt = Convert.ToInt32(textBox8.Text);

            float luong = ((lcb / 26) * snc + (snlt * 40000) - (snn * 40000) + pck + pccv + khthuong - kyluat);
            txbTong.Text = luong.ToString();
        }
    }
}
