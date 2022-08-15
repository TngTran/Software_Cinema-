using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace qlrapchieuphim
{
    public partial class fMainAdmin : Form
    {
        public fMainAdmin()
        {
            InitializeComponent();
        }
        public Form currentFormCon;
        public void moFormCon(Form formcon)
        {
            if (currentFormCon != null)
            {
                currentFormCon.Close();
            }
            currentFormCon = formcon;
            formcon.TopLevel = false;
            formcon.FormBorderStyle = FormBorderStyle.None;
            formcon.Dock = DockStyle.Fill;
            panel_body.Controls.Add(formcon);
            panel_body.Tag = formcon;
            formcon.BringToFront();
            formcon.Show();
        }

        private void btnKhachHang_Click(object sender, EventArgs e)
        {
            moFormCon(new fKhachHang());
            lbl_tieude.Text = btnKhachHang.Text;
        }

        private void btnVe_Click(object sender, EventArgs e)
        {
            moFormCon(new fVe());
            lbl_tieude.Text = btnVe.Text;
        }

        private void btnCaChieu_Click(object sender, EventArgs e)
        {
            moFormCon(new fCaChieu());
            lbl_tieude.Text = btnCaChieu.Text;
        }

        private void btnPhim_Click(object sender, EventArgs e)
        {
            moFormCon(new fPhim());
            lbl_tieude.Text = btnPhim.Text;
        }

        private void btnNhanVien_Click(object sender, EventArgs e)
        {
            moFormCon(new fNhanVien());
            lbl_tieude.Text = btnNhanVien.Text;
        }

        private void btnTaiKhoan_Click(object sender, EventArgs e)
        {
            moFormCon(new fTaiKhoan());
            lbl_tieude.Text = btnTaiKhoan.Text;
        }

        private void btnDangXuat_Click(object sender, EventArgs e)
        {
            this.Close();
            fPhanQuyen f = new fPhanQuyen();
            f.Show();
        }

        private void btnNhomQuyen_Click(object sender, EventArgs e)
        {
            moFormCon(new fNhomQuyen());
            lbl_tieude.Text = btnNhomQuyen.Text;
        }

    }
}
