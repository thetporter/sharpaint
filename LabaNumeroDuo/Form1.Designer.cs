
namespace LabaNumeroDuo
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.WinToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.NewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.allToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.changePaintStyleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.internalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showChildrenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.WinToolStripMenuItem,
            this.changePaintStyleToolStripMenuItem,
            this.internalToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.MdiWindowListItem = this.WinToolStripMenuItem;
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(6, 3, 0, 3);
            this.menuStrip1.Size = new System.Drawing.Size(1006, 30);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // WinToolStripMenuItem
            // 
            this.WinToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.NewToolStripMenuItem,
            this.loadToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem});
            this.WinToolStripMenuItem.Name = "WinToolStripMenuItem";
            this.WinToolStripMenuItem.Size = new System.Drawing.Size(59, 24);
            this.WinToolStripMenuItem.Text = "Файл";
            // 
            // NewToolStripMenuItem
            // 
            this.NewToolStripMenuItem.Name = "NewToolStripMenuItem";
            this.NewToolStripMenuItem.Size = new System.Drawing.Size(201, 26);
            this.NewToolStripMenuItem.Text = "Новый";
            this.NewToolStripMenuItem.Click += new System.EventHandler(this.NewToolStripMenuItem_Click);
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(201, 26);
            this.loadToolStripMenuItem.Text = "Загрузить...";
            this.loadToolStripMenuItem.Click += new System.EventHandler(this.loadToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.allToolStripMenuItem});
            this.saveToolStripMenuItem.Enabled = false;
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(201, 26);
            this.saveToolStripMenuItem.Text = "Сохранить";
            this.saveToolStripMenuItem.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.ptsPicked);
            // 
            // allToolStripMenuItem
            // 
            this.allToolStripMenuItem.Name = "allToolStripMenuItem";
            this.allToolStripMenuItem.Size = new System.Drawing.Size(110, 26);
            this.allToolStripMenuItem.Text = "All";
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Enabled = false;
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(201, 26);
            this.saveAsToolStripMenuItem.Text = "Сохранить как...";
            this.saveAsToolStripMenuItem.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.ptsPicked);
            // 
            // changePaintStyleToolStripMenuItem
            // 
            this.changePaintStyleToolStripMenuItem.Name = "changePaintStyleToolStripMenuItem";
            this.changePaintStyleToolStripMenuItem.Size = new System.Drawing.Size(145, 24);
            this.changePaintStyleToolStripMenuItem.Text = "Change Paint Style";
            this.changePaintStyleToolStripMenuItem.Click += new System.EventHandler(this.changePaintStyleToolStripMenuItem_Click);
            // 
            // internalToolStripMenuItem
            // 
            this.internalToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showChildrenToolStripMenuItem});
            this.internalToolStripMenuItem.Name = "internalToolStripMenuItem";
            this.internalToolStripMenuItem.Size = new System.Drawing.Size(73, 24);
            this.internalToolStripMenuItem.Text = "Internal";
            // 
            // showChildrenToolStripMenuItem
            // 
            this.showChildrenToolStripMenuItem.Name = "showChildrenToolStripMenuItem";
            this.showChildrenToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.showChildrenToolStripMenuItem.Text = "Show Children";
            this.showChildrenToolStripMenuItem.Click += new System.EventHandler(this.showChildrenToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1006, 721);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Form1";
            this.Text = "El Laba";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.MdiChildActivate += new System.EventHandler(this.NewPageOpened);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem WinToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem internalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showChildrenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem NewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem allToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem changePaintStyleToolStripMenuItem;
    }
}

