namespace DB
{
    partial class Specialty
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
            this.label7 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.panel17 = new System.Windows.Forms.Panel();
            this.label29 = new System.Windows.Forms.Label();
            this.textBox43 = new System.Windows.Forms.TextBox();
            this.textBox55 = new System.Windows.Forms.TextBox();
            this.textBox56 = new System.Windows.Forms.TextBox();
            this.label91 = new System.Windows.Forms.Label();
            this.label92 = new System.Windows.Forms.Label();
            this.panel17.SuspendLayout();
            this.SuspendLayout();
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label7.Location = new System.Drawing.Point(347, 122);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(269, 29);
            this.label7.TabIndex = 44;
            this.label7.Text = "Добавление записи:";
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button1.Location = new System.Drawing.Point(365, 377);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(216, 46);
            this.button1.TabIndex = 45;
            this.button1.Text = "Сохранить";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // panel17
            // 
            this.panel17.Controls.Add(this.label29);
            this.panel17.Controls.Add(this.textBox43);
            this.panel17.Controls.Add(this.textBox55);
            this.panel17.Controls.Add(this.textBox56);
            this.panel17.Controls.Add(this.label91);
            this.panel17.Controls.Add(this.label92);
            this.panel17.Location = new System.Drawing.Point(210, 155);
            this.panel17.Margin = new System.Windows.Forms.Padding(4);
            this.panel17.Name = "panel17";
            this.panel17.Size = new System.Drawing.Size(513, 196);
            this.panel17.TabIndex = 46;
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Font = new System.Drawing.Font("Times New Roman", 11.25F);
            this.label29.Location = new System.Drawing.Point(5, 110);
            this.label29.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(132, 21);
            this.label29.TabIndex = 41;
            this.label29.Text = "Срок обучения";
            // 
            // textBox43
            // 
            this.textBox43.Font = new System.Drawing.Font("Times New Roman", 11.25F);
            this.textBox43.Location = new System.Drawing.Point(192, 107);
            this.textBox43.Margin = new System.Windows.Forms.Padding(4);
            this.textBox43.Name = "textBox43";
            this.textBox43.Size = new System.Drawing.Size(283, 29);
            this.textBox43.TabIndex = 40;
            // 
            // textBox55
            // 
            this.textBox55.Font = new System.Drawing.Font("Times New Roman", 11.25F);
            this.textBox55.Location = new System.Drawing.Point(192, 43);
            this.textBox55.Margin = new System.Windows.Forms.Padding(4);
            this.textBox55.Name = "textBox55";
            this.textBox55.Size = new System.Drawing.Size(283, 29);
            this.textBox55.TabIndex = 7;
            // 
            // textBox56
            // 
            this.textBox56.Font = new System.Drawing.Font("Times New Roman", 11.25F);
            this.textBox56.Location = new System.Drawing.Point(192, 75);
            this.textBox56.Margin = new System.Windows.Forms.Padding(4);
            this.textBox56.Name = "textBox56";
            this.textBox56.Size = new System.Drawing.Size(283, 29);
            this.textBox56.TabIndex = 8;
            // 
            // label91
            // 
            this.label91.AutoSize = true;
            this.label91.Font = new System.Drawing.Font("Times New Roman", 11.25F);
            this.label91.Location = new System.Drawing.Point(4, 81);
            this.label91.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label91.Name = "label91";
            this.label91.Size = new System.Drawing.Size(89, 21);
            this.label91.TabIndex = 14;
            this.label91.Text = "Название ";
            // 
            // label92
            // 
            this.label92.AutoSize = true;
            this.label92.Font = new System.Drawing.Font("Times New Roman", 11.25F);
            this.label92.Location = new System.Drawing.Point(4, 49);
            this.label92.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label92.Name = "label92";
            this.label92.Size = new System.Drawing.Size(29, 21);
            this.label92.TabIndex = 12;
            this.label92.Text = "ID";
            // 
            // Specialty
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(978, 572);
            this.Controls.Add(this.panel17);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label7);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Specialty";
            this.Text = "Specialty";
            this.panel17.ResumeLayout(false);
            this.panel17.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panel17;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.TextBox textBox43;
        private System.Windows.Forms.TextBox textBox55;
        private System.Windows.Forms.TextBox textBox56;
        private System.Windows.Forms.Label label91;
        private System.Windows.Forms.Label label92;
    }
}