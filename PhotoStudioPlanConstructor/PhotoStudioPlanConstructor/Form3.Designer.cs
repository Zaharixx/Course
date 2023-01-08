
namespace PhotoStudioPlanConstructor
{
    partial class Form3
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
            this.Apply = new System.Windows.Forms.Button();
            this.ElemName = new System.Windows.Forms.Label();
            this.NameTB = new System.Windows.Forms.TextBox();
            this.PictureElem = new System.Windows.Forms.PictureBox();
            this.EditingArea = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.PictureElem)).BeginInit();
            this.EditingArea.SuspendLayout();
            this.SuspendLayout();
            // 
            // Apply
            // 
            this.Apply.Location = new System.Drawing.Point(338, 173);
            this.Apply.Name = "Apply";
            this.Apply.Size = new System.Drawing.Size(115, 34);
            this.Apply.TabIndex = 0;
            this.Apply.Text = "Применить";
            this.Apply.UseVisualStyleBackColor = true;
            this.Apply.Click += new System.EventHandler(this.Apply_Click);
            // 
            // ElemName
            // 
            this.ElemName.AutoSize = true;
            this.ElemName.Location = new System.Drawing.Point(76, 54);
            this.ElemName.Name = "ElemName";
            this.ElemName.Size = new System.Drawing.Size(44, 20);
            this.ElemName.TabIndex = 1;
            this.ElemName.Text = "Имя:";
            // 
            // NameTB
            // 
            this.NameTB.Location = new System.Drawing.Point(196, 48);
            this.NameTB.Name = "NameTB";
            this.NameTB.Size = new System.Drawing.Size(228, 26);
            this.NameTB.TabIndex = 2;
            // 
            // PictureElem
            // 
            this.PictureElem.Location = new System.Drawing.Point(323, 49);
            this.PictureElem.Name = "PictureElem";
            this.PictureElem.Size = new System.Drawing.Size(146, 144);
            this.PictureElem.TabIndex = 3;
            this.PictureElem.TabStop = false;
            // 
            // EditingArea
            // 
            this.EditingArea.Controls.Add(this.ElemName);
            this.EditingArea.Controls.Add(this.NameTB);
            this.EditingArea.Controls.Add(this.Apply);
            this.EditingArea.Location = new System.Drawing.Point(-1, 214);
            this.EditingArea.Name = "EditingArea";
            this.EditingArea.Size = new System.Drawing.Size(803, 236);
            this.EditingArea.TabIndex = 4;
            this.EditingArea.TabStop = false;
            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.EditingArea);
            this.Controls.Add(this.PictureElem);
            this.Name = "Form3";
            this.Text = "Edit";
            ((System.ComponentModel.ISupportInitialize)(this.PictureElem)).EndInit();
            this.EditingArea.ResumeLayout(false);
            this.EditingArea.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Apply;
        private System.Windows.Forms.Label ElemName;
        public System.Windows.Forms.TextBox NameTB;
        public System.Windows.Forms.PictureBox PictureElem;
        public System.Windows.Forms.GroupBox EditingArea;
    }
}