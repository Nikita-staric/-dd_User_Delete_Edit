using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace си_шарп_плюс_база_данных.Models
{
    public class ApplicationContext:DbContext
    {
        public DbSet<User> Users { get; set; }//DbSet представляет собой коллекцию объектов, которая сопоставляется с определенной таблицей в базе данных
        public ApplicationContext(DbContextOptions<ApplicationContext> options)//Через параметр options в конструктор контекста данных будут передаваться настройки контекста.
            : base(options)
        {
          //  Database.EnsureDeleted();   // удаляем бд со старой схемой
            Database.EnsureCreated();   // создаем бд с новой схемой
         //   Database.EnsureCreated();   // создаем базу данных при первом обращении
        }
    }
}
