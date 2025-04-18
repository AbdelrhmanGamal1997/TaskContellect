using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;

namespace CoreEntities
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbcontext>
    {
        public AppDbcontext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbcontext>();
            optionsBuilder.UseSqlServer("Server=.;Database=ContellectDataBase;User Id=sa;Password=1810;Encrypt=True;TrustServerCertificate=True;");

            return new AppDbcontext(optionsBuilder.Options);
        }
    }
}
