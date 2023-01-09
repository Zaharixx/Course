using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhotoStudioPlanConstructor
{
    public partial class Save : Form
    {
        public Save()
        {
            InitializeComponent();
        }

        private void Jpg_Click(object sender, EventArgs e)
        {
            Png.Checked = false;
        }

        private void Png_Click(object sender, EventArgs e)
        {
            Jpg.Checked = false;
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            DataBuffer.FilePath = FilePathTB.Text + FileNameTB.Text;
            if (Jpg.Checked)
                DataBuffer.FilePath += ".jpg";
            else
                DataBuffer.FilePath += ".png";
            DataBuffer.FilePath.Replace("/", "\\");
            DataBuffer.saveClick = true;
            Close();
        }
    }
}
