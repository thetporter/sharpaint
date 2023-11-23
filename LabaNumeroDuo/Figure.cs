using System;
using System.Collections.Generic;
using System.Drawing;

namespace LabaNumeroDuo
{

    //CHANGES PLAN
    //TLBR → List TLBR
    //

    [Serializable()]
    public abstract class Figure
    {
        //Точки
        protected Point Origin = new Point(); 
        protected List<Point> Points = new List<Point>();

        //Переменные стиля и ручек
        protected Color Color = Color.Black;
        protected Color Background = Color.White;
        protected int Width = 1;
        public bool drawback = true;
        [NonSerialized] protected Pen MainPen;
        [NonSerialized] protected SolidBrush BackBrush;

        #region Методы отрисовки
        public abstract void Draw(Graphics G);
        public abstract void DrawDashed(Graphics G);
        public abstract void Erase(Graphics G);
        #endregion
        #region Конверторы координат
        public Point[] PointArray
        {
            get { return Points.ToArray(); }
            set
            {
                Points.Clear();
                foreach (Point p in value)
                {
                    Points.Add(p);
                }
            }
        }

        public virtual void OffsetBy(int X = 0, int Y = 0)
        {
            Point temp;
            for (int i = 0; i < Points.Count; i++)
            {
                temp = Points[i];
                temp.X += X; temp.Y += Y;
                Points[i] = temp;
            }
            Origin.X += X; Origin.Y += Y;
        }
        #endregion
        #region Методы/атрибуты обновления стиля
        public Color FrontColor 
        {
            set {Color = value; if (MainPen != null) MainPen.Color = value;}
            get { return Color;  }
        }
        public int BorderWidth
        {
            set { Width = value; if (MainPen != null) MainPen.Width = value; }
            get { return Width; }
        }
        public Color BackColor
        {
            set { Background = value; if (BackBrush != null) BackBrush.Color = value; }
            get { return Background; }
        }
        public void UpdatePen()
        {
            MainPen.Width = Width;
            MainPen.Color = Color;
        }
        #endregion
        #region Методы сброса начальной точки
        public void ResetOrigin(int X, int Y)
        {
            ResetOrigin(new Point(X, Y));
        }
        public void ResetOrigin(Point New)
        {
            Points.Clear();
            Points.Add(New); Points.Add(New);
            Origin = New;
        }
        #endregion
        #region Методы предпросмотра
        public virtual void MoveAndRepaint(Graphics G, int X, int Y)
        {
            Erase(G);
            MovePointTo(X, Y);
            DrawDashed(G);
        }
        public virtual void MoveAndRepaint(Graphics G, Point New)
        {
            Erase(G);
            MovePointTo(New.X, New.Y);
            DrawDashed(G);
        }
        #endregion
        #region Методы перемещения контура фигуры
        public void MovePointTo(int X, int Y)
        {
            MovePointTo(new Point(X, Y));
        }
        public void MovePointTo(Point inter)
        {
            Points[Points.Count - 1] = inter;
        }
        #endregion
        #region Технические методы
        public Size GetSize()
        {
            int[] TLBR = { Int32.MaxValue, Int32.MaxValue, 0, 0 };
            foreach (Point p in Points)
            {
                if (p.X < TLBR[0]) TLBR[0] = p.X;
                if (p.Y < TLBR[1]) TLBR[1] = p.Y;
                if (p.X > TLBR[2]) TLBR[2] = p.X;
                if (p.Y > TLBR[3]) TLBR[3] = p.Y;
            }
            return new Size(TLBR[2] - TLBR[0], TLBR[3] - TLBR[1]);
        }
        public void SetSize(Size size)
        {
            Points.Clear();
            Points.Add(Origin); Points.Add(new Point(Origin.X + size.Width, Origin.Y + size.Height));
        }
        public Point GetOrigin()
        {
            return Points[0];
        }
        public int[] GetTLBR()
        {
            int[] TLBR = { 4096, 4096, 0, 0 };
            foreach (Point p in Points)
            {
                if (p.X < TLBR[0]) TLBR[0] = p.X;
                if (p.Y < TLBR[1]) TLBR[1] = p.Y;
                if (p.X > TLBR[2]) TLBR[2] = p.X;
                if (p.Y > TLBR[3]) TLBR[3] = p.Y;
            }
            return TLBR;
        }
        public override string ToString()
        {
            int[] t = this.GetTLBR();
            return "Figure " + this.GetType().ToString() + " with Origin point at (" + Origin.X.ToString() + " " + Origin.Y.ToString() + ") and X/Y coordinates in range of (" + t[0].ToString() + "-" + t[2].ToString() + "; " + t[1].ToString() + "-" + t[3].ToString() + ")";
        }
        #endregion
    }


