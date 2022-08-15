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
    public partial class fDangNhap : Form
    {
        public static string TenDN = "";
        public fDangNhap()
        {
            InitializeComponent();
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-42TS2U7;Initial Catalog=DB_RAPCHIEUPHIM;Integrated Security=True");
            try
            {

                conn.Open();
                string tk = txtTKDN.Text;
                TenDN = txtTKDN.Text;
                string mk = txtDN.Text;
                string sqL = "Select * from TAIKHOAN where TENDN='" + tk + "'and MATKHAU='" + mk + "'";
                SqlCommand cmd = new SqlCommand(sqL, conn);
                SqlDataReader dta = cmd.ExecuteReader();
                if (dta.Read() == true)
                {
                    MessageBox.Show("Đăng nhập thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    this.Hide();
                    fPhanQuyen frm = new fPhanQuyen();
                    frm.ShowDialog();

                }
                else
                {
                    MessageBox.Show("Đăng nhập thất bại");
                }
            }
            catch (Exception)
            {

                MessageBox.Show("lỗi đăng nhập");
            }
        }

    }
}
