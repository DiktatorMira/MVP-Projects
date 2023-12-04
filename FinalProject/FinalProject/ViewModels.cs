using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using System.Xml;
using Newtonsoft.Json;
using System.IO;
using Formatting = Newtonsoft.Json.Formatting;
using System.Security;
using System.Collections.ObjectModel;

namespace FinalProject {
    public abstract class VMBase : INotifyPropertyChanged {
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
    public class RegistrationVm : VMBase {
        private UserModel user;
        private string? repeatPassword;
        private Commands? registration, open_signin;
        private Navigation navigation;
        public string? Login {
            get { return user.Login; }
            set {
                user.Login = value;
                OnPropertyChanged(nameof(Login));
            }
        }
        public string? Password {
            get { return user.Password; }
            set {
                user.Password = value;
                OnPropertyChanged(nameof(Password));
            }
        }
        public string? RepeatPassword {
            get { return repeatPassword; }
            set {
                repeatPassword = value;
                OnPropertyChanged(nameof(RepeatPassword));
            }
        }
        public ICommand OpenSignIn {
            get {
                if (open_signin == null) open_signin = new Commands(exec => Open(), can => CanOpen());
                return open_signin;
            }
        }
        private bool CanOpen() { return true; }
        private void Open() {
            navigation.NavigateSignIn(1);
            navigation.NavigateRegistration(0);
        }
        public ICommand RegistrationCommand {
            get {
                if (registration == null) registration = new Commands(exec => Reg(), can => CanReg());
                return registration;
            }
        }
        private bool CanReg() {
            return Login != null && Password.Length >= 8 && Password == RepeatPassword;
        }
        private void Reg() {
            var data = new { Login, Password };
            try {
                if (File.Exists("userdata.json")) {
                    string currentData = File.ReadAllText("userdata.json");
                    List<dynamic>? currentUsers = JsonConvert.DeserializeObject<List<dynamic>>(currentData);
                    if (currentUsers.Any(u => u.Login == Login)) {
                        MessageBox.Show("Пользователь с таким именем уже существует.", "Ошибка",
                            MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    currentUsers.Add(data);
                    string newData = JsonConvert.SerializeObject(currentUsers, Formatting.Indented);
                    File.WriteAllText("userdata.json", newData);
                }
                else {
                    List<dynamic> users = new List<dynamic> { data };
                    string jsonData = JsonConvert.SerializeObject(users, Formatting.Indented);
                    File.WriteAllText("userdata.json", jsonData);
                }
                navigation.NavigateGallery();
                navigation.NavigateRegistration(0);
            }
            catch (Exception ex) {
                MessageBox.Show($"Ошибка сохранения в файл: {ex.Message}", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public RegistrationVm() {
            user = UserModel.Example;
            navigation = new Navigation();
        }
    }
    public class SignInVM : VMBase {
        private UserModel user;
        private Navigation navigation;
        private Commands? signin, open_reg;
        public string Login {
            get { return user.Login; }
            set {
                user.Login = value;
                OnPropertyChanged(nameof(Login));
            }
        }
        public string Password  {
            get { return user.Password; }
            set {
                user.Password = value;
                OnPropertyChanged(nameof(Password));
            }
        }
        public ICommand OpenRegCommand {
            get {
                if (open_reg == null) open_reg = new Commands(exec => Open(), can => CanOpen());
                return open_reg;
            }
        }
        private bool CanOpen() { return true; }
        private void Open() {
            navigation.NavigateRegistration(1);
            navigation.NavigateSignIn(0);
        }
        public ICommand SignInCommand {
            get {
                if (signin == null) signin = new Commands(exec => Sign(), can => CanSignIn());
                return signin;
            }
        }
        private bool CanSignIn() { return Login != null; }
        private void Sign() {
            try {
                if (File.Exists("userdata.json")) {
                    string data = File.ReadAllText("userdata.json");
                    List<dynamic>? users = JsonConvert.DeserializeObject<List<dynamic>>(data);
                    var userAuthenticate = users?.FirstOrDefault(u => u.Login == Login && u.Password == Password);
                    if (userAuthenticate != null) {
                        navigation.NavigateGallery();
                        navigation.NavigateSignIn(0);
                    }
                    else {
                        MessageBox.Show("Неверный логин или пароль.", "Отказано",
                            MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else {
                    MessageBox.Show("Файл с пользователями не найден!", "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex) {
                MessageBox.Show($"Ошибка чтения файла: {ex.Message}", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public SignInVM() {
            user = UserModel.Example;
            navigation = new Navigation();
        }
    }
    public class GalleryVM : VMBase {
        ObservableCollection<ImagesVM> Images { get; set; } = new ObservableCollection<ImagesVM>();
        private ImagesVM? images;
        private int position = 0, maxImages;
        private Commands? nextImage, prevImage, firstImage, lastImage;
        public ImagesVM? CurrentImage {
            get { return images; }
            set {
                if (images != value) {
                    images = value;
                    OnPropertyChanged(nameof(CurrentImage));
                }
            }
        }
        public int Position {
            get { return position; }
            set {
                position = value;
                CurrentImage = Images[Position];
                OnPropertyChanged(nameof(Position));
            }
        }
        public int MaxImages {
            get { return maxImages; }
            set {
                maxImages = value;
                OnPropertyChanged(nameof(MaxImages));
            }
        }
        public ICommand NextImageCommand {
            get {
                if (nextImage == null) nextImage = new Commands(exec => Next(), can => CanNext());
                return nextImage;
            }
        }
        private bool CanNext() { return Position != MaxImages; }
        private void Next() => Position++;
        public ICommand PreviousImageCommand {
            get {
                if (prevImage == null) prevImage = new Commands(exec => Previous(), can => CanPrevious());
                return prevImage;
            }
        }
        private bool CanPrevious() { return Position > 0; }
        private void Previous() => Position--;
        public ICommand FirstImageCommand {
            get {
                if (firstImage == null) firstImage = new Commands(exec => First(), can => CanFirst());
                return firstImage;
            }
        }
        private bool CanFirst() { return Position != 0; }
        private void First() => Position = 0;
        public ICommand LastImageCommand {
            get {
                if (lastImage == null) lastImage = new Commands(exec => Last(), can => CanLast());
                return lastImage;
            }
        }
        private bool CanLast() { return Position != maxImages; }
        private void Last() => Position = MaxImages;
        public GalleryVM() {
            try {
                string jsonContent = File.ReadAllText("../../../Resources/Images");
                List<ImagesModel>? images = JsonConvert.DeserializeObject<List<ImagesModel>>(jsonContent);
                foreach (var image in images) Images.Add(new ImagesVM(image));
                if (Images.Count > 0) Position = 0;
                maxImages = images.Count - 1;
            }
            catch (Exception ex) { MessageBox.Show("Ошибка: " + ex.Message); }
        }
    }
    public class ImagesVM : VMBase {
        private ImagesModel? image;
        private double mark;
        public ImagesModel? Image { get { return image; } }
        public double Mark {
            get { return mark; }
            set {
                if (mark == value) return;
                mark = value;
                int index = image.Ratings.FindIndex(x => x.Key == UserModel.Example.Login);
                if (index != -1) image.Ratings[index] = new KeyValue(UserModel.Example.Login, mark);
                else image.Ratings.Add(new KeyValue(UserModel.Example.Login, mark));
                OnPropertyChanged(nameof(Mark));
            }
        }
        public string? Name {
            get { return image.Name; }
            set {
                image.Name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        public string? Date {
            get { return image.Date; }
            set {
                image.Date = value;
                OnPropertyChanged(nameof(Date));
            }
        }
        public string? Author {
            get { return image.Author; }
            set {
                image.Author = value;
                OnPropertyChanged(nameof(Author));
            }
        }
        public string PathToPhoto {
            get {
                string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "Resources", "Images", Image.Path);
                return path;
            }
            set {
                image.Path = value;
                OnPropertyChanged(nameof(PathToPhoto));
            }
        }
        public string AvaregeRating {
            get { return (image.Ratings.Select(x => x.Value).Sum() / image.Ratings.Count).ToString("F2"); }
        }
        public string NumberOfRatings { get { return image.Ratings.Count.ToString(); } }
        public ImagesVM(ImagesModel image) {
            this.image = image;
            int index = image.Ratings.FindIndex(x => x.Key == UserModel.Example.Login);
            if (index != -1) Mark = image.Ratings[index].Value;
            else Mark = 0;
        }
    }
}
