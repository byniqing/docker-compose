using Docker.Api.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Docker.Api.Data
{
    public class DbUserInfoContext : DbContext
    {
        public DbUserInfoContext(DbContextOptions<DbUserInfoContext> options) : base(options) { }

        public DbSet<UseInfo> userInfos { get; set; }

        /// <summary>
        /// 模型创建时触发
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /*
             修改表名和主键，user对应数据库的表，mysql默认是区分大小写的
            show variables like '%lower%';
            lower_case_table_names 为0 区分，1 不区分
             */
            modelBuilder.Entity<UseInfo>(b => b.ToTable("user").HasKey(u => u.id));

            //or
            //modelBuilder.Entity<user>()
            //    .ToTable("user")
            //    .HasKey(u => u.id);

            base.OnModelCreating(modelBuilder);
        }
    }
}
