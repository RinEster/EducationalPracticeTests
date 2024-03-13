using System;
using System.Collections.Generic;
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

namespace EducationalPracticeTests.View
{
    /// <summary>
    /// Логика взаимодействия для User.xaml
    /// </summary>
    public partial class User : UserControl
    {
        public User()
        {
            InitializeComponent();
            userLogin.Text = MainWindow.userLogin;
            resultUser(userLogin.Text);
        }

        private void resultUser(string login)
        {
            using(SqlConnection conn = new SqlConnection(MainWindow.connection))
            {
                conn.Open();
                string proc = "dbo.resultUser";
                SqlCommand comm = new SqlCommand(proc, conn);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Parameters.Add(new SqlParameter("@userLogin", login));

                SqlParameter NameParam = new SqlParameter("@userName", System.Data.SqlDbType.NVarChar, 50);
                NameParam.Direction = System.Data.ParameterDirection.Output;
                comm.Parameters.Add(NameParam);

                SqlParameter SurnameParam = new SqlParameter("@userSurname", System.Data.SqlDbType.NVarChar, 50);
                SurnameParam.Direction = System.Data.ParameterDirection.Output;
                comm.Parameters.Add(SurnameParam);

                SqlParameter TypeParam = new SqlParameter("@userType", System.Data.SqlDbType.NVarChar, 50);
                TypeParam.Direction = System.Data.ParameterDirection.Output;
                comm.Parameters.Add(TypeParam);

                comm.ExecuteScalar();

                if(NameParam.Value.ToString() != "")
                {
                    userName.Text = NameParam.Value.ToString();
                }
                else
                {
                    userName.Text = "Заполнить данные";
                }
                if (SurnameParam.Value.ToString() != "")
                {
                   userSurname.Text = SurnameParam.Value.ToString();
                }
                else
                {
                     userSurname.Text = "Заполнить данные";
                }

                userType.Text = TypeParam.Value.ToString();
                MainWindow.type = TypeParam.Value.ToString();
            }
        }

        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            if(loginUpdateTextBox.Text!="")
            {
                string newLogin = loginUpdateTextBox.Text;

                using (SqlConnection conn = new SqlConnection(MainWindow.connection))
                {
                    conn.Open();
                    string proc = "dbo.updateLogin";
                    SqlCommand comm = new SqlCommand(proc, conn);
                    comm.CommandType = System.Data.CommandType.StoredProcedure;
                    comm.Parameters.Add(new SqlParameter("@login", MainWindow.userLogin));
                    comm.Parameters.Add(new SqlParameter("@newlogin", newLogin));                    

                    SqlParameter resParam = new SqlParameter("@res", System.Data.SqlDbType.NVarChar, 100);
                    resParam.Direction = System.Data.ParameterDirection.Output;
                    comm.Parameters.Add(resParam);

                    comm.ExecuteScalar();

                    MessageBox.Show(resParam.Value.ToString());
                    loginUpdateTextBox.Text = "";
                }

            }
            if(nameUpdateTextBox.Text!="")
            {
                string newName = nameUpdateTextBox.Text;

                using (SqlConnection conn = new SqlConnection(MainWindow.connection))
                {
                    conn.Open();
                    string proc = "dbo.updateName";
                    SqlCommand comm = new SqlCommand(proc, conn);
                    comm.CommandType = System.Data.CommandType.StoredProcedure;
                    comm.Parameters.Add(new SqlParameter("@login", MainWindow.userLogin));
                    comm.Parameters.Add(new SqlParameter("@newName", newName));

                    SqlParameter resParam = new SqlParameter("@res", System.Data.SqlDbType.NVarChar, 100);
                    resParam.Direction = System.Data.ParameterDirection.Output;
                    comm.Parameters.Add(resParam);

                    comm.ExecuteScalar();

                    MessageBox.Show(resParam.Value.ToString());
                    nameUpdateTextBox.Text = "";
                }
            }
            if (surnameUpdateTextBox.Text != "")
            {
                string newSurname = surnameUpdateTextBox.Text;

                using (SqlConnection conn = new SqlConnection(MainWindow.connection))
                {
                    conn.Open();
                    string proc = "dbo.updateSurname";
                    SqlCommand comm = new SqlCommand(proc, conn);
                    comm.CommandType = System.Data.CommandType.StoredProcedure;
                    comm.Parameters.Add(new SqlParameter("@login", MainWindow.userLogin));
                    comm.Parameters.Add(new SqlParameter("@newSurname", newSurname));

                    SqlParameter resParam = new SqlParameter("@res", System.Data.SqlDbType.NVarChar, 100);
                    resParam.Direction = System.Data.ParameterDirection.Output;
                    comm.Parameters.Add(resParam);

                    comm.ExecuteScalar();

                    MessageBox.Show(resParam.Value.ToString());
                    surnameUpdateTextBox.Text = "";
                }
            }
            resultUser(MainWindow.userLogin);
        }
    }
}
