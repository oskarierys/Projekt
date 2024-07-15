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
            
        }

        private void DeletePerson()
        {
            Persons.Remove(SelectedPerson);
        }

    }
}
    