﻿using Eyon.Core.Data.Repository.IRepository;
using Eyon.Models;
using Eyon.Models.Errors;
using Eyon.Models.Relationship;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Eyon.Core.Data.Repository
{
    public class RecipeRepository : PrivacyRepository<Recipe, ApplicationUserRecipe>, IRecipeRepository
    {
        private readonly ApplicationDbContext _db;

        public RecipeRepository( ApplicationDbContext db ) : base(db)
        {
            this._db = db;
        }

        public override void Add( Recipe entity )
        {
            entity.CreationDateTime = DateTime.Now.ToUniversalTime();
            entity.ModifiedDateTime = entity.CreationDateTime;
            base.Add(entity);
        }
        public void UpdateIfOwner( string currentUserId, Recipe recipe )
        {
            var objFromDb = ( from r in _db.Recipe
                              join a in _db.ApplicationUserRecipe on r.Id equals a.ObjectId
                              where a.ApplicationUserId.Equals(currentUserId) && r.Id == recipe.Id
                              select r ).FirstOrDefault();

            if ( objFromDb == null )
                throw new SafeException("An error ocurred.", new Exception(string.Format("Ownership relationship not found on record. currentUserId {0},  recipe.Id {1}", currentUserId, recipe.Id)));

            objFromDb.Name = recipe.Name;
            objFromDb.Cooktime = recipe.Cooktime;
            objFromDb.Servings = recipe.Servings;
            objFromDb.Description = recipe.Description;
            objFromDb.PrepTime = recipe.PrepTime;
            objFromDb.Privacy = recipe.Privacy;
            objFromDb.ModifiedDateTime = DateTime.Now.ToUniversalTime();
            recipe.ModifiedDateTime = objFromDb.ModifiedDateTime;
            dbSet.Update(objFromDb);
        }
    }
}
