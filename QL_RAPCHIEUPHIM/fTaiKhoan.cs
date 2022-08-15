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
using qlrapchieuphim.DAO;


namespace qlrapchieuphim
{
    public partial class fTaiKhoan : Form
    {

        SqlConnection ketnoi;

        SqlCommand cmd;
        string chuoiketnoi = @"Data Source=DESKTOP-42TS2U7;Initial Catalog=DB_RAPCHIEUPHIM;Integrated Security=True";
        int i = 0;
        SqlDataReader docdl;
        SqlDataAdapter da = new SqlDataAdapter();
        DataTable table = new DataTable();
        public fTaiKhoan()
        {
            InitializeComponent();
        }
        ThemXoaSua t = new ThemXoaSua();
        private void Loadcombo()
        {
            DataTable dt = t.docdulieu("select * from QUYEN");;

            cboMaQuyen.DataSource = dt;
            cboMaQuyen.DisplayMember = "MAQUYEN";
        }
        public void HienThi()
        {
            cmd = ketnoi.CreateCommand();
            cmd.CommandText = "SELECT * FROM TAIKHOAN";
            da.SelectCommand = cmd;
            table.Clear();
            da.Fill(table);
            dgvCaChieu.DataSource = table;
            Loadcombo();
        }

        private void fCaChieu_Load_1(object sender, EventArgs e)
        {
            ketnoi = new SqlConnection(chuoiketnoi);
            ketnoi.Open();
            HienThi();
        }
        private void dgvCaChieu_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int i;
            i = dgvCaChieu.CurrentRow.Index;
            txtTenDN.Text = dgvCaChieu.Rows[i].Cells[0].Value.ToString();
            txtMK.Text = dgvCaChieu.Rows[i].Cells[1].Value.ToString();
            cboMaQuyen.Text = dgvCaChieu.Rows[i].Cells[2].Value.ToString();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            txtTenDN.Text = "";
            txtMK.Text = "";
            txtTenDN.Focus();
        }


        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                cmd = ketnoi.CreateCommand();
                cmd.CommandText = "INSERT INTO TAIKHOAN VALUES ('" + txtTenDN.Text + "', '" + txtMK.Text + "', '" + cboMaQuyen.Text + "')";
                cmd.ExecuteNonQuery();
                MessageBox.Show("Lưu dữ liệu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                HienThi();
                btnSua.Enabled = true;
                btnXoa.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi! \n\n" + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                HienThi();
                btnSua.Enabled = true;
                btnXoa.Enabled = true;
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                cmd = ketnoi.CreateCommand();
                cmd.CommandText = "UPDATE TAIKHOAN SET MATKHAU = '" + txtMK.Text + "', MAQUYEN = '" + cboMaQuyen.Text + "' WHERE TENDN = '" + txtTenDN.Text + "'";
                cmd.ExecuteNonQuery();
                MessageBox.Show("Sửa dữ liệu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                HienThi();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi! \n\n" + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                HienThi();
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                cmd = ketnoi.CreateCommand();
                cmd.CommandText = "DELETE FROM TAIKHOAN WHERE TENDN = '" + txtTenDN.Text + "'";
                cmd.ExecuteNonQuery();
                MessageBox.Show("Xóa dữ liệu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                HienThi();
            }
            catch (Exception ex)
            {
                DialogResult dialogResult = MessageBox.Show("Đã xảy ra lỗi! \n\n" + ex.Message, "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
                if (dialogResult == DialogResult.OK)
                {
                    fDelete_CC_Ve f = new fDelete_CC_Ve();
                    f.Show();
                }
            }
        }

        
        //CURSOR IN DS CA CHIEU CUOI TUAN = 1
        //CURSOR IN DS TAI KHOAN WHERE MAQUYEN = 002
    }
}
