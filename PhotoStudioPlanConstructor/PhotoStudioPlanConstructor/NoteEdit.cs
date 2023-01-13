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
    public partial class NoteEdit : Form
    {
        public NoteEdit()
        {
            InitializeComponent();
        }

        private void close_Click(object sender, EventArgs e)
        {
            DataBuffer.Title = title.Text;
            DataBuffer.Comment = comment.Text;
            Close();
        }
    }
}
