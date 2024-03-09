using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NLN
{
    public partial class fManager : Form
    {
        public fManager()
        {
            InitializeComponent();
        }

        public static string quyen;

        private void fManager_Load(object sender, EventArgs e)
        {
            if (quyen == "admin")
            {
                quảnLýTàiKhoảnToolStripMenuItem.Enabled = true;
                đổiMậtKhẩuToolStripMenuItem.Enabled = true;
                nhânSựToolStripMenuItem.Enabled = true;
             
                chếĐộThaiSảnToolStripMenuItem.Enabled = true;
                BHYTToolStripMenuItem.Enabled = true;
                
            }
            else if (quyen == "user")
            {
                quảnLýTàiKhoảnToolStripMenuItem.Enabled = false;
                đổiMậtKhẩuToolStripMenuItem.Enabled = true;
                nhânSựToolStripMenuItem.Enabled = true;
             
                chếĐộThaiSảnToolStripMenuItem.Enabled = true;
                BHYTToolStripMenuItem.Enabled = true;
            }
        }

        private void quảnLýTàiKhoảnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fDangky dk = new fDangky();
            dk.Show();
            this.Hide();
        }

        private void đổiMậtKhẩuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fDoiMatKhau dmk = new fDoiMatKhau();
            dmk.Show();
            this.Hide();
        }

        private void nhânSựToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            fTTNhanSu nhansu = new fTTNhanSu();
            nhansu.Show();
            this.Hide();
        }

        private void chếĐộThaiSảnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fCĐThaiSan ts = new fCĐThaiSan();
            ts.Show();
            this.Hide();
        }

        private void BHYTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fBHYT bhyt = new fBHYT();
            bhyt.Show();
            this.Hide();
        }

        private void HoSoThuViecStripMenuItem1_Click(object sender, EventArgs e)
        {
            fHSThuViec tv = new fHSThuViec();
            tv.Show();
            this.Hide();
        }

        private void phòngBanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fPhongBan pb = new fPhongBan();
            pb.Show();
            this.Hide();
        }

        private void bộPhậnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fBoPhan bp = new fBoPhan();
            bp.Show();
            this.Hide();
        }

        private void bảngLươngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fBangLuong bl = new fBangLuong();
            bl.Show();
            this.Hide();
        }

        private void lươngNhânViênToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fLuongNV lnv = new fLuongNV();
            lnv.Show();
            this.Hide();
        }

        private void lươngThửViệcToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fLuongThuViec ltv = new fLuongThuViec();
            ltv.Show();
            this.Hide();
        }
    }
}
