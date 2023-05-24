using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace LabaNumeroDuo
{
    [Serializable()] public abstract class Figure
    {
        //Точки
        protected Point Origin = new Point();
        protected Point TL = new Point();
        protected Point BR = new Point();
        protected Point TR = new Point();
        protected Point BL = new Point();

        //Переменные стиля и ручек
        protected Color Color = Color.Black;
        protected Color Background = Color.White;
        protected int Width = 1;
        //protected bool Drawback = true;
        [NonSerialized] protected Pen MainPen;
        [NonSerialized] protected Pen BackPen;

        //Методы отрисовки
        public abstract void Draw(Graphics G);
        public abstract void DrawDashed(Graphics G);
        public abstract void Erase(Graphics G);

        //Методы обновления стиля
        public void SetColor(Color cl)
        {
            Color = cl;
            MainPen.Color = cl;
        }
        public void SetWidth(int width)
        {
            Width = width;
            MainPen.Width = width;
        }
        public void SetBackground(Color cl)
        {
            Background = cl;
            BackPen.Color = cl;
        }
        public void UpdatePen()
        {
            MainPen.Width = Width;
            MainPen.Color = Color;
        }

        //Методы сброса начальной точки
        public void ResetOrigin(int X, int Y)
        {
            Origin.X = X; Origin.Y = Y;
            TL = Origin; BR = Origin; TR = Origin; BL = Origin;
        }
        public void ResetOrigin(Point New)
        {
            Origin.X = New.X; Origin.Y = New.Y;
            TL = Origin; BR = Origin; TR = Origin; BL = Origin;
        }

        //Методы отрисовки контура
        public void MoveAndRepaint(Graphics G, int X, int Y)
        {
            Erase(G);
            MovePointTo(X, Y);
            DrawDashed(G);
        }
        public void MoveAndRepaint(Graphics G, Point New)
        {
            Erase(G);
            MovePointTo(New.X, New.Y);
            DrawDashed(G);
        }

        //Методы перемещения границ фигуры
        public void MovePointTo(int X, int Y)
        {
            Point inter = new Point(X, Y);
            SwapPoints(Origin, inter);
        }
        public void MovePointTo(Point inter)
        {
            SwapPoints(Origin, inter);
        }
        public void SwapPoints(Point Any, Point Opposite)
        {
            TL.X = Math.Min(Any.X, Opposite.X); TL.Y = Math.Min(Any.Y, Opposite.Y);
            TR.X = Math.Max(Any.X, Opposite.X); TR.Y = Math.Min(Any.Y, Opposite.Y);
            BL.X = Math.Min(Any.X, Opposite.X); BL.Y = Math.Max(Any.Y, Opposite.Y);
            BR.X = Math.Max(Any.X, Opposite.X); BR.Y = Math.Max(Any.Y, Opposite.Y);
        }

        //Технические методы
        public Size GetSize()
        {
            return new Size(BR.X - TL.X, BR.Y - TL.Y);
        }
        public void SetSize(Size SetSize)
        {
            SwapPoints(Origin, new Point(Origin.X + SetSize.Width, Origin.Y + SetSize.Height));
        }
        public Point GetOrigin()
        {
            return new Point(Origin.X, Origin.Y);
        }
        public int[] GetTLBR()
        {
            return new int[] {TL.X, TL.Y, BR.X, BR.Y};
        }
        public override string ToString()
        {
            return "Figure " + this.GetType().ToString() + " with Origin point at (" + Origin.X.ToString() + " " + Origin.Y.ToString() + ") and X/Y coordinates in range of (" + TL.X.ToString() + "-" + BR.X.ToString() + "; " + TL.Y.ToString() + "-" + BR.Y.ToString() + ")";
        }
    }

    
    [Serializable()] class Rect : Figure
    {
        //Конструкторы
        public Rect(Point First, Point Second)
        {
            BR.X = Math.Max(First.X, Second.X); BR.Y = Math.Max(First.Y, Second.Y);
            TL.X = Math.Min(First.X, Second.X); TL.Y = Math.Min(First.Y, Second.Y);
            BL.X = Math.Min(First.X, Second.X); BL.Y = Math.Max(First.Y, Second.Y);
            TR.X = Math.Max(First.X, Second.X); TR.Y = Math.Min(First.Y, Second.Y);
            Origin = TL;
            MainPen = new Pen(Color.Black, 1); BackPen = new Pen(Color.White, 1);
            MainPen.Color = Color; MainPen.Width = Width; BackPen.Color = Background;
        }
        public Rect(int XOne, int XTwo, int YOne, int YTwo)
        {
            BR.X = Math.Max(XOne, XTwo); BR.Y = Math.Max(YOne, YTwo);
            TL.X = Math.Min(XOne, XTwo); TL.Y = Math.Min(YOne, YTwo);
            BL.X = Math.Min(XOne, XTwo); BL.Y = Math.Max(YOne, YTwo);
            TR.X = Math.Max(XOne, XTwo); TR.Y = Math.Min(YOne, YTwo);
            Origin = TL;
            MainPen = new Pen(Color.Black, 1); BackPen = new Pen(Color.White, 1);
            MainPen.Color = Color; MainPen.Width = Width; BackPen.Color = Background;
        }
        public Rect()
        {
            BR = Point.Empty;
            TR = Point.Empty;
            BL = Point.Empty;
            TL = Point.Empty;
            Origin = Point.Empty;
            MainPen = new Pen(Color.Black, 1); BackPen = new Pen(Color.White, 1);
            MainPen.Color = Color; MainPen.Width = Width; BackPen.Color = Background;
        }

        //Методы рисования
        public override void Draw(Graphics G)
        {
            if (MainPen == null)
            {
                MainPen = new Pen(Color.Black, 1); UpdatePen();
            }
            if (BackPen == null)
            {
                BackPen = new Pen(Color.White, 1); BackPen.Color = Background;
            }
            /*if (Drawback)*/ for (int i = TL.Y; i < BL.Y; i++) G.DrawLine(BackPen, TL.X, i, TR.X, i);
            MainPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            G.DrawLine(MainPen, TL, TR);
            G.DrawLine(MainPen, TR, BR);
            G.DrawLine(MainPen, BR, BL);
            G.DrawLine(MainPen, BL, TL);
        }
        public override void DrawDashed(Graphics G)
        {
            if (MainPen == null)
            {
                MainPen = new Pen(Color.Black, 1); UpdatePen();
            }
            if (BackPen == null)
            {
                BackPen = new Pen(Color.White, 1); UpdatePen();
            }
            MainPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            G.DrawLine(MainPen, TL, TR);
            G.DrawLine(MainPen, TR, BR);
            G.DrawLine(MainPen, BR, BL);
            G.DrawLine(MainPen, BL, TL);
        }
        public override void Erase(Graphics G)
        {
            if (MainPen == null)
            {
                MainPen = new Pen(Color.Black, 1); UpdatePen();
            }
            if (BackPen == null)
            {
                BackPen = new Pen(Color.White, 1); UpdatePen();
            }
            Pen pen = new Pen(Color.White, MainPen.Width);
            for (int i = TL.Y; i < BL.Y; i++) G.DrawLine(pen, TL.X, i, TR.X, i);
            G.DrawLine(pen, TL, TR);
            G.DrawLine(pen, TR, BR);
            G.DrawLine(pen, BR, BL);
            G.DrawLine(pen, BL, TL);
            pen.Dispose();
        }
    }
}
