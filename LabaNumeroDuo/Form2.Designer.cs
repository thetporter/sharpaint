
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
            this.SuspendLayout();
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(782, 553);
            this.Cursor = System.Windows.Forms.Cursors.Cross;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Form2";
            this.Text = "Paint (real)";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form2_Closing);
            this.Load += new System.EventHandler(this.Form2_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form2_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Rect_Start);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MouseMovement);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Rect_Finish);
            this.ResumeLayout(false);

        }

        #endregion
    }
}