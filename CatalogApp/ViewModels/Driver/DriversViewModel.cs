
using CatalogApp.Domain;
using CatalogApp.Domain.Entities;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CatalogApp.ViewModels;

public class DriversViewModel
{
    private readonly IBaseRepository<Driver> _repository;

    public ObservableCollection<Driver> Drivers { get; } = new();

    public Driver? SelectedDriver { get; set; }

    public DriversViewModel(IBaseRepository<Driver> repository)
    {
        _repository = repository;
        LoadDrivers();
    }

    public void LoadDrivers()
    {
        Drivers.Clear();

        foreach (Driver driver in _repository.GetAll())
        {
            Drivers.Add(driver);
        }
    }

    public void DeleteDriver(int id)
    {
        _repository.Delete(id);
        LoadDrivers();
        SelectedDriver = null;
    }
}
