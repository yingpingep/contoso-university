using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace contoso_university.Models
{
    public partial class ContosoUniversityContext : DbContext
    {
       public override int SaveChanges()  
        {  
            var selectedEntityList = ChangeTracker.Entries()  
                                    .Where(v => hasDateModified(v.Entity) && (v.State == EntityState.Added || v.State == EntityState.Modified));

            foreach (var item in selectedEntityList)
            {
                ((IDateModified)item.Entity).DateModified = DateTime.UtcNow;
            }

            return base.SaveChanges();  
        } 

        private bool hasDateModified(Object obj) {
            return obj is Course || obj is Person || obj is Department;
        }
    }
}
