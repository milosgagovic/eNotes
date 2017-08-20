using Common;
using Common.Entities;
using Common.UserControls;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace eBeleznik.ViewModel
{
    public class UserAddAndEditViewModel : INotifyPropertyChanged
    {

        public User User { get; set; }
        private User logedUser;
        private ObservableCollection<Group> allGroups = new ObservableCollection<Group>();
        private ListView lista = new ListView();
        private List<Group> selecGroups = new List<Group>();
        private IServiceConract proxy;

        public ListView Lista { get => lista; set => lista = value; }
        public UserAddAndEditViewModel()
        {
            User = new User();
            Proxy = App.Proxy;
            List<Group> result = Proxy.GetAllGruops();
           
            AllGroups.Clear();
            foreach (var item in result)
            {
                AllGroups.Add(item);
            } 
            
        }

        public UserAddAndEditViewModel(User user,User logedUser)
        {
            Proxy = App.Proxy;
            User = user; /////treba naci usere i grupe
            List<Group> result = Proxy.GetAllGruops();
            SelectedGroups = Proxy.GetGroupsForUser(user);
            AllGroups.Clear();
            LoggedUser = logedUser;

            foreach (var item in SelectedGroups)
            {
                User.Group.Add(item);
            }
            foreach (var item in result)
            {
                AllGroups.Add(item);
            }
            if (User == null)
            {
            }

        }

        #region Commands
        private ICommand cancelCommand;
        private ICommand saveCommand;



        public ICommand CancelCommand
        {
            get
            {
                return cancelCommand ?? (cancelCommand = new RelayCommand((param) => this.CancelClick(param)));
            }
        }

        public ICommand SaveCommand
        {
            get
            {
                return saveCommand ?? (saveCommand = new RelayCommand((param) => this.SaveClick(param)));
            }
        }

        public IServiceConract Proxy
        {
            get
            {
                return proxy;
            }

            set
            {
                proxy = value;
            }
        }

        public ObservableCollection<Group> AllGroups
        {
            get { return allGroups; }
            set
            {
                allGroups = value;
                OnPropertyChanged("AllGroups");
            }
        }

        public List<Group> SelectedGroups
        {
            get { return selecGroups; }
            set
            {
                selecGroups = value;
                OnPropertyChanged("SelectedGroups");
            }
        }
        public User LoggedUser
        {
            get
            {
                return App.LoggedUser;
            }
            set
            {
                App.LoggedUser = value;
                OnPropertyChanged("LoggedUser");
            }
        }


        #endregion Commands

        #region Methods
        private void CancelClick(object param)
        {

            var userControl = param as UserControl;
            Window parentWindow = Window.GetWindow(userControl);

            parentWindow.Close();
        }

        private void SaveClick(object param)
        {
            
            UserInputView userControl = param as UserInputView;
            Window parentWindow = Window.GetWindow(userControl);
            
            bool success = false;

            //Add if not exist(Create new User)
            if (User.Id == 0)
            {
                foreach (Group item in userControl.Lista.SelectedItems)
                {
                    userControl.User.Group.Add(item);
                }
                success = Proxy.AddUser(User);
            }
            else
            {
                foreach (Group item in userControl.Lista.SelectedItems)
                {
                    userControl.User.Group.Add(item);
                }
                success = Proxy.UpdateUser(User);
            }

            if (success)
            {
                parentWindow.DialogResult = true;
                if (App.LoggedUser.Id == User.Id)
                {
                    parentWindow.Tag = User;
                }
                parentWindow.Close();
            }
            else
            {
                parentWindow.Close();
            }
        }
        #endregion Methods
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
