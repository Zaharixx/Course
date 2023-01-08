using PhotoStudioPlanConstructor;
using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PhotoStudioPlanConstructor.Tests
{
    [TestClass()]
    public class MathematicsTests
    {
        [TestMethod()]
        public void GetAngleTest1()
        {
            Point p1 = new Point(10, 10);
            Point p2 = new Point(15, 10);

            Form1 form1 = new Form1();
            float actual = Mathematics.GetAngle(p1, p2);
            float expected = 0.0F;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void GetAngleTest2()
        {
            Point p1 = new Point(10, 10);
            Point p2 = new Point(15, 15);

            Form1 form1 = new Form1();
            float actual = Mathematics.GetAngle(p1, p2);
            float expected = 45.0F;

            Assert.IsTrue(actual > expected - 0.1F && actual < expected + 0.1F);
        }

        [TestMethod()]
        public void GetAngleTest3()
        {
            Point p1 = new Point(10, 10);
            Point p2 = new Point(10, 15);

            Form1 form1 = new Form1();
            float actual = Mathematics.GetAngle(p1, p2);
            float expected = 90.0F;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void GetAngleTest4()
        {
            Point p1 = new Point(10, 10);
            Point p2 = new Point(5, 15);

            Form1 form1 = new Form1();
            float actual = Mathematics.GetAngle(p1, p2);
            float expected = 135.0F;

            Assert.IsTrue(actual > expected - 0.1F && actual < expected + 0.1F);
        }

        [TestMethod()]
        public void GetAngleTest5()
        {
            Point p1 = new Point(10, 10);
            Point p2 = new Point(5, 10);

            Form1 form1 = new Form1();
            float actual = Mathematics.GetAngle(p1, p2);
            float expected = 180.0F;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void GetAngleTest6()
        {
            Point p1 = new Point(10, 10);
            Point p2 = new Point(5, 5);

            Form1 form1 = new Form1();
            float actual = Mathematics.GetAngle(p1, p2);
            float expected = 225.0F;

            Assert.IsTrue(actual > expected - 0.1F && actual < expected + 0.1F);
        }

        [TestMethod()]
        public void GetAngleTest7()
        {
            Point p1 = new Point(10, 10);
            Point p2 = new Point(10, 5);

            Form1 form1 = new Form1();
            float actual = Mathematics.GetAngle(p1, p2);
            float expected = 270.0F;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void GetAngleTest8()
        {
            Point p1 = new Point(10, 10);
            Point p2 = new Point(15, 5);

            Form1 form1 = new Form1();
            float actual = Mathematics.GetAngle(p1, p2);
            float expected = 315.0F;

            Assert.IsTrue(actual > expected - 0.1F && actual < expected + 0.1F);
        }

        [TestMethod()]
        public void GetAngleTest9()
        {
            Point p1 = new Point(10, 10);
            Point p2 = new Point(12, 11);

            Form1 form1 = new Form1();
            float actual = Mathematics.GetAngle(p1, p2);
            float expected = 26.5F;

            Assert.IsTrue(actual > expected - 0.1F && actual < expected + 0.1F);
        }

        [TestMethod()]
        public void GetAngleTest10()
        {
            Point p1 = new Point(10, 10);
            Point p2 = new Point(22, 13);

            Form1 form1 = new Form1();
            float actual = Mathematics.GetAngle(p1, p2);
            float expected = 14.0F;

            Assert.IsTrue(actual > expected - 0.1F && actual < expected + 0.1F);
        }

        [TestMethod()]
        public void GetAngleTest11()
        {
            Point p1 = new Point(10, 10);
            Point p2 = new Point(12, 18);

            Form1 form1 = new Form1();
            float actual = Mathematics.GetAngle(p1, p2);
            float expected = 75.9F;

            Assert.IsTrue(actual > expected - 0.1F && actual < expected + 0.1F);
        }

        [TestMethod()]
        public void GetAngleTest12()
        {
            Point p1 = new Point(10, 10);
            Point p2 = new Point(8, 16);

            Form1 form1 = new Form1();
            float actual = Mathematics.GetAngle(p1, p2);
            float expected = 108.4F;

            Assert.IsTrue(actual > expected - 0.1F && actual < expected + 0.1F);
        }
    }
}