using Common;
using Common.Entities;
using eBeleznik.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace eBeleznik.ViewModel
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private IServiceConract proxy;
        private NetTcpBinding netTcpBinding = new NetTcpBinding();
        private Common.Entities.WindowState currentState = Common.Entities.WindowState.LOGIN;
        private string loggedUsername = "";
        private ObservableCollection<User> allUsers = new ObservableCollection<User>();
        private ObservableCollection<Group> allGroups = new ObservableCollection<Group>();
        private ObservableCollection<Note> allNotes = new ObservableCollection<Note>();
        public BitmapImage EditIcon { get; set; }
        public BitmapImage RemoveIcon { get; set; }
        private ICommand mainmenuCommand;
        private ICommand loginCommand;
        private ICommand logOutCommand;
        private ICommand showAllUsers;
        private ICommand editUserProfileCommand;
        private ICommand deleteUserCommand;
        private ICommand addUserCommand;
        private ICommand showAllGroups;
        private ICommand addGroupCommand;
        private ICommand editGroupProfileCommand;
        private ICommand deleteGroupCommand;
        private ICommand showAllNotesCommand;
        private ICommand addNoteCommand;
        private ICommand editNoteCommand;
        private ICommand deleteNoteCommand;
        private ICommand changeUserDataCommand;

        public MainWindowViewModel()
        {
            string executable = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string path = System.IO.Path.GetDirectoryName(executable);
            path = path.Substring(0, path.LastIndexOf("eBeleznik")) + "\\Common";
            EditIcon = new BitmapImage(new Uri(path + "\\Images\\edit.png"));
            RemoveIcon = new BitmapImage(new Uri(path + "\\Images\\delete.png"));
            Proxy = App.Proxy;

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
        public Common.Entities.WindowState CurrentState
        {
            get { return currentState; }
            set
            {
                currentState = value;
                OnPropertyChanged("CurrentState");
            }
        }
        public string LoggedUsername
        {
            get
            {
                return loggedUsername;
            }

            set
            {
                loggedUsername = value;
                OnPropertyChanged("LoggedUsername");
            }
        }
        #region Command Property
        public ICommand LoginCommand
        {
            get
            {
                return loginCommand ?? (loginCommand = new RelayCommand(param => this.LoginClick(param)));
            }
        }

        public ICommand LogOutCommand
        {
            get
            {
                return logOutCommand ?? (logOutCommand = new RelayCommand((param) => this.LogOut(param)));
            }

        }

        public ICommand ShowAllUsers
        {

            get
            {
                return showAllUsers ?? (showAllUsers = new RelayCommand((param) => this.ShowUsers()));
            }
        }

        public ICommand EditUserProfileCommand
        {
            get
            {
                return editUserProfileCommand ?? (editUserProfileCommand = new RelayCommand(param => this.EditUserProfile(param)));
            }
        }

        public ICommand DeleteUserCommand
        {
            get
            {
                return deleteUserCommand ?? (deleteUserCommand = new RelayCommand(param => this.DeleteUser(param)));
            }
        }

        public ICommand AddUserCommand
        {
            get
            {
                return addUserCommand ?? (addUserCommand = new RelayCommand(param => this.AddUser(param)));
            }
        }

        public ICommand ShowAllGroups
        {
            get
            {
                return showAllGroups ?? (showAllGroups = new RelayCommand(param => this.ShowGroups()));
            }
        }

        public ICommand EditGroupProfileCommand
        {
            get
            {
                return editGroupProfileCommand ?? (editGroupProfileCommand = new RelayCommand(param => this.EditGroup(param)));
            }
        }

        public ICommand DeleteGroupCommand
        {
            get
            {
                return deleteGroupCommand ?? (deleteGroupCommand = new RelayCommand(param => this.DeleteGroup(param)));
            }
        }

        public ICommand AddGroupCommand
        {
            get
            {
                return addGroupCommand ?? (addGroupCommand = new RelayCommand(param => this.AddGroup(param)));
            }
        }

        public ICommand MainMenuCommand
        {
            get
            {
                return mainmenuCommand ?? (mainmenuCommand = new RelayCommand(param => this.MainMenu()));
            }
        }

        public ICommand ShowAllNotes
        {
            get
            {
                return showAllNotesCommand ?? (showAllNotesCommand = new RelayCommand(param => this.ShowNotes()));
            }
        }

        public ICommand AddNoteCommand
        {
            get
            {
                return addNoteCommand ?? (addNoteCommand = new RelayCommand(param => this.AddNoteComm()));
            }
        }

        public ICommand EditNoteProfileCommand
        {
            get
            {
                return editNoteCommand ?? (editNoteCommand = new RelayCommand(param => this.EditNoteComm(param)));
            }
        }



        public ICommand DeleteNoteCommand
        {
            get
            {
                return deleteNoteCommand ?? (deleteNoteCommand = new RelayCommand(param => this.DeleteNoteComm(param)));
            }
        }

        public ICommand ChangeUserDataCommand
        {
            get
            {
                return changeUserDataCommand ?? (changeUserDataCommand = new RelayCommand(param => this.ChangeData(param)));
            }
        }

        private void ChangeData(object param)
        {
            
            UserAddAndEdit editDialog = new UserAddAndEdit(LoggedUser,LoggedUser);
            var res = editDialog.ShowDialog();

            if (res == true)
            {
              
            }
        }


        #endregion
        private void EditNoteComm(object param)
        {
            var Note = param as Note;
            if (Note == null)
            {
                return;
            }

            NoteAddAndEdit noteDialog = new NoteAddAndEdit(Note);
            var res = noteDialog.ShowDialog();

            ShowNotes();

        }

        private void DeleteNoteComm(object param)
        {
            var note = param as Note;
            if (note == null)
            {
                return;
            }
            bool success = false;

            success = Proxy.DeleteNote(note);
            if (success)
            {
                ShowNotes();
            }
            else
            {

            }
        }

        private void AddNoteComm()
        {
            NoteAddAndEdit noteAdd = new NoteAddAndEdit();
            var res = noteAdd.ShowDialog();

            if (res == true)
            {
                ShowNotes();
            }
        }

        private void ShowNotes()
        {
            try
            {
                AllNotes.Clear();
                List<Note> result = Proxy.GetNotesForUser(LoggedUser);

                foreach (var note in result)
                {
                    AllNotes.Add(note);
                }
            }
            catch (Exception e)
            {

            }
            CurrentState = Common.Entities.WindowState.NOTES;
        }

        private void MainMenu()
        {
            if (LoggedUser.Username == "admin")
            {
                CurrentState = Common.Entities.WindowState.ADMIN;
            }
            else
                CurrentState = Common.Entities.WindowState.USER;
        }

        private void AddGroup(object param)
        {
            GroupAddAndEdit groupAdd = new GroupAddAndEdit();
            var res = groupAdd.ShowDialog();

            if (res == true)
            {
                ShowGroups();
            }
        }

        private void EditGroup(object param)
        {
            var group = param as Group;
            if (group == null)
            {
                return;
            }

            GroupAddAndEdit editDialog = new GroupAddAndEdit(group);
            var res = editDialog.ShowDialog();
            ShowGroups();


        }

        private void DeleteGroup(object param)
        {
            var group = param as Group;
            if (group == null)
            {
            }


            bool success = false;

            success = Proxy.RemoveGroup(group);
            if (success)
            {
                ShowGroups();
            }
            else
            {

            }
        }

        public ObservableCollection<User> AllUsers
        {
            get { return allUsers; }
            set
            {
                allUsers = value;
                OnPropertyChanged("AllUsers");
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

        public ObservableCollection<Note> AllNotes
        {
            get { return allNotes; }
            set
            {
                allNotes = value;
                OnPropertyChanged("AllNotes");
            }
        }

        private void EditUserProfile(object param)
        {

            var user = param as User;
            if (user == null)
            {
                return;
            }

            UserAddAndEdit editDialog = new UserAddAndEdit(user,LoggedUser);
            var res = editDialog.ShowDialog();


            if (res == true)
            {
                if (editDialog.Tag != null)
                {
                    ///  LoggedUsername = editDialog.Tag.ToString();
                    ShowUsers();
                }
            }
        }

        private void ShowGroups()
        {
            try
            {
                AllGroups.Clear();
                List<Group> result = Proxy.GetAllGruops();

                foreach (var group in result)
                {
                    AllGroups.Add(group);
                }
            }
            catch (Exception e)
            {

            }
            CurrentState = Common.Entities.WindowState.GROUPS;
        }

        private void DeleteUser(object param)
        {


            var user = param as User;
            if (user == null)
            {
            }


            bool success = false;

            success = Proxy.RemoveUser(user);
            if (success)
            {
                ShowUsers();
            }
            else
            {

            }

        }

        private void AddUser(object param)
        {

            UserAddAndEdit userAdd = new UserAddAndEdit();
            var res = userAdd.ShowDialog();

            if (res == true)
            {
                ShowUsers();
            }


        }

        private void ShowUsers()
        {
            try
            {
                AllUsers.Clear();
                List<User> result = Proxy.GetAllUsers();

                foreach (var user in result)
                {
                    AllUsers.Add(user);
                }

            }
            catch (Exception e)
            {

            }
            CurrentState = Common.Entities.WindowState.USERS;
        }

        private void LoginClick(object param)
        {

            object[] parameters = param as object[];

            if (parameters == null)
            {

            }

            if (parameters[0] == null || parameters[1] == null)
            {
                return;
            }

            string username = parameters[0].ToString();
            string pass = parameters[1].ToString();

            bool success = Proxy.LogIn(username, pass);

            if (success)
            {
                LoggedUser = Proxy.GetUser(username);
                if (LoggedUser.Username == "admin" && LoggedUser.Password == "admin")
                    CurrentState = Common.Entities.WindowState.ADMIN;
                else
                    CurrentState = Common.Entities.WindowState.USER;
            }

        }

        private void LogOut(object param)
        {

            bool success = Proxy.LogOut(LoggedUser.Username);

            if (success)
            {
                LoggedUser.Username = "";
                LoggedUser = null;
                CurrentState = Common.Entities.WindowState.LOGIN;
            }
            else
            {
                MessageBox.Show("Error while loggout");

            }
        }



        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
