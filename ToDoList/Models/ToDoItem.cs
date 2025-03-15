using System;
using System.ComponentModel;
using System.Windows.Media;

namespace ToDoList.Models
{
    public class ToDoItem : INotifyPropertyChanged
    {
        private string _title = "New Task";
        private bool _isCompleted = false;
        private bool _isImportant = false;
        private SolidColorBrush _backgroundColor = new SolidColorBrush(Colors.White);

        public ToDoItem()
        { }

        public string Title
        {
            get { return _title; }
            set
            {
                if (_title != value)
                {
                    _title = value;
                    OnPropertyChanged(nameof(Title));
                }
            }
        }

        public bool IsCompleted
        {
            get { return _isCompleted; }
            set
            {
                if (_isCompleted != value)
                {
                    _isCompleted = value;
                    OnPropertyChanged(nameof(IsCompleted));
                }
            }
        }

        public bool IsImportant
        {
            get { return _isImportant; }
            set
            {
                if (_isImportant != value)
                {
                    _isImportant = value;
                    OnPropertyChanged(nameof(IsImportant));
                }
            }
        }

        public SolidColorBrush BackgroundColor
        {
            get { return _backgroundColor; }
            set
            {
                if (_backgroundColor != value)
                {
                    _backgroundColor = value;
                    OnPropertyChanged(nameof(BackgroundColor));
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}