using Common.Entities;
using Common.UserControls;
using eBeleznik.ViewModel;
using System;
using System.Windows;
using System.Windows.Data;

namespace eBeleznik.View
{
    /// <summary>
    /// Interaction logic for UserAddAndEdit.xaml
    /// </summary>
    public partial class UserAddAndEdit : Window
    {
        
        public UserAddAndEdit()
        {
            DataContext = new UserAddAndEditViewModel();
            Initialized += ProfileDialog_Initialized;
            
            InitializeComponent();
        }

        public UserAddAndEdit(User user,User LoggedUser)
        {
            DataContext = new UserAddAndEditViewModel(user,LoggedUser);
            Initialized += ProfileDialog_Initialized;

            InitializeComponent();
            
        }

        internal void ProfileDialog_Initialized(object sender, EventArgs e)
        {
            var viewModel = DataContext as UserAddAndEditViewModel;
            
            Binding binding = new Binding
            {
                Source = viewModel.User,
            };
            userInputControl.SetBinding(UserInputView.UserProperty, binding);

            binding = new Binding
            {
                Source = viewModel.AllGroups,
            };
            userInputControl.SetBinding(UserInputView.GroupsProperty, binding);

            binding = new Binding
            {
                Source = viewModel.SaveCommand,
            };
            userInputControl.SetBinding(UserInputView.SaveCommandProperty, binding);
            
            binding = new Binding
            {
                Source = viewModel.CancelCommand,
            };
            userInputControl.SetBinding(UserInputView.CancelCommandProperty, binding);

            //binding = new Binding
            //{
            //    Source = viewModel.Lista,
               
            //};
            //userInputControl.SetBinding(UserInputView.ListProp, binding);

            binding = new Binding
            {
                Source = viewModel.SelectedGroups,

            };
            userInputControl.SetBinding(UserInputView.SelectegGroupsProperty, binding);

            binding = new Binding
            {
                Source = viewModel.LoggedUser,

            };
            userInputControl.SetBinding(UserInputView.LogedUserProperty, binding);


        }

    }
}
