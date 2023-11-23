using System;
using System.Drawing;
using System.Windows.Forms;

namespace LabaNumeroDuo
{
    public partial class Form3 : Form
    {
        public Form3(Form1 home)
        {
            InitializeComponent();
            //Передает Form1, в которую перейдет результат
            this.home = home;
        }

        //Перевод значения с comboBox в цифру
        private int LabelToInt(string Label)
        {

            switch (Label)
            {
                case "1": return 1;
                case "2": return 2;
                case "5": return 5;
                case "8": return 8;
                case "10": return 10;
                case "12": return 12;
                case "15": return 15;
                default: return 1;
            }
        }

        private Form1 home;
        private Graphics g;
        private Rect Example = new Rect();

        private void Form3_Load(object sender, EventArgs e)
        {
            Example = new Rect();
            //Задать прямоугольник
            Example.ResetOrigin(Width - 100, 16);
            Example.MovePointTo(Width - 32, 78);
            Example.FrontColor = home.ActiveColor;
            Example.BackColor = home.BackColor;
            Example.BorderWidth = home.ActiveWidth;
            //Создать графику; нарисовать прямоугольник
            g = CreateGraphics();
            Example.Draw(g);
        }

        //Метод установки значений
        public void SetDefaults(Color main, Color back, int width)
        {
            g = CreateGraphics();
            colorDialogLine.Color = main;
            colorDialogBckg.Color = back;
            comboBox1.SelectedItem = width.ToString();
            comboBox1.Text = width.ToString();
        }

        //Кнопки закрытия
        private void button4_Click(object sender, EventArgs e)
        {
            home.ActiveColor = colorDialogLine.Color;
            if (checkBox1.CheckState == CheckState.Checked)
            {
                home.BackgroundColor = colorDialogBckg.Color;
                home.DoBackground = true; home.FillSelector.Checked = true;
            }
            else { home.DoBackground = false; home.FillSelector.Checked = false; }
            home.ActiveWidth = LabelToInt((string)comboBox1.SelectedItem);
            foreach (Form2 c in home.MdiChildren) { c.UpdatePenData(); }
            this.Close();
        }
        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //Смена переменных отрисовки
        private void button1_Click(object sender, EventArgs e)
        {
            colorDialogLine.ShowDialog();
            Example.Erase(g);
            Example.FrontColor = colorDialogLine.Color;
            Example.Draw(g);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            colorDialogBckg.ShowDialog();
            Example.Erase(g);
            Example.BackColor = colorDialogBckg.Color;
            Example.Draw(g);
        }

        private void numericUpDown1_ValueChanged_1(object sender, EventArgs e)
        {
            Example.Erase(g);
            Example.BorderWidth = LabelToInt((string)comboBox1.SelectedItem);
            Example.Draw(g);
        }

        private void checkBox1_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox1.CheckState == CheckState.Checked) button2.Enabled = true; else button2.Enabled = false;
            Example.drawback = checkBox1.Checked;
        }
    }
}
