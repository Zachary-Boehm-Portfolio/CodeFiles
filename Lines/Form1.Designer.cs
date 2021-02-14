namespace Lines
{
    partial class Form1
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
            this.Canvas = new System.Windows.Forms.Panel();
            this.Control = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.Line_Number = new System.Windows.Forms.TextBox();
            this.Angle = new System.Windows.Forms.TextBox();
            this.Length = new System.Windows.Forms.TextBox();
            this.Increment = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.Control.SuspendLayout();
            this.SuspendLayout();
            // 
            // Canvas
            // 
            this.Canvas.BackColor = System.Drawing.SystemColors.Window;
            this.Canvas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Canvas.Location = new System.Drawing.Point(0, 0);
            this.Canvas.Name = "Canvas";
            this.Canvas.Size = new System.Drawing.Size(455, 450);
            this.Canvas.TabIndex = 0;
            this.Canvas.Paint += new System.Windows.Forms.PaintEventHandler(this.Canvas_Paint);
            // 
            // Control
            // 
            this.Control.BackColor = System.Drawing.SystemColors.GrayText;
            this.Control.Controls.Add(this.button1);
            this.Control.Controls.Add(this.Increment);
            this.Control.Controls.Add(this.Length);
            this.Control.Controls.Add(this.Angle);
            this.Control.Controls.Add(this.Line_Number);
            this.Control.Controls.Add(this.label4);
            this.Control.Controls.Add(this.label3);
            this.Control.Controls.Add(this.label2);
            this.Control.Controls.Add(this.label1);
            this.Control.Dock = System.Windows.Forms.DockStyle.Top;
            this.Control.Location = new System.Drawing.Point(0, 0);
            this.Control.Name = "Control";
            this.Control.Size = new System.Drawing.Size(455, 34);
            this.Control.TabIndex = 1;
            this.Control.Paint += new System.Windows.Forms.PaintEventHandler(this.Panel2_Paint);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.GrayText;
            this.label1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "# Lines";
            this.label1.Click += new System.EventHandler(this.Label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.SystemColors.GrayText;
            this.label2.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label2.Location = new System.Drawing.Point(84, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Angle";
            this.label2.Click += new System.EventHandler(this.Label2_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.SystemColors.GrayText;
            this.label3.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label3.Location = new System.Drawing.Point(148, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Length";
            this.label3.Click += new System.EventHandler(this.Label3_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.SystemColors.GrayText;
            this.label4.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label4.Location = new System.Drawing.Point(218, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Increment";
            this.label4.Click += new System.EventHandler(this.Label4_Click);
            // 
            // Line_Number
            // 
            this.Line_Number.Location = new System.Drawing.Point(54, 5);
            this.Line_Number.Name = "Line_Number";
            this.Line_Number.Size = new System.Drawing.Size(30, 20);
            this.Line_Number.TabIndex = 4;
            this.Line_Number.Text = "1";
            this.Line_Number.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.Line_Number.TextChanged += new System.EventHandler(this.Line_Number_TextChanged);
            // 
            // Angle
            // 
            this.Angle.Location = new System.Drawing.Point(118, 5);
            this.Angle.Name = "Angle";
            this.Angle.Size = new System.Drawing.Size(30, 20);
            this.Angle.TabIndex = 5;
            this.Angle.Text = "90";
            this.Angle.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.Angle.TextChanged += new System.EventHandler(this.Angle_TextChanged);
            // 
            // Length
            // 
            this.Length.Location = new System.Drawing.Point(188, 5);
            this.Length.Name = "Length";
            this.Length.Size = new System.Drawing.Size(30, 20);
            this.Length.TabIndex = 6;
            this.Length.Text = "50";
            this.Length.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.Length.TextChanged += new System.EventHandler(this.Length_TextChanged);
            // 
            // Increment
            // 
            this.Increment.Location = new System.Drawing.Point(272, 5);
            this.Increment.Name = "Increment";
            this.Increment.Size = new System.Drawing.Size(30, 20);
            this.Increment.TabIndex = 7;
            this.Increment.Text = "1";
            this.Increment.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.Increment.TextChanged += new System.EventHandler(this.Increment_TextChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(361, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 8;
            this.button1.Text = "Go!";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(455, 450);
            this.Controls.Add(this.Control);
            this.Controls.Add(this.Canvas);
            this.Name = "Form1";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Fun with line patterns";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Control.ResumeLayout(false);
            this.Control.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel Canvas;
        private System.Windows.Forms.Panel Control;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox Increment;
        private System.Windows.Forms.TextBox Length;
        private System.Windows.Forms.TextBox Angle;
        private System.Windows.Forms.TextBox Line_Number;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
    }
}

