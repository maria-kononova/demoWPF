using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
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

namespace demo
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Employer> employers = new List<Employer>();
        List<string> posts = new List<string>();
        List<string> educations = new List<string>();
        String selectedSex = null;

        public MainWindow()
        {
            InitializeComponent();
            /*string connectionString = "SERVER=localhost;DATABASE=test;UID=root;PASSWORD=228338118;";

            MySqlConnection connection = new MySqlConnection(connectionString);

            MySqlCommand cmd = new MySqlCommand("Select * from test", connection);
            connection.Open();
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            connection.Close();
            dtGrid.DataContext = dt;*/
            employers.Add(new Employer(1,"Кононова Мария Ивановна", new DateTime(2004, 10, 05), "Женский", "Средне профессиональное", "Програмыст", 100000, 15));
            posts.Add("Программист");
            posts.Add("Дизайнер");
            educations.Add("Среднее");
            educations.Add("Средне профессиональное");
            educations.Add("Высшее");
            dataGrid.ItemsSource = employers;
            educationList.ItemsSource = educations;
            educations.Add("Отменить");
            educationList2.ItemsSource = educations;
            postList.ItemsSource = posts;
        }

        private void save_Click(object sender, RoutedEventArgs e)
        {
            if (nameInput.Text != "")
            {
                if (dateInput.Text != "")
                {
                    if (sexInputMale.IsChecked == true || sexInputFemale.IsChecked == true)
                    {
                        if (educationList.SelectedItem != null)
                        {
                            if (postList.SelectedItem != null)
                            {
                                if (salaryInput.Text != "")
                                {
                                    if (primeInput3.IsChecked == true || primeInput5.IsChecked == true || primeInput10.IsChecked == true || primeInput15.IsChecked == true)
                                    {
                                        employers.Add(new Employer(2, nameInput.Text, Convert.ToDateTime(dateInput.SelectedDate), selectedSex, Convert.ToString(educationList.SelectedItem), Convert.ToString(postList.SelectedItem), Convert.ToDouble(salaryInput.Text), 3));
                                        dataGrid.Items.Refresh();
                                    }
                                    else error.Text = "Выберете премию";
                                }
                                else error.Text = "Введите оклад";

                            }
                            else error.Text = "Выберете должность";

                        }
                        else error.Text = "Выберете образование";
                    }
                    else error.Text = "Выберете пол";
                }
                else error.Text = "Дата не выбрана";
            }
            else error.Text = "ФИО не заполнено";
        }

        private void sexInputMale_Checked(object sender, RoutedEventArgs e)
        {
            sexInputFemale.IsChecked = false;
            selectedSex = "Мужской";
        }

        private void sexInputFemale_Checked(object sender, RoutedEventArgs e)
        {
            sexInputMale.IsChecked = false;
            selectedSex = "Женский";
        }

        private void salaryInput_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = IsTextNumeric(e.Text);
        }
        private static bool IsTextNumeric(string str)
        {
            System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex("[^0-9]");
            return reg.IsMatch(str);

        }

        private void delete_Click(object sender, RoutedEventArgs e)
        {
            int index = dataGrid.SelectedIndex;
            employers.RemoveAt(index);
            dataGrid.Items.Refresh();
        }

        private void educationList2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string education = educationList2.SelectedItem.ToString();
            if(education != "Отменить")
            {
                List<Employer> filterList = new List<Employer>();
                for (int i = 0; i < employers.Count; i++)
                {
                    if (employers[i].Education == education) filterList.Add(employers[i]);
                }
                dataGrid.ItemsSource = filterList;
            }
            else
            {
                dataGrid.ItemsSource = employers;
            }
          
            dataGrid.Items.Refresh();
        }

        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = dataGrid.SelectedIndex;
            Employer emp =  employers[index];
            var today = DateTime.Today;

            // Calculate the age.
            var age = today.Year - emp.BirthDay.Year;
            // Go back to the year in which the person was born in case of a leap year
            var result = DateTime.Compare(emp.BirthDay.Date, today.Date);
            if (result < 0)
                age--;
            error.Text = age.ToString();
            if (emp.Sex == "Мужской")
            {
                if(65 - age > 0)
                {
                    textBlock.Text = "До пенсии " + (65 - age).ToString();
                }
                else
                {
                    textBlock.Text = "Уже на пенсии";
                }
               
            }
            else
            {
                if (60 - age > 0)
                {
                    textBlock.Text = "До пенсии " + (60 - age).ToString();
                }
                else
                {
                    textBlock.Text = "Уже на пенсии";
                }
            }
        }
    }
}
