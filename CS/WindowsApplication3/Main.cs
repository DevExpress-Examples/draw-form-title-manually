using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.Skins.XtraForm;
using DevExpress.Skins;
using DevExpress.Utils;
using System.Drawing.Drawing2D;
using DevExpress.Utils.Drawing;
using DevExpress.Utils.Drawing.Helpers;


namespace DXSample {
    public partial class Main: XtraForm {
        public Main() {
            InitializeComponent();
        }

        protected override DevExpress.Skins.XtraForm.FormPainter CreateFormBorderPainter()
        {
            return new MyFormPainter(this, DevExpress.LookAndFeel.UserLookAndFeel.Default.ActiveLookAndFeel);
        }
    }

    public class MyFormPainter : FormPainter
    {
        int x = 0, delta = 8;
        Image image;
        Timer timer;

        public MyFormPainter(Control owner, ISkinProvider provider) : base(owner, provider) {
            timer = new Timer();
            timer.Interval = 300;
            timer.Tick += OnTick;
            timer.Start();
        }
       
        Image Image {
            get
            {
                if (image == null)
                    image = CreateImage();
                return image;
            }
        }
        private Bitmap CreateImage()
        {
            Bitmap temp = new Bitmap(TextBounds.Width, TextBounds.Height);
            using (Graphics gr = Graphics.FromImage(temp))
            {
                for (int i = 0; i < temp.Width; i += delta)
                    for (int j = 0; j < temp.Height; j += delta)
                    {
                        Rectangle rect = new Rectangle(i, j, delta, delta);
                        LinearGradientBrush brush = new LinearGradientBrush(rect, Color.White, Color.Black, LinearGradientMode.BackwardDiagonal);
                        LinearGradientBrush borderBrush = new LinearGradientBrush(rect, Color.DarkGray, Color.White, LinearGradientMode.BackwardDiagonal);
                        Pen pen = new Pen(borderBrush);
                        gr.FillRectangle(brush, rect);
                        gr.DrawRectangle(pen, rect);
                    }
            }
            return temp;
        }

        void OnTick(object sender, EventArgs e)
        {
            if (x <= TextBounds.Right - delta)
                x += delta;
            else
                x = 0;
            UpdateText();
        }

        private void UpdateText()
        {
           DrawFrameNC(new Message());
        }

        protected override void DrawText(DevExpress.Utils.Drawing.GraphicsCache cache)
        {
            if (Text == null || Text.Length == 0 || TextBounds.IsEmpty) return;
            AppearanceObject appearance = new AppearanceObject(GetDefaultAppearance());
            cache.Graphics.DrawImage(Image, TextBounds);
            Rectangle textRect = new Rectangle(TextBounds.X + x, TextBounds.Y, TextBounds.Width - x, TextBounds.Height);
            DrawTextShadow(cache, appearance, textRect);
            cache.Graphics.DrawString(Text, appearance.Font, Brushes.WhiteSmoke, textRect, new StringFormat(StringFormatFlags.NoWrap));
        }

        public override void Dispose()
        {
            timer.Stop();
            timer.Tick -= OnTick;
            timer.Dispose();
            base.Dispose();
        }
    }
}
