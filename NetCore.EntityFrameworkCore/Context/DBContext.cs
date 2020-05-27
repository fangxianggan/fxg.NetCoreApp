using Microsoft.EntityFrameworkCore;
using NetCore.EntityFrameworkCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCore.EntityFrameworkCore.Context
{
    /// <summary>
    /// 
    /// </summary>
    public class DBContext : DbContext
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {

        }
      

        /// <summary>
        /// 
        /// </summary>
        public DbSet<TaskJob>  TaskJobs  { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DbSet<User> Users { get; set; }
    }
    
}
