using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using Task_Management.models;


namespace Task_Management.data
{
    internal class Context:DbContext
    {
        public DbSet<Tasks> tasks {  get; set; }
        public DbSet<Task_User> task_user { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=BEBOO\\MSSQLSERVER01;Initial Catalog=Management;Integrated Security=True;Trust Server Certificate=True");
        }

    }
}
