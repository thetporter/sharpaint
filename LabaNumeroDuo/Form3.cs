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
            //Задать прямоугольник
            Example.ResetOrigin(Width - 100, 16);
            Example.MovePointTo(Width - 32, 78);
            Example.SetColor(home.ActiveColor);
            Example.SetBackground(home.BackColor);
            Example.SetWidth(home.ActiveWidth);
            //Создать графику; нарисовать прямоугольник
            g = CreateGraphics();
            Example.Draw(g);
        }

        //Метод установки значений
        public void SetDefaults(Color main, Color back, int width)
        {
            colorDialogLine.Color = main;
            colorDialogBckg.Color = back;
            comboBox1.SelectedItem = width.ToString();
            comboBox1.Text = width.ToString();
        }

        //Кнопки закрытия
        private void button4_Click(object sender, EventArgs e)
        {
            home.ActiveColor = colorDialogLine.Color;
            home.BackgroundColor = colorDialogBckg.Color;
            home.ActiveWidth = LabelToInt((string)comboBox1.SelectedItem);
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
            Example.SetColor(colorDialogLine.Color);
            Example.Draw(g);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            colorDialogBckg.ShowDialog();
            Example.Erase(g);
            Example.SetBackground(colorDialogBckg.Color);
            Example.Draw(g);
        }

        private void numericUpDown1_ValueChanged_1(object sender, EventArgs e)
        {
            Example.Erase(g);
            Example.SetWidth(LabelToInt((string)comboBox1.SelectedItem));
            Example.Draw(g);
        }
    }
}
