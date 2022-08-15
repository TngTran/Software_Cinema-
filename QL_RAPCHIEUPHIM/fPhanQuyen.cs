using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace qlrapchieuphim
{
    public partial class fPhanQuyen : Form
    {
        SqlConnection ketnoi;
        SqlCommand cmd;
        string chuoiketnoi = @"Data Source=DESKTOP-42TS2U7;Initial Catalog=DB_RAPCHIEUPHIM;Integrated Security=True";
        public fPhanQuyen()
        {
            InitializeComponent();
        }

        private void btnAdmin_Click(object sender, EventArgs e)
        {
            this.Hide();
            fMainAdmin f = new fMainAdmin();
            f.ShowDialog();
        }

        private void btnNhanVien_Click(object sender, EventArgs e)
        {
            this.Hide();
            fMainNV f = new fMainNV();
            f.ShowDialog();
        }
        public string getMaQuyenTaiKhoan()
        {
            cmd = ketnoi.CreateCommand();
            cmd.CommandText = "select MAQUYEN from TAIKHOAN where TenDN = '" + fDangNhap.TenDN + "' ";
            return cmd.ExecuteScalar().ToString();
        }


        private void btnDangXuat_Click(object sender, EventArgs e)
        {
            btnAdmin.Enabled = true;
            btnNhanVien.Enabled = true;
            this.Hide();
            fDangNhap f = new fDangNhap();
            f.ShowDialog();

        }
        // Hàm kiểm tra quyền
        private void fPhanQuyen_Load_1(object sender, EventArgs e)
        {
            ketnoi = new SqlConnection(chuoiketnoi);
            ketnoi.Open();
            if (getMaQuyenTaiKhoan() == "Q001")
            {
                btnNhanVien.Enabled = false;
            }
            else
            {
                btnAdmin.Enabled = false;
            }
        }

    }
}
