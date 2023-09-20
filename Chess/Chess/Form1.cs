using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Chess
{
    public partial class Form1 : Form
    {
        Graphics graph;
        bool color = false;
        public Form1()
        {
            InitializeComponent();
            Height = 848;
            Width = 818;
            MaximizeBox = false;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            StartPosition = FormStartPosition.CenterScreen;
        }
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            using (graph = CreateGraphics())
            {
                for (int i = 0; i != 8; i++)
                {
                    for (int j = 0; j != 8; j++)
                    {
                        graph.FillRectangle(ChangeColor(), j * 100, i * 100, 100, 100);
                        if (j != 7) color = !color;
                    }
                }
                for(int i = 0; i != 8; i++) {
                    graph.DrawImage(Image.FromFile("textures/black/Pawn.png"), i * 100, 100);
                    graph.DrawImage(Image.FromFile("textures/white/Pawn.png"), i * 100, 600);
                }
                graph.DrawImage(Image.FromFile("textures/black/Rook.png"), 0, 0);
                graph.DrawImage(Image.FromFile("textures/black/Rook.png"), 700, 0);
                graph.DrawImage(Image.FromFile("textures/white/Rook.png"), 0, 700);
                graph.DrawImage(Image.FromFile("textures/white/Rook.png"), 700, 700);
                graph.DrawImage(Image.FromFile("textures/black/Horse.png"), 100, 0);
                graph.DrawImage(Image.FromFile("textures/black/Horse.png"), 600, 0);
                graph.DrawImage(Image.FromFile("textures/white/Horse.png"), 100, 700);
                graph.DrawImage(Image.FromFile("textures/white/Horse.png"), 600, 700);
                graph.DrawImage(Image.FromFile("textures/black/Bishop.png"), 200, 0);
                graph.DrawImage(Image.FromFile("textures/black/Bishop.png"), 500, 0);
                graph.DrawImage(Image.FromFile("textures/white/Bishop.png"), 200, 700);
                graph.DrawImage(Image.FromFile("textures/white/Bishop.png"), 500, 700);
                graph.DrawImage(Image.FromFile("textures/black/Queen.png"), 400, 0);
                graph.DrawImage(Image.FromFile("textures/white/Queen.png"), 400, 700);
                graph.DrawImage(Image.FromFile("textures/black/King.png"), 300, 0);
                graph.DrawImage(Image.FromFile("textures/white/King.png"), 300, 700);
            }
        }
        public Brush ChangeColor()
        {
            switch (color)
            {
                case true:
                    return Brushes.Sienna;
                case false:
                    return Brushes.Moccasin;
            }
        }
    }
}