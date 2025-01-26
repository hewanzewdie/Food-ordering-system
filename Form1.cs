using System.Collections;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace Riri_s_Paint_Application
{
    public partial class Form1 : Form
    {
        bool paint = false;
        int index = 1;
        int X, Y, sX, sY, cX, cY;
        Color colrf;
        Point pointX, pointY;
        Bitmap drawingSurface;
        Graphics graphics;
        Pen pen = new Pen(Color.Black, 2);
        Pen eraser = new Pen(Color.White, 2);

        Stack<Bitmap> undoStack = new Stack<Bitmap>();
        Stack<Bitmap> redoStack = new Stack<Bitmap>();

        public Form1()
        {
            InitializeComponent();
            button_pencil.BackColor = button_pencil.BackColor = Color.Purple;
            drawingSurface = new Bitmap(pictureBox.Width, pictureBox.Height);
            graphics = Graphics.FromImage(drawingSurface);

            pictureBox.Image = drawingSurface;
            pen.Color = colrf = pictureBox_pencolor.BackColor;
            eraser = new Pen(Color.White, 2);

            // event handlers
            pictureBox.MouseClick += pictureBox_MouseClick;
            pictureBox.MouseMove += pictureBox_MouseMove;
            pictureBox.MouseDown += pictureBox_MouseDown;
            pictureBox.MouseUp += pictureBox_MouseUp;
            button1.Click += button1_Click;
            buttoncolor1.Click += buttoncolor1_Click;
            button_circle.Click += button_circle_Click;
            button_eraser.Click += button_eraser_Click;
            button_line.Click += button_line_Click;
            button_rectangle.Click += button_rectangle_Click;
            button_triangle.Click += button_triangle_Click;
            button_ellipse.Click += button_ellipse_Click;
            button_colorset.Click += button_colorset_Click;
            button_save.Click += buttonsave_Click;
            button_clear.Click += buttonclear_Click;
            button_undo.Click += buttonundo_Click;
            button_redo.Click += buttonredo_Click;
            trackBar.Scroll += trackBar_Scroll;


            buttoncolor1.Click += (s, e) => ChangeColor(Color.Black);
            buttoncolor2.Click += (s, e) => ChangeColor(Color.Gray);
            buttoncolor3.Click += (s, e) => ChangeColor(Color.DarkRed);
            buttoncolor4.Click += (s, e) => ChangeColor(Color.Red);
            buttoncolor5.Click += (s, e) => ChangeColor(Color.OrangeRed);
            buttoncolor6.Click += (s, e) => ChangeColor(Color.Yellow);
            buttoncolor7.Click += (s, e) => ChangeColor(Color.Green);
            buttoncolor8.Click += (s, e) => ChangeColor(Color.Turquoise);
            buttoncolor9.Click += (s, e) => ChangeColor(Color.Indigo);
            buttoncolor10.Click += (s, e) => ChangeColor(Color.Purple);
            buttoncolor11.Click += (s, e) => ChangeColor(Color.White);
            buttoncolor12.Click += (s, e) => ChangeColor(Color.LightGray);
            buttoncolor13.Click += (s, e) => ChangeColor(Color.Brown);
            buttoncolor14.Click += (s, e) => ChangeColor(Color.RosyBrown);
            buttoncolor15.Click += (s, e) => ChangeColor(Color.Gold);
            buttoncolor16.Click += (s, e) => ChangeColor(Color.LightYellow);
            buttoncolor17.Click += (s, e) => ChangeColor(Color.Lime);
            buttoncolor18.Click += (s, e) => ChangeColor(Color.LightSkyBlue);
            buttoncolor19.Click += (s, e) => ChangeColor(Color.DarkBlue);
            buttoncolor20.Click += (s, e) => ChangeColor(Color.MediumPurple);

            MakePictureBoxesCircular();
        }
        private void MakePictureBoxesCircular()
        {
            foreach (var picBox in tableLayoutPanel1.Controls.OfType<PictureBox>())
            {

                picBox.Width = picBox.Height;
                using (GraphicsPath path = new GraphicsPath())
                {
                    path.AddEllipse(0, 0, picBox.Width, picBox.Height);
                    picBox.Region = new Region(path);
                }
            }
        }
        private void SaveImage()
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "PNG Files|*.png|JPEG Files|*.jpg|BMP Files|*.bmp";
            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                drawingSurface.Save(saveDialog.FileName, ImageFormat.Png);
            }
        }

        private void ClearCanvas()
        {

            PushUndoState();
            graphics.Clear(Color.White);
            pictureBox.Image = drawingSurface;
            pictureBox.Refresh();
        }

        private void Undo()
        {
            if (undoStack.Count > 0)
            {
                Bitmap lastState = undoStack.Pop();
                redoStack.Push(new Bitmap(drawingSurface));
                drawingSurface = lastState;
                pictureBox.Image = drawingSurface;
                pictureBox.Refresh();
            }
        }
        private void Redo()
        {
            if (redoStack.Count > 0)
            {
                Bitmap lastState = redoStack.Pop();
                undoStack.Push(new Bitmap(drawingSurface));
                drawingSurface = lastState;
                pictureBox.Image = drawingSurface;
                pictureBox.Refresh();
            }
        }
        private void PushUndoState()
        {
            undoStack.Push(new Bitmap(drawingSurface));
            redoStack.Clear();
        }
        private void Button_clear_Click(object? sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void ChangeColor(Color color)
        {
            pen.Color = color;
            pictureBox_pencolor.BackColor = color;
        }
        private Point SetPoint(Point screenPoint)
        {
            float scaleX = (float)pictureBox.Image.Width / pictureBox.Width;
            float scaleY = (float)pictureBox.Image.Height / pictureBox.Height;

            int imageX = (int)(screenPoint.X * scaleX);
            int imageY = (int)(screenPoint.Y * scaleY);

            return new Point(imageX, imageY);
        }
        private void Validate(Bitmap bitmap, Stack<Point> pointStack, int x, int y, Color colorNew, Color colorOld)
        {
            Color cx = bitmap.GetPixel(x, y);
            if (cx == colorOld)
            {
                pointStack.Push(new Point(x, y));
                bitmap.SetPixel(x, y, colorNew);
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
        }
        private void panel2_Paint(object sender, PaintEventArgs e)
        {
        }
        private void buttonpencile_Click(object sender, EventArgs e)
        {
        }
        private void button1_Click(object sender, EventArgs e)
        {
            index = 1;
            pen.Color = pictureBox_pencolor.BackColor;

        }
        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void label1_Click_1(object sender, EventArgs e)
        {
        }
        private void comboBox_brush_SelectedIndexChanged(object sender, EventArgs e)
        { 
        }
        private void buttoncolor6_Click(object sender, EventArgs e)
        {

        }

        private void buttoncolor1_Click(object sender, EventArgs e)
        {

        }

        private void button_circle_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        { 
        }

        private void pictureBox_Click(object sender, EventArgs e)
        {
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
        }
        private void FillUp(Bitmap bitmap, int x, int y, Color newColor)
        {
            Color oldColor = bitmap.GetPixel(x, y);
            Stack<Point> pixel = new Stack<Point>();
            pixel.Push(new Point(x, y));
            bitmap.SetPixel(x, y, newColor);
            if (oldColor != newColor) return;
            while (pixel.Count > 0)
            {
                Point point = (Point)pixel.Pop();
                if (point.X > 0 && point.Y > 0 && point.X < bitmap.Width - 1 && point.Y < bitmap.Height - 1)
                {
                    Validate(bitmap, pixel, point.X, point.Y, newColor, oldColor);
                    Validate(bitmap, pixel, point.X, point.Y - 1, newColor, oldColor);
                    Validate(bitmap, pixel, point.X + 1, point.Y, newColor, oldColor);
                    Validate(bitmap, pixel, point.X, point.Y + 1, newColor, oldColor);
                }
            }
        }

        private void pictureBox_MouseClick(object sender, MouseEventArgs e)
        {
            Point point = SetPoint(e.Location);
            if (index == 2)
                FillUp(drawingSurface, point.X, point.Y, colrf);
            else if (index == 3)
            {
                colrf = pen.Color = pictureBox_pencolor.BackColor = ((Bitmap)pictureBox.Image).GetPixel(point.X, point.Y);
                pen.Color = colrf;
                pictureBox_pencolor.BackColor = colrf;
            }
            pictureBox.Image = drawingSurface;
        }
        private void pictureBox_MouseMove(object sender, MouseEventArgs e)
        { 
            if (paint)
            {
                if (index == 1) // Pen 
                {
                    pointX = e.Location;
                    graphics.DrawLine(pen, pointX, pointY);
                    pointY = pointX;
                }
                else if (index == 4) // Eraser 
                {
                    pointX = e.Location;
                    graphics.DrawLine(eraser, pointX, pointY);
                    pointY = pointX;
                }
                else if (index == 5) // Line 
                {
                    sX = e.X - cX;
                    sY = e.Y - cY;
                    pictureBox.Refresh();
                }
                else if (index == 6) // Rectangle
                {
                    sX = e.X - cX;
                    sY = e.Y - cY;
                    pictureBox.Refresh();
                }
                else if (index == 7) // Ellipse 
                {
                    sX = e.X - cX;
                    sY = e.Y - cY;
                    pictureBox.Refresh();
                }
                else if (index == 8) // Triangle shape 
                {
                    sX = e.X - cX;
                    sY = e.Y - cY;
                    pictureBox.Refresh();
                }
                else if (index == 10)
                {
                    sX = e.X - cX;
                    sY = e.Y - cY;
                    pictureBox.Refresh();
                }
            }
            pictureBox.Refresh();
        }

        private void pictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            paint = false;
            PushUndoState();
            sX = X - cX;
            sY = Y - cY;

            if (index == 5)
            {
                graphics.DrawLine(pen, cX, cY, X, Y);
            }
            else if (index == 6)
            {
                graphics.DrawRectangle(pen, cX, cY, sX, sY);
            }
            else if (index == 7)
            {
                graphics.DrawEllipse(pen, cX, cY, sX, sY);
            }
            else if (index == 8)
            {
                DrawTriangle(cX, cY, e.X, e.Y);
            }
            pictureBox.Refresh();
        }

        private void pictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            paint = true;
            Point imagePoint = SetPoint(e.Location);
            cX = imagePoint.X;
            cY = imagePoint.Y;
            X = e.X; Y = e.Y;
        }

        private void button_pencil_MouseClick(object sender, MouseEventArgs e)
        {
            index =;
        }
        private void DrawTriangle(int startX, int startY, int endX, int endY)
        {
            int thirdX = (startX + endX) / 2;
            int thirdY = startY - (endY - startY);

            Point[] trianglePoints = new Point[]
            {
            new Point(startX, startY),
            new Point(endX, endY),
            new Point(thirdX, thirdY)
            };
            graphics.DrawPolygon(pen, trianglePoints);
        }
        private void button_line_Click(object sender, EventArgs e)
        {
            index = 5;
        }
        /* private void button_circle_Click(object sender, EventArgs e)
         {
             index = 10;
         }*/
        private void button_rectangle_Click(object sender, EventArgs e)
        {
            index = 6;
        }

        private void button_triangle_Click(object sender, EventArgs e)
        {
            index = 8;
        }
        private void button_ellipse_Click(object sender, EventArgs e)
        {
            index = 7;
        }
        private void button_colordropper_Click(object sender, EventArgs e)
        {
            index = 3;
        }

        private void button_eraser_Click(object sender, EventArgs e)
        {
            index = 4;
            eraser.Color = pictureBox.BackColor;

        }

        private void button_fillcolor_Click(object sender, EventArgs e)
        {
            index = 2;
        }

        private void button_colorset_Click(object sender, EventArgs e)
        {


            ColorDialog colorDialog = new ColorDialog();
            colorDialog.Color = pen.Color;

            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                pen.Color = colorDialog.Color;
                pictureBox_pencolor.BackColor = pen.Color;

                eraser.Color = Color.White;
            }
        }

        private void pictureBox_Paint(object sender, PaintEventArgs e)
        {
            Graphics graphicsPaint = e.Graphics;
            if (index == 5)
            {
                graphicsPaint.DrawLine(pen, cX, cY, X, Y);
            }
            else if (index == 6)
            {
                graphicsPaint.DrawRectangle(pen, cX, cY, sX, sY);
            }
            else if (index == 7)
            {
                graphicsPaint.DrawEllipse(pen, cX, cY, sX, sY);
            }
        }

        private void buttonpenWidth_Click(object sender, EventArgs e)
        {
            foreach (var btn in panel_penwidth.Controls.OfType<Button>())
                btn.BackColor = Color.White;
            Button artanButton = (Button)sender;
            artanButton.BackColor = Color.Purple;
            pen.Width = eraser.Width = Convert.ToInt32(artanButton.Tag);
        }

        private void button_click(object sender, EventArgs e)
        {
            foreach (var btn in tableLayoutPanel1.Controls.OfType<Button>())
                btn.BackColor = Color.White;
            Button miniButton = (Button)sender;
            miniButton.BackColor = Color.Purple;
            index = Convert.ToInt32(miniButton.Tag);

        }

        private void buttonsave_Click(object sender, EventArgs e)
        {
            SaveImage();
        }

        private void buttonclear_Click(object sender, EventArgs e)
        {
            ClearCanvas();
        }
        private void buttonundo_Click(object sender, EventArgs e)
        {
            Undo();
        }
        private void buttonredo_Click(object sender, EventArgs e)
        {
            Redo();
        }
        private void trackBar_Scroll(object sender, EventArgs e)
        {
            pen.Width = trackBar.Value;
        }
        private void label_size_Click(object sender, EventArgs e)
        {
        }
        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
