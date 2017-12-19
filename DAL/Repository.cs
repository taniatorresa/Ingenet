using Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Repository<TEntity> :
     IRepository<TEntity> where TEntity : class
    {
        IngenetEntities Context = null;
        public Repository()
        {
            Context = new IngenetEntities();
            Context.Configuration.ProxyCreationEnabled = false;

        }
        private DbSet<TEntity> EntitySet
        {
            get
            {
                return Context.Set<TEntity>();
            }
        }
        public TEntity Create(TEntity toCreate)
        {
            TEntity Result = null;
            try
            {
                EntitySet.Add(toCreate);
                Context.SaveChanges();
                Result = toCreate;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return Result;
        }
        public bool Delete(TEntity toDelete)
        {
            bool Result = false;
            try
            {
                EntitySet.Attach(toDelete);
                EntitySet.Remove(toDelete);
                Result = Context.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return Result;
        }
        public bool Update(TEntity toUpdate)
        {
            bool Result = false;
            try
            {
                EntitySet.Attach(toUpdate);
                Context.Entry<TEntity>(toUpdate).State = EntityState.Modified;
                Result = Context.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return Result;
        }
        public TEntity Retrieve(Expression<Func<TEntity, bool>> criteria)
        {
            TEntity Result = null;
            try
            {
                Result = EntitySet.FirstOrDefault(criteria);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return Result;
        }

        public List<TEntity> RetrieveAllOrder(Expression<Func<TEntity, string>> criteria)
        {
            List<TEntity> Result = null;
            try
            {
                Result = EntitySet.OrderBy(criteria).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return Result;
        }

        public List<TEntity> Filter(Expression<Func<TEntity, bool>> criteria)
        {
            List<TEntity> Result = null;
            try
            {
                Result = EntitySet.Where(criteria).ToList();
            }
            catch
            {

            }
            return Result;
        }

        public void Dispose()
        {
            if (Context != null)
            {
                Context.Dispose();
            }
        }
    }

}