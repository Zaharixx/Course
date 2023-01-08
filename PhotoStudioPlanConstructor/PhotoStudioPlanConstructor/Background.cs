using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhotoStudioPlanConstructor
{
    class Background : PlanObject
    {
        public Background() : base() { }
        public Background(int x, int y, int size) : base(x, y, size) { }
        public Background(int x, int y, int width, int height) : base(x, y, width, height) { }
    }
}
