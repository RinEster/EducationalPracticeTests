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

namespace EducationalPracticeTests.View
{
    /// <summary>
    /// Логика взаимодействия для ResultTestStudent.xaml
    /// </summary>
    public partial class ResultTestStudent : UserControl
    {
        public ResultTestStudent()
        {
            InitializeComponent();

            using (SqlConnection conn = new SqlConnection(MainWindow.connection))
            {
                conn.Open();
                string proc = "dbo.resultStudent";
                SqlCommand sqlCommand = new SqlCommand(proc, conn);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.Add(new SqlParameter("@login", MainWindow.userLogin));
                sqlCommand.ExecuteScalar();

                SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                result.ItemsSource = dataTable.DefaultView;
            }

        }
    }
}
