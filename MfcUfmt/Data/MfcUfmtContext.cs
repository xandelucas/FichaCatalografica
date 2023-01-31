using Microsoft.EntityFrameworkCore;

namespace MfcUfmt.Data
{
    public class MfcUfmtContext : DbContext
    {
        public MfcUfmtContext (DbContextOptions<MfcUfmtContext> options)
            : base(options)
        {
        }

        public DbSet<MfcUfmt.Models.Curso> Curso { get; set; }

        public DbSet<MfcUfmt.Models.Trabalho> Trabalho { get; set; }
    }
}
