using Common;
using Common.Entities;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace eBeleznik.ViewModel
{
    public class GroupAddAndEditViewModel
    {
        public Group Group { get; set; }

        private IServiceConract proxy;
        public GroupAddAndEditViewModel()
        {
            Group = new Group();
            Proxy = App.Proxy;
        }

        public GroupAddAndEditViewModel(Group group)
        {
            Proxy = App.Proxy;
            Group = group;

            if (Group == null)
            {
            }

        }

        #region Commands
        private ICommand cancelCommand;
        private ICommand saveCommand;

        public ICommand CancelCommandG
        {
            get
            {
                return cancelCommand ?? (cancelCommand = new RelayCommand((param) => this.CancelClick(param)));
            }
        }

        public ICommand SaveCommandG
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

            var userControl = param as UserControl;
            Window parentWindow = Window.GetWindow(userControl);

            bool success = false;

            //Add if not exist(Create new Group)
            if (Group.Id == 0)
            {
                success = Proxy.AddGroup(Group);
            }
            else
            {
                success = Proxy.UpdateGroup(Group);
            }

            if (success)
            {
                parentWindow.DialogResult = true;
                //if (App.LoggedUser.Id == Group.Id)
                //{
                //    parentWindow.Tag = Group;
                //}
                parentWindow.Close();
            }
            else
            {
                parentWindow.Close();
            }
        }
        #endregion Methods
    }


}

