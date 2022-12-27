using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;

namespace PhotoStudioPlanConstructor
{
    class MainObject
    {
        private const int size = 50;
        private Bitmap softbox;
        private int x;
        private int y;
        private Point center;
        private float angle;

        public MainObject()
        {
            this.x = 0;
            this.y = 0;
            SetCenter();
            this.angle = 0.0F;
        }
        public MainObject(int x, int y)
        {
            this.x = x;
            this.y = y;
            SetCenter();
            this.angle = 0.0F;
        }

        public float GetAngle()
        {
            return this.angle;
        }

        public void SetAngle(float angle)
        {
            this.angle = angle;
        }
        public Point GetCenter()
        {
            return this.center;
        }
        private void SetCenter()
        {
            this.center = new Point(this.x + size / 2, this.y + size / 2);
        }

        public int GetSize()
        {
            return size;
        }

        public int GetX()
        {
            return this.x;
        }

        public void SetX(int x)
        {
            this.x = x;
            SetCenter();
        }

        public int GetY()
        {
            return this.y;
        }

        public void SetY(int y)
        {
            this.y = y;
            SetCenter();
        }
    }
}
