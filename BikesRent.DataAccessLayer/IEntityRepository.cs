using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BikesRent.DataAccessLayer.Entities;

namespace BikesRent.DataAccessLayer;

public interface IEntityRepository<T> where T: BaseEntity
{
    public Task<ICollection<T>> GetAll();
    public Task<ICollection<T>> Where(Expression<Func<T, bool>> expression);
    public Task Create(T entity);
    public Task Update();
}