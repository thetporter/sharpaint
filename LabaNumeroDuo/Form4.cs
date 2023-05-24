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
    public partial class Form4 : Form
    {
        public Form1 ReturnForm;
        private Size ReturnedSize = new Size();

        public Form4()
        {
            InitializeComponent();
        }

        public void SetSize(Size Recsize)
        {
            if ((Recsize.Width == 800)&&(Recsize.Height == 600)) this.radioButton3.Checked = true;
            else if ((Recsize.Width == 640) && (Recsize.Height == 480)) this.radioButton2.Checked = true;
            else if ((Recsize.Width == 320) && (Recsize.Height == 240)) this.radioButton1.Checked = true;
            else
            {
                this.radioButton4.Checked = true;
                this.numericUpDown1.Enabled = true; this.numericUpDown2.Enabled = true;
                this.numericUpDown1.Value = Recsize.Width; this.numericUpDown2.Value = Recsize.Height;
            }
            ReturnedSize = Recsize;
        }

        private void CheckWasMoved1(object sender, EventArgs e)
        {
            ReturnedSize.Width = 320; ReturnedSize.Height = 240;
            numericUpDown1.Enabled = false; numericUpDown2.Enabled = false;
        }

        private void CheckWasMoved2(object sender, EventArgs e)
        {
            ReturnedSize.Width = 640; ReturnedSize.Height = 480;
            numericUpDown1.Enabled = false; numericUpDown2.Enabled = false;
        }

        private void CheckWasMoved3(object sender, EventArgs e)
        {
            ReturnedSize.Width = 800; ReturnedSize.Height = 600;
            numericUpDown1.Enabled = false; numericUpDown2.Enabled = false;
        }

        private void CheckWasChanged4(object sender, EventArgs e)
        {
            numericUpDown1.Enabled = true; numericUpDown2.Enabled = true;
        }

        private void Val1Edited(object sender, EventArgs e)
        {
            ReturnedSize.Width = (int)numericUpDown1.Value;
        }

        private void Val2Edited(object sender, EventArgs e)
        {
            ReturnedSize.Height = (int)numericUpDown2.Value;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ReturnForm.CreatedSize = ReturnedSize;
            this.Close();
        }
    }
}
