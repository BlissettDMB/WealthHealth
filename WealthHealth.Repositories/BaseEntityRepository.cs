using System;
using System.Data;
using System.Data.Entity;
using System.Linq;

namespace WealthHealth.Repositories
{
    public class BaseEntityRepository<TC, T> : IBaseEntityRepository<T>
        where T : class
        where TC : DbContext, new()
    {
        private TC _dataContext;

        public virtual TC DataContext
        {
            get
            {
                if (_dataContext == null)
                {
                    _dataContext = new TC();
                    // By Default, Setup DataContext to use the set POCO types
                    // Instead of proxy types. Proxy types add overhead
                    // and disrupt serialization of entities
                    AllowSerialization = true;
                }
                return _dataContext;
            }
        }

        public virtual bool AllowSerialization
        {
            get
            {
                return _dataContext.Configuration.ProxyCreationEnabled;
            }

            set
            {
                // To disable creating proxy objects you have to set the value of the ProxyCreationEnabled property to false
                // There is some overhead involved in generating dynamic POCO proxy types. 
                // Initializing POCO types involves the least overhead compared to proxy and EntityObject derived types. 
                _dataContext.Configuration.ProxyCreationEnabled = !value;
            }
        }

        public void Dispose()
        {
            if (DataContext != null)
            {
                DataContext.Dispose();
            }
        }

        public T Find(int id)
        {
            return DataContext.Set<T>().Find(id);
        }

        protected IQueryable<T> Get(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            if (predicate == null)
            {
                throw new ArgumentException("Predicate expression must be provided");
            }

            return DataContext.Set<T>().Where(predicate);
        }

        protected IQueryable<T> GetOrderByAscending(System.Linq.Expressions.Expression<Func<T, string>> orderBy)
        {
            if (orderBy == null)
            {
                throw new ArgumentException("Predicate expression and Order By expression must be provided");
            }

            return DataContext.Set<T>().OrderBy(orderBy);
        }

        protected IQueryable<T> GetOrderByAscending(System.Linq.Expressions.Expression<Func<T, bool>> predicate, System.Linq.Expressions.Expression<Func<T, string>> orderBy)
        {
            if (predicate == null || orderBy == null)
            {
                throw new ArgumentException("Predicate expression and Order By expression must be provided");
            }

            return DataContext.Set<T>().Where(predicate).OrderBy(orderBy);
        }

        protected IQueryable<T> GetOrderByDescending(System.Linq.Expressions.Expression<Func<T, string>> orderBy)
        {
            if (orderBy == null)
            {
                throw new ArgumentException("Predicate expression and Order By expression must be provided");
            }

            return DataContext.Set<T>().OrderByDescending(orderBy);
        }

        protected IQueryable<T> GetOrderByDescending(System.Linq.Expressions.Expression<Func<T, bool>> predicate, System.Linq.Expressions.Expression<Func<T, string>> orderBy)
        {
            if (predicate == null || orderBy == null)
            {
                throw new ArgumentException("Predicate expression and Order By expression must be provided");
            }

            return DataContext.Set<T>().Where(predicate).OrderByDescending(orderBy);
        }

        protected IQueryable<T> GetQueryable()
        {
            return DataContext.Set<T>();
        }

        public DbOperationStatus ExecuteStoredProcedure(string command)
        {
            if (command == null)
            {
                throw new ArgumentException("The stored procedure must be provided");
            }

            var opStatus = new DbOperationStatus { OperationSuccessStatus = false };

            try
            {
                opStatus.OperationSuccessStatus = DataContext.Database.ExecuteSqlCommand(command) > 0;
            }
            catch (Exception ex)
            {
                opStatus = DbOperationStatus.CreateFromException("Error executing Stored Procedure", ex);
            }

            return opStatus;
        }

        protected DbOperationStatus Insert(T entity)
        {
            var opStatus = new DbOperationStatus
            {
                OperationSuccessStatus = false,
                AffectedIndices = null
            };

            try
            {
                DataContext.Set<T>().Add(entity);
                opStatus.OperationSuccessStatus = DataContext.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                opStatus = DbOperationStatus.CreateFromException("Error inserting " + entity.GetType(), ex);
            }
            return opStatus;
        }

        protected DbOperationStatus InsertEntity<TE>(TE entity) where TE : class
        {
            var opStatus = new DbOperationStatus
            {
                OperationSuccessStatus = false,
                AffectedIndices = null
            };

            try
            {
                DataContext.Set<TE>().Add(entity);
                opStatus.OperationSuccessStatus = DataContext.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                opStatus = DbOperationStatus.CreateFromException("Error inserting " + entity.GetType(), ex);
            }
            return opStatus;
        }

        protected DbOperationStatus Update(T entity)
        {
            var opStatus = new DbOperationStatus
            {
                OperationSuccessStatus = false,
                AffectedIndices = null
            };

            try
            {
                DataContext.Set<T>().Attach(entity);
                DataContext.Entry(entity).State = EntityState.Modified;
                opStatus.OperationSuccessStatus = DataContext.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                opStatus = DbOperationStatus.CreateFromException("Error updating " + entity.GetType(), ex);
            }
            return opStatus;
        }

        protected DbOperationStatus UpdateEntity<TE>(TE entity) where TE : class
        {
            var opStatus = new DbOperationStatus
            {
                OperationSuccessStatus = false,
                AffectedIndices = null
            };

            try
            {
                DataContext.Set<TE>().Attach(entity);
                DataContext.Entry(entity).State = EntityState.Modified;
                opStatus.OperationSuccessStatus = DataContext.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                opStatus = DbOperationStatus.CreateFromException("Error updating " + entity.GetType(), ex);
            }
            return opStatus;
        }

        protected DbOperationStatus Delete(T entity)
        {
            var opStatus = new DbOperationStatus
            {
                OperationSuccessStatus = false,
                AffectedIndices = null
            };

            try
            {
                DataContext.Set<T>().Remove(entity);
                opStatus.OperationSuccessStatus = DataContext.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                opStatus = DbOperationStatus.CreateFromException("Error deleting " + entity.GetType(), ex);
            }
            return opStatus;
        }

        protected DbOperationStatus DeleteEntity<TE>(TE entity) where TE : class
        {
            var opStatus = new DbOperationStatus
            {
                OperationSuccessStatus = false,
                AffectedIndices = null
            };

            try
            {
                DataContext.Set<TE>().Remove(entity);
                opStatus.OperationSuccessStatus = DataContext.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                opStatus = DbOperationStatus.CreateFromException("Error deleting " + entity.GetType(), ex);
            }
            return opStatus;
        }

        protected DbOperationStatus Save()
        {
            var opStatus = new DbOperationStatus
            {
                OperationSuccessStatus = false,
                AffectedIndices = null
            };

            try
            {
                opStatus.OperationSuccessStatus = DataContext.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                opStatus = DbOperationStatus.CreateFromException("Error saving.", ex);
            }
            return opStatus;
        }
    }
}