using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;

namespace PhotoStudioPlanConstructor
{
    class PlanObject
    {
        private int width;
        private int height;
        private int x;
        private int y;
        private float angle;

        private const int defaultsize = 50;

        public PlanObject()
        {
            this.width = defaultsize;
            this.height = defaultsize;
            this.x = 0;
            this.y = 0;
            this.angle = 0.0F;
        }

        public PlanObject(int x, int y, int size)
        {
            this.width = size;
            this.height = size;
            this.x = x;
            this.y = y;
            this.angle = 0.0F;
        }

        public PlanObject(int x, int y, int width, int height)
        {
            this.width = width;
            this.height = height;
            this.x = x;
            this.y = y;
            this.angle = 0.0F;
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

        public void SetWidth(int width)
        {
            this.width = width;
        }

        public int GetHeight()
        {
            return this.height;
        }

        public void SetHeight(int height)
        {
            this.height = height;
        }

        public void SetSize(int size)
        {
            this.width = size;
            this.height = size;
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
            return new Point(this.x + this.width / 2, this.y + this.height / 2);
        }
    }
}
