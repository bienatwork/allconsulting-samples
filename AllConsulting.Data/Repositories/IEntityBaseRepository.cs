// Infrastructure
// *****************************************************************************************
//
// Name:		IEntityBaseRepository.cs
//
// Created:		28.01.2016 ACAG  
// Modified:	28.01.2016 ACAG  	: Creation 
//
// *****************************************************************************************
using System;
using System.Linq;
using System.Linq.Expressions;
using ACAG.Entities;

namespace ACAG.Data.Repositories
{
    /// <summary>
    /// Interface Entity Base Repository
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IEntityBaseRepository<T> where T : class, IEntityBase, new()
    {
        /// <summary>
        /// All Including
        /// </summary>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        IQueryable<T> AllIncluding(params Expression<Func<T, object>>[] includeProperties);
        /// <summary>
        /// All
        /// </summary>
        IQueryable<T> All { get; }
        /// <summary>
        /// Get All
        /// </summary>
        /// <returns></returns>
        IQueryable<T> GetAll();
        /// <summary>
        /// Get Single
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T GetSingle(int id);
        /// <summary>
        /// Find By
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        IQueryable<T> FindBy(Expression<Func<T, bool>> predicate);
        /// <summary>
        /// Add
        /// </summary>
        /// <param name="entity"></param>
        void Add(T entity);
        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="entity"></param>
        void Delete(T entity);
        /// <summary>
        /// Edit
        /// </summary>
        /// <param name="entity"></param>
        void Edit(T entity);
    }
}
