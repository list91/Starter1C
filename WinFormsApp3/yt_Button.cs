using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Starter1C
{
    public partial class yt_Button : Control
    {
        private StringFormat SF = new StringFormat();

        private bool MouseEntered = false;
        private bool MousePressed = false;
        private bool isVisible = true;
        public Rectangle rect;

        public yt_Button()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor | ControlStyles.UserPaint, true);
            DoubleBuffered = true;

            Size = new Size(100, 30);

            BackColor = Color.Tomato;
            ForeColor = Color.White;


            SF.Alignment = StringAlignment.Center;
            SF.LineAlignment = StringAlignment.Center;
        }
        public bool Visible() {
            return isVisible;
        }
        [Category("Appearance")]
        [Description("Определяет, отображается ли кнопка.")]
        public bool IsVisible
        {
            get { return isVisible; }
            set
            {
                isVisible = value;
                Invalidate(); // Перерисовать кнопку, чтобы отразить изменение видимости
            }
        }
        private Color borderColor = Color.Black;
        private int borderWidth = 1;
        private Image buttonImage;

        // Новые свойства для рамки кнопки и изображения
        [Category("Appearance")]
        [Description("Цвет рамки кнопки.")]
        public Color BorderColor
        {
            get { return borderColor; }
            set
            {
                borderColor = value;
                Invalidate();
            }
        }

        [Category("Appearance")]
        [Description("Толщина рамки кнопки.")]
        public int BorderWidth
        {
            get { return borderWidth; }
            set
            {
                borderWidth = value;
                Invalidate();
            }
        }

        [Category("Appearance")]
        [Description("Изображение, отображаемое слева от текста в кнопке.")]
        public Image ButtonImage
        {
            get { return buttonImage; }
            set
            {
                buttonImage = value;
                Invalidate();
            }
        }

        public void ShowButton()
        {
            IsVisible = true;
        }

        public void HideButton()
        {
            IsVisible = false;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (!IsVisible)
                return;

            Graphics graph = e.Graphics;
            graph.SmoothingMode = SmoothingMode.HighQuality;

            graph.Clear(Parent.BackColor);

            rect = new Rectangle(0, 0, Width - 1, Height - 1);

            int cornerRadius = 3; // Задайте желаемое значение для угла скругления

            // Создаем графический путь с использованием угла скругления
            GraphicsPath path = new GraphicsPath();
            path.AddArc(rect.X, rect.Y, cornerRadius * 2, cornerRadius * 2, 180, 90);
            path.AddArc(rect.Width - cornerRadius * 2, rect.Y, cornerRadius * 2, cornerRadius * 2, 270, 90);
            path.AddArc(rect.Width - cornerRadius * 2, rect.Height - cornerRadius * 2, cornerRadius * 2, cornerRadius * 2, 0, 90);
            path.AddArc(rect.X, rect.Height - cornerRadius * 2, cornerRadius * 2, cornerRadius * 2, 90, 90);
            path.CloseFigure();

            graph.DrawPath(new Pen(borderColor), path);
            graph.FillPath(new SolidBrush(BackColor), path);

            if (MouseEntered)
            {
                graph.DrawPath(new Pen(Color.FromArgb(60, Color.White)), path);
                graph.FillPath(new SolidBrush(Color.FromArgb(60, Color.White)), path);
            }

            if (MousePressed)
            {
                graph.DrawPath(new Pen(Color.FromArgb(30, Color.Black)), path);
                graph.FillPath(new SolidBrush(Color.FromArgb(30, Color.Black)), path);
            }

            // Отрисовка изображения слева от текста, если оно задано
            if (ButtonImage != null)
            {
                int imageWidth = 20;
                int imageHeight = 20;
                int imageMargin = 5;

                Rectangle imageRect = new Rectangle(imageMargin, (Height - imageHeight) / 2, imageWidth, imageHeight);
                graph.DrawImage(ButtonImage, imageRect);
                SF.Alignment = StringAlignment.Far;
            }

            graph.DrawString(Text, Font, new SolidBrush(ForeColor), rect, SF);
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);

            MouseEntered = true;

            Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);

            MouseEntered = false;

            Invalidate();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            MousePressed = true;

            Invalidate();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            MousePressed = false;

            Invalidate();
        }
    }
}