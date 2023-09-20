using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TicTacToe {
    public class MyButton : Button {
        public bool IsActive { get; set; } = false;
        public bool IsCircle { get; set; } = false;
        public bool IsCross { get; set; } = false;
        public int X { get; set; }
        public int Y { get; set; }
    }
    public static class Model {
        public static MyButton[,] buttons;
        public static int button_size = 100;
        public static bool difficult { get; set; } = false;
        public static bool step { get; set; } = true;
        public static int active = 0;
        public static Random rand = new Random();
        public static void InitButtons(Form current) {
            buttons = new MyButton[3, 3];
            for (int i = 0; i < 3; i++) {
                for (int j = 0; j < 3; j++) {
                    MyButton button = new MyButton();
                    button.X = j;
                    button.Y = i;
                    button.Size = new Size(button_size, button_size);
                    button.Location = new Point((button_size * i) + 55, (button_size * j) + 50);
                    button.BackgroundImage = Image.FromFile("textures/button.png");
                    button.FlatAppearance.BorderSize = 0;
                    button.FlatStyle = FlatStyle.Flat;
                    button.MouseUp += new MouseEventHandler(ClickMouse);
                    current.Controls.Add(button);
                    buttons[i, j] = button;
                }
            }
        }
        public static void InitField(Form current) {
            current.Height = buttons.GetLength(1) * button_size + 100;
            current.Width = buttons.GetLength(0) * button_size + 130;
        }
        public static void ClickMouse(object sender, MouseEventArgs e) {
            MyButton button = (MyButton)sender;
            if (e.Button == MouseButtons.Left && button.IsActive == true) {
                if (step) {
                    Presenter.PlayerStep(button);
                    if (!difficult) Presenter.CompStep();
                    else Presenter.HardCompStep();
                }
                else {
                    if (!difficult) Presenter.CompStep();
                    else Presenter.HardCompStep();
                    Presenter.PlayerStep(button);
                }
                CheckWin();
            }
        }
        public static void CheckWin()
        {
            if (buttons[0, 0].IsCross && buttons[0, 1].IsCross && buttons[0, 2].IsCross ||
                buttons[1, 0].IsCross && buttons[1, 1].IsCross && buttons[1, 2].IsCross ||
                buttons[2, 0].IsCross && buttons[2, 1].IsCross && buttons[2, 2].IsCross ||
                buttons[0, 0].IsCross && buttons[1, 0].IsCross && buttons[2, 0].IsCross ||
                buttons[0, 1].IsCross && buttons[1, 1].IsCross && buttons[2, 1].IsCross ||
                buttons[0, 2].IsCross && buttons[1, 2].IsCross && buttons[2, 2].IsCross ||
                buttons[0, 0].IsCross && buttons[1, 1].IsCross && buttons[2, 2].IsCross ||
                buttons[0, 2].IsCross && buttons[1, 1].IsCross && buttons[2, 0].IsCross)
            {
                DialogResult res = MessageBox.Show("Х победил. Хотите начать заново?", "Конец",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (res == DialogResult.Yes) Application.Restart();
                else Application.Exit();
            }
            else if (buttons[0, 0].IsCircle && buttons[0, 1].IsCircle && buttons[0, 2].IsCircle ||
                buttons[1, 0].IsCircle && buttons[1, 1].IsCircle && buttons[1, 2].IsCircle ||
                buttons[2, 0].IsCircle && buttons[2, 1].IsCircle && buttons[2, 2].IsCircle ||
                buttons[0, 0].IsCircle && buttons[1, 0].IsCircle && buttons[2, 0].IsCircle ||
                buttons[0, 1].IsCircle && buttons[1, 1].IsCircle && buttons[2, 1].IsCircle ||
                buttons[0, 2].IsCircle && buttons[1, 2].IsCircle && buttons[2, 2].IsCircle ||
                buttons[0, 0].IsCircle && buttons[1, 1].IsCircle && buttons[2, 2].IsCircle ||
                buttons[0, 2].IsCircle && buttons[1, 1].IsCircle && buttons[2, 0].IsCircle)
            {
                DialogResult res = MessageBox.Show("0 победил. Хотите начать заново?", "Конец",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (res == DialogResult.Yes) Application.Restart();
                else Application.Exit();
            }
            else if (active >= 9)
            {
                DialogResult res = MessageBox.Show("Ничья. Хотите начать заново?", "Конец",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (res == DialogResult.Yes) Application.Restart();
                else Application.Exit();
            }
        }
    }
    public static class Database {
        public static string GetTexturePath(MyButton button) {
            switch (Model.step) {
                case true:
                    button.IsCross = true;
                    return "cross.png";
                case false:
                    button.IsCircle = true;
                    return "circle.png";
            }
        }
    }
}
