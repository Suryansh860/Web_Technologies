using Question_2.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Question_2.Models;

using System.Data.Entity;

namespace Question_2.Models.Repo
{
    public class MovieRepo
    {
        public class MoviesRepo<T> : IMovieRepo<T> where T : class
        {
            MoviesDbContext db;
            DbSet<T> dbset;

            public void MovieRepo()
            {
                db = new MoviesDbContext();
                dbset = db.Set<T>();
            }
            public void Delete(object Id)
            {
                T getmodel = dbset.Find(Id);
                dbset.Remove(getmodel);
            }

            public IEnumerable<T> GetAll()
            {
                return dbset.ToList();
            }

            public T GetById(object Id)
            {
                return dbset.Find(Id);
            }

            public void Insert(T obj)
            {
                dbset.Add(obj);
            }

            public void Save()
            {
                db.SaveChanges();
            }

            public void Update(T obj)
            {
                db.Entry(obj).State = EntityState.Modified;
            }
        }
    }
}