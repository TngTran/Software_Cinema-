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
    public partial class fMainNV : Form
    {
        public fMainNV()
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

        private void btnVe_Click(object sender, EventArgs e)
        {
            moFormCon(new fVe());
            lbl_tieude.Text = btnVe.Text;
        }

        private void btnDangXuat_Click(object sender, EventArgs e)
        {
            this.Close();
            fPhanQuyen f = new fPhanQuyen();
            f.Show();
        }

    }
}


