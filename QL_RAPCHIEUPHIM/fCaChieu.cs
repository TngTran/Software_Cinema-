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
    public partial class fCaChieu : Form
    {

        SqlConnection ketnoi;

        SqlCommand cmd;
        string chuoiketnoi = @"Data Source=DESKTOP-42TS2U7;Initial Catalog=DB_RAPCHIEUPHIM;Integrated Security=True";
        int i = 0;
        SqlDataReader docdl;
        SqlDataAdapter da = new SqlDataAdapter();
        DataTable table = new DataTable();
        public fCaChieu()
        {
            InitializeComponent();
        }

        public void HienThi()
        {
            cmd = ketnoi.CreateCommand();
            cmd.CommandText = "PRO_GETCACHIEU";
            cmd.CommandType = CommandType.StoredProcedure;
            da.SelectCommand = cmd;
            table.Clear();
            da.Fill(table);
            dgvCaChieu.DataSource = table;
            //select * from CACHIEU
            txtMaCaChieu.Enabled = false;
        }

        private void fCaChieu_Load_1(object sender, EventArgs e)
        {
            ketnoi = new SqlConnection(chuoiketnoi);
            ketnoi.Open();
            HienThi();
        }
        public static String MaCaChieu = "";
        private void dgvCaChieu_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int i;
            i = dgvCaChieu.CurrentRow.Index;
            txtMaCaChieu.Text = dgvCaChieu.Rows[i].Cells[0].Value.ToString();
            dtpGioBD.Text = dgvCaChieu.Rows[i].Cells[1].Value.ToString();
            dtpGioKT.Text = dgvCaChieu.Rows[i].Cells[2].Value.ToString();
            txtCuoiTuan.Text = dgvCaChieu.Rows[i].Cells[3].Value.ToString();
            txtPhuThuCaChieu.Text = dgvCaChieu.Rows[i].Cells[4].Value.ToString();
            //Lấy macachieu
            MaCaChieu = txtMaCaChieu.Text;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            txtMaCaChieu.Text = CreateAutoID();
            dtpGioBD.Text = "";
            dtpGioKT.Text = "";
            txtCuoiTuan.Text = "";
            txtPhuThuCaChieu.Text = "";
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnLuu.Enabled = true;
            txtMaCaChieu.Focus();
        }
        public string CreateAutoID()
        {
            cmd = ketnoi.CreateCommand();
            cmd.CommandText = "SELECT dbo.FUNC_MACACHIEU()";
            return cmd.ExecuteScalar().ToString();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                cmd = ketnoi.CreateCommand();
                cmd.CommandText = "PRO_INSERTCACHIEU";
                cmd.Parameters.AddWithValue("@MACACHIEU", txtMaCaChieu.Text);
                cmd.Parameters.AddWithValue("@GIOBD", dtpGioBD.Text);
                cmd.Parameters.AddWithValue("@GIOKT", dtpGioKT.Text);
                cmd.Parameters.AddWithValue("@CUOITUAN", txtCuoiTuan.Text);
                cmd.Parameters.AddWithValue("@PHUTHUCACHIEU", txtPhuThuCaChieu.Text);
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
                cmd.CommandText = "PRO_UPDATECACHIEU";
                cmd.Parameters.AddWithValue("@MACACHIEU", txtMaCaChieu.Text);
                cmd.Parameters.AddWithValue("@GIOBD", dtpGioBD.Text);
                cmd.Parameters.AddWithValue("@GIOKT", dtpGioKT.Text);
                cmd.Parameters.AddWithValue("@CUOITUAN", txtCuoiTuan.Text);
                cmd.Parameters.AddWithValue("@PHUTHUCACHIEU", txtPhuThuCaChieu.Text);
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
                cmd.CommandText = "PRO_DELETECACHIEU";
                cmd.Parameters.AddWithValue("@MACACHIEU", txtMaCaChieu.Text);
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
                    fDelete_CC_Ve f = new fDelete_CC_Ve();
                    f.Show();
                }
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            try
            {
                cmd = ketnoi.CreateCommand();
                DataTable table = new DataTable();
                
                if(rbtcuoituan.Checked)
                {
                    cmd.CommandText = "SELECT * FROM CACHIEU WHERE CUOITUAN = '" + txtTimKiem.Text +"' ";
                    da.SelectCommand = cmd;
                    table.Clear();
                    da.Fill(table);
                    dgvCaChieu.DataSource = table;
                }
                else if(rbtPhuThu.Checked)
                {
                    cmd.CommandText = "SELECT * FROM CACHIEU WHERE PHUTHUCACHIEU = '" + txtTimKiem.Text +"' ";
                    da.SelectCommand = cmd;
                    table.Clear();
                    da.Fill(table);
                    dgvCaChieu.DataSource = table;
                }   
                else
                {
                    cmd.CommandText = "SELECT * FROM dbo.FUNC_TIMKIEM_CACHIEU (@MACACHIEU)";
                    cmd.Parameters.AddWithValue("@MACACHIEU", txtTimKiem.Text);
                    da.SelectCommand = cmd;
                    table.Clear();
                    da.Fill(table);
                    dgvCaChieu.DataSource = table;
                } 
             }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi! \n\n" + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            HienThi();
        }

        private void txtPhuThuCaChieu_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        //CURSOR IN DS CA CHIEU CUOI TUAN = 1
        //CURSOR IN DS TAI KHOAN WHERE MAQUYEN = 002
    }
}
