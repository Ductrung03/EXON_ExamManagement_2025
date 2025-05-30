using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static EXON.MONITOR.GUI.frmXemBaiLam;

namespace EXON.MONITOR.GUI
{
    public partial class frmChangeHeightAns : MetroFramework.Forms.MetroForm
    {
        SubData.MTAQuizDbContext DB = new SubData.MTAQuizDbContext();
        int ansid;
        public frmChangeHeightAns(int AnsID)
        {
            InitializeComponent();
            ansid = AnsID;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
DB.ANSWERS.Find(ansid).HeightToDisplay = int.Parse(textBox1.Text);
            DB.SaveChanges();this.Dispose();
                MessageBox.Show("Thay đổi độ cao thành công!");
            } catch(Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.ToString());
            }
            
            
        }

        private void frmChangeHeightAns_Load(object sender, EventArgs e)
        {
            
            label3.Text = DB.ANSWERS.Find(ansid).HeightToDisplay.ToString();
        }
    }
}
