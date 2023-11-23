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
    public partial class Form5 : Form
    {
        public Form1 receiver;
        public Form5(Form1 R)
        {
            InitializeComponent();
            receiver = R;
            comboBox1.SelectedIndex = receiver.ActiveFigure;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            receiver.UncheckTools();
            switch (comboBox1.SelectedIndex)
            {
                case 0: receiver.ActiveFigure = 1; receiver.SquareTool.Enabled = true; break;
                case 1: receiver.ActiveFigure = 2; receiver.EllipseTool.Enabled = true; break;
                case 2: receiver.ActiveFigure = 3; receiver.LineTool.Enabled = true; break;
                case 3: receiver.ActiveFigure = 4; receiver.CurveTool.Enabled = true; break;
                case 4: receiver.ActiveFigure = 5; receiver.TextTool.Enabled = true; break;
            }
        }
    }
}
