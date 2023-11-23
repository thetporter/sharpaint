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

        public BufferedGraphics buff;
        private Figure current = new Elli();
        private bool preview = false;
        public List<Figure> history = new List<Figure>();
        //Данные для изменения
        public List<int> editedIndex = new List<int>();
        private Point estart = new Point();
        private Point eoffset = new Point();
        private int LNum = 0;
        //Данные для сохранения
        private bool mod = false;
        public string HomeAddress;
        //
        public Size ActiveSurface = new Size();

        private void Form2_Load(object sender, EventArgs e)
        {
            BufferedGraphicsManager.Current.MaximumBuffer = SystemInformation.PrimaryMonitorMaximizedWindowSize;
            buff = BufferedGraphicsManager.Current.Allocate(CreateGraphics(), new Rectangle(0,0,ActiveSurface.Width, ActiveSurface.Height));
        }
        #region Рисование
        private void Rect_Start(object sender, MouseEventArgs e)
        {
            //При нажатии ЛКМ начать отрисовку фигуры
            if (e.Button == MouseButtons.Left && 
                (e.X-AutoScrollPosition.X < this.ActiveSurface.Width) && 
                (e.Y-AutoScrollPosition.Y < this.ActiveSurface.Height))
            {
                //В режиме выбора
                if ((ParentForm as Form1).ActiveFigure == 0)
                {
                    eoffset = Point.Empty;
                    //Найти верхнюю фигуру на координатах
                    int ind = history.FindLastIndex((f) =>
                    {
                        int[] t = f.GetTLBR();
                        return (t[0] + AutoScrollOffset.X <= e.X && t[1] + AutoScrollOffset.Y <= e.Y &&
                                t[2] + AutoScrollOffset.X >= e.X && t[3] + AutoScrollOffset.Y >= e.Y);
                    });
                    //Сбросить при нажатии снаружи
                    if (ind == -1) { editedIndex.Clear(); }
                    else
                    {
                        //При нажатии на выбранную фигуру войти в режим перемещения
                        if (editedIndex.Contains(ind)) { 
                            preview = true; 
                            estart.X = e.X + AutoScrollOffset.X;
                            estart.Y = e.Y + AutoScrollOffset.Y;
                        }
                        //При нажатии на невыбранную фигуру добавить ее в выбранные
                        else editedIndex.Add(ind);
                    }
                    Refresh();
                }
                else
                {
                    switch ((ParentForm as Form1).ActiveFigure)
                    {
                        case 1: current = new Rect(); break;
                        case 2: current = new Elli(); break;
                        case 3: current = new Line(); break;
                        case 4: current = new Curve(e.Location); break;
                        case 5: current = new Text(new Point(e.X, e.Y), "", (ParentForm as Form1).ActiveFont); break;
                    }
                    if ((ParentForm as Form1).SnapToCoordinates) current.ResetOrigin(Snap(e.X, e.Y)); else current.ResetOrigin(e.X, e.Y);
                    preview = true;
                    current.drawback = (ParentForm as Form1).DoBackground;
                    current.FrontColor = (ParentForm as Form1).ActiveColor;
                    current.BackColor = (ParentForm as Form1).BackgroundColor;
                    current.BorderWidth = (ParentForm as Form1).ActiveWidth;
                }
            }
            else if (e.Button == MouseButtons.Right & !preview)
            {
                //Дебаг - текущее содержание history при нажатии ПКМ
                string Listed = ""; int number = 0;
                foreach (Figure rect in history)
                {
                    number++;
                    Listed = Listed + number.ToString() + ". " + rect.ToString() + "\n";
                }
                MessageBox.Show("Current history:\n" + Listed);
            }
            else if (e.Button == MouseButtons.Right & preview)
            {
                //Сброс отрисовки при ПКМ
                current.Erase(buff.Graphics);
                preview = false;
            }
        }
        private void MouseMovement(object sender, MouseEventArgs e)
        {
            MouseCoordinate.Text = $"{e.X}, {e.Y}";
            
            if (preview && 
                (e.X - AutoScrollPosition.X < this.ActiveSurface.Width) &&
                (e.Y - AutoScrollPosition.Y < this.ActiveSurface.Height))
            {
                //При перемещении в режиме выбора сдвигать контуры
                if ((ParentForm as Form1).ActiveFigure == 0)
                {
                    if ((ParentForm as Form1).SnapToCoordinates)
                    { eoffset.X = Snap(e.Location).X - Snap(estart).X; eoffset.Y = Snap(e.Location).Y - Snap(estart).Y; }
                    else { eoffset.X = e.X - estart.X; eoffset.Y = e.Y - estart.Y; }
                }
                else
                {
                    //При перемещении мыши отрисовывать фигуру пунктиром
                    if ((ParentForm as Form1).SnapToCoordinates)
                    {
                        if (current.GetType().Equals(typeof(Curve)))
                            if (e.X - Snap(e.Location).X == 0 || e.Y - Snap(e.Location).Y == 0) 
                                current.MoveAndRepaint(buff.Graphics, Snap(e.Location)); 
                            else;
                        else current.MoveAndRepaint(buff.Graphics, Snap(e.Location));
                    }
                    else current.MoveAndRepaint(buff.Graphics, e.X, e.Y);
                }
                Refresh();
            }
        }

        private void Rect_Finish(object sender, MouseEventArgs e)
        {
            //Сохранение фигуры и ее полная отрисовка
            if ((e.Button == MouseButtons.Left) && preview)
            {
                if (((ParentForm as Form1).ActiveFigure == 0))
                {
                    if ((ParentForm as Form1).SnapToCoordinates)
                    { eoffset.X = Snap(e.Location).X - Snap(estart).X; eoffset.Y = Snap(e.Location).Y - Snap(estart).Y; }
                    else { eoffset.X = e.X - estart.X; eoffset.Y = e.Y - estart.Y; }
                    //Если не было перемещения, отменить выбор текущей фигуры
                    if (eoffset == Point.Empty) 
                    {
                        int ind = history.FindLastIndex((f) =>
                        {
                            int[] t = f.GetTLBR();
                            return (t[0] + AutoScrollOffset.X <= e.X && t[1] + AutoScrollOffset.Y <= e.Y &&
                                    t[2] + AutoScrollOffset.X >= e.X && t[3] + AutoScrollOffset.Y >= e.Y);
                        });
                        editedIndex.Remove(ind);
                        preview = false;
                    } else {
                        foreach (int i in editedIndex)
                        {
                            if (history[i].GetTLBR()[2] + eoffset.X > ActiveSurface.Width ||
                                history[i].GetTLBR()[3] + eoffset.Y > ActiveSurface.Height ||
                                history[i].GetTLBR()[0] + eoffset.X < 0 ||
                                history[i].GetTLBR()[1] + eoffset.Y < 0)
                            {
                                //При выходе за границы выдать сообщение об ошибке и отменить перемещение
                                eoffset = Point.Empty; estart = Point.Empty;
                                MessageBox.Show("Error: Figure moved outside of image boundary.");
                                preview = false;
                                Refresh();
                                return;
                            } else continue;
                        }
                        //Сдвинуть фигуры
                        foreach (int i in editedIndex) history[i].OffsetBy(eoffset.X, eoffset.Y);
                        estart = Point.Empty; eoffset = Point.Empty;
                        preview = false;
                    }
                }
                else {
                    Graphics tempG = CreateGraphics();
                    current.Erase(buff.Graphics);
                    if ((ParentForm as Form1).SnapToCoordinates)
                        current.MovePointTo(Snap(e.Location));
                    else
                        current.MovePointTo(e.Location);
                    current.Erase(buff.Graphics);
                    current.Draw(buff.Graphics);
                    buff.Render(tempG);
                    tempG.Dispose();
                    preview = false;
                    Figure Saved;
                    switch ((ParentForm as Form1).ActiveFigure)
                    {
                        case 1: Saved = new Rect(); break;
                        case 2: Saved = new Elli(); break;
                        case 3: Saved = new Line(current.GetOrigin(), current.PointArray[1]); break;
                        case 4: Saved = new Curve(current.GetOrigin()); break;
                        case 5:
                            TextBox t = new TextBox();
                            Saved = new Text(current.GetOrigin(), t.Text, (ParentForm as Form1).ActiveFont);
                            t.Location = new Point(current.GetTLBR()[0], current.GetTLBR()[1]);
                            t.Width = current.GetSize().Width; t.Height = current.GetSize().Height;
                            t.Parent = this;
                            t.Multiline = true; t.WordWrap = true;
                            t.KeyDown += DetectEnter;
                            t.Font = (ParentForm as Form1).ActiveFont;
                            t.ForeColor = (ParentForm as Form1).ActiveColor;
                            t.Show();
                            t.Focus();
                            break;
                        default: Saved = new Rect(); break;
                    }
                    Saved.PointArray = current.PointArray;
                    Saved.OffsetBy(-AutoScrollOffset.X, -AutoScrollOffset.Y);
                    Saved.drawback = current.drawback;
                    Saved.FrontColor = current.FrontColor;
                    Saved.BackColor = current.BackColor;
                    Saved.BorderWidth = current.BorderWidth;
                    history.Add(Saved);
                    mod = true;
                    current.ResetOrigin(0, 0);
                }
                Refresh();
                //MessageBox.Show("Added " + current.ToString() + " to History.");
            }
        }

        private void DetectEnter(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                (history.FindLast(f => f.GetType() == typeof(Text)) as Text).text = (sender as TextBox).Text;
                (sender as TextBox).Hide();
                (sender as TextBox).Dispose();
            }
        }
        #endregion

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
            if (!e.Cancel) buff.Dispose();
        }

        private void Form2_Paint(object sender, PaintEventArgs e)
        {
            if (buff.Graphics == null) return;
            Figure Painter = new Rect(0, ActiveSurface.Width-AutoScrollPosition.X,
                                      0, ActiveSurface.Height-AutoScrollPosition.Y);
            Painter.BackColor = Color.White; Painter.FrontColor = Color.Transparent;
            Painter.Draw(buff.Graphics);
            if ((ParentForm as Form1).ShowCoordinates) {
                int d = 1;
                Pen tempen = new Pen(Color.DimGray, 1);
                while (d * (ParentForm as Form1).CScale < Math.Max(ActiveSurface.Width, ActiveSurface.Height))
                {
                    buff.Graphics.DrawLine(tempen, new Point(d * (ParentForm as Form1).CScale, 0), new Point(d * (ParentForm as Form1).CScale, ActiveSurface.Height));
                    buff.Graphics.DrawLine(tempen, new Point(0, d * (ParentForm as Form1).CScale), new Point(ActiveSurface.Width, d * (ParentForm as Form1).CScale));
                    d++;
                }
                LNum = d;
                tempen.Dispose();
            }
            if (!mod) ImageSize.Text = $"{ActiveSurface.Width}x{ActiveSurface.Height}";

            foreach (Figure Loaded in history)
            {
                if ((true))
                {   switch (Loaded.GetType().ToString())
                    {
                        case "LabaNumeroDuo.Rect": Painter = new Rect(); break;
                        case "LabaNumeroDuo.Elli": Painter = new Elli(); break;
                        case "LabaNumeroDuo.Line": Painter = new Line(Loaded.PointArray[0], Loaded.PointArray[1]); break;
                        case "LabaNumeroDuo.Curve": Painter = new Curve(Loaded.GetOrigin()); break;
                        case "LabaNumeroDuo.Text": Painter = new Text(Loaded.GetOrigin(), (Loaded as Text).text, (Loaded as Text).font); break;
                    }
                    Painter.PointArray = Loaded.PointArray;
                    Painter.drawback = Loaded.drawback;
                    Painter.FrontColor = Loaded.FrontColor;
                    Painter.BackColor = Loaded.BackColor;
                    Painter.BorderWidth = Loaded.BorderWidth;
                    Painter.OffsetBy(AutoScrollOffset.X, AutoScrollOffset.Y);
                    Painter.Draw(buff.Graphics);
                }
            }
            if (preview && (ParentForm as Form1).ActiveFigure != 0) current.DrawDashed(buff.Graphics);
            if ((ParentForm as Form1).ActiveFigure == 0)
            {
                foreach (int i in editedIndex)
                {
                    switch (history[i].GetType().ToString())
                    {
                        case "LabaNumeroDuo.Rect": Painter = new Rect(); break;
                        case "LabaNumeroDuo.Elli": Painter = new Elli(); break;
                        case "LabaNumeroDuo.Line": Painter = new Line(history[i].PointArray[0], history[i].PointArray[1]); break;
                        case "LabaNumeroDuo.Curve": Painter = new Curve(history[i].GetOrigin()); break;
                        case "LabaNumeroDuo.Text": Painter = new Text(history[i].GetOrigin(), (history[i] as Text).text, (history[i] as Text).font); break;
                    }
                    Painter.PointArray = history[i].PointArray;
                    Painter.FrontColor = history[i].FrontColor;
                    Painter.BorderWidth = history[i].BorderWidth + 1;
                    Painter.OffsetBy(AutoScrollOffset.X + eoffset.X, AutoScrollOffset.Y + eoffset.Y);
                    Painter.DrawDashed(buff.Graphics);
                }
            }
            buff.Render(e.Graphics);
        }

        private void Form2_Scroll(object sender, ScrollEventArgs e)
        {
            Refresh();
        }

        private void Form2_Shown(object sender, EventArgs e)
        {
            Refresh();
        }

        public void Modded() { this.mod = true; }
        public void Demod() { this.mod = false; }

        private Point Snap(int X, int Y)
        {
            Point Res = new Point();
            int precor = 0;
            for (int i = 1; i < LNum; i++)
            { if (Math.Abs(X - precor) < Math.Abs(X - (ParentForm as Form1).CScale * i)) break; precor = (ParentForm as Form1).CScale * i; }
            Res.X = precor;
            precor = 0;
            for (int i = 1; i < LNum; i++)
            { if (Math.Abs(Y - precor) < Math.Abs(Y - (ParentForm as Form1).CScale * i)) break; precor = (ParentForm as Form1).CScale * i; }
            Res.Y = precor;
            return Res;
        }
        private Point Snap(Point Source)
        {
            return Snap(Source.X, Source.Y);
        }
        private Point[] Snap(Point[] Source)
        {
            Point[] Pointer = new Point[Source.Length];
            Pointer.Initialize();
            for (int i = 0; i < Source.Length; i++) Pointer[i] = Snap(Source[i]);
            return Pointer;
        }
        private void Snap(ref Figure Target)
        {
            Point[] Pointer = Target.PointArray;
            Pointer = Snap(Pointer);
            Target.PointArray = Pointer;
        }
        public void SnapAll()
        {
            for (int i = 0; i < history.Count; i++)
            {
                Figure t = history[i];
                Snap(ref t);
                history[i] = t;
            }
        }

        public void UpdatePenData()
        {
            this.PenInfo.Text = $"Main Pen Width: {(ParentForm as Form1).ActiveWidth}, Color: ";
            this.MainPenColor.ForeColor = (ParentForm as Form1).ActiveColor;
            this.BackBrushColor.ForeColor = (ParentForm as Form1).BackgroundColor;
            if ((ParentForm as Form1).DoBackground) BackBrushColor.Visible = true; else BackBrushColor.Visible = false;
            string t = "NO";
            if ((ParentForm as Form1).DoBackground) t = "YES";
            this.BackgroundInfo.Text = $"Background: {t}";
        }
    }
}
