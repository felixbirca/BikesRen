using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BikesRent.DataAccessLayer.Entities;

namespace BikesRent.DataAccessLayer;

public interface IEntityRepository
{
    public Task<ICollection<T>> GetAll<T>() where T : BaseEntity;
    public Task<ICollection<T>> Where<T>(Expression<Func<T, bool>> expression) where T : BaseEntity;
    public Task Create<T>(T entity) where T : BaseEntity;
    public Task Update();
}