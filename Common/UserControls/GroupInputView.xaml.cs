using Common.Entities;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for GroupInputView.xaml
    /// </summary>
    public partial class GroupInputView : UserControl
    {
        public GroupInputView()
        {
            InitializeComponent();
            DataContext = this;
        }

        public event EventHandler SaveClickedG;
        public event EventHandler CancelClickedG;

        public static readonly DependencyProperty GroupProperty =
        DependencyProperty.Register(
            "Group",
            typeof(Group),
            typeof(GroupInputView),
            new UIPropertyMetadata(null));

        public Group Group
        {
            get { return (Group)GetValue(GroupProperty); }
            set { SetValue(GroupProperty, value); }
        }

        public static readonly DependencyProperty SaveCommandPropertyG =
        DependencyProperty.Register(
            "SaveCommandG",
            typeof(ICommand),
            typeof(GroupInputView),
            new UIPropertyMetadata(null));

        public ICommand SaveCommandG
        {
            get { return (ICommand)GetValue(SaveCommandPropertyG); }
            set { SetValue(SaveCommandPropertyG, value); }
        }

        public static readonly DependencyProperty CancelCommandPropertyG =
        DependencyProperty.Register(
            "CancelCommandG",
            typeof(ICommand),
            typeof(GroupInputView),
            new UIPropertyMetadata(null));

        public ICommand CancelCommandG
        {
            get { return (ICommand)GetValue(CancelCommandPropertyG); }
            set { SetValue(CancelCommandPropertyG, value); }
        }

        private void Save_ClickG(object sender, RoutedEventArgs e)
        {


            if (SaveClickedG != null)
            {
                SaveClickedG(sender, e);
            }

            if (SaveCommandG != null && SaveCommandG.CanExecute(this))
            {
                SaveCommandG.Execute(this);
            }
        }

        private void Cancel_btn_ClickG(object sender, RoutedEventArgs e)
        {
            if (CancelClickedG != null)
            {
                CancelClickedG(sender, e);
            }

            if (CancelCommandG != null && CancelCommandG.CanExecute(this))
            {
                CancelCommandG.Execute(this);
            }
        }
    }
}