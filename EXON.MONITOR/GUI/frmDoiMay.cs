using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework.Forms;
using EXON.SubData.Services;
using EXON.SubModel.Models;
using EXON.MONITOR.Report;
namespace EXON.MONITOR.GUI
{
    public partial class frmDoiMay : MetroForm
    {
        private IDivisionShiftService _DivisionShiftService;
        public BindingSource source;
        public int divisionShiftID;
        public frmDoiMay(int _dvsID)
        {
            InitializeComponent();
            DIVISION_SHIFTS ds = new DIVISION_SHIFTS();
            _DivisionShiftService = new DivisionShiftService();
            ds = _DivisionShiftService.GetById(_dvsID);
            divisionShiftID = _dvsID;
            txtKyThi.Text = ds.ROOMTEST.LOCATION.CONTEST.ContestName.ToUpper();
            txtCaThi.Text = ds.SHIFT.ShiftName.ToUpper();
            BindingList<CHANGECOMPUTER_LOG> lstJR = new BindingList<CHANGECOMPUTER_LOG>();
            ViolationService _ViolationService = new ViolationService();
            int dautien = 0;
            List<CHANGECOMPUTER_LOG> lstVio = _ViolationService.GetChangeComputerHistoryRecords(_dvsID).ToList();
            foreach(var i in lstVio)
            {
                if (dautien == 0)
                {
                    txtMonThi.Text = i.ProjectName;
                    
                }
                    
                lstJR.Add(i);
            }
            dGVDoiMay.AutoGenerateColumns = false;
            source = new BindingSource(lstJR, null);
            dGVDoiMay.DataSource = source;
        }

        private void frmDoiMay_Load(object sender, EventArgs e)
        {
  
        }

        private void btnBaoCao_Click(object sender, EventArgs e)
        {
            frmBienBanDoiMay frm = new frmBienBanDoiMay(divisionShiftID, source);
            frm.Show();
        }

        private void txtKyThi_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblCaThi_Click(object sender, EventArgs e)
        {

        }

        private void txtMonThi_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void dGVDoiMay_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void txtCaThi_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
