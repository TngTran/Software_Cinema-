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
    public partial class fPhim : Form
    {
        SqlConnection ketnoi;

        SqlCommand cmd;
        string chuoiketnoi = @"Data Source=DESKTOP-42TS2U7;Initial Catalog=DB_RAPCHIEUPHIM;Integrated Security=True";
        int i = 0;
        SqlDataReader docdl;
        SqlDataAdapter da = new SqlDataAdapter();
        DataTable table = new DataTable();
        public fPhim()
        {
            InitializeComponent();
        }
        ThemXoaSua t = new ThemXoaSua();


        public void HienThi()
        {
            
            cmd = ketnoi.CreateCommand();
            cmd.CommandText = "select *from PHIM ";
            da.SelectCommand = cmd;
            table.Clear();
            da.Fill(table);
            dgvPhim.DataSource = table;
            txtMaPhim.Enabled = false;

        }
        public static String MaPhim = "";
        private void dgvPhim_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = 0;
            i = dgvPhim.CurrentRow.Index;
            txtMaPhim.Text = dgvPhim.Rows[i].Cells[0].Value.ToString();
            txtTenPhim.Text = dgvPhim.Rows[i].Cells[1].Value.ToString();
            txtDaoDien.Text = dgvPhim.Rows[i].Cells[2].Value.ToString();
            txtND.Text = dgvPhim.Rows[i].Cells[3].Value.ToString();
            txtTheLoai.Text = dgvPhim.Rows[i].Cells[4].Value.ToString();
            txtNuocSX.Text = dgvPhim.Rows[i].Cells[5].Value.ToString();
            txtNamSX.Text = dgvPhim.Rows[i].Cells[6].Value.ToString();
            txtTL.Text = dgvPhim.Rows[i].Cells[7].Value.ToString();
            // Lay MAPHIM
            MaPhim = txtMaPhim.Text;
        }

        public string CreateAutoID()
        {
            cmd = ketnoi.CreateCommand();
            cmd.CommandText = "SELECT dbo.FUNC_MAPHIM()";
            return cmd.ExecuteScalar().ToString();
        }
        private void BTNtHEM_Click(object sender, EventArgs e)
        {
            txtMaPhim.Text = CreateAutoID();
            txtTenPhim.Text = "";
            txtDaoDien.Text = "";
            txtND.Text = "";
            txtTheLoai.Text = "";
            txtNuocSX.Text = "";
            txtNamSX.Text = "";
            txtTL.Text = "";
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnLuu.Enabled = true;
           
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                cmd = ketnoi.CreateCommand();
                cmd.CommandText = "PRO_INSERTPHIM";
                cmd.Parameters.AddWithValue("@MAPHIM", txtMaPhim.Text);
                cmd.Parameters.AddWithValue("@TENPHIM", txtTenPhim.Text);
                cmd.Parameters.AddWithValue("@DAODIEN", txtDaoDien.Text);
                cmd.Parameters.AddWithValue("@NOIDUNG", txtND.Text);
                cmd.Parameters.AddWithValue("@THELOAI", txtTheLoai.Text);
                cmd.Parameters.AddWithValue("@NUOCSX", txtNuocSX.Text);
                cmd.Parameters.AddWithValue("@NAMSX", txtNamSX.Text);
                cmd.Parameters.AddWithValue("@THOILUONG", txtTL.Text);
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
                cmd.CommandText = "PRO_UPDATEPHIM";
                cmd.Parameters.AddWithValue("@MAPHIM", txtMaPhim.Text);
                cmd.Parameters.AddWithValue("@TENPHIM", txtTenPhim.Text);
                cmd.Parameters.AddWithValue("@DAODIEN", txtDaoDien.Text);
                cmd.Parameters.AddWithValue("@NOIDUNG", txtND.Text);
                cmd.Parameters.AddWithValue("@THELOAI", txtTheLoai.Text);
                cmd.Parameters.AddWithValue("@NUOCSX", txtNuocSX.Text);
                cmd.Parameters.AddWithValue("@NAMSX", txtNamSX.Text);
                cmd.Parameters.AddWithValue("@THOILUONG", txtTL.Text);

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
                cmd.CommandText = "PRO_DELETEPHIM";
                cmd.Parameters.AddWithValue("@MAPHIM", txtMaPhim.Text);
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

        private void btnLoad_Click(object sender, EventArgs e)
        {
            HienThi();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            cmd = ketnoi.CreateCommand();
            cmd.CommandText = "select *from dbo.FUNC_TIMKIEM_PHIM(@MAPHIM) ";
            cmd.Parameters.AddWithValue("@MAPHIM", txtTimKiem.Text.ToString());
            da.SelectCommand = cmd;
            table.Clear();
            da.Fill(table);
            dgvPhim.DataSource = table;
        }

        private void fPhim_Load(object sender, EventArgs e)
        {
            ketnoi = new SqlConnection(chuoiketnoi);
            ketnoi.Open();
            HienThi();
            
        }

        private void txtNamSX_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void txtTL_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }
    }
}
