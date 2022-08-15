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
    public partial class fDelete_PHIM_Ve : Form
    {
        SqlConnection ketnoi;

        SqlCommand cmd;
        string chuoiketnoi = @"Data Source=DESKTOP-42TS2U7;Initial Catalog=DB_RAPCHIEUPHIM;Integrated Security=True";
        int i = 0;
        SqlDataAdapter da = new SqlDataAdapter();
        SqlDataAdapter da2 = new SqlDataAdapter();
        DataTable table = new DataTable();
        DataTable table_Dgv2 = new DataTable();
        public fDelete_PHIM_Ve()
        {
            InitializeComponent();
        }

        private void fVe_Load(object sender, EventArgs e)
        {
            ketnoi = new SqlConnection(chuoiketnoi);
            ketnoi.Open();
            HienThi();
        }
        public void HienThi()
        {
            cmd = ketnoi.CreateCommand();
            cmd.CommandText = "SELECT * FROM VE WHERE MAPHIM = '" + fPhim.MaPhim + "'";
            da.SelectCommand = cmd;
            table.Clear();
            da.Fill(table);
            dgvVe1.DataSource = table;
        }

        private void dgvVe_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int i;
            i = dgvVe1.CurrentRow.Index;

            //Load dl len datagridview1
            cmd = ketnoi.CreateCommand();
            cmd.CommandText = "SELECT * FROM dbo.FUNC_GETDATAGRIDVIEW(@MAVE)";
            cmd.Parameters.AddWithValue("@MAVE", dgvVe1.Rows[i].Cells[0].Value.ToString());
            da.SelectCommand = cmd;
            table_Dgv2.Clear();
            da.Fill(table_Dgv2);
            dataGridView1.DataSource = table_Dgv2;

        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                cmd = ketnoi.CreateCommand();
                cmd.CommandText = "PRO_DELETEVE";
                cmd.Parameters.AddWithValue("@MAVE", dgvVe1.Rows[i].Cells[0].Value.ToString());
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

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }



    }
}
