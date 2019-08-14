using Microsoft.EntityFrameworkCore;
using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace InsuranceTest
{
    
        public static class GetDbHelper
        {
        public const string DbConnectionString = "Server=YUXARM0018L;Database=Insurance;Integrated Security=False;User ID=earias;Password=Eja.30230267;";

            public static CpeDbContext GetDbContext()
            {
                var optionsBuilder = new DbContextOptionsBuilder<CpeDbContext>();
                optionsBuilder.UseSqlServer(DbConnectionString);

                var context = new CpeDbContext(optionsBuilder.Options);

                return context;
            }
        }
    
}
