using Projekt.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Projekt.MVVM;
using System.Security.Cryptography.X509Certificates;

namespace Projekt.ViewModels
{
    internal class MainWindowViewModel : ViewModelBase
    {
        public ObservableCollection<Person> Persons { get; set; }

        public RelayCommand AddCommand => new RelayCommand(execute => AddPerson());
        public RelayCommand DeleteCommand => new RelayCommand(execute => DeletePerson(), canExecute => SelectedPerson != null);

        public MainWindowViewModel()
        {
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
        }

        private void DeletePerson()
        {
            Persons.Remove(SelectedPerson);
        }

    }
}
    