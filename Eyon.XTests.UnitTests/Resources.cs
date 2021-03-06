﻿using System;
using System.Collections.Generic;
using System.Text;
using Eyon.Core.Data;
using Eyon.Core.Data.Repository;
using Eyon.Core.Data.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Eyon.XTests.UnitTests
{
    public class Resources
    {
        //https://www.carlrippon.com/testing-ef-core-repositories-with-xunit-and-an-in-memory-db/

        /// <summary>
        /// Creates a new Instance of an in memory database
        /// </summary>
        /// <param name="inMemoryDatabaseName">The In Memory database name</param>
        /// <returns>The UnitOfWork</returns>
        public IUnitOfWork GetInMemoryUnitOfWork(string inMemoryDatabaseName)
        {
            DbContextOptions<Eyon.Core.Data.ApplicationDbContext> options;
            var builder = new DbContextOptionsBuilder<Eyon.Core.Data.ApplicationDbContext>();
            builder.UseInMemoryDatabase(inMemoryDatabaseName);
            options = builder.Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            dbContext.Database.EnsureCreated();
            dbContext.Database.EnsureDeleted();
            return new UnitOfWork(dbContext);
        }

    }
}
