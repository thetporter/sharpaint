using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace LabaNumeroDuo
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            this.AutoScroll = true;
            this.AutoScrollMinSize = new Size(300, 300);
        }

        Graphics g;
        private Rect current = new Rect();
        private bool preview = false;
        public List<Figure> history = new List<Figure>();
        //Данные для сохранения
        private bool mod = false;
        public string HomeAddress;
        //
        public Size ActiveSurface = new Size();

        private void Form2_Load(object sender, EventArgs e)
        {
            g = CreateGraphics();
        }

        private void Rect_Start(object sender, MouseEventArgs e)
        {
            //При нажатии ЛКМ начать отрисовку фигуры
            if (e.Button == MouseButtons.Left)
            {
                current.ResetOrigin(e.X,e.Y);
                preview = true;
                current.SetColor((ParentForm as Form1).ActiveColor);
                current.SetBackground((ParentForm as Form1).BackgroundColor);
                current.SetWidth((ParentForm as Form1).ActiveWidth);
            }
            else if (e.Button == MouseButtons.Right & !preview)
            {
                //Дебаг - текущее содержание history при нажатии ПКМ
                string Listed = ""; int number = 0;
                foreach (Rect rect in history)
                {
                    number++;
                    Listed = Listed + number.ToString() + ". " + rect.ToString() + "\n";
                }
                MessageBox.Show("Current history:\n" + Listed);
            }
            else if (e.Button == MouseButtons.Right & preview)
            {
                //Сброс отрисовки при ПКМ
                current.Erase(g);
                preview = false;
            }
            else if (e.Button == MouseButtons.Middle)
            {
                MessageBox.Show(AutoScrollPosition.ToString());
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
            if ((e.Button == MouseButtons.Left) && preview)
            {
                current.Erase(g);
                current.MovePointTo(e.X, e.Y);
                current.Erase(g);
                current.Draw(g);
                preview = false;
                Rect SavedRect = new Rect();
                SavedRect.SwapPoints(new Point(current.GetTLBR()[0] - AutoScrollPosition.X, current.GetTLBR()[1] - AutoScrollPosition.Y),
                                     new Point(current.GetTLBR()[2] - AutoScrollPosition.X, current.GetTLBR()[3] - AutoScrollPosition.Y));
                history.Add(SavedRect);
                mod = true;
                current.ResetOrigin(0, 0);
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
            Rect DrawnRect = new Rect();
            int[] TLBR;
            foreach (Rect rect in history)
            {
                if ((true))
                {
                    TLBR = rect.GetTLBR();
                    DrawnRect.SwapPoints(new Point(TLBR[0] + AutoScrollPosition.X, TLBR[1] + AutoScrollPosition.Y),
                                         new Point(TLBR[2] + AutoScrollPosition.X, TLBR[3] + AutoScrollPosition.Y));
                    DrawnRect.Draw(paintgr);
                }
            }
            current.DrawDashed(paintgr);
        }

        private void Form2_Scroll(object sender, ScrollEventArgs e)
        {
            Invalidate();
        }
    }
}
