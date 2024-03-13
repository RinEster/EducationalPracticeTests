using System;
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

namespace EducationalPracticeTests.UserControls
{
    /// <summary>
    /// Логика взаимодействия для TestUserControl.xaml
    /// </summary>
    public partial class TestUserControl : UserControl
    {
        public TestUserControl()
        {
            InitializeComponent();
        }

        public string pAnswer;

        public void questionsData(string TestName, string text, string one, string two, string three, string four, string rightanswer)
        {
            testName.Text = TestName;
            questionText.Text = text;
            optionOne.Text = one;
            optionTwo.Text = two;
            optionThree.Text = three;
            optionFour.Text = four;

            pAnswer = rightanswer;

        }
        public string responseForVerification(string response)
        {
            if (oneRadioButton.IsChecked == true) response = optionOne.Text;
            if (twoRadioButton.IsChecked == true) response = optionTwo.Text;
            if (threeRadioButton.IsChecked == true) response = optionThree.Text;
            if (fourRadioButton.IsChecked == true) response = optionFour.Text;
            return response;
        
        
        }
        public bool checkAnswer()
        {
            bool result = false;

            string response = responseForVerification(pAnswer);
            if (response == pAnswer)
            {
                result = true;
            }
            else
            {
                result = false;
            }
            return result;
        }
    }
}
