using EXON.SubData.Services;
using EXON.SubModel.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyHoiDongThiVer2.GUI
{
    public partial class FrmDoiMatKhau : MetroFramework.Forms.MetroForm
    {
        private MTAQuizDbContext db = DAO.DBService.db;
        private EXAMINATIONCOUNCIL_STAFFS tg = new EXAMINATIONCOUNCIL_STAFFS();

        #region constructor
        public FrmDoiMatKhau()
        {
            InitializeComponent();
            DAO.DBService.Reload();
        }

        public FrmDoiMatKhau(EXAMINATIONCOUNCIL_STAFFS pc)
        {
            
            InitializeComponent();
            DAO.DBService.Reload();
            tg = pc;
            this.KeyPreview = true;
        }
        #endregion

        #region sự kiện
        private void btnDoiMatKhau_Click(object sender, EventArgs e)
        {
            if (txtMatKhauCu.Text == "")
            {
                MessageBox.Show("Mật khẩu cũ không được để trống", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (txtMatKhauMoi.Text == "")
            {
                MessageBox.Show("Mật khẩu mới không được để trống", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (txtMatKhauCu.Text != tg.Password)
            {
                MessageBox.Show("Mật khẩu cũ không chính xác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (txtXacNhan.Text != txtMatKhauMoi.Text)
            {
                MessageBox.Show("Xác nhận mật khẩu không chính xác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            MTAQuizDbContext db = new MTAQuizDbContext();
            ExaminationcouncilStaffService _ExaminationcouncilStaffService = new ExaminationcouncilStaffService();
            EXAMINATIONCOUNCIL_STAFFS exs = _ExaminationcouncilStaffService.GetByStaffID((int)tg.StaffID);
            exs = db.EXAMINATIONCOUNCIL_STAFFS.SingleOrDefault(b => b.StaffID == tg.StaffID);
            exs.Password = txtMatKhauMoi.Text;
            //db.Entry(tg).State = System.Data.Entity.EntityState.Modified;
            //db.EXAMINATIONCOUNCIL_STAFFS.Attach(tg);
            db.SaveChanges();

            MessageBox.Show("Đổi mật khẩu thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void FrmDoiMatKhau_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                btnDoiMatKhau_Click(sender, e);
            }
        }
        #endregion

        
    }
}
