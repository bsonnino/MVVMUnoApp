using System;
using System.Collections.Generic;
using System.Linq;
using CustomerLib;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;

namespace MVVMUnoApp.ViewModel
{
    public class MainViewModel : ObservableObject
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IEnumerable<Customer> _allCustomers;
        private Customer _selectedCustomer;

        public MainViewModel(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository ??
                                  throw new ArgumentNullException("customerRepository");
            _allCustomers = _customerRepository.Customers;
            Customers = _allCustomers;
            AddCommand = new RelayCommand(DoAdd);
            RemoveCommand = new RelayCommand(DoRemove, () => SelectedCustomer != null);
            SaveCommand = new RelayCommand(DoSave);
            SearchCommand = new RelayCommand<string>(DoSearch);
        }

        public IEnumerable<Customer> Customers {get; private set;}

        public Customer SelectedCustomer
        {
            get => _selectedCustomer;
            set
            {
                SetProperty(ref _selectedCustomer, value);
                RemoveCommand.NotifyCanExecuteChanged();
            }
        }

        public IRelayCommand AddCommand { get; }
        public IRelayCommand RemoveCommand { get; }
        public IRelayCommand SaveCommand { get; }
        public IRelayCommand<string> SearchCommand { get; }

        private void DoAdd()
        {
            var customer = new Customer();
            _customerRepository.Add(customer);
            SelectedCustomer = customer;
            OnPropertyChanged("Customers");
        }

        private void DoRemove()
        {
            if (SelectedCustomer != null)
            {
                _customerRepository.Remove(SelectedCustomer);
                SelectedCustomer = null;
                OnPropertyChanged("Customers");
            }
        }

        private void DoSave()
        {
            _customerRepository.Commit();
        }

        private void DoSearch(string textToSearch)
        {
            if (!string.IsNullOrWhiteSpace(textToSearch))
                Customers = _allCustomers.Where(c => ((Customer)c).Country.ToLower().Contains(textToSearch.ToLower()));
            else
                Customers = _allCustomers;
            OnPropertyChanged("Customers");
        }
    }
}