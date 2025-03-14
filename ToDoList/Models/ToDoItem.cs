using System;
using System.ComponentModel;
using System.Windows.Media;

namespace ToDoList.Models
{
    public class ToDoItem : INotifyPropertyChanged
    {
        private string _title = "";
        private bool _isCompleted;
        private bool _isImportant;
        private SolidColorBrush _backgroundColor = new SolidColorBrush();

        public ToDoItem()
        {
            Title = "New Task";
            IsCompleted = false;
            IsImportant = false;
            BackgroundColor = new SolidColorBrush(Colors.White);
        }

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