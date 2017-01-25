using System.Windows.Input;
using Core.Models;
using MugenMvvmToolkit.Models;
using MugenMvvmToolkit.ViewModels;

namespace Core.ViewModels
{
    public class MainViewModel : EditableViewModel<User>
    {
        private GridViewModel<User> _userGridViewModel;

        public MainViewModel()
        {
            AddUserCommand = new RelayCommand(AddUser, CanAddUser, this);
            DeleteUserCommand = new RelayCommand(DeleteUser, CanDeleteUser, this);
        }

        public ICommand AddUserCommand { get; private set; }
        public ICommand DeleteUserCommand { get; private set; }

        public string Lastname
        {
            get { return Entity.Lastname; }
            set
            {
                if (Equals(Entity.Lastname, value)) return;
                Entity.Lastname = value;
                OnPropertyChanged();
            }
        }

        public string Firstname
        {
            get { return Entity.Firstname; }
            set
            {
                if (Equals(Entity.Firstname, value)) return;
                Entity.Firstname = value;
                OnPropertyChanged();
            }
        }

        public GridViewModel<User> UserGridViewModel
        {
            get { return _userGridViewModel; }
            private set
            {
                if (Equals(value, _userGridViewModel)) return;
                _userGridViewModel = value;
                OnPropertyChanged();
            }
        }

        protected override void OnInitialized()
        {
            UserGridViewModel = GetViewModel<GridViewModel<User>>();
            InitializeNewUser();
        }

        private void AddUser()
        {
            ApplyChanges();
            var entity = Entity;
            UserGridViewModel.ItemsSource.Add(entity);
            UserGridViewModel.SelectedItem = entity;
            InitializeNewUser();
        }

        private bool CanAddUser()
        {
            return IsValid;
        }

        private void DeleteUser()
        {
            UserGridViewModel.ItemsSource.Remove(UserGridViewModel.SelectedItem);
        }

        private bool CanDeleteUser()
        {
            return UserGridViewModel.SelectedItem != null;
        }

        private void InitializeNewUser()
        {
            var user = new User();
            InitializeEntity(user, true);
        }
    }
}
