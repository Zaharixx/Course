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
        private Dictionary<string, List<(PlanObject, PictureBox, string)>> Elements;
        private Dictionary<string, int> elementsCount;

        private const int bgSize = 200;
        private const int bigSize = 60;
        private const int middleSize = 50;
        private const int smallSize = 40;

        private const string ROTATE = "Вращать";
        private const string DELETE = "Удалить";

        private const string ROTATE_S = "Вращение";
        private const string MOVE_S = "Перемещение";
        private const string DELETE_S = "Удаление";

        private const string bgName = "backgrounds";
        private const string sbName = "softboxes";
        private const string moName = "mainObjects";
        private const string obName = "octoboxes";

        private const string COLOR1 = "Белый";
        private const string COLOR2 = "Светло-серый";
        private const string COLOR3 = "Коричневый";

        private string activeFloorCl;

        private string pathBackground = Application.StartupPath + "\\Фон2.png";
        private string pathSmallSoft = Application.StartupPath + "\\Софт.png";
        private string pathBigSoft = Application.StartupPath + "\\СофтБ.png";
        private string pathOcto = Application.StartupPath + "\\Октобокс.png";
        private string pathMainObject = Application.StartupPath + "\\Объект.png";
        private string pathScale = Application.StartupPath + "\\Метр.png";

        private const string SELECTED = "Выбранный элемент: ";

        private bool rotating;
        private Thread threadRotate;
        private delegate void RefreshPBDel(Object ob);

        private string currentElementType;
        private int currentElementIndex;
        private string currentElementImagePath;

        private List<ToolStripMenuItem> actions;
        private List<ToolStripStatusLabel> statuses;
        private List<ToolStripMenuItem> floorColors;

        private Point startPos;
        public Form1()
        {
            InitializeComponent();
            rotating = false;
            room = new Room(pb1.Height, pb1.Width);
            Elements = new Dictionary<string, List<(PlanObject, PictureBox, string)>>() 
            {   
                { "backgrounds", new List<(PlanObject, PictureBox, string)>() },
                { "softboxes", new List<(PlanObject, PictureBox, string)>() },
                { "octoboxes", new List<(PlanObject, PictureBox, string)>() },
                { "mainObjects", new List<(PlanObject, PictureBox, string)>() } 
            };
            elementsCount = new Dictionary<string, int>();
            actions = new List<ToolStripMenuItem>() 
            {
                new ToolStripMenuItem(ROTATE) { Name = ROTATE},
                new ToolStripMenuItem(DELETE) { Name = DELETE} 
            };
            foreach (var action in actions)
                action.Click += toolStripMenuItemClick2;
            statuses = new List<ToolStripStatusLabel>()
            {
                new ToolStripStatusLabel(ROTATE_S) { Name = ROTATE_S},
                new ToolStripStatusLabel(MOVE_S) { Name = MOVE_S},
                new ToolStripStatusLabel(DELETE_S) { Name = DELETE_S}
            };
            floorColors = new List<ToolStripMenuItem>()
            {
                new ToolStripMenuItem(COLOR1) { Name = COLOR1},
                new ToolStripMenuItem(COLOR2) { Name = COLOR2},
                new ToolStripMenuItem(COLOR3) { Name = COLOR3}
            };
            foreach (var color in floorColors)
                color.Click += toolStripMenuItemClick3;
            toolStripMenuItem1.DropDownItems.AddRange(floorColors.ToArray());

            currentElementType = "";
            currentElementIndex = -1;
            currentElementImagePath = "";

            activeFloorCl = COLOR1;

            Bitmap scale = new Bitmap(pathScale);
            scale.MakeTransparent(Color.White);
            scalePB.Image = scale;

            scalePB.Refresh();
            pb1.Refresh();
        }

        private void GraphicElement_MouseDown(object sender, MouseEventArgs e)
        {
            startPos = e.Location;
            Control control = (Control)sender;
            SelectElem(control);
            statusStrip1.Items.Add(new ToolStripStatusLabel(MOVE_S) { Name = MOVE_S });
        }

        private void GraphicElement_MouseUp(object sender, MouseEventArgs e)
        {
            statusStrip1.Items.RemoveByKey(MOVE_S);
        }

        private void GraphicElement_MouseMove(object sender, MouseEventArgs e)
        {
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
                pb1.Refresh();


                Elements[currentElementType][currentElementIndex].Item1.SetX(Elements[currentElementType][currentElementIndex].Item1.GetX() + shiftX);
                Elements[currentElementType][currentElementIndex].Item1.SetY(Elements[currentElementType][currentElementIndex].Item1.GetY() + shiftY);
            }
        }

        private void GraphicElement_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            PictureBox pb = (PictureBox)sender;
            Form3 EditWindow = new Form3();
            Size pictureElemSize = pb.Size;
            EditWindow.PictureElem.Size = pictureElemSize;
            EditWindow.PictureElem.Location = new Point((EditWindow.Width - 2 * SystemInformation.BorderSize.Width - pictureElemSize.Width) / 2, 20);
            EditWindow.PictureElem.Image = new Bitmap(currentElementImagePath);
            int heigth = SystemInformation.CaptionHeight + 2 * 20 + pictureElemSize.Height + EditWindow.EditingArea.Height + 10;
            EditWindow.Size = new Size(EditWindow.Size.Width, heigth);
            EditWindow.EditingArea.Location = new Point(0, EditWindow.Size.Height - SystemInformation.CaptionHeight - EditWindow.EditingArea.Size.Height - 10);
            EditWindow.NameTB.Text = Elements[currentElementType][currentElementIndex].Item3;
            EditWindow.ShowDialog();
            UpdateData();
        }

        private void GraphicElement_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            Control control = (Control)sender;

            if (e.KeyCode == Keys.Delete)
            {
                DeleteCurrentElem();
            }
            else if (e.Control && e.KeyCode == Keys.R)
            { 
                if (currentElementType != bgName)
                    RotateElem(sender);
            }
            else if (e.KeyCode == Keys.Enter)
            {
                if (rotating)
                {
                    rotating = false;
                    threadRotate.Abort();
                    statusStrip1.Items.RemoveByKey(ROTATE_S);
                }
            }

        }

        private void DeleteCurrentElem()
        {
            statusStrip1.Items.Add(new ToolStripStatusLabel(DELETE_S) { Name = DELETE_S });
            toolStripDropDownButton1.DropDownItems.RemoveByKey(Elements[currentElementType][currentElementIndex].Item3);
            Elements[currentElementType][currentElementIndex].Item2.Dispose();
            Elements[currentElementType].RemoveAt(currentElementIndex);
            DeselectElem();
            pb1.Refresh();
        }

        private void RotateElem(Object sender)
        {
            try
            {
                statusStrip1.Items.Add(new ToolStripStatusLabel(ROTATE_S) { Name = ROTATE_S });
                threadRotate = new Thread(RotationMode);
                threadRotate.Start(sender);
            }
            catch (Exception ex)
            {
                Form2 form2 = new Form2();
                form2.ErrorMessage.Text = ex.Message;
                form2.ShowDialog();
                threadRotate.Abort();
            }
        }

        private void toolStripMenuItemClick1(Object sender, EventArgs e)
        {
            ToolStripMenuItem item = (ToolStripMenuItem)sender;
            var currentElement = GetByName(item.Name).Item2;
            SelectElem(currentElement);
        }

        private void toolStripMenuItemClick2(Object sender, EventArgs e)
        {
            ToolStripMenuItem item = (ToolStripMenuItem)sender;
            switch (item.Name)
            {
                case ROTATE:
                    {
                        RotateElem(Elements[currentElementType][currentElementIndex].Item2);
                    }
                    break;
                case DELETE:
                    {
                        DeleteCurrentElem();
                    }
                    break;
            }
        }

        private void toolStripMenuItemClick3(Object sender, EventArgs e)
        {
            ToolStripMenuItem item = (ToolStripMenuItem)sender;
            switch(item.Name)
            {
                case COLOR1:
                    {
                        room.ChangeColor(Color.FromArgb(255, 240, 240, 240));
                    }
                    break;
                case COLOR2:
                    {
                        room.ChangeColor(Color.FromArgb(255, 180, 180, 180));
                    }
                    break;
                case COLOR3:
                    {
                        room.ChangeColor(Color.FromArgb(255, 120, 60, 0));
                    }
                    break;
            }
            pb1.Refresh();
        }

        private void SelectElem(Control control)
        {
            DeselectElem();
            currentElementType = GetElementType(control);
            currentElementIndex = Elements[currentElementType].FindIndex(obj => obj.Item2 == (PictureBox)control);
            currentElementImagePath = GetElementImagePath(currentElementType, currentElementIndex);

            if (currentElementType == bgName)
                {
                    foreach (var action in actions)
                        if (action.Name != ROTATE)
                            toolStripDropDownButton2.DropDownItems.Add(action);
                }
            else
                toolStripDropDownButton2.DropDownItems.AddRange(actions.ToArray());
            string selectedElem = SELECTED + Elements[currentElementType][currentElementIndex].Item3;
            statusStrip1.Items.Add(new ToolStripStatusLabel(selectedElem) { Name = selectedElem });
            control.Focus();
        }

        private void DeselectElem()
        {
            currentElementType = "";
            currentElementIndex = -1;
            currentElementImagePath = "";

            toolStripDropDownButton2.DropDownItems.Clear();
            statusStrip1.Items.Clear();
            this.Focus();
        }

        private void RefreshPB(Object ob)
        {
            PictureBox pb = (PictureBox)ob;
            pb.Refresh();
        }

        private void RotationMode(Object sender)
        {
            Control control = (Control)sender;
            rotating = true;
            int borderWidth = SystemInformation.BorderSize.Width;
            int captionHeight = SystemInformation.CaptionHeight;
            // координаты курсора относительно точки центра объекта
            Point LocalXY = new Point(Cursor.Position.X - this.Location.X - borderWidth - pb1.Location.X,
                                    Cursor.Position.Y - this.Location.Y - captionHeight - pb1.Location.Y);

            Point center = Elements[currentElementType][currentElementIndex].Item1.GetCenter();
            float angle = Elements[currentElementType][currentElementIndex].Item1.GetAngle();
 
            while (rotating)
            {
                Point NewLocalXY = new Point(Cursor.Position.X - this.Location.X - borderWidth - pb1.Location.X,
                                             Cursor.Position.Y - this.Location.Y - captionHeight - pb1.Location.Y);
                Point ShiftXY = new Point(NewLocalXY.X - LocalXY.X,
                                          NewLocalXY.Y - LocalXY.Y);
                if (ShiftXY.X != 0 || ShiftXY.Y != 0)
                {
                    angle = Mathematics.GetAngle(center, NewLocalXY);
                    Elements[currentElementType][currentElementIndex].Item1.SetAngle(angle);
                    Bitmap bmp = new Bitmap(currentElementImagePath);
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

        private (PlanObject, PictureBox, string) GetByName(string name)
        {
            foreach (var group in Elements)
                foreach (var element in group.Value)
                    if (element.Item3 == name)
                        return element;
            return (null, null, null);
        }

        private string GetElementType(Control elem)
        {
            PictureBox elemPB = (PictureBox)elem;
            string elementType = "";
            foreach (var element in Elements)
            {
                foreach (var value in element.Value)
                {
                    if (elemPB == value.Item2)
                        elementType = element.Key;
                }
            }
            return elementType;
        }

        private string GetElementImagePath(string key, int index)
        {
            switch (key)
            {
                case bgName:
                    return pathBackground;
                case sbName:
                    if (Elements[key][index].Item1.GetWidth() == bigSize)
                        return pathBigSoft;
                    else
                        return pathSmallSoft;
                case obName:
                    return pathOcto;
                case moName:
                    return pathMainObject;
                default:
                    return "";
            }
        }

        private void UpdateData()
        {
            string oldName = Elements[currentElementType][currentElementIndex].Item3;
            string newName = DataBuffer.Name;
            statusStrip1.Items.Clear();
            statusStrip1.Items.Add(new ToolStripStatusLabel(newName) { Name = newName });

            int index = toolStripDropDownButton1.DropDownItems.IndexOfKey(oldName);
            toolStripDropDownButton1.DropDownItems.RemoveAt(index);
            toolStripDropDownButton1.DropDownItems.Insert(index, new ToolStripMenuItem(newName) { Name = newName });

            var elem = Elements[currentElementType][currentElementIndex];
            elem.Item3 = DataBuffer.Name;
            Elements[currentElementType][currentElementIndex] = elem;
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        private void treeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            PictureBox hitbox = new PictureBox();
            hitbox.MouseDown += GraphicElement_MouseDown;
            hitbox.MouseUp += GraphicElement_MouseUp;
            hitbox.MouseMove += GraphicElement_MouseMove;
            hitbox.PreviewKeyDown += GraphicElement_PreviewKeyDown;
            hitbox.MouseDoubleClick += GraphicElement_MouseDoubleClick;
            string name = "";

            if (e.Node.Text == "Фон")
            {
                int size = bgSize;
                Background background = new Background(pb1.Width / 2 - size / 2, pb1.Height / 2 - size / 2, size);
                hitbox.Location = new Point(pb1.Location.X + background.GetX(), pb1.Location.Y + background.GetY());
                hitbox.Width = size;
                hitbox.Height = size;
                Bitmap image = new Bitmap(pathBackground);
                image.MakeTransparent(Color.White);
                hitbox.Image = image;
                this.Controls.Add(hitbox);
                hitbox.BringToFront();

                if (!elementsCount.ContainsKey(bgName))
                    elementsCount.Add(bgName, 1);
                else
                    elementsCount[bgName]++;

                name = "Фон " + elementsCount[bgName];
                ToolStripMenuItem item = new ToolStripMenuItem(name) { Name = name };
                item.Click += toolStripMenuItemClick1;
                toolStripDropDownButton1.DropDownItems.Add(item);
                Elements[bgName].Add((background, hitbox, name));
            }
            else if (e.Node.Text == "Большой софт")
            {
                int size = bigSize;
                SoftBox softBox = new SoftBox(pb1.Width / 2 - size / 2, pb1.Height / 2 - size / 2, size);
                hitbox.Location = new Point(pb1.Location.X + softBox.GetX(), pb1.Location.Y + softBox.GetY());
                hitbox.Width = size;
                hitbox.Height = size;
                Bitmap image = new Bitmap(pathBigSoft);
                image.MakeTransparent(Color.White);
                hitbox.Image = image;
                this.Controls.Add(hitbox);
                hitbox.BringToFront();

                if (!elementsCount.ContainsKey(sbName))
                    elementsCount.Add(sbName, 1);
                else
                    elementsCount[sbName]++;

                name = "Софтбокс " + elementsCount[sbName];
                ToolStripMenuItem item = new ToolStripMenuItem(name) { Name = name };
                item.Click += toolStripMenuItemClick1;
                toolStripDropDownButton1.DropDownItems.Add(item);
                Elements[sbName].Add((softBox, hitbox, name));
            }
            else if (e.Node.Text == "Маленький софт")
            {
                int size = smallSize;
                SoftBox softBox = new SoftBox(pb1.Width / 2 - size / 2, pb1.Height / 2 - size / 2, size);
                hitbox.Location = new Point(pb1.Location.X + softBox.GetX(), pb1.Location.Y + softBox.GetY());
                hitbox.Width = size;
                hitbox.Height = size;
                Bitmap image = new Bitmap(pathSmallSoft);
                image.MakeTransparent(Color.White);
                hitbox.Image = image;
                this.Controls.Add(hitbox);
                hitbox.BringToFront();

                if (!elementsCount.ContainsKey(sbName))
                    elementsCount.Add(sbName, 1);
                else
                    elementsCount[sbName]++;

                name = "Софтбокс " + elementsCount[sbName];
                ToolStripMenuItem item = new ToolStripMenuItem(name) { Name = name };
                item.Click += toolStripMenuItemClick1;
                toolStripDropDownButton1.DropDownItems.Add(item);
                Elements[sbName].Add((softBox, hitbox, name));
            }
            else if (e.Node.Text == "Октобокс")
            {
                int size = bigSize;
                OctoBox octoBox = new OctoBox(pb1.Width / 2 - size / 2, pb1.Height / 2 - size / 2, size);
                hitbox.Location = new Point(pb1.Location.X + octoBox.GetX(), pb1.Location.Y + octoBox.GetY());
                hitbox.Width = size;
                hitbox.Height = size;
                Bitmap image = new Bitmap(pathOcto);
                image.MakeTransparent(Color.White);
                hitbox.Image = image;
                this.Controls.Add(hitbox);
                hitbox.BringToFront();

                if (!elementsCount.ContainsKey(obName))
                    elementsCount.Add(obName, 1);
                else
                    elementsCount[obName]++;

                name = "Октобокс " + elementsCount[obName];
                ToolStripMenuItem item = new ToolStripMenuItem(name) { Name = name };
                item.Click += toolStripMenuItemClick1;
                toolStripDropDownButton1.DropDownItems.Add(item);
                Elements[obName].Add((octoBox, hitbox, name));
            }
            else if (e.Node.Text == "Объект")
            {
                int size = smallSize;
                MainObject mainObject = new MainObject(pb1.Width / 2 - size / 2, pb1.Height / 2 - size / 2, size);
                hitbox.Location = new Point(pb1.Location.X + mainObject.GetX(), pb1.Location.Y + mainObject.GetY());
                hitbox.Width = size;
                hitbox.Height = size;
                Bitmap image = new Bitmap(pathMainObject);
                image.MakeTransparent(Color.White);
                hitbox.Image = image;
                this.Controls.Add(hitbox);
                hitbox.BringToFront();

                if (!elementsCount.ContainsKey(moName))
                    elementsCount.Add(moName, 1);
                else
                    elementsCount[moName]++;

                name = "Объект " + elementsCount[moName];
                ToolStripMenuItem item = new ToolStripMenuItem(name) { Name = name };
                item.Click += toolStripMenuItemClick1;
                toolStripDropDownButton1.DropDownItems.Add(item);
                Elements[moName].Add((mainObject, hitbox, name));
            }

            try
            {
                SelectElem(hitbox);
            }
            catch (KeyNotFoundException) { }
            pb1.Refresh();
        }

        private void pb1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(room.GetRoomBitmap(), 0, 0, pb1.Width, pb1.Height);
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

        private void statusStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void scalePB_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
