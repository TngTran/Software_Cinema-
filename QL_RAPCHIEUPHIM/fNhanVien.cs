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
    public partial class fNhanVien : Form
    {
        SqlConnection ketnoi;

        SqlCommand cmd;
        string chuoiketnoi = @"Data Source=DESKTOP-42TS2U7;Initial Catalog=DB_RAPCHIEUPHIM;Integrated Security=True";
        int i = 0;
        SqlDataReader docdl;
        SqlDataAdapter da = new SqlDataAdapter();
        DataTable table = new DataTable();
        public fNhanVien()
        {
            InitializeComponent();
        }
        ThemXoaSua t = new ThemXoaSua();

        private void fNhanVien_Load(object sender, EventArgs e)
        {
            ketnoi = new SqlConnection(chuoiketnoi);
            ketnoi.Open();
            HienThi();
        }
        public void HienThi()
        {
            cmd = ketnoi.CreateCommand();
            cmd.CommandText = "select *from NHANVIEN ";
            da.SelectCommand = cmd;
            table.Clear();
            da.Fill(table);
            dgvNV.DataSource = table;
            //select * from VE
        }
        public static String Manv = "";
        private void dgvNV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = 0;
            i = dgvNV.CurrentRow.Index;
            txtMaNV.Text = dgvNV.Rows[i].Cells[0].Value.ToString();
            txtTenNV.Text = dgvNV.Rows[i].Cells[1].Value.ToString();
            cboGioiTinh.Text = dgvNV.Rows[i].Cells[2].Value.ToString();
            txtDT.Text = dgvNV.Rows[i].Cells[3].Value.ToString();
            dtpNgaySinh.Text = dgvNV.Rows[i].Cells[4].Value.ToString();
            txtDiaChi.Text = dgvNV.Rows[i].Cells[5].Value.ToString();
            // Lây  MANV
            Manv = txtMaNV.Text;
        }

        public string CreateAutoID()
        {
            cmd = ketnoi.CreateCommand();
            cmd.CommandText = "SELECT dbo.FUNC_MANV()";
            return cmd.ExecuteScalar().ToString();
        }

        private void BTNtHEM_Click(object sender, EventArgs e)
        {
            txtMaNV.Text = CreateAutoID();
            txtTenNV.Text = "";
            cboGioiTinh.Text = "";
            txtDT.Text = "";
            txtDiaChi.Text = "";
            dtpNgaySinh.Text = "";
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnLuu.Enabled = true;

        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                cmd = ketnoi.CreateCommand();
                cmd.CommandText = "PRO_INSERTNHANVIEN";
                cmd.Parameters.AddWithValue("@MANV", txtMaNV.Text);
                cmd.Parameters.AddWithValue("@HOTEN", txtTenNV.Text);
                cmd.Parameters.AddWithValue("@GIOITINH", cboGioiTinh.Text);
                cmd.Parameters.AddWithValue("@DIENTHOAI", txtDT.Text);
                cmd.Parameters.AddWithValue("@DIACHI", txtDiaChi.Text);
                cmd.Parameters.AddWithValue("@NGAYSINH", DateTime.Parse(dtpNgaySinh.Text));
                cmd.CommandType = CommandType.StoredProcedure;
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
                cmd.CommandText = "PRO_UPDATENHANVIEN";
                cmd.Parameters.AddWithValue("@MANV", txtMaNV.Text);
                cmd.Parameters.AddWithValue("@HOTEN", txtTenNV.Text);
                cmd.Parameters.AddWithValue("@GIOITINH", cboGioiTinh.Text);
                cmd.Parameters.AddWithValue("@DIENTHOAI", txtDT.Text);
                cmd.Parameters.AddWithValue("@NGAYSINH", DateTime.Parse(dtpNgaySinh.Text));
                cmd.Parameters.AddWithValue("@DIACHI", txtDiaChi.Text);
                
                cmd.CommandType = CommandType.StoredProcedure;
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
                cmd.CommandText = "PRO_DELETENHANVIEN";
                cmd.Parameters.AddWithValue("@MANV", txtMaNV.Text);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
                MessageBox.Show("Xóa dữ liệu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                HienThi();
            }
            catch (Exception ex)
            {

                DialogResult dialogResult = MessageBox.Show("Đã xảy ra lỗi! \n\n" + ex.Message, "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
                if (dialogResult == DialogResult.OK)
                {
                    fDelete_NV_Ve f = new fDelete_NV_Ve();
                    f.Show();
                }
                
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            HienThi();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            try
            {
                cmd = ketnoi.CreateCommand();
                cmd.CommandText = "select *from dbo.FUNC_TIMKIEM_NHANVIEN(@MANV) ";
                cmd.Parameters.AddWithValue("@MANV", txtTimKiem.Text.ToString());
                da.SelectCommand = cmd;
                table.Clear();
                da.Fill(table);
                dgvNV.DataSource = table;
            }
            catch (Exception ex)
            {

                MessageBox.Show("Đã xảy ra lỗi! \n\n" + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                
            }
        }

        private void txtDT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;

        }
    }
}
