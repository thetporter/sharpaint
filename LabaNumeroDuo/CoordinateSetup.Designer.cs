
namespace LabaNumeroDuo
{
    partial class CoordinateSetup
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
            this.COffsetSetter = new System.Windows.Forms.NumericUpDown();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.COffsetSetter)).BeginInit();
            this.SuspendLayout();
            // 
            // COffsetSetter
            // 
            this.COffsetSetter.Location = new System.Drawing.Point(230, 36);
            this.COffsetSetter.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.COffsetSetter.Name = "COffsetSetter";
            this.COffsetSetter.Size = new System.Drawing.Size(48, 23);
            this.COffsetSetter.TabIndex = 10;
            this.COffsetSetter.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.COffsetSetter.ValueChanged += new System.EventHandler(this.COffsetSetter_ValueChanged);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(12, 12);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(209, 19);
            this.checkBox1.TabIndex = 1;
            this.checkBox1.Text = "Отображать координатную сетку";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(12, 37);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(115, 19);
            this.checkBox2.TabIndex = 2;
            this.checkBox2.Text = "Привязка фигур";
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(156, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 15);
            this.label1.TabIndex = 3;
            this.label1.Text = "Шаг сетки:";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 62);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(266, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "Привязать существущие фигуры";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // CoordinateSetup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(293, 96);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.checkBox2);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.COffsetSetter);
            this.Name = "CoordinateSetup";
            this.Text = "Настройка координатной сетки";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CoordinateSetup_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.COffsetSetter)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown COffsetSetter;
        public System.Windows.Forms.CheckBox checkBox1;
        public System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
    }
}