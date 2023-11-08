using System;
using System.Collections.Generic;
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
using System.Collections.ObjectModel;

namespace Recipes {
    public partial class MainWindow : Window {
        public MainWindow() => InitializeComponent();
        private void ChangeSelect(object sender, SelectionChangedEventArgs e) {
            if (listBox.SelectedItem != null) SetFlowDocument(listBox.SelectedIndex);
        }
        private void OnStateChanged(object sender, EventArgs e) {
            image.Width = image.Height = 350;
            EllipseGeometry ellipse = new EllipseGeometry(new Point(150, 150), 340, 340);
            image.Clip = ellipse;
        }
        private void AddListItem(string text) {
            ListItem item = new ListItem();
            Paragraph paragraph = new Paragraph(new Run(text));
            paragraph.Style = (Style)FindResource("ParagraphStyle");
            item.Blocks.Add(paragraph);
            list.ListItems.Add(item);
        }
        private void SetFlowDocument(int index) {
            str.Text = "Ингредиенты:";
            foreach (var item in list.ListItems.ToList()) list.ListItems.Remove(item);
            description.Inlines.Clear();
            switch (index) {
                case 0:
                    title.Content = "Мясо по-французски";
                    image.Source = new BitmapImage(new Uri("Images/FranceMeat.jpg", UriKind.Relative));
                    AddListItem("Свинина 200г");
                    AddListItem("Лук 40г");
                    AddListItem("Помидоры 50г");
                    AddListItem("Сыр 100г");
                    AddListItem("Майонез 100г");
                    AddListItem("Оливковое масло 20г");
                    description.Inlines.Add(new Run("Мясо по-французски — это очень популярное блюдо для праздничного " +
                        "стола и отлично подходит для угощения гостей. Это и не удивительно, " +
                        "блюдо довольно быстро и просто готовятся, для его приготовления нужны доступные ингредиенты и " +
                        "список их небольшой, за раз можно приготовить большое количество порций, и ещё одним, и, " +
                        "наверное, главным плюсом является отличный вкус готового блюда."));
                    break;
                case 1:
                    title.Content = "Сациви";
                    image.Source = new BitmapImage(new Uri("Images/Satsivi.jpg", UriKind.Relative));
                    AddListItem("Курятина до 2кг");
                    AddListItem("Сливочное масло 20г");
                    AddListItem("Лук 1кг");
                    AddListItem("Кинза 4 ветки");
                    AddListItem("Грецуий орех 600г");
                    AddListItem("Чёрныц перец 1 ст.л.");
                    AddListItem("Молотый кориандр 1 ст.л.");
                    AddListItem("Чеснок 6 зубчиков");
                    AddListItem("Белый винный уксус 1 ст.л.");
                    description.Inlines.Add(new Run("Сациви по-грузински традиционно готовят с индейкой. " +
                        "Кстати, само слово «сациви» означает лишь соус на основе измельченных орехов, " +
                        "поэтому, по большому счету, в нем можно приготовить любую птицу и даже мясо. " +
                        "Но мы все-таки остановимся на грузинской классике и возьмем индейку. " +
                        "Какую птицу выбрать? В идеале — самую жирную, какую только сможете найти: от этого зависит " +
                        "насыщенность вкуса блюда. Соус должен быть довольно густым, поэтому на количестве орехов " +
                        "экономить не стоит. И, наконец, помните, что сациви по-грузински принято подавать на большой " +
                        "тарелке, чтобы гости раскладывали его в свои тарелки самостоятельно, в зависимости от желания " +
                        "и аппетита."));
                    break;
                case 2:
                    title.Content = "Торт \"Мишка на севере\"";
                    image.Source = new BitmapImage(new Uri("Images/BearOnNorth.jpg", UriKind.Relative));
                    AddListItem("Мука 400г");
                    AddListItem("Сметана 850г");
                    AddListItem("Сахар 120г");
                    AddListItem("Сливочное масло 60г");
                    AddListItem("Какао 2 ст.л.");
                    AddListItem("Сахарная пудра 150г");
                    AddListItem("Грецкие орехи 1 стакан");
                    AddListItem("Сода 1 ч.л.");
                    AddListItem("Ваниль 2 ч.л.");
                    description.Inlines.Add(new Run("Многие помнят с детства вкус нежного торта Мишка на севере. " +
                        "Тонкие коржики, пропитанные ароматным сметанным кремом с ванилью и лёгкий хруст грецких " +
                        "орешков. За счет последних, кстати, тортик выходит достаточно сытным, но при этом остаётся " +
                        "очень лёгким. Его любят и взрослые, и дети. Мишка на севере буквально поражает своим " +
                        "потрясающим вкусом. Мало кто догадается, что делается торт достаточно просто и из самых" +
                        " обычных продуктов, которые есть в ближайшем магазине."));
                    break;
                case 3:
                    title.Content = "Суп чихиртма";
                    image.Source = new BitmapImage(new Uri("Images/Chihirtma.jpg", UriKind.Relative));
                    AddListItem("Курятина 1,5-2кг");
                    AddListItem("Яйца 4шт");
                    AddListItem("Белый винный уксус 4 ст.л.");
                    AddListItem("Кинза 1 пучок");
                    AddListItem("Вода 4л");
                    AddListItem("Лук 2шт");
                    AddListItem("Пшеничная мука 4 ст.л.");
                    AddListItem("Масло сливочное 50г");
                    AddListItem("Чеснок 4 зубчика");
                    description.Inlines.Add(new Run("Чихиртма – вкусный и полезный грузинский суп с насыщенным вкусом," +
                        " который может стать достойным угощением для дорогих гостей и порадовать вашу семью за обедом. " +
                        "В классическом варианте чихиртма состоит из минимума ингредиентов, а большая часть вкусовых " +
                        "приправ подаются к ней отдельно, чтобы каждый, кто сидит за столом, заправил нежный кремовый " +
                        "суп по своему вкусу, сделал его более острым, кислым или жгучим, или оставил его исходный " +
                        "мягкий вкус. Чихиртму лучше готовить в том количестве, которое вы и ваша семья сможет съесть " +
                        "за один раз, потому что греть готовый суп не стоит, яйца свернутся, и он расслоится и " +
                        "схватится хлопьями. Вы можете приготовить много бульона с заправкой и курицей, но яйца лучше " +
                        "вводить непосредственно перед подачей супа на стол."));
                    break;
                case 4:
                    title.Content = "Свинина в рукаве";
                    image.Source = new BitmapImage(new Uri("Images/Pork.jpg", UriKind.Relative));
                    AddListItem("Свинина 1-2кг");
                    AddListItem("Чеснок 6 зубчиков");
                    AddListItem("Растительное масло 3 ст.л.");
                    description.Inlines.Add(new Run("Наивкуснейшая, сочная, аппетитная, на праздничный стол! " +
                        "Свинина в пакете для запекания в духовке на любом столе займет почетное место. Это уникальное " +
                        "блюдо: и второе, и закуска, причем, как горячая, так и холодная. Вы можете подавать его с " +
                        "гарниром, салатом или использовать как ингредиент к другим блюдам."));
                    break;
            }
        }
    }
}
