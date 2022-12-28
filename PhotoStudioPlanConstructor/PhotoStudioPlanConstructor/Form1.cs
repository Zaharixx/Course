using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhotoStudioPlanConstructor
{
    public partial class Form1 : Form
    {
        private Room room;
        private List<Background> backgrounds;
        private List<PictureBox> bgHitBoxes;
        private List<SoftBox> softBoxes;
        private List<PictureBox> sbHitBoxes;
        private List<MainObject> mainObjects;
        private List<PictureBox> moHitBoxes;
        private List<OctoBox> octoBoxes;
        private List<PictureBox> obHitBoxes;

        private const int bigSize = 60;
        private const int smallSize = 40;

        private const string bgName = "bg";
        private const string sbName = "sb";
        private const string moName = "mo";
        private const string obName = "ob";
        private int bgcount;
        private int sbcount;
        private int mocount;
        private int obcount;

        private string pathBackground = Application.StartupPath + "\\Фон1.png";
        private string pathSmallSoft = Application.StartupPath + "\\Софт.png";
        private string pathBigSoft = Application.StartupPath + "\\СофтБ.png";
        private string pathOcto = Application.StartupPath + "\\Октобокс.png";
        private string pathMainObject = Application.StartupPath + "\\Объект.png";

        private bool rotating;
        private Thread threadRotate;
        private delegate void RefreshPBDel(Object ob);

        //private bool btndown;
        private Point startPos;
        public Form1()
        {
            InitializeComponent();
            rotating = false;
            room = new Room(pb1.Height, pb1.Width);
            backgrounds = new List<Background>();
            bgHitBoxes = new List<PictureBox>();
            softBoxes = new List<SoftBox>();
            sbHitBoxes = new List<PictureBox>();
            mainObjects = new List<MainObject>();
            moHitBoxes = new List<PictureBox>();
            octoBoxes = new List<OctoBox>();
            obHitBoxes = new List<PictureBox>();

            bgcount = 0;
            sbcount = 0;
            mocount = 0;
            obcount = 0;

            pb1.Refresh();

        }

        private void GraphicElement_MouseDown(object sender, MouseEventArgs e)
        {
            startPos = e.Location;
            //btndown = true;
        }

        private void GraphicElement_MouseMove(object sender, MouseEventArgs e)
        {
            //int mouseX = Cursor.Position.X - this.Location.X;
            //int mouseY = Cursor.Position.Y - this.Location.Y;
            if (e.Button == MouseButtons.Left)
            {
                Control control = (Control)sender;
                int edge = room.GetThickness();
                var shiftX = (e.X - startPos.X);
                var shiftY = (e.Y - startPos.Y);
                var x = control.Location.X;
                var y = control.Location.Y;
                if (shiftX >= 0 && control.Location.X + control.Size.Width < pb1.Location.X + pb1.Size.Width - edge ||
                    shiftX <= 0 && control.Location.X > pb1.Location.X + edge)
                    x += shiftX;
                if (shiftY >= 0 && control.Location.Y + control.Size.Height < pb1.Location.Y + pb1.Size.Height - edge ||
                    shiftY <= 0 && control.Location.Y > pb1.Location.Y + edge)
                    y += shiftY;
                control.Location = new Point(x, y);

                //((PictureBox)sender).Location = new Point((Cursor.Position.X - this.Location.X), (Cursor.Position.Y - this.Location.Y));


                string elementType = GetElementType(control);

                switch (elementType)
                {
                    case bgName:
                        {
                            int index = bgHitBoxes.IndexOf((PictureBox)control);

                            backgrounds[index].SetX(backgrounds[index].GetX() + shiftX);
                            backgrounds[index].SetY(backgrounds[index].GetY() + shiftY);
                            pb1.Refresh();
                        }
                        break;
                    case sbName:
                        {
                            int index = sbHitBoxes.IndexOf((PictureBox)control);
                            softBoxes[index].SetX(softBoxes[index].GetX() + shiftX);
                            softBoxes[index].SetY(softBoxes[index].GetY() + shiftY);
                        }
                        break;
                    case moName:
                        {
                            int index = moHitBoxes.IndexOf((PictureBox)control);
                            mainObjects[index].SetX(mainObjects[index].GetX() + shiftX);
                            mainObjects[index].SetY(mainObjects[index].GetY() + shiftY);
                        }
                        break;
                    case obName:
                        {
                            int index = obHitBoxes.IndexOf((PictureBox)control);
                            octoBoxes[index].SetX(octoBoxes[index].GetX() + shiftX);
                            octoBoxes[index].SetY(octoBoxes[index].GetY() + shiftY);
                        }
                        break;
                }


            }
        }

        private void GraphicElement_Click(object sender, EventArgs e)
        {
            Control control = (Control)sender;
            control.Focus();
        }

        private void GraphicElement_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            Control control = (Control)sender;
            string elementType = GetElementType(control);

            if (e.KeyCode == Keys.Delete)
            {
                switch (elementType) {
                    case bgName:
                        {
                            int index = bgHitBoxes.IndexOf((PictureBox)control);
                            bgHitBoxes.Remove((PictureBox)control);
                            backgrounds.RemoveAt(index);
                        }
                        break;
                    case sbName:
                        {
                            int index = sbHitBoxes.IndexOf((PictureBox)control);
                            sbHitBoxes.Remove((PictureBox)control);
                            softBoxes.RemoveAt(index);
                        }
                        break;
                    case moName:
                        {
                            int index = moHitBoxes.IndexOf((PictureBox)control);
                            moHitBoxes.Remove((PictureBox)control);
                            mainObjects.RemoveAt(index);
                        }
                        break;
                    case obName:
                        {
                            int index = obHitBoxes.IndexOf((PictureBox)control);
                            obHitBoxes.Remove((PictureBox)control);
                            octoBoxes.RemoveAt(index);
                        }
                        break;
                }
                control.Dispose();
            }
            else if (e.Control && e.KeyCode == Keys.R)
            {
                threadRotate = new Thread(RotationMode);
                threadRotate.Start(sender);
            }
            else if (e.KeyCode == Keys.Enter)
            {
                if (rotating)
                {
                    rotating = false;
                }
            }

        }

        public float GetAngle(Point p1, Point p2)
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
            else if (p1.X == 0  && p2.X == 0 && p1.Y == 0 && p2.Y == 0)
            {
                return 0.0F;
            }
            else
            {
                throw new Exception();
            }
        }

        private void RefreshPB(Object ob)
        {
            PictureBox pb = (PictureBox)ob;
            pb.Refresh();
        }

        private void RotationMode(Object sender)
        {
            Control control = (Control)sender;
            string elementType = GetElementType(control);
            rotating = true;
            int borderWidth = SystemInformation.BorderSize.Width;
            int captionHeight = SystemInformation.CaptionHeight;
            // координаты курсора относительно точки центра объекта
            Point LocalXY = new Point(Cursor.Position.X - this.Location.X - borderWidth - pb1.Location.X,
                                    Cursor.Position.Y - this.Location.Y - captionHeight - pb1.Location.Y);
            int index;
            Point center;
            float angle;
            switch (elementType)
            {
                case sbName:
                    {
                        index = sbHitBoxes.IndexOf((PictureBox)control);
                        center = softBoxes[index].GetCenter();
                        angle = softBoxes[index].GetAngle();
                    }
                    break;
                case moName:
                    {
                        index = moHitBoxes.IndexOf((PictureBox)control);
                        center = mainObjects[index].GetCenter();
                        angle = mainObjects[index].GetAngle();
                    }
                    break;
                case obName:
                    {
                        index = obHitBoxes.IndexOf((PictureBox)control);
                        center = octoBoxes[index].GetCenter();
                        angle = octoBoxes[index].GetAngle();
                    }
                    break;
                default:
                    {
                        throw new Exception();
                    }
            }
            while (rotating)
            {
                Point NewLocalXY = new Point(Cursor.Position.X - this.Location.X - borderWidth - pb1.Location.X,
                                             Cursor.Position.Y - this.Location.Y - captionHeight - pb1.Location.Y);
                Point ShiftXY = new Point(NewLocalXY.X - LocalXY.X,
                                          NewLocalXY.Y - LocalXY.Y);
                if (ShiftXY.X != 0 || ShiftXY.Y != 0)
                {
                    angle = GetAngle(center, NewLocalXY);
                    Bitmap bmp;
                    switch (elementType)
                    {
                        case sbName:
                            {
                                softBoxes[index].SetAngle(angle);
                                if (softBoxes[index].GetSize() == bigSize)
                                    bmp = new Bitmap(pathBigSoft);
                                else
                                    bmp = new Bitmap(pathSmallSoft);
                            }
                            break;
                        case moName:
                            {
                                mainObjects[index].SetAngle(angle);
                                bmp = new Bitmap(pathMainObject);
                            }
                            break;
                        case obName:
                            {
                                octoBoxes[index].SetAngle(angle);
                                bmp = new Bitmap(pathOcto);
                            }
                            break;
                        default:
                            {
                                throw new Exception();
                            }
                    }
                    bmp.MakeTransparent(Color.White);
                    Image image = RotateImage(bmp, angle);
                    ((PictureBox)control).Image = image;
                    RefreshPBDel refresh = RefreshPB;
                    Invoke(refresh, sender);
                }

                LocalXY = NewLocalXY;

                Thread.Sleep(50);
            }
        }

        /*private int GetElementIndex(Control elem)
        {
            string number = "";
            foreach (var ch in elem.Name)
            {
                if (ch >= '0' && ch <= '9')
                    number += ch;
            }
            return Convert.ToInt32(number);
        }*/

        private string GetElementType(Control elem)
        {
            string elementType = "";
            foreach (var ch in elem.Name)
            {
                if (ch >= 'a' && ch <= 'z')
                    elementType += ch;
            }
            return elementType;
        }

        public static Image RotateImage(Image img, float rotationAngle)
        {
            Bitmap bmp = new Bitmap(img.Width, img.Height);
            Graphics gfx = Graphics.FromImage(bmp);
            gfx.TranslateTransform((float)bmp.Width / 2, (float)bmp.Height / 2);
            gfx.RotateTransform(rotationAngle);
            gfx.TranslateTransform(-(float)bmp.Width / 2, -(float)bmp.Height / 2);
            gfx.InterpolationMode = InterpolationMode.HighQualityBicubic;
            gfx.DrawImage(img, new Point(0, 0));
            gfx.Dispose();
            return bmp;
        }

        /*private void GraphicElement_MouseUp(object sender, MouseEventArgs e)
        {
            btndown = false;
        }*/

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        private void treeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            PictureBox hitbox = new PictureBox();
            hitbox.MouseDown += GraphicElement_MouseDown;
            hitbox.MouseMove += GraphicElement_MouseMove;
            hitbox.PreviewKeyDown += GraphicElement_PreviewKeyDown;
            hitbox.Click += GraphicElement_Click;

            if (e.Node.Text == "Фон")
            {
                Background background = new Background(pb1.Width / 4, pb1.Height / 2, pb1.Width / 2);
                backgrounds.Add(background);
                hitbox.Name = bgName + bgcount.ToString();
                bgcount++;
                hitbox.Location = new Point(pb1.Location.X + background.GetX(), pb1.Location.Y + background.GetY());
                hitbox.Width = background.GetWidth();
                hitbox.Height = background.GetThickness();
                Bitmap image = new Bitmap(pathBackground);
                image.MakeTransparent(Color.White);
                hitbox.Image = image;
                this.Controls.Add(hitbox);
                hitbox.BringToFront();

                bgHitBoxes.Add(hitbox);
            }
            else if (e.Node.Text == "Большой софт")
            {
                SoftBox softBox = new SoftBox();
                int size = bigSize;
                softBox.SetSize(size);
                softBox.SetX(pb1.Width / 2 - size / 2);
                softBox.SetY(pb1.Height / 2 - size / 2);
                softBoxes.Add(softBox);
                hitbox.Name = sbName + sbcount.ToString();
                sbcount++;
                hitbox.Location = new Point(pb1.Location.X + softBox.GetX(), pb1.Location.Y + softBox.GetY());
                hitbox.Width = size;
                hitbox.Height = size;
                Bitmap image = new Bitmap(pathBigSoft);
                image.MakeTransparent(Color.White);
                hitbox.Image = image;
                this.Controls.Add(hitbox);
                hitbox.BringToFront();

                sbHitBoxes.Add(hitbox);
            }
            else if (e.Node.Text == "Маленький софт")
            {
                SoftBox softBox = new SoftBox();
                int size = smallSize;
                softBox.SetSize(size);
                softBox.SetX(pb1.Width / 2 - size / 2);
                softBox.SetY(pb1.Height / 2 - size / 2);
                softBoxes.Add(softBox);
                hitbox.Name = sbName + sbcount.ToString();
                sbcount++;
                hitbox.Location = new Point(pb1.Location.X + softBox.GetX(), pb1.Location.Y + softBox.GetY());
                hitbox.Width = size;
                hitbox.Height = size;
                Bitmap image = new Bitmap(pathSmallSoft);
                image.MakeTransparent(Color.White);
                hitbox.Image = image;
                this.Controls.Add(hitbox);
                hitbox.BringToFront();

                sbHitBoxes.Add(hitbox);
            }
            else if (e.Node.Text == "Октобокс")
            {
                OctoBox octoBox = new OctoBox();
                int size = bigSize;
                octoBox.SetSize(size);
                octoBox.SetX(pb1.Width / 2 - size / 2);
                octoBox.SetY(pb1.Height / 2 - size / 2);
                octoBoxes.Add(octoBox);
                hitbox.Name = obName + obcount.ToString();
                obcount++;
                hitbox.Location = new Point(pb1.Location.X + octoBox.GetX(), pb1.Location.Y + octoBox.GetY());
                hitbox.Width = size;
                hitbox.Height = size;
                Bitmap image = new Bitmap(pathOcto);
                image.MakeTransparent(Color.White);
                hitbox.Image = image;
                this.Controls.Add(hitbox);
                hitbox.BringToFront();

                obHitBoxes.Add(hitbox);
            }
            else if (e.Node.Text == "Объект")
            {
                MainObject mainObject = new MainObject();
                int size = mainObject.GetSize();
                mainObject.SetX(pb1.Width / 2 - size / 2);
                mainObject.SetY(pb1.Height / 2 - size / 2);
                mainObjects.Add(mainObject);
                hitbox.Name = moName + mocount.ToString();
                mocount++;
                hitbox.Location = new Point(pb1.Location.X + mainObject.GetX(), pb1.Location.Y + mainObject.GetY());
                hitbox.Width = size;
                hitbox.Height = size;
                Bitmap image = new Bitmap(pathMainObject);
                image.MakeTransparent(Color.White);
                hitbox.Image = image;
                this.Controls.Add(hitbox);
                hitbox.BringToFront();

                moHitBoxes.Add(hitbox);
            }

            pb1.Refresh();
        }

        private void pb1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(room.GetRoomBitmap(), 0, 0, pb1.Width, pb1.Height);
            /*for (int i = 0; i < backgrounds.Count; i++)
                e.Graphics.DrawImage(backgrounds[i].GetBackgroundBitmap(), backgrounds[i].GetX(), backgrounds[i].GetY(), backgrounds[i].GetWidth(), backgrounds[i].GetThickness());*/
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pb1_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void pb1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {

        }

        private void pb1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
