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
     public partial class frmPrintBagOfTest : Form
     {
          private int _contestID;
          public int _TypeShow;
          private int _locationID { get; set; }
          public frmPrintBagOfTest(int Contestid, int LocationID)
          {
               InitializeComponent();
               this._contestID = Contestid;

               this._locationID = LocationID;
          }
          protected override void OnLoad(EventArgs e)
          {
               base.OnLoad(e);
               this.ControlBox = false;
               this.WindowState = FormWindowState.Maximized;
               this.BringToFront();
          }
          public void InitControl()
          {
               pnlMain.Controls.Clear();

               Control.ucPrintBagOfTest uc = new Control.ucPrintBagOfTest(_contestID, _locationID);
               uc.Dock = DockStyle.Fill;
               pnlMain.Controls.Add(uc);


               this.Update();
          }
          private void frmPrintBagOfTest_Load(object sender, EventArgs e)
          {
               InitControl();
          }
     }
}
