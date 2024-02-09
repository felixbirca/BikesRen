using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BikesRent.DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace BikesRent.DataAccessLayer;

public class EntityRepository<T> : IEntityRepository<T> where T : BaseEntity
{
    private readonly BikesDbContext _dbContext;

    public EntityRepository(BikesDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ICollection<T>> GetAll()
    {
        return await _dbContext.Set<T>().ToListAsync();
    }

    public async Task<ICollection<T>> Where(Expression<Func<T, bool>> expression)
    {
        return await _dbContext.Set<T>().Where(expression).ToListAsync();
    }

    public async Task Create(T entity)
    {
        await _dbContext.Set<T>().AddAsync(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task Update()
    {
        await _dbContext.SaveChangesAsync();
    }
}