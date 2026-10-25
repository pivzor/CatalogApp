using CatalogApp.Domain;
using CatalogApp.Domain.Entities;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CatalogApp.ViewModels.Machin;

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

        foreach (Machine machine in _repository.GetAll())
        {
            Machines.Add(machine);
        }
    }

    public void DeleteMachine(int id)
    {
        _repository.Delete(id);
        LoadMachines();
    }
}