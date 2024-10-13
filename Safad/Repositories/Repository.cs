﻿using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Safad.Interfaces;
using Safad.Data;

namespace Safad.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly SafadDBContext _context;

        public Repository(SafadDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Método genérico para obtener todos los registros de una tabla en particular en la base de datos
        /// </summary>
        /// <returns>Lista con la información encontrada</returns>
        public async Task<IEnumerable<T>> GetAll()
        {
            DetachAllEntities();
            return await _context.Set<T>().ToListAsync();
        }

        /// <summary>
        /// Método para limpiar contexto y se tomen los cambios en la base de datos
        /// </summary>
        public void DetachAllEntities()
        {
            _context.ChangeTracker.Clear();
        }

        // <summary>
        /// Método genérico para obtener por id un registro en particular de la base de datos
        /// </summary>
        /// <param name="id">Identificador del registro a obtener</param>
        /// <returns>La información del registro solicitado</returns>
        public async Task<T> GetById(int id)
        {
            var response = await _context.Set<T>().FindAsync(id);
            return response;
        }

        /// <summary>
        /// Método genérico para agregar un registro a la base de datos
        /// </summary>
        /// <param name="entity">Entidad con los datos a registrar</param>
        /// <returns></returns>
        public async Task Add(T entity)
        {
            _context.Set<T>().Add(entity);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Método genérico para actualizar los datos de un registro en la base de datos
        /// </summary>
        /// <param name="entity">Información a actualizar en la base de datos</param>
        /// <returns></returns>
        public async Task Update(T entity)
        {
            DetachAllEntities();
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Método génerico para eliminar un registro de la base de datos
        /// </summary>
        /// <param name="entity">Entidad con la información a eliminar</param>
        /// <returns></returns>
        public async Task Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Método genérico para obtener el último registro en la base de datos.
        /// </summary>
        /// <param name="entity">Entidad que se va a consultar.</param>
        /// <returns>Retorna el último registro de la entidad T. Si no se encuentra ningún registro, retornará null.</returns>
        public async Task<T> GetSequence(T entity)
        {
            DetachAllEntities();
            var data = await _context.Set<T>().ToListAsync();
            var lastId = data.LastOrDefault();
            return lastId;
        }
    }
}
