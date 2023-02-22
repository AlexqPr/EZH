using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace wanna_suicide
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DatePicker.SelectedDate = DateTime.Today;
        }
        public void Create()
        {
            List<Zametki> zapis = Myconv.MyDeserialize<List<Zametki>>("Заметки.json");//читаем данные из файла
            DateTime dates = Convert.ToDateTime(DatePicker.Text);
            Zametki newzametka = new Zametki();
            newzametka.Name = Names.Text;
            newzametka.Desc = Descs.Text;
            newzametka.data = dates;
            zapis.Add(newzametka);
            Myconv.Myserialize(zapis, "Заметки.json");
            Names.Text = "";
            Descs.Text = "";
            Read();
        }
        public void Read()
        {
            List<string> naimen = new List<string>();
            List<Zametki> zapis = Myconv.MyDeserialize<List<Zametki>>("Заметки.json");//читаем данные из файла
            DateTime dates = Convert.ToDateTime(DatePicker.Text); //выбранная дата
            foreach (var item in zapis)
            {
                if (dates == item.data)
                {
                    naimen.Add(item.Name);

                }
            }
            LB.ItemsSource = naimen;
        }
        public void NewRead(string selected)
        {
            List<Zametki> zapis = Myconv.MyDeserialize<List<Zametki>>("Заметки.json");//читаем данные из файла
            DateTime dates = Convert.ToDateTime(DatePicker.Text); //выбранная дата
           
            foreach (var item in zapis)
            {
                if(item.Name == selected)
                {
                    Names.Text = item.Name;
                    Descs.Text = item.Desc;
                }
            }
        }
        public void Update()
        {
            List<Zametki> zapis = Myconv.MyDeserialize<List<Zametki>>("Заметки.json");//читаем данные из файла
            Zametki upd = new Zametki();
            List<Zametki> forupd = new List<Zametki>();
            DateTime dates = Convert.ToDateTime(DatePicker.Text); //выбранная дата
            foreach (var item in zapis)
            {
                if((item.Name != Names.Text && item.Desc == Descs.Text) || (item.Name == Names.Text && item.Desc != Descs.Text))
                {
                    upd.Name = Names.Text;
                    upd.Desc = Descs.Text; //тут мы короче находидм заметку, которая изменена
                    upd.data = dates;
                   
                }
            }
            foreach (var item in zapis)
            {
                if(item.Name == upd.Name || item.Desc == upd.Desc)
                {

                }
                else
                {
                    forupd.Add(item);
                }
            }
            forupd.Add(upd);
            Myconv.Myserialize(forupd, "Заметки.json");
            Descs.Text = "";
            Names.Text = "";
            
            Read();
        }
        public void Delete()
        {
            List<Zametki> zapis = Myconv.MyDeserialize<List<Zametki>>("Заметки.json");//читаем данные из файла
            Zametki fordel1 = new Zametki();
            
            List<Zametki> fordel = new List<Zametki>();
            foreach (var item in zapis)
            {
                if(item.Name == Names.Text)
                {
                    fordel1.Name = item.Name;
                    fordel1.Desc = item.Desc;
                    fordel1.data = item.data;
                }
            }
            foreach (var item in zapis)
            {
                if (item.Desc == fordel1.Desc)
                {

                }
                else
                {
                    fordel.Add(item);
                }
            }
            Myconv.Myserialize(fordel, "Заметки.json");
            Read();
            
        }
        public void gjrenge()
        {
            Zametki zam1 = new Zametki();
            zam1.Name = "Первая заметка";
            zam1.Desc = "Вторая заметка";
            zam1.data = DatePicker.DisplayDate;
            List<Zametki> foow = new List<Zametki>();
            foow.Add(zam1);
            Myconv.Myserialize(foow, "Заметки.json");
           
        }

        private void BD_Click(object sender, RoutedEventArgs e)
        {
            Delete();
        }

        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            Names.Text = "";
            Descs.Text = "";
            Read();
        }
        private void BC_Click_1(object sender, RoutedEventArgs e)
        {
            Create();
        }

        private void LB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                string selected = LB.SelectedItem.ToString();
                NewRead(selected);
            }
            catch (System.NullReferenceException)
            {
                Names.Text = "";
                Descs.Text = "";
            }
           
            
        }

        private void BS_Click(object sender, RoutedEventArgs e)
        {
            Update();
        }
    }
}
