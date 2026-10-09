using System;
using System.Collections.Generic;
using CrispyApp.Domain.Entities;
using CrispyApp.Domain.Repositories;
using CrispyApp.Infrastructure.Data;
using LiteDB;

namespace CrispyApp.Infrastructure.Repositories;

public class Repository<T> : IRepository<T> where T : BaseEntity
{
    protected readonly ILiteCollection<T> _collection;

    public Repository(LiteDbContext dbContext)
    {
        // Use the pluralized name of the type for the collection name
        _collection = dbContext.Database.GetCollection<T>(typeof(T).Name + "s");
    }

    public virtual T? GetById(Guid id)
    {
        return _collection.FindById(id);
    }

    public virtual IEnumerable<T> GetAll()
    {
        return _collection.FindAll();
    }

    public virtual void Add(T entity)
    {
        _collection.Insert(entity);
    }

    public virtual void Update(T entity)
    {
        _collection.Update(entity);
    }

    public virtual void Delete(Guid id)
    {
        _collection.Delete(id);
    }
}
