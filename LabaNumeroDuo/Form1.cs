using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;



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
        public int ActiveWidth = 1;

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        //Создание нового рисунка:
        private void NewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 nova = new Form2();
            nova.MdiParent = this;
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
                Form2 nova = new Form2();
                nova.MdiParent = this;
                nova.Text = opener.FileName;
                ActiveForms++;
                nova.HomeAddress = opener.FileName;
                BinaryFormatter formatter = new BinaryFormatter();
                Stream stream = new FileStream(opener.FileName, FileMode.Open, FileAccess.Read, FileShare.Read);
                nova.history = (List<Figure>)formatter.Deserialize(stream);
                stream.Close();
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
            if ((SaveAs)|(forma.HomeAddress.Length<5))
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
            BinaryFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(forma.HomeAddress, FileMode.Create, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, forma.history);
            stream.Close();
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
            if (saveToolStripMenuItem.DropDownItems.Contains(e.ClickedItem)) SaveAs = false; else SaveAs = true; 
            foreach (Form2 forma in MdiChildren)
            {
                if (e.ClickedItem == allToolStripMenuItem) saveForm(forma, SaveAs);
                else if (forma.Text == e.ClickedItem.Text) 
                {
                    saveForm(forma, SaveAs);
                }
            }
        }

        //Change Paint Style
        private void changePaintStyleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form3 Dial = new Form3(this);
            Dial.SetDefaults(ActiveColor, BackColor, ActiveWidth);
            Dial.Show();
        }
    }
}