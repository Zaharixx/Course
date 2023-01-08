using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;

namespace PhotoStudioPlanConstructor
{
    public static class Mathematics
    {
        public static float GetAngle(Point p1, Point p2)
        {
            if (p2.X - p1.X > 0 && p2.Y - p1.Y >= 0)
            {
                double alpha = Math.Atan(((double)p2.Y - p1.Y) / (p2.X - p1.X));
                return (float)(alpha * 180 / Math.PI);
            }
            else if (p2.X - p1.X < 0 && p2.Y - p1.Y >= 0)
            {
                double alpha = Math.PI - Math.Atan(((double)p2.Y - p1.Y) / (p1.X - p2.X));
                return (float)(alpha * 180 / Math.PI);
            }
            else if (p2.X - p1.X < 0 && p2.Y - p1.Y < 0)
            {
                double alpha = Math.PI + Math.Atan(((double)p1.Y - p2.Y) / (p1.X - p2.X));
                return (float)(alpha * 180 / Math.PI);
            }
            else if (p2.X - p1.X > 0 && p2.Y - p1.Y < 0)
            {
                double alpha = 2 * Math.PI - Math.Atan(((double)p1.Y - p2.Y) / (p2.X - p1.X));
                return (float)(alpha * 180 / Math.PI);
            }
            else if (p2.X - p1.X == 0 && p2.Y - p1.Y > 0)
            {
                return 90.0F;
            }
            else if (p2.X - p1.X == 0 && p2.Y - p1.Y < 0)
            {
                return 270.0F;
            }
            else if (p1.X == 0 && p2.X == 0 && p1.Y == 0 && p2.Y == 0)
            {
                return 0.0F;
            }
            else
            {
                throw new Exception("Неожиданный результат функции нахождения текущего угла.");
            }
        }
    }
}
