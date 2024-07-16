using Projekt.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Projekt.MVVM;
using System.Security.Cryptography.X509Certificates;
using System.Linq;
using Projekt.Data;

namespace Projekt.ViewModels
{
    internal class MainWindowViewModel : ViewModelBase
    {
        private PersonRepository _repository;

        public ObservableCollection<Person> Persons { get; set; }

        public RelayCommand AddCommand => new RelayCommand(execute => AddPerson());
        public RelayCommand DeleteCommand => new RelayCommand(execute => DeletePerson(), canExecute => SelectedPerson != null);

        public MainWindowViewModel()
        {
            _repository = new PersonRepository();
            Persons = new ObservableCollection<Person>();
        }

        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged();
            }
        }

        private string surname;
        public string Surname
        {
            get { return surname; }
            set
            {
                surname = value;
                OnPropertyChanged();
            }
        }

        private int age;
        public int Age
        {
            get { return age; }
            set
            {
                age = value;
                OnPropertyChanged();
            }
        }

        private string gender;
        public string Gender
        {
            get { return gender; }
            set
            {
                gender = value;
                OnPropertyChanged();
            }
        }

        private Person selectedPerson;
        public Person SelectedPerson
        {
            get { return selectedPerson; }
            set 
            { 
                selectedPerson = value;
                OnPropertyChanged();
            }
        }

        private void AddPerson()
        {
            var newPerson = new Person
            {
                Name = this.Name,
                Surname = this.Surname,
                Age = this.Age,
                Gender = this.Gender
            };

            Persons.Add(newPerson);
            Name = string.Empty;
            Surname = string.Empty;
            Age = 0;

            Gender = string.Empty;
        }

        private void DeletePerson()
        {
            Persons.Remove(SelectedPerson);
        }

    }
}
    