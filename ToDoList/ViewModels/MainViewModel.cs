using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows.Media;
using ToDoList.Commands;
using ToDoList.Models;

namespace ToDoList.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<ToDoItem> _tasks;
        private ToDoItem _selectedTask;
        private const int MAX_TASKS = 20;

        public MainViewModel()
        {
            // Initialize the task list
            Tasks = new ObservableCollection<ToDoItem>();

            // Initialize Commands
            AddTaskCommand = new RelayCommand(AddTask, CanAddTask);
            RemoveTaskCommand = new RelayCommand(RemoveTask, CanEditTask);
            MoveTaskUpCommand = new RelayCommand(MoveTaskUp, CanMoveTaskUp);
            MoveTaskDownCommand = new RelayCommand(MoveTaskDown, CanMoveTaskDown);
            ToggleImportantCommand = new RelayCommand(ToggleImportant, CanEditTask);
            ChangeBackgroundColorCommand = new RelayCommand(ChangeBackgroundColor, CanEditTask);

            // Add a sample task
            AddTask(null);
        }

        public ObservableCollection<ToDoItem> Tasks
        {
            get { return _tasks; }
            set
            {
                _tasks = value;
                OnPropertyChanged(nameof(Tasks));
            }
        }

        public ToDoItem SelectedTask
        {
            get { return _selectedTask; }
            set
            {
                _selectedTask = value;
                OnPropertyChanged(nameof(SelectedTask));
                CommandManager.InvalidateRequerySuggested();
            }
        }

        // Command defintions
        public ICommand AddTaskCommand { get; }
        public ICommand RemoveTaskCommand { get; }
        public ICommand MoveTaskUpCommand { get; }
        public ICommand MoveTaskDownCommand { get; }
        public ICommand ToggleImportantCommand { get; }
        public ICommand ChangeBackgroundColorCommand { get; }

        // Available colors for task backgrounds
        public List<SolidColorBrush> AvailableColors { get; } = new List<SolidColorBrush>
        {
            new SolidColorBrush(Colors.White),
            new SolidColorBrush(Colors.LightBlue),
            new SolidColorBrush(Colors.LightGreen),
            new SolidColorBrush(Colors.LightPink),
            new SolidColorBrush(Colors.LightYellow),
            new SolidColorBrush(Colors.LightGray)
        };

        // Command methods
        private bool CanAddTask(object parameter)
        {
            return Tasks.Count < MAX_TASKS;
        }

        // Add task method
        private void AddTask(object parameter)
        {
            var newTask = new ToDoItem();
            Tasks.Add(newTask);
            SelectedTask = newTask;
        }

        // Check if task can be edited
        private bool CanEditTask(object parameter)
        {
            return SelectedTask != null;
        }

        // Remove task method
        private void RemoveTask(object parameter)
        {
            int index = Tasks.IndexOf(SelectedTask);
            Tasks.Remove(SelectedTask);
            if (Tasks.Count > 0)
            {
                SelectedTask = Tasks[Math.Min(index, Tasks.Count - 1)];
            }
        }

        // Check if task can be moved one slot up
        private bool CanMoveTaskUp(object parameter)
        {
            if (SelectedTask == null)
                return false;

            int index = Tasks.IndexOf(SelectedTask);
            return index > 0 && !SelectedTask.IsCompleted;
        }

        // Move task one slot up
        private void MoveTaskUp(object parameter)
        {
            int index = Tasks.IndexOf(SelectedTask);
            Tasks.Move(index, index - 1);
        }

        // Check if task can be moved one slot down
        private bool CanMoveTaskDown(object parameter)
        {
            if (SelectedTask == null)
                return false;

            int index = Tasks.IndexOf(SelectedTask);
            // Can't move down if it's the last task or if it's completed
            return index < Tasks.Count - 1 && !SelectedTask.IsCompleted;
        }

        // Move task one slot down
        private void MoveTaskDown(object parameter)
        {
            int index = Tasks.IndexOf(SelectedTask);
            Tasks.Move(index, index + 1);
        }

        // Mark task as important
        private void ToggleImportant(object parameter)
        {
            SelectedTask.IsImportant = !SelectedTask.IsImportant;
            if (SelectedTask.IsImportant && !SelectedTask.IsCompleted)
            {
                MoveToTop(SelectedTask);
            }
        }

        // Move task to the top of the list
        private void MoveToTop(ToDoItem task)
        {
            int currentIndex = Tasks.IndexOf(task);
            if (currentIndex > 0)
            {
                Tasks.Move(currentIndex, 0);
            }
        }

        // Change background color of the sleceted task
        private void ChangeBackgroundColor(object parameter)
        {
            if (SelectedTask != null && parameter is SolidColorBrush colorBrush)
            {
                SelectedTask.BackgroundColor = colorBrush;
            }
        }

        // Property changed implementation
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