    [Serializable()]
    class Rect : Figure
    {
        #region Конструкторы
        public Rect(Point First, Point Second)
        {
            Points.Add(First); Points.Add(Second);
            Origin = First;
            MainPen = new Pen(Color.Black, 1); BackBrush = new SolidBrush(Color.White);
            MainPen.Color = Color; MainPen.Width = Width; BackBrush.Color = Background;
        }
        public Rect(int XOne, int XTwo, int YOne, int YTwo)
        {
            Points.Add(new Point(XOne, YOne)); Points.Add(new Point(XTwo, YTwo));
            Origin = Points[0];
            MainPen = new Pen(Color.Black, 1); BackBrush = new SolidBrush(Color.White);
            MainPen.Color = Color; MainPen.Width = Width; BackBrush.Color = Background;
        }
        public Rect()
        {
            Points.Add(Point.Empty);
            Points.Add(Point.Empty);
            Points.Add(Point.Empty);
            Points.Add(Point.Empty);
            Origin = Point.Empty;
            MainPen = new Pen(Color.Black, 1); BackBrush = new SolidBrush(Color.White);
            MainPen.Color = Color; MainPen.Width = Width; BackBrush.Color = Background;
        }
        #endregion
        #region Методы рисования
        public override void Draw(Graphics G)
        {
            int[] t = this.GetTLBR();

            if (MainPen == null) { MainPen = new Pen(Color.Black, 1); UpdatePen(); }

            if (drawback) G.FillRectangle(BackBrush, Rectangle.FromLTRB(t[0], t[1], t[2], t[3]));
            MainPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            G.DrawRectangle(MainPen, Rectangle.FromLTRB(t[0], t[1], t[2], t[3]));
        }
        public override void DrawDashed(Graphics G)
        {
            int[] t = this.GetTLBR();

            if (MainPen == null) { MainPen = new Pen(Color.Black, 1); UpdatePen(); }

            MainPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            G.DrawRectangle(MainPen, Rectangle.FromLTRB(t[0], t[1], t[2], t[3]));
        }
        public override void Erase(Graphics G)
        {
            int[] t = this.GetTLBR();

            if (MainPen == null) { MainPen = new Pen(Color.Black, 1); UpdatePen(); }

            Pen pen = new Pen(Color.White, MainPen.Width);
            G.FillRectangle(new SolidBrush(Color.White), Rectangle.FromLTRB(t[0], t[1], t[2], t[3]));
            G.DrawRectangle(pen, Rectangle.FromLTRB(t[0], t[1], t[2], t[3]));
            pen.Dispose();
        }
        #endregion
    }

    [Serializable()]
    class Elli : Figure
    {
        #region Конструкторы
        public Elli(Point First, Point Second)
        {
            Points.Add(First); Points.Add(Second);
            Origin = First;
            MainPen = new Pen(Color.Black, 1); BackBrush = new SolidBrush(Color.White);
            MainPen.Color = Color; MainPen.Width = Width; BackBrush.Color = Background;
        }
        public Elli(int XOne, int XTwo, int YOne, int YTwo)
        {
            Points.Add(new Point(XOne, YOne)); Points.Add(new Point(XTwo, YTwo));
            Origin = Points[0];
            MainPen = new Pen(Color.Black, 1); BackBrush = new SolidBrush(Color.White);
            MainPen.Color = Color; MainPen.Width = Width; BackBrush.Color = Background;
        }
        public Elli()
        {
            Points.Add(Point.Empty); Points.Add(Point.Empty);
            Origin = Point.Empty;
            MainPen = new Pen(Color.Black, 1); BackBrush = new SolidBrush(Color.White);
            MainPen.Color = Color; MainPen.Width = Width; BackBrush.Color = Background;
        }
        #endregion
        #region Методы рисования
        public override void Draw(Graphics G)
        {
            int[] t = this.GetTLBR();
            if (MainPen == null) { MainPen = new Pen(Color.Black, 1); UpdatePen(); }
            if (BackBrush == null) { BackBrush = new SolidBrush(Color.White); BackBrush.Color = Background; }
            MainPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            if (drawback) G.FillEllipse(BackBrush, t[0], t[1], Math.Abs(t[2] - t[0]), Math.Abs(t[3] - t[1]));
            G.DrawEllipse(MainPen, t[0], t[1], Math.Abs(t[2] - t[0]), Math.Abs(t[3] - t[1]));
            
        }
        public override void DrawDashed(Graphics G)
        {
            int[] t = this.GetTLBR();
            if (MainPen == null) { MainPen = new Pen(Color.Black, 1); UpdatePen(); }
            MainPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            
            G.DrawEllipse(MainPen, t[0], t[1], Math.Abs(t[2] - t[0]), Math.Abs(t[3] - t[1]));
        }
        public override void Erase(Graphics G)
        {
            int[] t = this.GetTLBR();
            if (MainPen == null) { MainPen = new Pen(Color.Black, 1); UpdatePen(); }
            Pen pen = new Pen(Color.White, MainPen.Width);
            if (drawback) G.FillEllipse(new SolidBrush(Color.White), t[0], t[1], Math.Abs(t[2] - t[0]), Math.Abs(t[3] - t[1]));
            G.DrawEllipse(pen, t[0], t[1], Math.Abs(t[2] - t[0]), Math.Abs(t[3] - t[1]));
            
            pen.Dispose();
        }
        #endregion
    }

