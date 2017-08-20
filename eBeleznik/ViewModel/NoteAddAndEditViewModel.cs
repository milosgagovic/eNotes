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
    public class NoteAddAndEditViewModel: INotifyPropertyChanged
    {
        public Note Note { get; set; }
        private IServiceConract proxy;
        private ObservableCollection<Group> allGroups = new ObservableCollection<Group>();
        private ListView lista = new ListView();
        private string noteText;
        private string noteTitle;
        public NoteAddAndEditViewModel()
        {
            Note = new Note();
            Proxy = App.Proxy;
            List<Group> result = Proxy.GetAllGruops();
            NoteText = Note.RTFText;
            NoteTitle = Note.Title;
            AllGroups.Clear();
            foreach (var item in result)
            {
                AllGroups.Add(item);
            }

        }

        public NoteAddAndEditViewModel(Note note)
        {
            Proxy = App.Proxy;
            Note = note; ////
            NoteText = Note.RTFText;
            NoteTitle = Note.Title;
            List<Group> result = Proxy.GetAllGruops();
            AllGroups.Clear();
             
            foreach (var item in result)
            {
                AllGroups.Add(item);
            }
            

        }

        private ICommand cancelCommand;
        private ICommand saveCommand;


        public ListView Lista { get => lista; set => lista = value; }
        public ObservableCollection<Group> AllGroups
        {
            get { return allGroups; }
            set
            {
                allGroups = value;
                OnPropertyChanged("AllGroups");
            }
        }

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

        private void SaveClick(object param)
        {
            NoteInputView noteControl = param as NoteInputView;
            Window parentWindow = Window.GetWindow(noteControl);
            bool success = false;

            if (Note.Id == 0)
            {
                foreach (Group item in noteControl.Lista.SelectedItems)
                {
                    noteControl.Note.Groups.Add(item);
                }
                success = Proxy.AddNote(Note);
            }
            else
            {
                foreach (Group item in noteControl.Lista.SelectedItems)
                {
                    noteControl.Note.Groups.Add(item);
                }
                Note n = Proxy.GetNote(Note.Id);
                if (n != null)
                {
                    if (n.RTFText != NoteText || n.Title != NoteTitle)
                    {
                        MessageBoxResult m = MessageBox.Show("Other user has changed this note. \n Do you want to keep youurs ?", "Save", MessageBoxButton.YesNo);
                        if (m == MessageBoxResult.Yes) 
                        {
                            success = Proxy.UpdateNote(Note);
                        }
                        else if (m == MessageBoxResult.No)
                        {
                            parentWindow.Close();
                            return;
                        }
                    }
                    success = Proxy.UpdateNote(Note);

                }
                else
                {
                    MessageBoxResult m = MessageBox.Show("Other user has delete this note. \n Do you want to keep youurs ?", "Save", MessageBoxButton.YesNo);
                    if (m == MessageBoxResult.Yes)
                    {
                        success = Proxy.AddNote(Note);
                    }
                    else if (m == MessageBoxResult.No)
                    {
                        parentWindow.Close();
                        return;
                    }
                }


            }

            if (success)
            {
                parentWindow.DialogResult = true;

                parentWindow.Close();
            }
            else
            {
                parentWindow.Close();
            }
        }

        private void CancelClick(object param)
        {
            var userControl = param as UserControl;
            Window parentWindow = Window.GetWindow(userControl);

            parentWindow.Close();
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
       
        public string NoteText { get => noteText; set => noteText = value; }
        public string NoteTitle { get => noteTitle; set => noteTitle = value; }

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
