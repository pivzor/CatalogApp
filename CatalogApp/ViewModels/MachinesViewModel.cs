
using CatalogApp.Domain;
using CatalogApp.Domain.Entities;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CatalogApp.ViewModels;

public class MachinesViewModel
{
    private readonly IBaseRepository<Machine> _repository;

    public ObservableCollection<Machine> Machines { get; } = new();

    public MachinesViewModel(IBaseRepository<Machine> repository)
    {
        _repository = repository;
        LoadMachines();
    }

    public void LoadMachines()
    {
        Machines.Clear();

        List<Machine> machines = _repository.GetAll();

        foreach (Machine machine in machines)
        {
            Machines.Add(machine);
        }
    }
}
