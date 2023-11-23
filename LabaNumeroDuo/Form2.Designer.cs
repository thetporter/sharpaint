
namespace LabaNumeroDuo
{
    partial class Form2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.MouseCoordinate = new System.Windows.Forms.ToolStripStatusLabel();
            this.Separator = new System.Windows.Forms.ToolStripStatusLabel();
            this.ImageSize = new System.Windows.Forms.ToolStripStatusLabel();
            this.Separator2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.PenInfo = new System.Windows.Forms.ToolStripStatusLabel();
            this.MainPenColor = new System.Windows.Forms.ToolStripStatusLabel();
            this.Separator3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.BackgroundInfo = new System.Windows.Forms.ToolStripStatusLabel();
            this.BackBrushColor = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MouseCoordinate,
            this.Separator,
            this.ImageSize,
            this.Separator2,
            this.PenInfo,
            this.MainPenColor,
            this.Separator3,
            this.BackgroundInfo,
            this.BackBrushColor});
            this.statusStrip1.Location = new System.Drawing.Point(0, 393);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(684, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // MouseCoordinate
            // 
            this.MouseCoordinate.Name = "MouseCoordinate";
            this.MouseCoordinate.Size = new System.Drawing.Size(25, 17);
            this.MouseCoordinate.Text = "0, 0";
            // 
            // Separator
            // 
            this.Separator.Name = "Separator";
            this.Separator.Size = new System.Drawing.Size(13, 17);
            this.Separator.Text = "│";
            // 
            // ImageSize
            // 
            this.ImageSize.Name = "ImageSize";
            this.ImageSize.Size = new System.Drawing.Size(65, 17);
            this.ImageSize.Text = "800x600 px";
            // 
            // Separator2
            // 
            this.Separator2.Name = "Separator2";
            this.Separator2.Size = new System.Drawing.Size(13, 17);
            this.Separator2.Text = "│";
            // 
            // PenInfo
            // 
            this.PenInfo.Name = "PenInfo";
            this.PenInfo.Size = new System.Drawing.Size(142, 17);
            this.PenInfo.Text = "Main Pen Width: 1, Color:";
            // 
            // MainPenColor
            // 
            this.MainPenColor.Name = "MainPenColor";
            this.MainPenColor.Size = new System.Drawing.Size(18, 17);
            this.MainPenColor.Text = "█";
            // 
            // Separator3
            // 
            this.Separator3.Name = "Separator3";
            this.Separator3.Size = new System.Drawing.Size(13, 17);
            this.Separator3.Text = "│";
            // 
            // BackgroundInfo
            // 
            this.BackgroundInfo.Name = "BackgroundInfo";
            this.BackgroundInfo.Size = new System.Drawing.Size(96, 17);
            this.BackgroundInfo.Text = "Background: YES";
            // 
            // BackBrushColor
            // 
            this.BackBrushColor.ForeColor = System.Drawing.Color.White;
            this.BackBrushColor.Name = "BackBrushColor";
            this.BackBrushColor.Size = new System.Drawing.Size(18, 17);
            this.BackBrushColor.Text = "█";
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightGray;
            this.ClientSize = new System.Drawing.Size(684, 415);
            this.Controls.Add(this.statusStrip1);
            this.Cursor = System.Windows.Forms.Cursors.Cross;
            this.DoubleBuffered = true;
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Name = "Form2";
            this.Text = "Paint (real)";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form2_Closing);
            this.Load += new System.EventHandler(this.Form2_Load);
            this.Shown += new System.EventHandler(this.Form2_Shown);
            this.Scroll += new System.Windows.Forms.ScrollEventHandler(this.Form2_Scroll);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form2_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Rect_Start);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MouseMovement);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Rect_Finish);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel MouseCoordinate;
        private System.Windows.Forms.ToolStripStatusLabel ImageSize;
        private System.Windows.Forms.ToolStripStatusLabel PenInfo;
        private System.Windows.Forms.ToolStripStatusLabel BackgroundInfo;
        private System.Windows.Forms.ToolStripStatusLabel Separator;
        private System.Windows.Forms.ToolStripStatusLabel Separator2;
        private System.Windows.Forms.ToolStripStatusLabel MainPenColor;
        private System.Windows.Forms.ToolStripStatusLabel Separator3;
        private System.Windows.Forms.ToolStripStatusLabel BackBrushColor;
    }
}