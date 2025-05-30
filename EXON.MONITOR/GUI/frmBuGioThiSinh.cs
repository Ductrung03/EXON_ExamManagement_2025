using EXON.MONITOR.Common;
using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace EXON.MONITOR.GUI
{
     public partial class frmBuGioThiSinh : MetroForm
     {
          public delegate void SendDateTime(int ThoiGianGianDoan, string Note);

          public event SendDateTime Sender;
          public frmBuGioThiSinh()
          {
               InitializeComponent();
               textBox1.Text = "5";
          }

          private void mbtnInput_Click(object sender, EventArgs e)
          {
               if (txtNote.Text != "")
               {
                    int ThoiGianGianDoan = 0;

                    if (Convert.ToInt32(textBox1.Text.ToString()) >= 0 )
                    {

                         ThoiGianGianDoan = DatetimeConvert.ConvertDateTimeToUnix(DatetimeConvert.GetDateTimeServer()) - Convert.ToInt32(textBox1.Text.ToString())*60;
                         Sender(ThoiGianGianDoan, txtNote.Text);
                         this.Close();
                    }
                    else
                    {
                         MessageBox.Show("Thời gian bù giờ không hợp lệ !");
                    }
               }
               else
               {
                    MessageBox.Show("Nguyên nhân không được để trống!");
               }
          }

          private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
          {
               if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
               {
                    e.Handled = true;
               }
          }
     }
}
