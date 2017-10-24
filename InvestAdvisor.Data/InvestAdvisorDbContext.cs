using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Validation;
using System.Text;
using InvestAdvisor.Model;

namespace InvestAdvisor.Data
{
    using System.Data.Entity;

    public class InvestAdvisorDbContext : DbContext
    {
        public InvestAdvisorDbContext()
            : base("name=InvestAdvisorDb")
        {
        }

        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<Image> Images { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        public override int SaveChanges()
        {
            try
            {
                return base.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                var sb = new StringBuilder();

                foreach (var failure in ex.EntityValidationErrors)
                {
                    sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
                    foreach (var error in failure.ValidationErrors)
                    {
                        sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
                        sb.AppendLine();
                    }
                }

                throw new DbEntityValidationException(
                    "Entity Validation Failed - errors follow:\n" +
                    sb, ex
                );
            }
        }
    }
}