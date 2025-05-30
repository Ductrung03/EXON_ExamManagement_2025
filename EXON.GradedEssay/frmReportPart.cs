using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EXON.GradedEssay
{
     public partial class frmReportPart : Form
     {
          private int _contestID;
          private int _locationID { get; set; }
          public frmReportPart(int Contestid, int LocationID)
          {
               InitializeComponent();
               this._contestID = Contestid;
               this._locationID = LocationID;
          }
          public void InitControl()
          {
                   pnlMain.Controls.Clear();
                   Control.ucReportPart uc = new Control.ucReportPart(_contestID, _locationID);
                    uc.Dock = DockStyle.Fill;
                    pnlMain.Controls.Add(uc);
                    this.Update();
               
          }
          protected override void OnLoad(EventArgs e)
          {
               base.OnLoad(e);
               this.ControlBox = false;
               this.WindowState = FormWindowState.Maximized;
               this.BringToFront();
          }

          private void frmReportPart_Load(object sender, EventArgs e)
          {
               InitControl();
          }
     }
}
