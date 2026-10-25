
using CatalogApp.Domain;
using CatalogApp.Domain.Entities;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CatalogApp.ViewModels;

public class CrewsViewModel
{
    private readonly IBaseRepository<Crew> _repository;

    public ObservableCollection<Crew> Crews { get; } = new();

    public Crew? SelectedCrew { get; set; }

    public CrewsViewModel(IBaseRepository<Crew> repository)
    {
        _repository = repository;
        LoadCrews();
    }

    public void LoadCrews()
    {
        Crews.Clear();

        foreach (Crew crew in _repository.GetAll())
        {
            Crews.Add(crew);
        }
    }

    public void DeleteCrew(int id)
    {
        _repository.Delete(id);
        SelectedCrew = null;
        LoadCrews();
    }
}
