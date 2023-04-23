using DrShop2City.DataLayer.Context;
using DrShop2City.DataLayer.Entities.Common;
using Microsoft.EntityFrameworkCore;

namespace DrShop2City.DataLayer.Repository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        #region constructor

        private readonly DrShop2CityDBContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public GenericRepository(DrShop2CityDBContext context)
        {
            _context = context;
           _dbSet =_context.Set<TEntity>();
        }

        #endregion


        public IQueryable<TEntity?> GetEntitiesQuery()
        {
            return _dbSet.AsQueryable();
        }

        public async Task<TEntity?> GetEntityById(long entityId)
        {
            return await _dbSet.SingleOrDefaultAsync(e => e.Id == entityId);
        }

        public async Task AddEntity(TEntity? entity)
        {
            if (entity != null)
            {
                entity.CreateDate = DateTime.Now;
                entity.LastUpdateDate = entity.CreateDate;
                await _dbSet.AddAsync(entity);
            }
        }

        public void UpdateEntity(TEntity? entity)
        {
            if (entity == null) return;
            entity.LastUpdateDate = DateTime.Now;
            _dbSet.Update(entity);
        }

        public void RemoveEntity(TEntity? entity)
        {
            if (entity == null) return;
            entity.IsDelete = true;
            UpdateEntity(entity);
        }

        public async Task RemoveEntity(long entityId)
        {
            var entity = await GetEntityById(entityId);
            RemoveEntity(entity);
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
