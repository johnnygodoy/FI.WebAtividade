using FI.AtividadeEntrevista.DML;
using System.Data.Entity;
using System.Data.SqlClient;

namespace FI.AtividadeEntrevista.DAL
{
    // Classe de contexto que representa o banco de dados
    public class AppDbContext : DbContext
    {
        // DbSet para a entidade Cliente
        public DbSet<Cliente> Clientes { get; set; }

        // Construtor que permite passar a string de conexão para o DbContext
        public AppDbContext() : base("BancoDeDados")
        {
        }

        // Método para configurar o modelo (opcional)
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Aqui você pode adicionar configurações específicas para suas entidades, como chaves primárias, índices, etc.
            modelBuilder.Entity<Cliente>().HasKey(c => c.Id);
            modelBuilder.Entity<Cliente>().Property(c => c.CPF).IsRequired();
            modelBuilder.Entity<Cliente>().Property(c => c.CPF).HasMaxLength(11); // Definindo o tamanho máximo do CPF
            // Adicione outras configurações conforme necessário
        }

    }
}
