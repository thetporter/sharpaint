using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;



namespace LabaNumeroDuo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private int TotalFormsMade = 0;
        private int ActiveForms = 0;

        //Хранение стилей
        public Color ActiveColor = Color.Black;
        public Color BackgroundColor = Color.White;
        public bool DoBackground = true;
        public int ActiveWidth = 1;
        public int ActiveFigure = 1;
        public Font ActiveFont = SystemFonts.DefaultFont;
        //Координатная сетка
        public bool ShowCoordinates = false;
        public int CScale = 10;
        public bool SnapToCoordinates = false;
        public CoordinateSetup CForm = null;
        //Хранение размера
        public Size CreatedSize = new Size(800, 600);

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        #region Работа с изображениями
        //Создание нового рисунка:
        private void NewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 nova = new Form2();
            nova.MdiParent = this;
            nova.Size = CreatedSize;
            nova.ActiveSurface = new Size(Math.Max(CreatedSize.Width, 300), Math.Max(CreatedSize.Height, 300));
            TotalFormsMade++; ActiveForms++;
            nova.Text = "Рисунок " + TotalFormsMade.ToString();
            nova.HomeAddress = string.Empty;
            ToolStripMenuItem FileRepresent = new ToolStripMenuItem(nova.Text);
            saveToolStripMenuItem.DropDownItems.Add(FileRepresent);
            ToolStripMenuItem FileRepresentAs = new ToolStripMenuItem(nova.Text);
            saveAsToolStripMenuItem.DropDownItems.Add(FileRepresentAs);
            nova.Show();
        }

        //Загрузка существующего рисунка
        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog opener = new OpenFileDialog();
            opener.Filter = "Binary Format File (*.bff)|*.bff";
            opener.FilterIndex = 2;
            if (opener.ShowDialog() == DialogResult.OK)
            {
                //Создание экземпляра Form2
                Form2 nova = new Form2();
                nova.MdiParent = this;
                nova.Text = opener.SafeFileName;
                ActiveForms++;
                //Работа с загрузкой:
                ////Сохранение источника
                nova.HomeAddress = opener.FileName;
                ////Декодирование данных
                BinaryFormatter formatter = new BinaryFormatter();
                Stream stream = new FileStream(opener.FileName, FileMode.Open, FileAccess.Read, FileShare.Read);
                nova.history = (List<Figure>)formatter.Deserialize(stream);
                stream.Close();
                ////Установление размера и удаление его носителя до его отрисовки
                nova.Size = nova.history.Last().GetSize();
                nova.history.Remove(nova.history.Last());
                nova.ActiveSurface = nova.history.Last().GetSize();
                nova.history.Remove(nova.history.Last());
                ////Создание элементов меню
                ToolStripMenuItem FileRepresent = new ToolStripMenuItem(nova.Text);
                saveToolStripMenuItem.DropDownItems.Add(FileRepresent);
                ToolStripMenuItem FileRepresentAs = new ToolStripMenuItem(nova.Text);
                saveAsToolStripMenuItem.DropDownItems.Add(FileRepresentAs);
                nova.Show();
            }
        }

        //Действия при открытии/закрытии форм
        private void NewPageOpened(object sender, EventArgs e)
        {
            if (ActiveForms > 0)
            {
                //Enables save menus when a new form is opened;
                saveToolStripMenuItem.Enabled = true;
                saveAsToolStripMenuItem.Enabled = true;
            }
        }

        public void pageClosed(Form2 closedForm)
        {
            //Removing page entries from save menus
            foreach (ToolStripItem pts in saveToolStripMenuItem.DropDownItems)
            {
                if (pts.Text == closedForm.Text)
                {
                    pts.Visible = false;
                    saveToolStripMenuItem.DropDownItems.Remove(pts);
                    break;
                }
            }
            foreach (ToolStripItem pts in saveAsToolStripMenuItem.DropDownItems)
            {
                if (pts.Text == closedForm.Text)
                {
                    pts.Visible = false;
                    saveAsToolStripMenuItem.DropDownItems.Remove(pts);
                    break;
                }
            }
            ActiveForms--;
            //Disabling the save menus, resetting the counter
            if (ActiveForms == 0)
            {
                saveToolStripMenuItem.Enabled = false;
                saveAsToolStripMenuItem.Enabled = false;
                TotalFormsMade = 0;
                //MessageBox.Show("Last page closed");
            }
        }

        //Метод сохранения
        public void saveForm(Form2 forma, bool SaveAs)
        {
            //Checking if this file has already been saved earlier;
            if ((SaveAs) | (forma.HomeAddress.Length < 5))
            {
                SaveFileDialog savior = new SaveFileDialog();
                savior.Title = "Сохранение " + forma.Text;
                savior.Filter = "Binary Formatter File (*.bff)|*.bff";
                savior.FilterIndex = 2;
                savior.CheckFileExists = false;
                savior.InitialDirectory = Directory.GetCurrentDirectory();
                savior.RestoreDirectory = true;
                savior.OverwritePrompt = true;
                if (savior.ShowDialog() == DialogResult.OK)
                {
                    forma.HomeAddress = savior.FileName;
                    forma.Text = savior.FileName;
                }
                else return;
            }
            //Добавление носителей размеров в сохраняемую структуру:
            Rect ActSurRect = new Rect(0, forma.ActiveSurface.Width, 0, forma.ActiveSurface.Height);
            forma.history.Add(ActSurRect);
            Rect FalseRect = new Rect(0, forma.Size.Width, 0, forma.Size.Height);
            forma.history.Add(FalseRect);
            BinaryFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(forma.HomeAddress, FileMode.Create, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, forma.history);
            stream.Close();
            forma.history.Remove(FalseRect);
            forma.history.Remove(ActSurRect);
        }

        //Дебаг - показать дочерние формы
        private void showChildrenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string formality = ""; int i = 1;
            foreach (Form form in this.MdiChildren)
            {
                formality = formality + i.ToString() + ". " + form.ToString() + "\n";
                i++;
            }
            MessageBox.Show("Children amt: " + this.MdiChildren.Length.ToString() + "\n" + formality);
        }

        //БылВыбранЭлементМенюСохранения
        private void ptsPicked(object sender, ToolStripItemClickedEventArgs e)
        {
            bool SaveAs;
            if (saveToolStripMenuItem.DropDownItems.Contains(e.ClickedItem) || e.ClickedItem == SaveFiles)
                SaveAs = false; else SaveAs = true;
            foreach (Form2 forma in MdiChildren)
            {
                if (e.ClickedItem == allToolStripMenuItem || e.ClickedItem == SaveFiles) saveForm(forma, SaveAs);
                else if (forma.Text == e.ClickedItem.Text)
                {
                    saveForm(forma, SaveAs);
                }
            }
        }
        #endregion
        #region Работа с инструментами
        #region Изменения стиля/фигуры/размера
        private void changePaintStyleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form3 Dial = new Form3(this);
            Dial.SetDefaults(ActiveColor, BackColor, ActiveWidth);
            Dial.Show();
        }
        private void WinSizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form4 SizeSelector = new Form4();
            SizeSelector.SetSize(CreatedSize);
            SizeSelector.ReturnForm = this;
            SizeSelector.Show();
        }
        private void chfgToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form5 FigureSelector = new Form5(this);
            FigureSelector.Show();
        }
        private void FontChange_Click(object sender, EventArgs e)
        {
            FontDialog s = new FontDialog();
            s.ShowDialog();
            ActiveFont = s.Font;
        }
        private void CoordinatesTSMI_Click(object sender, EventArgs e)
        {
            if (CForm != null) CForm.Focus();
            else
            {
                CForm = new CoordinateSetup(this);
                CForm.Show();
            }
        }
        #endregion
        #region Выбор и сброс инструментов
        private void SelectSquareTool(object sender, EventArgs e)
        {
            this.ActiveFigure = 1;
            SquareTool.Checked = true;
            EllipseTool.Checked = false;
            LineTool.Checked = false;
            CurveTool.Checked = false;
            TextTool.Checked = false;
            SelectMoveTool.Checked = false;
            foreach (Form2 f in MdiChildren) f.editedIndex.Clear();
        }
        private void SelectEllipseTool(object sender, EventArgs e)
        {
            this.ActiveFigure = 2;
            SquareTool.Checked = false;
            EllipseTool.Checked = true;
            LineTool.Checked = false;
            CurveTool.Checked = false;
            TextTool.Checked = false;
            SelectMoveTool.Checked = false;
            foreach (Form2 f in MdiChildren) f.editedIndex.Clear();
        }
        private void SelectLineTool(object sender, EventArgs e)
        {
            this.ActiveFigure = 3;
            SquareTool.Checked = false;
            EllipseTool.Checked = false;
            LineTool.Checked = true;
            CurveTool.Checked = false;
            TextTool.Checked = false;
            SelectMoveTool.Checked = false;
            foreach (Form2 f in MdiChildren) f.editedIndex.Clear();
        }
        private void SelectCurveTool(object sender, EventArgs e)
        {
            this.ActiveFigure = 4;
            SquareTool.Checked = false;
            EllipseTool.Checked = false;
            LineTool.Checked = false;
            CurveTool.Checked = true;
            TextTool.Checked = false;
            SelectMoveTool.Checked = false;
            foreach (Form2 f in MdiChildren) f.editedIndex.Clear();
        }
        private void SelectTextTool(object sender, EventArgs e)
        {
            this.ActiveFigure = 5;
            SquareTool.Checked = false;
            EllipseTool.Checked = false;
            LineTool.Checked = false;
            CurveTool.Checked = false;
            TextTool.Checked = true;
            SelectMoveTool.Checked = false;
            foreach (Form2 f in MdiChildren) f.editedIndex.Clear();
        }
        private void SelectSelectTool(object sender, EventArgs e)
        {
            this.ActiveFigure = 0;
            SquareTool.Checked = false;
            EllipseTool.Checked = false;
            LineTool.Checked = false;
            CurveTool.Checked = false;
            TextTool.Checked = false;
            SelectMoveTool.Checked = true;
        }
        public void UncheckTools()
        {
            SquareTool.Checked = false;
            EllipseTool.Checked = false;
            LineTool.Checked = false;
            CurveTool.Checked = false;
            TextTool.Checked = false;
            SelectMoveTool.Checked = false;
            foreach (Form2 f in MdiChildren) f.editedIndex.Clear();
        }
        #endregion
        #region Функционал кнопок меню
        private void SaveFiles_Click(object sender, EventArgs e)
        {
            foreach (Form2 f in MdiChildren) saveForm(f, false);
        }
        private void FillSelector_Click(object sender, EventArgs e)
        {
            DoBackground = !DoBackground;
            FillSelector.Checked = !FillSelector.Checked;
            foreach (Form2 c in MdiChildren) { c.UpdatePenData(); }
        }

        private void DeleteTool_Click(object sender, EventArgs e)
        {
            foreach (Form2 f in MdiChildren)
            {
                if (!f.Focused) { continue; }
                f.editedIndex.Sort();
                for (int i = f.editedIndex.Count() - 1; i >= 0; i--)
                {
                    f.history.RemoveAt(f.editedIndex[i]);
                    f.editedIndex.RemoveAt(i);
                }
                if (f.history.Count < 1) f.Demod();
                f.Refresh();
            }
        }
        private void DeleteAll_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show($"Delete all selected figures in {MdiChildren.Count()} windows?", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                foreach (Form2 f in MdiChildren)
                {
                    f.editedIndex.Sort();
                    for (int i = f.editedIndex.Count() - 1; i >= 0; i--)
                    {
                        f.history.RemoveAt(f.editedIndex[i]);
                        f.editedIndex.RemoveAt(i);
                    }
                    if (f.history.Count < 1) f.Demod();
                    f.Refresh();
                }
            }
        }
        private void SelectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 af = (MdiChildren.ToList().Find((f) => { return f.Focused; }) as Form2);
            for (int i = 0; i < af.history.Count; i++) af.editedIndex.Add(i);
            af.Refresh();
        }
        #endregion
        #region Копирование и вставка
        private void CopyTSMI_Click(object sender, EventArgs e)
        {
            Form2 af = (MdiChildren.ToList().Find((f) => { return f.Focused; }) as Form2);
            List<Figure> copfig = new List<Figure>();
            List<int> ind = new List<int>(af.editedIndex);
            ind.Sort();
            foreach (int i in ind) copfig.Add(af.history[i]);
            if (sender.Equals(CutTSMI)) for (int i = ind.Count - 1; i >= 0; i--) af.history.RemoveAt(ind[i]);

            ////Fillup test
            //string t = copfig.ToString();
            //foreach (Figure f in copfig) t += $"\n{f}";
            //MessageBox.Show(t);

            //Via DataObject
            DataFormats.Format tform = DataFormats.GetFormat("FigureStorage");
            DataObject data = new DataObject(tform.Name, copfig);

            Clipboard.SetDataObject(data, true);

            if (Clipboard.ContainsData(tform.Name)) MessageBox.Show("A copy has been successfully created!");
            else MessageBox.Show("Copying failed - no data reached Clipboard");
            af.Refresh();
        }
        private void AsMetafileTSMI_Click(object sender, EventArgs e)
        {
            Form2 af = (MdiChildren.ToList().Find((f) => { return f.Focused; }) as Form2);
            List<Figure> copfig = new List<Figure>();
            List<int> ind = new List<int>(af.editedIndex);
            ind.Sort();
            foreach (int i in ind) copfig.Add(af.history[i]);

            Graphics g = CreateGraphics();
            IntPtr gh = g.GetHdc();
            Metafile mf = new Metafile(gh, EmfType.EmfOnly);
            g.ReleaseHdc();
            g.Dispose();

            g = Graphics.FromImage(mf);
            foreach (Figure f in copfig)
            {
                f.Draw(g);
            }
            g.Dispose();

            MetafileCopier.PutEnhMetafileOnClipboard(af.Handle, mf);

            if (Clipboard.ContainsImage()) MessageBox.Show("A copy has been successfully created!");
            else MessageBox.Show("Copying failed - no data reached Clipboard");
        }

        private void InsertTSMI_Click(object sender, EventArgs e)
        {
            try
            {
                Form2 af = (MdiChildren.ToList().Find((f) => { return f.Focused; }) as Form2);
                List<Figure> insfig = (List<Figure>)Clipboard.GetDataObject().GetData("FigureStorage");

                int[] t = insfig[0].GetTLBR();
                foreach (Figure f in insfig)
                {
                    t[0] = Math.Min(t[0], f.GetTLBR()[0]);
                    t[1] = Math.Min(t[1], f.GetTLBR()[1]);
                    t[2] = Math.Max(t[2], f.GetTLBR()[2]);
                    t[3] = Math.Max(t[3], f.GetTLBR()[3]);
                }
                foreach (Figure f in insfig) f.OffsetBy(-t[0], -t[1]);
                Size limbox = new Size(t[2]-t[0], t[3]-t[1]);
                if (af.ActiveSurface.Width < limbox.Width || af.ActiveSurface.Height < limbox.Height)
                    MessageBox.Show($"Unable to insert: inserted fragment ({limbox.Width}x{limbox.Height}) is too big.", "Image too big");

                else
                {
                    af.editedIndex.Clear();
                    foreach (Figure f in insfig)
                    {
                        af.editedIndex.Add(af.history.Count());
                        af.history.Add(f);
                    }
                    af.Modded();
                    af.Refresh();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error when inserting from clipboard: {ex.Message}");
            }
        }

        private void EditToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            foreach (ToolStripMenuItem t in EditToolStripMenuItem.DropDownItems) t.Enabled = true;
            if (MdiChildren.Length < 1)
            {
                CopyTSMI.Enabled = false;
                CutTSMI.Enabled = false;
                InsertTSMI.Enabled = false;
                DeleteSelectionTSMI.Enabled = false;
                return;
            }
            if ((MdiChildren.ToList().Find((f) => { return f.Focused; }) as Form2).editedIndex.Count < 1)
            {
                CopyTSMI.Enabled = false;
                CutTSMI.Enabled = false;
                DeleteSelectionTSMI.Enabled = false;
            }
            if (!Clipboard.ContainsData("FigureStorage")) InsertTSMI.Enabled = false;
        }

        #endregion

        #endregion

        private void CoordinateFlipper_Click(object sender, EventArgs e)
        {
            ShowCoordinates = CoordinateFlipper.Checked;
            SnapFlipper.Enabled = CoordinateFlipper.Checked;
            if (CForm != null) CForm.checkBox1.Checked = CoordinateFlipper.Checked;
            if (!CoordinateFlipper.Checked)
            {
                SnapToCoordinates = false;
                SnapFlipper.Checked = false;
            }
            foreach (Form2 f in MdiChildren) f.Refresh();
        }

        private void SnapFlipper_Click(object sender, EventArgs e)
        {
            SnapToCoordinates = !SnapToCoordinates;
            if (CForm != null) CForm.checkBox2.Checked = SnapFlipper.Checked;
        }
    }
}