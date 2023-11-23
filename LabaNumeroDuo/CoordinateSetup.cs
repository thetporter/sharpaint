using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LabaNumeroDuo
{
    public partial class CoordinateSetup : Form
    {
        Form1 home;
        public CoordinateSetup(Form1 home)
        {
            InitializeComponent();
            this.home = home;
            checkBox1.Checked = home.ShowCoordinates;
            checkBox2.Checked = home.SnapToCoordinates;
            COffsetSetter.Value = home.CScale;
            checkBox2.Enabled = checkBox1.Checked;
            button1.Enabled = checkBox1.Checked;
            COffsetSetter.Enabled = checkBox1.Checked;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            home.ShowCoordinates = checkBox1.Checked;
            home.CoordinateFlipper.Checked = checkBox1.Checked;
            checkBox2.Enabled = checkBox1.Checked;
            button1.Enabled = checkBox1.Checked;
            COffsetSetter.Enabled = checkBox1.Checked;
            foreach (Form2 f in home.MdiChildren) f.Refresh();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            home.SnapToCoordinates = checkBox2.Checked;
            home.SnapFlipper.Checked = checkBox2.Checked;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (Form2 f in home.MdiChildren)
            {
                f.SnapAll();
                f.Refresh();
            }
        }

        private void COffsetSetter_ValueChanged(object sender, EventArgs e)
        {
            home.CScale = (int)COffsetSetter.Value;
            foreach (Form2 f in home.MdiChildren) f.Refresh();
        }

        private void CoordinateSetup_FormClosing(object sender, FormClosingEventArgs e)
        {
            home.CForm = null;
        }
    }
}
