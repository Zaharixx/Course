using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;

namespace PhotoStudioPlanConstructor
{
    class Note : PlanObject
    {
        private Bitmap bmp;
        private const int thickness = 5;
        private string title;
        private string comment;
        private Font fontTitle = new Font("Microsoft Sans Sefif", 6.5f, FontStyle.Bold);
        private Font fontComment = new Font("Microsoft Sans Sefif", 6f, FontStyle.Bold);
        public Note() : base() { SetBmp(); }
        public Note(int x, int y, int size) : base(x, y, size) { SetBmp(); }
        public Note(int x, int y, int width, int height) : base(x, y, width, height) {SetBmp(); }

        private void SetBmp()
        {
            int width = base.GetWidth();
            int height = base.GetHeight();
            bmp = new Bitmap(width, height);
            Clear();
        }

        private void Clear()
        {
            Graphics g = Graphics.FromImage(bmp);

            SolidBrush br1 = new SolidBrush(Color.FromArgb(255, 255, 255, 0));
            SolidBrush br2 = new SolidBrush(Color.FromArgb(255, 240, 240, 240));
            Pen pen = new Pen(Color.Black, thickness);
            Rectangle r1 = new Rectangle(0, 0, width - 1, (height - 1) / 4);
            Rectangle r2 = new Rectangle(0, 0, width - 1, height - 1);
            g.FillRectangle(br2, r2);
            g.FillRectangle(br1, r1);
            g.DrawRectangle(pen, r2);

            pen = new Pen(Color.Black, thickness / 2);
            g.DrawRectangle(pen, r1);
        }

        public Bitmap GetBmp()
        {
            return this.bmp;
        }

        public void SetText(string title, string comment)
        {
            Clear();
            this.title = title;
            this.comment = comment;
            Graphics g = Graphics.FromImage(bmp);
            int shiftx = 3;
            int shifty = 2;
            g.DrawString(title, fontTitle, new SolidBrush(Color.Black), new Rectangle(shiftx, shifty, base.GetWidth() - 1 - shiftx, (base.GetHeight() - 1) / 4 - shifty), null);
            g.DrawString(comment, fontComment, new SolidBrush(Color.Black), new Rectangle(shiftx, (base.GetHeight() - 1) / 4 + shifty, base.GetWidth() - 1 - shiftx, base.GetHeight() - 1 - shifty), null);
        }
    }
}
