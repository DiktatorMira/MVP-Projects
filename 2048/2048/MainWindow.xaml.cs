using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Formats.Asn1.AsnWriter;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace _2048 {
    public partial class MainWindow : Window {
        public Color color;
        public Brush background;
        public bool start = false, generate = true;
        public int highscore, score = 0;
        public Button[,] buttons = new Button[4, 4];
        public int[,] board = new int[4, 4];
        public Random rand = new Random();
        public MainWindow() {
            InitializeComponent();
            LoadFromFile();
            SetWindow();
        }
        public void LoadFromFile() {
            using(StreamReader file = new StreamReader("highscore.txt", Encoding.UTF8)) {
                highscore = int.Parse(file.ReadLine());
                text2.Text = "Рекорд:\n" + highscore;
            }
        }
        public void SetWindow() {
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            row1.Height = new GridLength(0.05, GridUnitType.Star);
            row2.Height = new GridLength(0.25, GridUnitType.Star);
            SetScore(score);
            List<Button> temp = new List<Button>();
            foreach (var elem in grid1.Children) if (elem is Button) temp.Add((Button)elem);
            for (int i = 0; i < buttons.GetLength(0); i++) {
                for(int j = 0; j < buttons.GetLength(1); j++) buttons[i,j] = temp[i * buttons.GetLength(1) + j];
            }
        }
        public void GenerateNumbers() {
            if (generate) {
                int temp1, temp2, value = (rand.Next(10) == 0) ? 4 : 2;
                while (true) {
                    temp1 = rand.Next(board.GetLength(0));
                    temp2 = rand.Next(board.GetLength(1));
                    if (board[temp1, temp2] == 0) break;
                }
                buttons[temp1, temp2].Content = value.ToString();
                board[temp1, temp2] = value;
                SetColor(buttons[temp1, temp2]);
            }
        }
        public void Checks() {
            SetGenerate();
            CheckLose();
            CheckWin();
            GenerateNumbers();
        }
        public void SetGenerate() {
            foreach (var but in buttons) {
                if (but.Content == "") {
                    generate = true;
                    return;
                }
            }
            generate = false;
        }
        public void CheckLose() {
            foreach (var num in board) if (num == 0) return;
            for (int row = 0; row < board.GetLength(0); row++) {
                for (int col = 0; col < board.GetLength(1) - 1; col++) {
                    if (board[row, col] == board[row, col + 1]) return;
                }
            }
            for (int col = 0; col < board.GetLength(1); col++) {
                for (int row = 0; row < board.GetLength(0) - 1; row++) {
                    if (board[row, col] == board[row + 1, col]) return;
                }
            }
            MessageBoxResult res = MessageBox.Show("Возможных ходов нет... Вы проиграли. Хотите начать заново?", "Не совсем молодец",
                        MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
            if (res == MessageBoxResult.Yes) Restart();
            else Application.Current.Shutdown();
        }
        public void CheckWin() {
            foreach (var num in board) {
                if (num == 2048) {
                    MessageBoxResult res = MessageBox.Show("Поздравляю, вы победили! Хотите начать заново?", "Молодец",
                        MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
                    if (res == MessageBoxResult.Yes) Restart();
                    else Application.Current.Shutdown();
                }
            }
        }
        public void Restart() {
            string temp = Application.ResourceAssembly.Location;
            var path = new StringBuilder(temp);
            path[path.Length - 1] = 'e';
            path[path.Length - 2] = 'x';
            path[path.Length - 3] = 'e';
            Process.Start(new ProcessStartInfo { FileName = path.ToString(), UseShellExecute = true });
            Application.Current.Shutdown();
        }
        public void SetScore(int value) {
            score += value;
            text1.Text = "Очки:\n" + score;
            if (score > highscore) {
                highscore = score;
                text2.Text = "Рекорд:\n" + highscore;
            }
        }
        public void SetColor(Button button) {
            color = GetColor(button);
            background = new SolidColorBrush(color);
            button.Background = background;
        }
        public Color GetColor(Button button) {
            switch(button.Content) {
                case "2":
                    return Color.FromRgb(153, 107, 90);
                case "4":
                    return Color.FromRgb(186, 130, 95);
                case "8":
                    return Color.FromRgb(196, 148, 96);
                case "16":
                    return Color.FromRgb(204, 160, 94);
                case "32":
                    return Color.FromRgb(212, 176, 93);
                case "64":
                    return Color.FromRgb(212, 200, 93);
                case "128":
                    return Color.FromRgb(190, 196, 75);
                case "256":
                    return Color.FromRgb(188, 219, 77);
                case "512":
                    return Color.FromRgb(157, 232, 77);
                case "1024":
                    return Color.FromRgb(126, 242, 73);
                case "2048":
                    return Color.FromRgb(69, 255, 69);
                default:
                    return Color.FromRgb(179, 159, 152);
            }
        }
        private void WinClosing(object sender, System.ComponentModel.CancelEventArgs e) {
            using (StreamWriter file = new StreamWriter("highscore.txt", false)) file.Write(highscore);
        }
        private void StartClick(object sender, RoutedEventArgs e) {
            color = Color.FromRgb(179, 159, 152);
            background = new SolidColorBrush(color);
            menuItem1.Background = background;
            menuItem1.IsEnabled = false;
            start = !start;
            GenerateNumbers();
        }
        private void RestartClick(object sender, RoutedEventArgs e) => Restart();
        private void WinKeyDown(object sender, KeyEventArgs e) {
            if (start) {
                switch (e.Key) {
                    case Key.W:
                    case Key.Up:
                        for(int col = 0; col < board.GetLength(1); col++) {
                            for(int row = 1; row < board.GetLength(0); row++){
                                if (board[row, col] != 0) {
                                    int currow = row;
                                    while(currow > 0 && board[currow - 1, col] == 0) {
                                        board[currow - 1, col] = board[currow, col];
                                        buttons[currow - 1, col].Content = buttons[currow, col].Content;
                                        SetColor(buttons[currow - 1, col]);
                                        board[currow, col] = 0;
                                        buttons[currow, col].Content = "";
                                        SetColor(buttons[currow, col]);
                                        currow--;
                                    }
                                    if (currow > 0 && board[currow - 1, col] == board[currow, col]
                                        && board[currow, col] != 0) {
                                        board[currow - 1, col] *= 2;
                                        buttons[currow - 1, col].Content = board[currow - 1, col].ToString();
                                        SetColor(buttons[currow - 1, col]);
                                        board[currow, col] = 0;
                                        buttons[currow, col].Content = "";
                                        SetColor(buttons[currow, col]);
                                        SetScore(board[currow - 1, col]);
                                    }
                                }
                            }
                        }
                        Checks();
                        break;
                    case Key.S:
                    case Key.Down:
                        for (int col = 0; col < board.GetLength(1); col++)  {
                            for (int row = board.GetLength(0) - 2; row >= 0; row--) {
                                if (board[row, col] != 0) {
                                    int currow = row;
                                    while (currow < board.GetLength(0) - 1 && board[currow + 1, col] == 0) {
                                        board[currow + 1, col] = board[currow, col];
                                        buttons[currow + 1, col].Content = buttons[currow, col].Content;
                                        SetColor(buttons[currow + 1, col]);
                                        board[currow, col] = 0;
                                        buttons[currow, col].Content = "";
                                        SetColor(buttons[currow, col]);
                                        currow++;
                                    }
                                    if (currow < board.GetLength(0) - 1 && board[currow + 1, col] == board[currow, col] && board[currow, col] != 0) {
                                        board[currow + 1, col] *= 2;
                                        buttons[currow + 1, col].Content = board[currow + 1, col].ToString();
                                        SetColor(buttons[currow + 1, col]);
                                        board[currow, col] = 0;
                                        buttons[currow, col].Content = "";
                                        SetColor(buttons[currow, col]);
                                        SetScore(board[currow + 1, col]);
                                    }
                                }
                            }
                        }
                        Checks();
                        break;
                    case Key.A:
                    case Key.Left:
                        for (int row = 0; row < board.GetLength(0); row++) {
                            for (int col = 1; col < board.GetLength(1); col++) {
                                if (board[row, col] != 0) {
                                    int curcol = col;
                                    while (curcol > 0 && board[row, curcol - 1] == 0) {
                                        board[row, curcol - 1] = board[row, curcol];
                                        buttons[row, curcol - 1].Content = buttons[row, curcol].Content;
                                        SetColor(buttons[row, curcol - 1]);
                                        board[row, curcol] = 0;
                                        buttons[row, curcol].Content = "";
                                        SetColor(buttons[row, curcol]);
                                        curcol--;
                                    }
                                    if (curcol > 0 && board[row, curcol - 1] == board[row, curcol]) {
                                        board[row, curcol - 1] *= 2;
                                        buttons[row, curcol - 1].Content = board[row, curcol - 1].ToString();
                                        SetColor(buttons[row, curcol - 1]);
                                        board[row, curcol] = 0;
                                        buttons[row, curcol].Content = "";
                                        SetColor(buttons[row, curcol]);
                                        SetScore(board[row, curcol - 1]);
                                    }
                                }
                            }
                        }
                        Checks();
                        break;
                    case Key.D:
                    case Key.Right:
                        for (int row = 0; row < board.GetLength(0); row++) {
                            for (int col = board.GetLength(1) - 2; col >= 0; col--) {
                                if (board[row, col] != 0) {
                                    int curcol = col;
                                    while (curcol < board.GetLength(1) - 1 && board[row, curcol + 1] == 0) {
                                        board[row, curcol + 1] = board[row, curcol];
                                        buttons[row, curcol + 1].Content = buttons[row, curcol].Content;
                                        SetColor(buttons[row, curcol + 1]);
                                        board[row, curcol] = 0;
                                        buttons[row, curcol].Content = "";
                                        SetColor(buttons[row, curcol]);
                                        curcol++;
                                    }
                                    if (curcol < board.GetLength(1) - 1 && board[row, curcol + 1] == board[row, curcol] && board[row, curcol + 1] != 0) {
                                        board[row, curcol + 1] *= 2;
                                        buttons[row, curcol + 1].Content = board[row, curcol + 1].ToString();
                                        SetColor(buttons[row, curcol + 1]);
                                        board[row, curcol] = 0;
                                        buttons[row, curcol].Content = "";
                                        SetColor(buttons[row, curcol]);
                                        SetScore(board[row, curcol + 1]);
                                    }
                                }
                            }
                        }
                        Checks();
                        break;
                }
                e.Handled = true;
            }
        }
    }
}
