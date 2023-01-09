
namespace PhotoStudioPlanConstructor
{
    partial class Save
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
            this.SaveBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Jpg = new System.Windows.Forms.RadioButton();
            this.Png = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.FilePathTB = new System.Windows.Forms.TextBox();
            this.FileNameTB = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // SaveBtn
            // 
            this.SaveBtn.Location = new System.Drawing.Point(241, 238);
            this.SaveBtn.Name = "SaveBtn";
            this.SaveBtn.Size = new System.Drawing.Size(112, 39);
            this.SaveBtn.TabIndex = 0;
            this.SaveBtn.Text = "Сохранить";
            this.SaveBtn.UseVisualStyleBackColor = true;
            this.SaveBtn.Click += new System.EventHandler(this.SaveBtn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(29, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(126, 22);
            this.label1.TabIndex = 1;
            this.label1.Text = "Путь к файлу:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(29, 83);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(110, 22);
            this.label2.TabIndex = 2;
            this.label2.Text = "Имя файла:";
            // 
            // Jpg
            // 
            this.Jpg.AutoSize = true;
            this.Jpg.Location = new System.Drawing.Point(440, 147);
            this.Jpg.Name = "Jpg";
            this.Jpg.Size = new System.Drawing.Size(55, 24);
            this.Jpg.TabIndex = 3;
            this.Jpg.TabStop = true;
            this.Jpg.Text = "jpg";
            this.Jpg.UseVisualStyleBackColor = true;
            this.Jpg.Click += new System.EventHandler(this.Jpg_Click);
            // 
            // Png
            // 
            this.Png.AutoSize = true;
            this.Png.Location = new System.Drawing.Point(440, 192);
            this.Png.Name = "Png";
            this.Png.Size = new System.Drawing.Size(61, 24);
            this.Png.TabIndex = 4;
            this.Png.TabStop = true;
            this.Png.Text = "png";
            this.Png.UseVisualStyleBackColor = true;
            this.Png.Click += new System.EventHandler(this.Png_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(29, 147);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(199, 22);
            this.label3.TabIndex = 5;
            this.label3.Text = "Сохранить в формате:";
            // 
            // FilePathTB
            // 
            this.FilePathTB.Location = new System.Drawing.Point(214, 34);
            this.FilePathTB.Name = "FilePathTB";
            this.FilePathTB.Size = new System.Drawing.Size(346, 26);
            this.FilePathTB.TabIndex = 6;
            // 
            // FileNameTB
            // 
            this.FileNameTB.Location = new System.Drawing.Point(214, 82);
            this.FileNameTB.Name = "FileNameTB";
            this.FileNameTB.Size = new System.Drawing.Size(346, 26);
            this.FileNameTB.TabIndex = 7;
            // 
            // Save
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(593, 303);
            this.Controls.Add(this.FileNameTB);
            this.Controls.Add(this.FilePathTB);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Png);
            this.Controls.Add(this.Jpg);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.SaveBtn);
            this.Name = "Save";
            this.Text = "Save";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button SaveBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton Jpg;
        private System.Windows.Forms.RadioButton Png;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox FilePathTB;
        private System.Windows.Forms.TextBox FileNameTB;
    }
}