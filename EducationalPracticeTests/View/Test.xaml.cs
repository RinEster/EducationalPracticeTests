using EducationalPracticeTests.UserControls;
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
    /// Логика взаимодействия для Test.xaml
    /// </summary>
    public partial class Test : UserControl
    { 
        
        public Test()
        {
            InitializeComponent();
            resultEdicationSubject();
           
           
        }

        private void resultEdicationSubject()
        {
            using (SqlConnection conn = new SqlConnection(MainWindow.connection))
            {
                conn.Open();
                string proc = "dbo.listEducationalSubject";
                SqlCommand sqlCommand = new SqlCommand(proc, conn);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                SqlDataReader read = sqlCommand.ExecuteReader();
                while(read.Read())
                {
                    educationSubjects.Items.Add(read.GetValue(0).ToString());
                }
            }
        }

        private void educationSubjects_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            test.Items.Clear();
            var selectedItemName = (sender as ListView).SelectedItem;
            string subject = selectedItemName.ToString();
            using(SqlConnection conn = new SqlConnection(MainWindow.connection))
            {
                conn.Open();
                string proc = "dbo.testEducationalSubject";

                SqlCommand sql = new SqlCommand(proc, conn);
                sql.CommandType = System.Data.CommandType.StoredProcedure;
                sql.Parameters.Add(new SqlParameter("@EducationalSubject", subject));

                SqlDataReader read = sql.ExecuteReader();
                while(read.Read())
                {
                    test.Items.Add(read.GetValue(0).ToString());
                }

            }

        }
        public int minCountQuestion = 1;
        public int maxCountQuestion = 0;
        public string testname;
       
        private void test_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItemTest = (sender as ListView).SelectedItem;
            testname = selectedItemTest.ToString();
          
            
            using (SqlConnection conn= new SqlConnection(MainWindow.connection))
            {
                conn.Open();
                string proc = "dbo.questionsTest";
                SqlCommand sqlCommand = new SqlCommand(proc, conn);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                sqlCommand.Parameters.Add(new SqlParameter("@testName", testname));

                SqlParameter resParam = new SqlParameter("@resCount", System.Data.SqlDbType.Int);
                resParam.Direction = System.Data.ParameterDirection.Output;
                sqlCommand.Parameters.Add(resParam);

                sqlCommand.ExecuteReader();

                maxCountQuestion = Convert.ToInt32(resParam.Value);
            }

            resultDataTest(minCountQuestion, testname);
            accept.Visibility = Visibility.Visible;
           
        }
       
        public  void resultDataTest (int count, string testname)
        {    
            using(SqlConnection conn = new SqlConnection(MainWindow.connection))
            {
                conn.Open();
                string strproc = "resultDataTest";
                SqlCommand command = new SqlCommand(strproc, conn);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@testName", testname));
                command.Parameters.Add(new SqlParameter("@count", count));

                SqlParameter questionTextParam = new SqlParameter("@questionText", System.Data.SqlDbType.NVarChar, 200);
                questionTextParam.Direction = System.Data.ParameterDirection.Output;
                command.Parameters.Add(questionTextParam);

                SqlParameter rightAnswerParam = new SqlParameter("@rightAnswer", System.Data.SqlDbType.NVarChar, 200);
                rightAnswerParam.Direction = System.Data.ParameterDirection.Output;
                command.Parameters.Add(rightAnswerParam);

                SqlParameter answerOneParam = new SqlParameter("@answerOne", System.Data.SqlDbType.NVarChar, 200);
                answerOneParam.Direction = System.Data.ParameterDirection.Output;
                command.Parameters.Add(answerOneParam);

                SqlParameter answerTwoParam = new SqlParameter("@answerTwo", System.Data.SqlDbType.NVarChar, 200);
                answerTwoParam.Direction = System.Data.ParameterDirection.Output;
                command.Parameters.Add(answerTwoParam);

                SqlParameter answerThreeParam = new SqlParameter("@answerThree", System.Data.SqlDbType.NVarChar, 200);
                answerThreeParam.Direction = System.Data.ParameterDirection.Output;
                command.Parameters.Add(answerThreeParam);

                SqlParameter answerFourParam = new SqlParameter("@answerFour", System.Data.SqlDbType.NVarChar, 200);
                answerFourParam.Direction = System.Data.ParameterDirection.Output;
                command.Parameters.Add(answerFourParam);

                command.ExecuteScalar();
               
                while (testDataGrid.Children.Count > 0)
                {
                    testDataGrid.Children.RemoveAt(0);
                }
              ;
                TestUserControl tUC = new TestUserControl();
                tUC.questionsData(testname, questionTextParam.Value.ToString(), answerOneParam.Value.ToString(), answerTwoParam.Value.ToString(), answerThreeParam.Value.ToString(), answerFourParam.Value.ToString(), rightAnswerParam.Value.ToString());
                testDataGrid.Children.Add(tUC);
            }
        }
        public int resultTest = 0;
        private void accept_Click(object sender, RoutedEventArgs e)
        {
           MessageBoxResult result = MessageBox.Show("Принять ответ?", "Ответ",
           MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                TestUserControl testUserControl = new TestUserControl();
                bool res = testUserControl.checkAnswer();
                if (res == true)
                {
                    resultTest++;
                }


                if (minCountQuestion<maxCountQuestion)
                {
                    minCountQuestion++;
                    resultDataTest(minCountQuestion, testname);
                    
                }
                else
                {      
                    using(SqlConnection conn = new SqlConnection(MainWindow.connection))
                    {
                        conn.Open();
                        string proc = "dbo.addResultTest";
                        SqlCommand command = new SqlCommand(proc, conn);
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("@login", MainWindow.userLogin));
                        command.Parameters.Add(new SqlParameter("@testName", testname));
                        command.Parameters.Add(new SqlParameter("@result", resultTest));
                        command.Parameters.Add(new SqlParameter("@date", DateTime.Now));
                        command.ExecuteScalar();
                    }
                    MessageBox.Show("Тест пройден");
                }
               
            }
            
        }

       


    }
}
