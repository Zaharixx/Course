﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;

namespace PhotoStudioPlanConstructor
{
    class SoftBox : PlanObject
    {
        public SoftBox() : base() { }
        public SoftBox(int x, int y, int size) : base(x, y, size) { }
        public SoftBox(int x, int y, int width, int height) : base(x, y, width, height) { }
    }
}
