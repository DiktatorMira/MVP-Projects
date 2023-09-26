namespace Clock
{
    public partial class Form1 : Form
    {
        public int clock_radius, num_radius, arrow_length, centerX, centerY;
        public Form1()
        {
            InitializeComponent();
            DoubleBuffered = true;
            Size = new Size(400, 400);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            StartPosition = FormStartPosition.CenterScreen;
            timer1.Interval = 1000;
            timer1.Tick += Timer_Tick;
            timer1.Start();
            centerX = ClientSize.Width / 2;
            centerY = ClientSize.Height / 2;
            clock_radius = Math.Min(ClientSize.Width, ClientSize.Height) / 2 - 20;
            num_radius = clock_radius - 30;
            arrow_length = clock_radius - 40;
        }
        private void Timer_Tick(object sender, EventArgs e) => Invalidate();
        protected override void OnPaint(PaintEventArgs e) {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.FillEllipse(Brushes.White, ClientSize.Width / 2 - clock_radius, ClientSize.Height / 2 - clock_radius, 2 * clock_radius, 2 * clock_radius);
            g.DrawEllipse(Pens.Black, ClientSize.Width / 2 - clock_radius, ClientSize.Height / 2 - clock_radius, 2 * clock_radius, 2 * clock_radius);

            for (int i = 1; i <= 12; i++) {
                double corner = i * 30 * Math.PI / 180;
                int numeralX = (int)(ClientSize.Width / 2 + num_radius * Math.Sin(corner));
                int numeralY = (int)(ClientSize.Height / 2 - num_radius * Math.Cos(corner));
                g.DrawString(i.ToString(), Font, Brushes.Black, numeralX - 10, numeralY - 10);
                int scaleX1 = (int)(ClientSize.Width / 2 + (num_radius - 10) * Math.Sin(corner));
                int scaleY1 = (int)(ClientSize.Height / 2 - (num_radius - 10) * Math.Cos(corner));
                int scaleX2 = (int)(ClientSize.Width / 2 + (num_radius - 20) * Math.Sin(corner));
                int scaleY2 = (int)(ClientSize.Height / 2 - (num_radius - 20) * Math.Cos(corner));
                g.DrawLine(Pens.Black, scaleX1, scaleY1, scaleX2, scaleY2);
            }

            DateTime now = DateTime.Now;
            int sec = now.Second, min = now.Minute, hour = now.Hour % 12;

            double secondsAngle = (sec - 15) * 6; 
            int secondsX = (int)(centerX + arrow_length * Math.Cos(Math.PI * secondsAngle / 180));
            int secondsY = (int)(centerY + arrow_length * Math.Sin(Math.PI * secondsAngle / 180));
            g.DrawLine(Pens.Red, centerX, centerY, secondsX, secondsY);

            double minutesAngle = (min - 15) * 6 - secondsAngle / 60;
            int minutesX = (int)(centerX + arrow_length * 0.85 * Math.Cos(Math.PI * minutesAngle / 180));
            int minutesY = (int)(centerY + arrow_length * 0.85 * Math.Sin(Math.PI * minutesAngle / 180));
            g.DrawLine(Pens.Blue, centerX, centerY, minutesX, minutesY);

            double hoursAngle = (hour - 3) * 30 - minutesAngle / 12;
            int hoursX = (int)(centerX + arrow_length * 0.65 * Math.Cos(Math.PI * hoursAngle / 180));
            int hoursY = (int)(centerY + arrow_length * 0.65 * Math.Sin(Math.PI * hoursAngle / 180));
            g.DrawLine(Pens.Black, centerX, centerY, hoursX, hoursY);
        }
    }
}