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
    public partial class fVe : Form
    {
        SqlConnection ketnoi;

        SqlCommand cmd;
        string chuoiketnoi = @"Data Source=DESKTOP-42TS2U7;Initial Catalog=DB_RAPCHIEUPHIM;Integrated Security=True";
        int i = 0;
        SqlDataReader docdl;
        SqlDataAdapter da = new SqlDataAdapter();
        SqlDataAdapter da2 = new SqlDataAdapter();
        DataTable table = new DataTable();
        DataTable table_Dgv2 = new DataTable();
        public fVe()
        {
            InitializeComponent();
        }
        ThemXoaSua t = new ThemXoaSua();
        private void Loadcombo()
        {
            DataTable dt = t.docdulieu("select * from KHACHHANG");
            DataTable dt1 = t.docdulieu("select * from CACHIEU");
            DataTable dt2 = t.docdulieu("select * from NHANVIEN");
            DataTable dt3 = t.docdulieu("select * from PHIM");

            cboMaKH.DataSource = dt;
            cboMaKH.DisplayMember = "MAKH";

            cboMaPhim.DataSource = dt3;
            cboMaPhim.DisplayMember = "MAPHIM";

            cboMaCaChieu.DataSource = dt1;
            cboMaCaChieu.DisplayMember = "MACACHIEU";

            cboMaNV.DataSource = dt2;
            cboMaNV.DisplayMember = "MANV";
        }
        private void fVe_Load(object sender, EventArgs e)
        {
            ketnoi = new SqlConnection(chuoiketnoi);
            ketnoi.Open();
            HienThi();
            Loadcombo();
        }
        public void HienThi()
        {
            cmd = ketnoi.CreateCommand();   
            cmd.CommandText = "PRO_GETVE";
            cmd.CommandType = CommandType.StoredProcedure;
            da.SelectCommand = cmd;
            table.Clear();
            da.Fill(table);
            dgvVe.DataSource = table;
            //select * from VE
            txtMaVe.Enabled = false;   
        }

        private void dgvVe_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int i;
            i = dgvVe.CurrentRow.Index;
            txtMaVe.Text = dgvVe.Rows[i].Cells[0].Value.ToString();
            cboMaPhim.Text = dgvVe.Rows[i].Cells[1].Value.ToString();
            cboMaKH.Text = dgvVe.Rows[i].Cells[2].Value.ToString();
            cboMaCaChieu.Text = dgvVe.Rows[i].Cells[3].Value.ToString();
            txtSoGhe.Text = dgvVe.Rows[i].Cells[4].Value.ToString();
            txtGia.Text = dgvVe.Rows[i].Cells[5].Value.ToString();
            cboMaNV.Text = dgvVe.Rows[i].Cells[6].Value.ToString();
            dtpNgayChieu.Text = dgvVe.Rows[i].Cells[7].Value.ToString();  

            //Load dl len datagridview1
            cmd = ketnoi.CreateCommand();
            cmd.CommandText = "SELECT * FROM dbo.FUNC_GETDATAGRIDVIEW(@MAVE)";
            cmd.Parameters.AddWithValue("@MAVE", dgvVe.Rows[i].Cells[0].Value.ToString()); 
            da.SelectCommand = cmd;
            table_Dgv2.Clear();
            da.Fill(table_Dgv2);
            dataGridView1.DataSource = table_Dgv2;
            
        }

        public string CreateAutoID()
        {
            cmd = ketnoi.CreateCommand();
            //cmd.CommandText = @"SELECT CONCAT('VE', RIGHT(CONCAT('00',ISNULL(right(max(MAVE),3),0) + 1),3)) FROM VE where MAVE like 'VE%'";
            //return cmd.ExecuteScalar().ToString();
            cmd.CommandText = "SELECT dbo.FUNC_MAVE()";
            //cmd.CommandType = CommandType.StoredProcedure;
            //SqlCommand cm = new SqlCommand("select dbo.f_getTenLop(@malop)", sqlconnect);
            //cmd.Parameters.AddWithValue("@malop", "L01");
            return cmd.ExecuteScalar().ToString();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            txtMaVe.Text = CreateAutoID();
            cboMaPhim.Text = "";
            cboMaKH.Text = " ";
            cboMaCaChieu.Text = "";
            txtSoGhe.Text = "";
            txtGia.Text = "";
            dtpNgayChieu.Text = "";
            cboMaNV.Text = "";
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnLuu.Enabled = true;
            txtMaVe.Focus();
            Loadcombo();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                cmd = ketnoi.CreateCommand();
                cmd.CommandText = "PRO_INSERTVE";
                cmd.Parameters.AddWithValue("@MAVE", txtMaVe.Text);
                cmd.Parameters.AddWithValue("@MAPHIM", cboMaPhim.Text);
                cmd.Parameters.AddWithValue("@MAKH", cboMaKH.Text);
                cmd.Parameters.AddWithValue("@MACACHIEU", cboMaCaChieu.Text);
                cmd.Parameters.AddWithValue("@SOGHE", txtSoGhe.Text);
                cmd.Parameters.AddWithValue("@GIA", txtGia.Text);
                cmd.Parameters.AddWithValue("@MANV", cboMaNV.Text);
                cmd.Parameters.AddWithValue("@NGAYCHIEU", DateTime.Parse(dtpNgayChieu.Text));
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
                cmd.CommandText = "PRO_UPDATEVE";
                cmd.Parameters.AddWithValue("@MAVE", txtMaVe.Text);
                cmd.Parameters.AddWithValue("@MAPHIM", cboMaPhim.Text);
                cmd.Parameters.AddWithValue("@MAKH", cboMaKH.Text);
                cmd.Parameters.AddWithValue("@MACACHIEU", cboMaCaChieu.Text);
                cmd.Parameters.AddWithValue("@SOGHE", txtSoGhe.Text);
                cmd.Parameters.AddWithValue("@GIA", txtGia.Text);
                cmd.Parameters.AddWithValue("@MANV", cboMaNV.Text);
                cmd.Parameters.AddWithValue("@NGAYCHIEU", DateTime.Parse(dtpNgayChieu.Text));
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
                cmd.CommandText = "PRO_DELETEVE";
                cmd.Parameters.AddWithValue("@MAVE", txtMaVe.Text);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
                MessageBox.Show("Xóa dữ liệu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                HienThi();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi! \n\n" + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        private void txtGia_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void dataGridView1_Scroll(object sender, ScrollEventArgs e)
        {
            
        }

        private void btnXuatExcel_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Nhờ bạn nào xây dựng giúp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

    }
}
