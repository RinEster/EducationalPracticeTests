using EducationalPracticeTests.View;
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
using System.Windows.Shapes;

namespace EducationalPracticeTests
{
    /// <summary>
    /// Логика взаимодействия для NavPanel.xaml
    /// </summary>
    public partial class NavPanel : Window
    {
        public NavPanel()
        {
            InitializeComponent();
           
        }

        private void user_MouseDown(object sender, MouseButtonEventArgs e)
        {
            User user = new User();
            user.Width = 615;
            user.Margin = new Thickness(0, 0, 0, 0);
            Pages.Children.Add(user);
        }

        private void output_MouseDown(object sender, MouseButtonEventArgs e)
        {
           MessageBoxResult result = MessageBox.Show("Закрыть приложение?", "Завершение работы",
           MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                Close();
            }
        }

        private void test_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Test test = new Test();
            test.Width = 615;
            test.Margin = new Thickness(0, 0, 0, 0);
            Pages.Children.Add(test);
        }

        private void res_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (MainWindow.type != "Студент")
            {
                 ResultTest resultTest = new ResultTest();
                 resultTest.Width = 615;
                  resultTest.Margin = new Thickness(0, 0, 0, 0);
                  Pages.Children.Add(resultTest);
            }
            else
            {
                ResultTestStudent resultTestStudent = new ResultTestStudent();
                resultTestStudent.Width = 615;
                resultTestStudent.Margin = new Thickness(0, 0, 0, 0);
                Pages.Children.Add(resultTestStudent);
            }
            
          
        }
    }
}
