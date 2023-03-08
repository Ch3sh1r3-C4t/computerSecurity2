using System.Windows.Input;
 
namespace trustid
{
    public class EnrollmentViewModel : ViewModelBase
    {
        private ICommand _gotoView1Command;
        private ICommand _gotoView2Command;
        private ICommand _gotoView3Command;
        private object _currentView;
        private object _view1;
        private object _view2;
        private object _view3;

        public EnrollmentViewModel()
        {
            _view1 = new ucEnrollStepOne();
            _view2 = new ucEnrollStepTwo();
            _view3 = new ucEnrollStepFinal();

            CurrentView = _view1;
        }

        public object GotoView1Command
        {
            get
            {
                return _gotoView1Command ?? (_gotoView1Command = new RelayCommand(
                   x =>
                   {
                       GotoView1();
                   }));
            }
        }

        public ICommand GotoView2Command
        {
            get
            {
                return _gotoView2Command ?? (_gotoView2Command = new RelayCommand(
                   x =>
                   {
                       GotoView2();
                   }));
            }
        }

        public ICommand GotoView3Command
        {
            get
            {
                return _gotoView3Command ?? (_gotoView3Command = new RelayCommand(
                   x =>
                   {
                       GotoView3();
                   }));
            }
        }

        public object CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                OnPropertyChanged("CurrentView");
            }
        }

        private void GotoView1()
        {
            CurrentView = _view1;
        }

        private void GotoView2()
        {
            CurrentView = _view2;
        }

        private void GotoView3()
        {
            CurrentView = _view3;
        }

    }
}