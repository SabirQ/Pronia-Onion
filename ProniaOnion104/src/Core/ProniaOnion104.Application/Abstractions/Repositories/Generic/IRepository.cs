using ProniaOnion104.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ProniaOnion104.Application.Abstractions.Repositories
{
    public interface IRepository<T> where T : BaseEntity, new()
    {

        IQueryable<T> GetAll(bool isTracking = true,
           bool ignoreQuery = false,
           params string[] includes);
        IQueryable<T> GetAllWhere(
           Expression<Func<T, bool>>? expression = null,
           Expression<Func<T, object>>? orderExpression = null,
           bool isDescending = false,
           int skip = 0,
           int take = 0,
           bool isTracking = true,
           bool ignoreQuery = false,
           params string[] includes
           
           );

        Task<T> GetByIdAsync(int id, bool isTracking = true,bool ignoreQuery=false, params string[] includes);
        Task<T> GetByExpressionAsync(Expression<Func<T,bool>> expression, bool isTracking = true, bool ignoreQuery = false, params string[] includes);
        Task<bool> IsExistAsync(Expression<Func<T, bool>> expression, bool ignoreQuery = false);
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
        void SoftDelete(T entity);
        void ReverseSoftDelete(T entity);
        Task SaveChangesAsync();
    }
}
