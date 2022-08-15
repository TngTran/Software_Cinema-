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
    public partial class fDelete_Quyen_TK : Form
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
        public fDelete_Quyen_TK()
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
            cmd.CommandText = "SELECT * FROM TAIKHOAN WHERE MAQUYEN = '" + fNhomQuyen.Ma + "'";
            da.SelectCommand = cmd;
            table.Clear();
            da.Fill(table);
            dgvCaChieu.DataSource = table;
            //select * from VE
  
        }


        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                cmd = ketnoi.CreateCommand();
                cmd.CommandText = "DELETE FROM TAIKHOAN WHERE TENDN = '"+ dgvCaChieu.Rows[i].Cells[0].Value.ToString()+"'";
                cmd.ExecuteNonQuery();
                MessageBox.Show("Xóa dữ liệu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                HienThi();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi! \n\n" + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvCaChieu_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int i;
            i = dgvCaChieu.CurrentRow.Index;
        }



    }
}
