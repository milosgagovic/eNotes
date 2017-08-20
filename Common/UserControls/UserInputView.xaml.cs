using Common.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace Common.UserControls
{
    /// <summary>
    /// Interaction logic for UserInputView.xaml
    /// </summary>
    public partial class UserInputView : UserControl
    {
        public UserInputView()
        {
            InitializeComponent();
            DataContext = this;
        }

        public event EventHandler SaveClicked;
        public event EventHandler CancelClicked;

        public static readonly DependencyProperty UserProperty =
        DependencyProperty.Register(
            "User",
            typeof(User),
            typeof(UserInputView),
            new UIPropertyMetadata(null));

        public static readonly DependencyProperty LogedUserProperty =
       DependencyProperty.Register(
           "LogedUser",
           typeof(User),
           typeof(UserInputView),
           new UIPropertyMetadata(null));

        public static readonly DependencyProperty ListProp =
        DependencyProperty.Register(
            "Lista",
            typeof(ListView),
            typeof(UserInputView),
            new UIPropertyMetadata(null));

        public static readonly DependencyProperty GroupsProperty =
       DependencyProperty.Register(
           "AllGroups",
           typeof(ObservableCollection<Group>),
           typeof(UserInputView),
           new UIPropertyMetadata(null));

        public static readonly DependencyProperty SelectegGroupsProperty =
      DependencyProperty.Register(
          "SelectedGroups",
          typeof(List<Group>),
          typeof(UserInputView),
          new UIPropertyMetadata(null));

        private List<Group> lstMyObject = new List<Group>();

        public List<Group> SelectedGroups
        {
            get { return (List<Group>)lstMyObject; }
            set { SetValue(SelectegGroupsProperty, value); }
        }


        public User User
        {
            get { return (User)GetValue(UserProperty); }
            set { SetValue(UserProperty, value); }
        }

        public User LogedUser
        {
            get { return (User)GetValue(LogedUserProperty); }
            set { SetValue(LogedUserProperty, value); }
        }

        public ObservableCollection<Group> AllGroups
        {
            get { return (ObservableCollection<Group>)GetValue(GroupsProperty); }
            set { SetValue(GroupsProperty, value); }
        }

        public static readonly DependencyProperty SaveCommandProperty =
        DependencyProperty.Register(
            "SaveCommand",
            typeof(ICommand),
            typeof(UserInputView),
            new UIPropertyMetadata(null));

        public ICommand SaveCommand
        {
            get { return (ICommand)GetValue(SaveCommandProperty); }
            set { SetValue(SaveCommandProperty, value); }
        }

        public static readonly DependencyProperty CancelCommandProperty =
        DependencyProperty.Register(
            "CancelCommand",
            typeof(ICommand),
            typeof(UserInputView),
            new UIPropertyMetadata(null));

        public ICommand CancelCommand
        {
            get { return (ICommand)GetValue(CancelCommandProperty); }
            set { SetValue(CancelCommandProperty, value); }
        }
        public ListView Lista
        {
            get { return listView1; }
            set { SetValue(ListProp, value); }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (passBox.Password.Trim() != String.Empty)
            {
                User.Password = passBox.Password;
            }

            if (User.Password != String.Empty)
            {
                if (SaveClicked != null)
                {
                    SaveClicked(sender, e);
                }

                if (SaveCommand != null && SaveCommand.CanExecute(this))
                {
                    SaveCommand.Execute(this);
                }
            }
            else
            {
                passBox.Background = Brushes.Red;
            }
        }

        private void Cancel_btn_Click(object sender, RoutedEventArgs e)
        {
            if (CancelClicked != null)
            {
                CancelClicked(sender, e);
            }

            if (CancelCommand != null && CancelCommand.CanExecute(this))
            {
                CancelCommand.Execute(this);
            }
        }

        

        private void listView1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (Group item in e.RemovedItems)
            {
                lstMyObject.Remove(item);
            }

            foreach (Group item in e.AddedItems)
            {
                lstMyObject.Add(item);
            }
        }
    }
}