using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;

namespace PhotoStudioPlanConstructor
{
    class Room
    {
        private int length;
        private int width;
        private Bitmap room;
        private const int thickness = 5;
        public Room(int length, int width)
        {
            this.length = length;
            this.width = width;
            room = new Bitmap(width, length);
            Pen pen = new Pen(Color.Black, thickness);
            Rectangle r = new Rectangle(0, 0, width-1, length-1);
            Graphics g = Graphics.FromImage(room);
            g.DrawRectangle(pen, r);
        }

        public Bitmap GetRoomBitmap()
        {
            return this.room;
        }

        public int GetThickness()
        {
            return thickness;
        }

        public void ChangeColor(Color clr)
        {
            Pen pen = new Pen(Color.Black, thickness);
            Rectangle r = new Rectangle(0, 0, width - 1, length - 1);
            Graphics g = Graphics.FromImage(this.room);
            g.Clear(clr);
            g.DrawRectangle(pen, r);
        }

    }
}