    [Serializable()] class Line : Figure
    {
        #region Конструкторы
        public Line(Point First, Point Second)
        {
            Points.Add(First); Points.Add(Second);
            Origin = First;
            MainPen = new Pen(Color.Black, 1);
            MainPen.Color = Color; MainPen.Width = Width;
        }
        public Line(int XOne, int XTwo, int YOne, int YTwo)
        {
            Points.Add(new Point(XOne, YOne)); Points.Add(new Point(XTwo, YTwo));
            Origin = Points[0];
            MainPen = new Pen(Color.Black, 1);
            MainPen.Color = Color; MainPen.Width = Width;
        }
        public Line()
        {
            Points.Add(Point.Empty); Points.Add(Point.Empty);
            Origin = Point.Empty;
            MainPen = new Pen(Color.Black, 1);
            MainPen.Color = Color; MainPen.Width = Width;
        }
        #endregion
        #region Методы рисования
        public override void Draw(Graphics G)
        {
            if (MainPen == null) { MainPen = new Pen(Color.Black, 1); UpdatePen(); }
            MainPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            G.DrawLine(MainPen, Origin, Points[1]);
        }
        public override void DrawDashed(Graphics G)
        {
            if (MainPen == null) { MainPen = new Pen(Color.Black, 1); UpdatePen(); }
            MainPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            G.DrawLine(MainPen, Origin, Points[1]);
        }
        public override void Erase(Graphics G)
        {
            if (MainPen == null) { MainPen = new Pen(Color.Black, 1); UpdatePen(); }
            Pen pen = new Pen(Color.White, MainPen.Width);
            G.DrawLine(pen, Origin, Points[1]);
            pen.Dispose();
        }
        #endregion
    }

    [Serializable()] class Curve : Figure
    {
        public Curve(Point ZeroPoint)
        {
            Origin = ZeroPoint;
            Points.Add(Origin); Points.Add(Origin);
            MainPen = new Pen(Color.Black, 1); 
            MainPen.Color = Color; MainPen.Width = Width;
        }

        #region Особые методы
        public void AddPoint(int NewX, int NewY)
        {
            Points.Add(new Point(NewX, NewY));
        }
        public void AddPoint(Point New)
        {
            Points.Add(new Point(New.X, New.Y));
        }

        public override void MoveAndRepaint(Graphics G, int X, int Y)
        {
            AddPoint(X, Y);
            DrawDashed(G);
        }
        public override void MoveAndRepaint(Graphics G, Point New)
        {
            AddPoint(New);
            DrawDashed(G);
        }
        #endregion
        #region Методы рисования
        public override void Draw(Graphics G)
        {
            if (MainPen == null) { MainPen = new Pen(Color.Black, 1); UpdatePen(); }
            MainPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            Point[] arr = Points.ToArray();
            G.DrawCurve(MainPen, arr);
        }
        public override void DrawDashed(Graphics G)
        {
            if (MainPen == null) { MainPen = new Pen(Color.Black, 1); UpdatePen(); }
            MainPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            Point[] arr = Points.ToArray();
            G.DrawCurve(MainPen, arr);
        }
        public override void Erase(Graphics G)
        {
            if (MainPen == null) { MainPen = new Pen(Color.Black, 1); UpdatePen(); }
            Pen pen = new Pen(Color.White, MainPen.Width);
            Point[] arr = Points.ToArray();
            G.DrawCurve(pen, arr);
            pen.Dispose();
        }
        #endregion
    }

    [Serializable()] class Text : Figure
    {
        public string text;
        public Font font;

        #region Конструкторы
        public Text()
        {
            Points.Add(Point.Empty); Points.Add(Point.Empty);
            Origin = Point.Empty; text = "";
            font = SystemFonts.DefaultFont;
        }

        public Text(string Text, Font Font)
        {
            Points.Add(Point.Empty); Points.Add(Point.Empty);
            Origin = Point.Empty; text = Text;
            font = Font;
        }

        public Text(Point Origin, string Text, Font Font)
        {
            Points.Add(Origin); Points.Add(Origin);
            this.Origin = Origin; text = Text;
            font = Font;
        }
        #endregion
        #region Методы рисования
        public override void Draw(Graphics G)
        {
            if (MainPen == null) { MainPen = new Pen(Color.Black, 1); UpdatePen(); }
            G.DrawString(text, font, new SolidBrush(MainPen.Color), Rectangle.FromLTRB(
                this.GetTLBR()[0], this.GetTLBR()[1], this.GetTLBR()[2], this.GetTLBR()[3]));
        }
        public override void DrawDashed(Graphics G)
        {
            int[] t = this.GetTLBR();

            if (MainPen == null) { MainPen = new Pen(Color.Black, 1); UpdatePen(); }

            MainPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            G.DrawRectangle(MainPen, Rectangle.FromLTRB(t[0], t[1], t[2], t[3]));
        }
        public override void Erase(Graphics G)
        {
            int[] t = this.GetTLBR();

            G.DrawString(text, font, new SolidBrush(Color.White), Rectangle.FromLTRB(
                this.GetTLBR()[0], this.GetTLBR()[1], this.GetTLBR()[2], this.GetTLBR()[3]));
        }
        #endregion
    }
}
