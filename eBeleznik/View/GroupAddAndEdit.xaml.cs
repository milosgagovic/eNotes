using Common.Entities;
using Common.UserControls;
using eBeleznik.ViewModel;
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
using System.Windows.Shapes;

namespace eBeleznik.View
{
    /// <summary>
    /// Interaction logic for GroupAddAndEdit.xaml
    /// </summary>
    public partial class GroupAddAndEdit : Window
    {
        public GroupAddAndEdit()
        {
            DataContext = new GroupAddAndEditViewModel();
            Initialized += GroupDialog_Initialized;

            InitializeComponent();
        }

        public GroupAddAndEdit(Group group)
        {
            DataContext = new GroupAddAndEditViewModel(group);
            Initialized += GroupDialog_Initialized;

            InitializeComponent();

        }

        internal void GroupDialog_Initialized(object sender, EventArgs e)
        {
            var viewModel = DataContext as GroupAddAndEditViewModel;

            Binding binding = new Binding
            {
                Source = viewModel.Group,
            };
            groupInputControl.SetBinding(GroupInputView.GroupProperty, binding);

            binding = new Binding
            {
                Source = viewModel.SaveCommandG,
            };
            groupInputControl.SetBinding(GroupInputView.SaveCommandPropertyG, binding);

            binding = new Binding
            {
                Source = viewModel.CancelCommandG,
            };
            groupInputControl.SetBinding(GroupInputView.CancelCommandPropertyG, binding);


        }

    }
}
