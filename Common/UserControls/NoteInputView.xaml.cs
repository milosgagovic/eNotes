using Common.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Common.UserControls
{
    /// <summary>
    /// Interaction logic for NoteInputView.xaml
    /// </summary>
    public partial class NoteInputView : UserControl
    {
        public NoteInputView()
        {
            InitializeComponent();
            DataContext = this;
            undoBtn.Background = new ImageBrush( new BitmapImage(new Uri(@"Common/Images/uundo.png", UriKind.RelativeOrAbsolute)));
            redoBtn.Background = new ImageBrush(new BitmapImage(new Uri(@"Common/Images/rredo.png", UriKind.RelativeOrAbsolute)));
        }
        public event EventHandler SaveClicked;
        public event EventHandler CancelClicked;
        private List<Group> lstMyObject = new List<Group>();


        public static readonly DependencyProperty NoteProperty =
      DependencyProperty.Register(
          "Note",
          typeof(Note),
          typeof(NoteInputView),
          new UIPropertyMetadata(null));

        public Note Note
        {
            get { return (Note)GetValue(NoteProperty); }
            set { SetValue(NoteProperty, value); }
        }

        public static readonly DependencyProperty GroupsProperty =
       DependencyProperty.Register(
           "AllGroups",
           typeof(ObservableCollection<Group>),
           typeof(NoteInputView),
           new UIPropertyMetadata(null));

        public ObservableCollection<Group> AllGroups
        {
            get { return (ObservableCollection<Group>)GetValue(GroupsProperty); }
            set { SetValue(GroupsProperty, value); }
        }

        public static readonly DependencyProperty ListProp =
        DependencyProperty.Register(
            "LisyView",
            typeof(ListView),
            typeof(NoteInputView),
            new UIPropertyMetadata(null));

        public ListView Lista
        {
            get { return listView1; }
            set { SetValue(ListProp, value); }
        }

        public static readonly DependencyProperty RtbProperty =
     DependencyProperty.Register(
         "RTB",
         typeof(RichTextBox),
         typeof(NoteInputView),
         new UIPropertyMetadata(null));

        public RichTextBox RTB
        {
            get { return rtbEditor; }
            set { SetValue(RtbProperty, value); }
        }


        public static readonly DependencyProperty SaveCommandProperty =
      DependencyProperty.Register(
          "SaveCommand",
          typeof(ICommand),
          typeof(NoteInputView),
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
            typeof(NoteInputView),
            new UIPropertyMetadata(null));

        public ICommand CancelCommand
        {
            get { return (ICommand)GetValue(CancelCommandProperty); }
            set { SetValue(CancelCommandProperty, value); }
        }

        private void rtbEditor_SelectionChanged(object sender, RoutedEventArgs e)
        {
            object temp = rtbEditor.Selection.GetPropertyValue(Inline.FontWeightProperty);
            object tempI = rtbEditor.Selection.GetPropertyValue(Inline.FontStyleProperty);
            object tempU = rtbEditor.Selection.GetPropertyValue(Inline.TextDecorationsProperty);
            btnBold.IsChecked = (temp != DependencyProperty.UnsetValue) && (temp.Equals(FontWeights.Bold));
            btnItalic.IsChecked = (tempI != DependencyProperty.UnsetValue) && (tempI.Equals(FontStyles.Italic));
            btnUnderLine.IsChecked = (temp != DependencyProperty.UnsetValue) && (tempU.Equals(TextDecorations.Underline));

            temp = rtbEditor.Selection.GetPropertyValue(Inline.FontFamilyProperty);


            //  Console.WriteLine(new TextRange(rtbEditor.Document.ContentStart, rtbEditor.Document.ContentEnd).Text);
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {

            if (Title.Text == "")
            {
                MessageBox.Show("You need to insert title for this note");
                return;
            }

            string rtfText; //string to save to db
            TextRange tr = new TextRange(rtbEditor.Document.ContentStart, rtbEditor.Document.ContentEnd);
            using (MemoryStream ms = new MemoryStream())  // serijalizacija RTB u string za smestanje u bazu
            {
                tr.Save(ms, DataFormats.Rtf);
                rtfText = Encoding.ASCII.GetString(ms.ToArray());
            }
            Note.RTFText = rtfText;
            if (SaveClicked != null)
                {
                    SaveClicked(sender, e);
                }

                if (SaveCommand != null && SaveCommand.CanExecute(this))
                {
                    SaveCommand.Execute(this);
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

        private void redoClick(object sender, RoutedEventArgs e)
        {
            rtbEditor.Redo();
        }

        private void undoClick(object sender, RoutedEventArgs e)
        {
            rtbEditor.Undo();
        }
    }
}
