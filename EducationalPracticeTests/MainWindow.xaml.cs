using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
using EducationalPracticeTests.Utilities;
namespace EducationalPracticeTests
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
         
            
        }
        public static string connection = "Data Source=DESKTOP-E6HKS9J\\SQLEXPRESS;Initial Catalog=EducationalPracticeTests;Integrated Security=True";
        public static string userLogin;
        public static string type;
        private void TextBlockClose_MouseDown(object sender, MouseButtonEventArgs e)
        {
           MessageBoxResult result = MessageBox.Show("Закрыть приложение?", "Завершение работы",
           MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                 Close();
            }           
        }

        private void authenticationButton_Click(object sender, RoutedEventArgs e)
        {
          

            if (loginTextBox.Text!=""&&passTextBox.Password!="")
            { 
                string login = loginTextBox.Text;
                string pass = passTextBox.Password;  
                
                encriptionXOR encriptionXOR = new encriptionXOR();
                var encryptedMessageByPass = encriptionXOR.Encrypt(pass, encriptionXOR.password_key);
                pass = encryptedMessageByPass.ToString();
                using (SqlConnection conn = new SqlConnection(connection))
                {
                    conn.Open();
                    string proc = "dbo.Autorization";
                    SqlCommand sql = new SqlCommand(proc, conn);
                    sql.CommandType = System.Data.CommandType.StoredProcedure;

                    sql.Parameters.Add(new SqlParameter("@login", login));
                    sql.Parameters.Add(new SqlParameter("@pass", pass));

                    SqlParameter resParam = new SqlParameter("@result", SqlDbType.NVarChar, 50);
                    resParam.Direction = ParameterDirection.Output;
                    sql.Parameters.Add(resParam);

                    sql.ExecuteScalar();

                    string result = resParam.Value.ToString();
                    MessageBox.Show(result);
                    if (result == "Вход выполнен")
                    {
                        userLogin = login;
                        NavPanel navPanel = new NavPanel();
                        navPanel.Show();
                        Close();
                    }
                    else
                    {
                        loginTextBox.Background = Brushes.Coral;
                        passTextBox.Background = Brushes.Coral;
                        loginTextBox.ToolTip = "Неверно введены данные";
                        passTextBox.ToolTip = "Неверно введены данные";
                    }
                }
            }
            else
            {
                loginTextBox.Background = Brushes.Coral;
                passTextBox.Background = Brushes.Coral;
                loginTextBox.ToolTip = "Поле обязательно для заполнения";
                passTextBox.ToolTip = "Поле обязательно для заполнения";
            }
                 
        }

        private void registrationButton_Click(object sender, RoutedEventArgs e)
        {   
            if (loginRegTextBox.Text != "" && passRegTextBox.Password != "")
            {
                string login = loginRegTextBox.Text;
                 string pass = passRegTextBox.Password;
                encriptionXOR encriptionXOR = new encriptionXOR();
                var encryptedMessageByPass = encriptionXOR.Encrypt(pass, encriptionXOR.password_key);
                pass = encryptedMessageByPass.ToString();
                if (passRegTextBox.Password == passDRegTextBox.Password)
                {
                    if(roleUser.Text == "Администратор"|| roleUser.Text == "Преподаватель"|| roleUser.Text == "Студент")
                    {
                        if (roleUser.Text == "Администратор")
                        {
                            if(secretCodeTextBox.Text=="admin524")
                            {
                                string typeU = roleUser.Text;
                                using (SqlConnection conn = new SqlConnection(connection))
                                {
                                    conn.Open();
                                    string proc = "dbo.Registration";
                                    SqlCommand sql = new SqlCommand(proc, conn);
                                    sql.CommandType = System.Data.CommandType.StoredProcedure;

                                    sql.Parameters.Add(new SqlParameter("@login", login));
                                    sql.Parameters.Add(new SqlParameter("@pass", pass));
                                    sql.Parameters.Add(new SqlParameter("@type", typeU));

                                    SqlParameter resParam = new SqlParameter("@result", SqlDbType.NVarChar, 50);
                                    resParam.Direction = ParameterDirection.Output;
                                    sql.Parameters.Add(resParam);

                                    sql.ExecuteScalar();

                                    string result = resParam.Value.ToString();
                                    MessageBox.Show(result);
                                    if (result == "Регистрация успешна")
                                    {
                                        NavPanel navPanel = new NavPanel();
                                        navPanel.Show();
                                        Close();
                                        userLogin = login;
                                        type = typeU;
                                    }
                                    else
                                    {
                                        loginRegTextBox.Background = Brushes.Coral;
                                        passRegTextBox.Background = Brushes.Coral;
                                        loginRegTextBox.ToolTip = "Неверно введены данные";
                                        passRegTextBox.ToolTip = "Неверно введены данные";
                                    }
                                }
                            }
                            else
                            {
                                MessageBox.Show("Неверный код");
                            }
                        }
                        else
                        {
                            string typeU = roleUser.Text;
                            using (SqlConnection conn = new SqlConnection(connection))
                            {
                                conn.Open();
                                string proc = "dbo.Registration";
                                SqlCommand sql = new SqlCommand(proc, conn);
                                sql.CommandType = System.Data.CommandType.StoredProcedure;

                                sql.Parameters.Add(new SqlParameter("@login", login));
                                sql.Parameters.Add(new SqlParameter("@pass", pass));
                                sql.Parameters.Add(new SqlParameter("@type", typeU));

                                SqlParameter resParam = new SqlParameter("@result", SqlDbType.NVarChar, 50);
                                resParam.Direction = ParameterDirection.Output;
                                sql.Parameters.Add(resParam);

                                sql.ExecuteScalar();

                                string result = resParam.Value.ToString();
                                MessageBox.Show(result);
                                if (result == "Регистрация успешна")
                                {
                                    NavPanel navPanel = new NavPanel();
                                    navPanel.Show();
                                    Close();
                                    userLogin = login;
                                    type = typeU;
                                }
                                else
                                {
                                    loginRegTextBox.Background = Brushes.Coral;
                                    passRegTextBox.Background = Brushes.Coral;
                                    loginRegTextBox.ToolTip = "Неверно введены данные";
                                    passRegTextBox.ToolTip = "Неверно введены данные";
                                }
                            }
                        }                       
                    }
                    else
                    {
                        MessageBox.Show("Выберите роль");
                    }                    
                }
                else
                {
                    passRegTextBox.Background = Brushes.Coral;
                    passDRegTextBox.Background = Brushes.Coral;
                    passRegTextBox.ToolTip = "Данные в полях должны совпадать";
                    passDRegTextBox.ToolTip = "Данные в полях должны совпадать";
                }    
            } 
            else
            {
                loginRegTextBox.Background = Brushes.Coral;
                passRegTextBox.Background = Brushes.Coral;
                loginRegTextBox.ToolTip = "Поле обязательно для заполнения";
                passRegTextBox.ToolTip = "Поле обязательно для заполнения";
            }
        }

        private void chooseButton_Click(object sender, RoutedEventArgs e)
        {
            if (roleUser.Text == "Администратор")
            {
                roleTextBlock.Visibility = Visibility.Visible;
                secretCodeTextBox.Visibility = Visibility.Visible;
            }
            else
            {
                roleTextBlock.Visibility = Visibility.Hidden;
                secretCodeTextBox.Visibility = Visibility.Hidden;
            }
        }
    }
}
