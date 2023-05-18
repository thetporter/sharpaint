using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LabaNumeroDuo
{
    public partial class Form2: Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        Graphics g;
        private Rect current = new Rect();
        private bool preview = false;
        public List<Figure> history = new List<Figure>();
        //Данные для сохранения
        private bool mod = false;
        public string HomeAddress;

        private void Form2_Load(object sender, EventArgs e)
        {
            g = CreateGraphics();
        }

        private void Rect_Start(object sender, MouseEventArgs e)
        {
            //При нажатии ЛКМ начать отрисовку фигуры
            if (e.Button == MouseButtons.Left) {
                current = new Rect(e.X,e.X,e.Y,e.Y);
                preview = true;
                current.SetColor((ParentForm as Form1).ActiveColor);
                current.SetBackground((ParentForm as Form1).BackgroundColor);
                current.SetWidth((ParentForm as Form1).ActiveWidth);
            } else if (e.Button == MouseButtons.Right & !preview)
            {
                //Дебаг - текущее содержание history при нажатии ПКМ
                string Listed = ""; int number = 0;
                foreach (Rect rect in history)
                {
                    number++;
                    Listed = Listed + number.ToString() + ". " + rect.ToString() + "\n";
                }
                MessageBox.Show("Current history:\n" + Listed);
            } else if (e.Button == MouseButtons.Right & preview)
            {
                //Сброс отрисовки при ПКМ
                current.Erase(g);
                preview = false;
            }
        }
        private void MouseMovement(object sender, MouseEventArgs e)
        {
            //При перемещении мыши отрисовывать фигуру пунктиром
            if (preview)
            {
                current.MoveAndRepaint(g, e.X, e.Y);
                Invalidate();
            }
        }

        private void Rect_Finish(object sender, MouseEventArgs e)
        {
            //Сохранение фигуры и ее полная отрисовка
            if ((e.Button == MouseButtons.Left) && preview) {
                current.MovePointTo(e.X, e.Y);
                current.Draw(g);
                preview = false;
                history.Add(current);
                mod = true;
                //MessageBox.Show("Added " + current.ToString() + " to History.");
            }
        }

        //Предлагать сохранение при закрытии измененного рисунка
        private void Form2_Closing(object sender, FormClosingEventArgs e)
        {
            if (mod) switch (MessageBox.Show(this, "Would you like to save the changes?", "Save Request", MessageBoxButtons.YesNoCancel))
                {
                    case DialogResult.Yes: (ParentForm as Form1).pageClosed(this); (ParentForm as Form1).saveForm(this, false); break;
                    case DialogResult.No: (ParentForm as Form1).pageClosed(this); break;
                    default: e.Cancel = true; break;
                }
            else (ParentForm as Form1).pageClosed(this);
        }

        private void Form2_Paint(object sender, PaintEventArgs e)
        {
            Graphics paintgr = CreateGraphics();
            foreach (Rect rect in history)
            {
                rect.Draw(paintgr);
            }
            current.DrawDashed(paintgr);
        }
    }
}
