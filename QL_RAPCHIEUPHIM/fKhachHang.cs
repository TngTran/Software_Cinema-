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
    public partial class fKhachHang : Form
    {
        
        SqlConnection ketnoi;
        SqlCommand cmd;
        string chuoiketnoi = @"Data Source=DESKTOP-42TS2U7;Initial Catalog=DB_RAPCHIEUPHIM;Integrated Security=True";
        int i = 0;
        SqlDataReader docdl;
        SqlDataAdapter da = new SqlDataAdapter();
        DataTable table = new DataTable();
        public fKhachHang()
        {
            InitializeComponent();
        }
        ThemXoaSua t = new ThemXoaSua();

        private void fKhachHang_Load(object sender, EventArgs e)
        {
            ketnoi =new SqlConnection(chuoiketnoi);
            ketnoi.Open();
            HienThi();

        }
        public void HienThi()
        {
            cmd = ketnoi.CreateCommand();
            cmd.CommandText = "select *from KHACHHANG ";
            da.SelectCommand = cmd;
            table.Clear();
            da.Fill(table);
            dgvKH.DataSource = table;

        }
        public static String Makh = "";
        private void dgvKH_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = 0;
            i = dgvKH.CurrentRow.Index;
            txtMaKH.Text = dgvKH.Rows[i].Cells[0].Value.ToString();
            txtTenKH.Text = dgvKH.Rows[i].Cells[1].Value.ToString();
            dtpKH.Text = dgvKH.Rows[i].Cells[2].Value.ToString();
            cboGioiTinh.Text = dgvKH.Rows[i].Cells[3].Value.ToString();
            txtCMND.Text = dgvKH.Rows[i].Cells[4].Value.ToString();
            txtDT.Text = dgvKH.Rows[i].Cells[5].Value.ToString();
            txtDiaChi.Text = dgvKH.Rows[i].Cells[6].Value.ToString();
            txtSLX.Text = dgvKH.Rows[i].Cells[7].Value.ToString();
            // Lấy MaKH
            Makh = txtMaKH.Text;
            
        }
        public string CreateAutoID()
        {
            cmd = ketnoi.CreateCommand();
            cmd.CommandText = "SELECT dbo.FUNC_MAKH()";
            return cmd.ExecuteScalar().ToString();
        }
        private void BTNtHEM_Click(object sender, EventArgs e)
        {
            txtMaKH.Text = CreateAutoID();
            txtTenKH.Text = "";
            dtpKH.Text = "";
            cboGioiTinh.Text = "";
            txtCMND.Text = "";
            txtDT.Text = "";
            txtDiaChi.Text = "";
            txtSLX.Text = "";
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnLuu.Enabled = true;
            
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                cmd = ketnoi.CreateCommand();
                cmd.CommandText = "PRO_DELETEKHACHHANG";
                cmd.Parameters.AddWithValue("@MAKH", txtMaKH.Text);
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
                    fDelete_KH_Ve f = new fDelete_KH_Ve();
                    f.Show();
                }
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                cmd = ketnoi.CreateCommand();
                cmd.CommandText = "PRO_UPDATEKHACHHANG";
                cmd.Parameters.AddWithValue("@MAKH", txtMaKH.Text);
                cmd.Parameters.AddWithValue("@TENKH", txtTenKH.Text);
                cmd.Parameters.AddWithValue("@NGAYSINH", DateTime.Parse(dtpKH.Text));
                cmd.Parameters.AddWithValue("@GIOITINH", cboGioiTinh.Text);
                cmd.Parameters.AddWithValue("@CMND", txtCMND.Text);
                cmd.Parameters.AddWithValue("@DIENTHOAI", txtDT.Text);
                cmd.Parameters.AddWithValue("@DIACHI", txtDiaChi.Text);
                cmd.Parameters.AddWithValue("@SOLANXEM", txtSLX.Text);
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

        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                cmd = ketnoi.CreateCommand();
                cmd.CommandText = "PRO_INSERTKHACHHANG";
                cmd.Parameters.AddWithValue("@MAKH", txtMaKH.Text);
                cmd.Parameters.AddWithValue("@TENKH", txtTenKH.Text);
                cmd.Parameters.AddWithValue("@NGAYSINH",DateTime.Parse(dtpKH.Text));
                cmd.Parameters.AddWithValue("@GIOITINH", cboGioiTinh.Text);
                cmd.Parameters.AddWithValue("@CMND", txtCMND.Text);
                cmd.Parameters.AddWithValue("@DIENTHOAI", txtDT.Text);
                cmd.Parameters.AddWithValue("@DIACHI", txtDiaChi.Text);
                cmd.Parameters.AddWithValue("@SOLANXEM", txtSLX.Text);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
                MessageBox.Show("Lưu dữ liệu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                HienThi();
                btnSua.Enabled = true;
                btnXoa.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi! \n\n" + ex.Message, "Thông báo", MessageBoxButtons.OKCancel,MessageBoxIcon.Asterisk);
                HienThi();
                btnSua.Enabled = true;
                btnXoa.Enabled = true;
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {

            
            cmd = ketnoi.CreateCommand();
            cmd.CommandText = "select *from dbo.FUNC_TIMKIEM_KHACHHANG(@MAKH) ";
            cmd.Parameters.AddWithValue("@MAKH", txtTimKiem.Text.ToString());
            da.SelectCommand = cmd;
            table.Clear();
            da.Fill(table);
            dgvKH.DataSource = table;

   
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            HienThi();
        }

        private void txtCMND_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void txtDT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void txtSLX_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }

    }
}
