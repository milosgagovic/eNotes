using Common.Entities;
using Common.UserControls;
using eBeleznik.ViewModel;
using System;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;

namespace eBeleznik.View
{
    /// <summary>
    /// Interaction logic for NoteAddAndEdit.xaml
    /// </summary>
    public partial class NoteAddAndEdit : Window
    {
        public NoteAddAndEdit()
        {
            DataContext = new NoteAddAndEditViewModel();
            Initialized += ProfileDialog_Initialized;

            InitializeComponent();
        }

        public NoteAddAndEdit(Note note)
        {
            DataContext = new NoteAddAndEditViewModel(note);
            Initialized += ProfileDialog_Initialized;


            InitializeComponent();
            DeserialiseRTB(note,noteInputControl.RTB);
            

        }

        internal void ProfileDialog_Initialized(object sender, EventArgs e)
        {
            var viewModel = DataContext as NoteAddAndEditViewModel;

            Binding binding = new Binding
            {
                Source = viewModel.Note,
            };
            noteInputControl.SetBinding(NoteInputView.NoteProperty, binding);

            binding = new Binding
            {
                Source = viewModel.SaveCommand,
            };
            noteInputControl.SetBinding(NoteInputView.SaveCommandProperty, binding);

            binding = new Binding
            {
                Source = viewModel.CancelCommand,
            };
            noteInputControl.SetBinding(NoteInputView.CancelCommandProperty, binding);

            binding = new Binding
            {
                Source = viewModel.AllGroups,
            };
            noteInputControl.SetBinding(NoteInputView.GroupsProperty, binding);

            binding = new Binding
            {
                Source = viewModel.Lista,
            };
            noteInputControl.SetBinding(NoteInputView.ListProp, binding);


        }

        public void DeserialiseRTB(Note note,RichTextBox Rtb)
        {
            string rtfText = note.RTFText;
            if (rtfText == null)
            {
                return;
            }
            byte[] byteArray = Encoding.ASCII.GetBytes(rtfText);
            using (MemoryStream ms = new MemoryStream(byteArray))
            {
                TextRange tr = new TextRange(Rtb.Document.ContentStart, Rtb.Document.ContentEnd);
                tr.Load(ms, DataFormats.Rtf);
            }
        }

    }
}
