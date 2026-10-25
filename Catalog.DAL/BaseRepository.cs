
using Catalog.DAL;
using CatalogApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CatalogApp.Domain;

public class BaseRepository<T> : IBaseRepository<T> where T : class
{
    private readonly IDbContextFactory<AppDbContext> _contextFactory;

    public BaseRepository(IDbContextFactory<AppDbContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public List<T> GetAll()
    {
        using AppDbContext db = _contextFactory.CreateDbContext();

        IQueryable<T> query = db.Set<T>().AsNoTracking();

        if (typeof(T) == typeof(Crew))
        {
            query = query
                .Include("Machine")
                .Include("Driver");
        }

        return query.ToList();
    }

    public T? GetOneById(int id)
    {
        using AppDbContext db = _contextFactory.CreateDbContext();

        return db.Set<T>().Find(id);
    }

    public void Add(T entity)
    {
        using AppDbContext db = _contextFactory.CreateDbContext();

        db.Set<T>().Add(entity);
        db.SaveChanges();
    }

    public void Update(T entity)
    {
        using AppDbContext db = _contextFactory.CreateDbContext();

        Microsoft.EntityFrameworkCore.Metadata.IEntityType? entityType =
            db.Model.FindEntityType(typeof(T));

        Microsoft.EntityFrameworkCore.Metadata.IKey? primaryKey =
            entityType?.FindPrimaryKey();

        if (primaryKey == null)
        {
            throw new InvalidOperationException(
                $"Не найден первичный ключ сущности {typeof(T).Name}.");
        }

        System.Reflection.PropertyInfo? keyProperty =
            primaryKey.Properties[0].PropertyInfo;

        if (keyProperty == null)
        {
            throw new InvalidOperationException(
                "Не найдено свойство первичного ключа.");
        }

        object? keyValue = keyProperty.GetValue(entity);

        if (keyValue == null)
        {
            throw new InvalidOperationException(
                "Значение первичного ключа не задано.");
        }

        T? existingEntity = db.Set<T>().Find(keyValue);

        if (existingEntity == null)
        {
            db.Set<T>().Update(entity);
        }
        else
        {
            db.Entry(existingEntity).CurrentValues.SetValues(entity);

            if (entity is Crew updatedCrew &&
                existingEntity is Crew existingCrew)
            {
                db.Entry(existingCrew)
                    .Property(crew => crew.CreatedAt)
                    .IsModified = false;
            }
        }

        db.SaveChanges();
    }

    public void Delete(int id)
    {
        using AppDbContext db = _contextFactory.CreateDbContext();

        T? entity = db.Set<T>().Find(id);

        if (entity != null)
        {
            db.Set<T>().Remove(entity);
            db.SaveChanges();
        }
    }
}
