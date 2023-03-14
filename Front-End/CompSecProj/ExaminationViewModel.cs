using System.Windows.Input;
 
namespace trustid
{
    public class ExaminationViewModel : ViewModelBase
    {
        private ICommand _gotoView1Command;
        private object _currentView;
        private object _view1;

        public ExaminationViewModel()
        {
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

    }
}