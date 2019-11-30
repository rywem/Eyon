using Eyon.DataAccess.Data.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eyon.DataAccess.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;
        public ICategoryRepository Category { get; private set; }
        public ISiteImageRepository SiteImage { get; private set; }
        public UnitOfWork(ApplicationDbContext db)
        {
            this._db = db;
            this.Category = new CategoryRepository(this._db);
            this.SiteImage = new SiteImageRepository(this._db);
        }
        public bool SaveTransaction()
        {
            bool returnValue = true;
            using (var dbContextTransaction = _db.Database.BeginTransaction())
            {
                try
                {
                    _db.SaveChanges();
                    dbContextTransaction.Commit();
                }
                catch (Exception)
                {
                    //Log Exception Handling message                      
                    returnValue = false;
                    dbContextTransaction.Rollback();
                }
            }
            return returnValue;
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public void Dispose()
        {
            this._db.Dispose();
        }
    }
    }