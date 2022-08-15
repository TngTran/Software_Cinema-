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
    public partial class fNhomQuyen : Form
    {

        SqlConnection ketnoi;

        SqlCommand cmd;
        string chuoiketnoi = @"Data Source=DESKTOP-42TS2U7;Initial Catalog=DB_RAPCHIEUPHIM;Integrated Security=True";
        int i = 0;
        SqlDataReader docdl;
        SqlDataAdapter da = new SqlDataAdapter();
        DataTable table = new DataTable();
        public fNhomQuyen()
        {
            InitializeComponent();
        }
        public void HienThi()
        {
            cmd = ketnoi.CreateCommand();
            cmd.CommandText = "SELECT * FROM QUYEN";
            da.SelectCommand = cmd;
            table.Clear();
            da.Fill(table);
            dgvCaChieu.DataSource = table;

        }

        private void fCaChieu_Load_1(object sender, EventArgs e)
        {
            ketnoi = new SqlConnection(chuoiketnoi);
            ketnoi.Open();
            HienThi();
        }
        public static String Ma = "";
        private void dgvCaChieu_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int i;
            i = dgvCaChieu.CurrentRow.Index;
            txtMaQuyen.Text = dgvCaChieu.Rows[i].Cells[0].Value.ToString();
            txtTenQuyen.Text = dgvCaChieu.Rows[i].Cells[1].Value.ToString();
            //Lay maquyen
            Ma = txtMaQuyen.Text;

        }
        public string CreateAutoID()
        {
            cmd = ketnoi.CreateCommand();
            cmd.CommandText = "SELECT dbo.FUNC_MAQUYEN()";
            return cmd.ExecuteScalar().ToString();
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            txtMaQuyen.Text = CreateAutoID();
            txtTenQuyen.Text = "";
            txtMaQuyen.Focus();
        }


        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                cmd = ketnoi.CreateCommand();
                cmd.CommandText = "INSERT INTO QUYEN VALUES ('" + txtMaQuyen.Text + "', '" + txtTenQuyen.Text + "')";
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
                cmd.CommandText = "UPDATE QUYEN SET TENQUYEN = '" + txtTenQuyen.Text + "'  WHERE MAQUYEN = '" + txtMaQuyen.Text + "'";
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
                cmd.CommandText = "DELETE FROM QUYEN WHERE MAQUYEN = '" + txtMaQuyen.Text + "'";
                cmd.ExecuteNonQuery();
                MessageBox.Show("Xóa dữ liệu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                HienThi();
            }
            catch (Exception ex)
            {
                DialogResult dialogResult = MessageBox.Show("Đã xảy ra lỗi! \n\n" + ex.Message, "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
                if (dialogResult == DialogResult.OK)
                {
                    fDelete_Quyen_TK f = new fDelete_Quyen_TK();
                    f.Show();
                }
            }
        }

        
        //CURSOR IN DS CA CHIEU CUOI TUAN = 1
        //CURSOR IN DS TAI KHOAN WHERE MAQUYEN = 002
    }
}
