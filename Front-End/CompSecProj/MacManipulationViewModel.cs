using System.Windows.Input;
 
namespace trustid
{
    public class MacManipulationViewModel : ViewModelBase
    {
        private ICommand _gotoView1Command;
        private ICommand _gotoView2Command;
        private ICommand _gotoView3Command;
        private ICommand _gotoView4Command;
        private ICommand _gotoView5Command;
        private ICommand _gotoView6Command;
        private ICommand _gotoView7Command;


        private object _currentView;
        private object _view1;
        private object _view2;
        private object _view3;
        private object _view4;
        private object _view5;
        private object _view6;
        private object _view7;
        public MacManipulationViewModel()
        {
            _view1 = new MacManScreen();
            _view2 = new MacScenarioPart_1();
            _view3 = new MacScenarioPart_2();
            _view4 = new MacMacroAttack_1();
            _view5 = new MacMacroAttack_2();
            _view6 = new MacMacroAttack_3();
            _view7 = new MacMacroAttack_4();


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

        public ICommand GotoView4Command
        {
            get
            {
                return _gotoView4Command ?? (_gotoView4Command = new RelayCommand(
                   x =>
                   {
                       GotoView4();
                   }));
            }
        }

        public ICommand GotoView5Command
        {
            get
            {
                return _gotoView5Command ?? (_gotoView5Command = new RelayCommand(
                   x =>
                   {
                       GotoView5();
                   }));
            }
        }


        public ICommand GotoView6Command
        {
            get
            {
                return _gotoView6Command ?? (_gotoView6Command = new RelayCommand(
                   x =>
                   {
                       GotoView6();
                   }));
            }
        }


        public ICommand GotoView7Command
        {
            get
            {
                return _gotoView7Command ?? (_gotoView7Command = new RelayCommand(
                   x =>
                   {
                       GotoView7();
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

        private void GotoView4()
        {
            CurrentView = _view4;
        }

        private void GotoView5()
        {
            CurrentView = _view5;
        }


        private void GotoView6()
        {
            CurrentView = _view6;
        }

        private void GotoView7()
        {
            CurrentView = _view7;
        }

    }
}