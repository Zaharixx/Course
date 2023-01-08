using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;

namespace PhotoStudioPlanConstructor
{
    class OctoBox : PlanObject
    {
        public OctoBox() : base() { }
        public OctoBox(int x, int y, int size) : base(x, y, size) { }
        public OctoBox(int x, int y, int width, int height) : base(x, y, width, height) { }
    }
}
