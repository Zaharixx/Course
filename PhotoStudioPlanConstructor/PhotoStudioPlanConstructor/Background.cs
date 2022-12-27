using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhotoStudioPlanConstructor
{
    class Background
    {
        private int width;
        private const int thickness = 10;
        private Bitmap background;
        private int x = 0;
        private int y = 0; //{get; set;}

        public Background(int width)
        {
            this.width = width;
            background = new Bitmap(this.width, thickness);
            Graphics g = Graphics.FromImage(background);
            Pen pen = new Pen(Color.Black, 3);
            Rectangle r = new Rectangle(0, 0, this.width - 1, thickness - 1);
            g.DrawRectangle(pen, r);
        }

        public Background(int x, int y, int width)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            background = new Bitmap(this.width, thickness);
            Graphics g = Graphics.FromImage(background);
            Pen pen = new Pen(Color.Black, 3);
            Rectangle r = new Rectangle(0, 0, this.width - 1, thickness - 1);
            g.DrawRectangle(pen, r);
        }

        public Bitmap GetBackgroundBitmap()
        {
            return this.background;
        }
        public int GetX()
        {
            return this.x;
        }

        public void SetX(int x)
        {
            this.x = x;
        }

        public int GetY()
        {
            return this.y;
        }

        public void SetY(int y)
        {
            this.y = y;
        }

        public int GetWidth()
        {
            return this.width;
        }

        public int GetThickness()
        {
            return thickness;
        }
    }
}
