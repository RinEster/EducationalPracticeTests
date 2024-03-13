using EducationalPracticeTests.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationalPracticeTests.ViewModel
{
    class NavigationVM : Utilities.BaseVM
    {
        private object _currentView;
        public object CurrentView
        {
            get { return _currentView; }
            set { _currentView = value; OnPropertyChanged(); }
        }

        public RelayCommand UserCommand { get; set; }
        public RelayCommand TestCommand { get; set; }
        public RelayCommand ResultTestCommand { get; set; }
   

        private void User(object obj) => CurrentView = new UserVM();
        private void Test(object obj) => CurrentView = new TestVM();
        private void ResultTest(object obj) => CurrentView = new ResultTestVM();
     

        public NavigationVM()
        {
            UserCommand = new RelayCommand(User);
            TestCommand = new RelayCommand(Test);
            ResultTestCommand = new RelayCommand(ResultTest);

            //Первая открытая страница
            CurrentView = new UserVM();
        }
    }
}
