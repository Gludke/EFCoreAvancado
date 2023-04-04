using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Proj.Console.Domain.Models;

namespace Proj.Console.Data
{
    public class ApplicationContext : DbContext
    {
        //Gera o log das operações no DB. Necessário o package 'Microsoft.Extensions.Logging.Console'
        //Adiciona o log no console da app
        private static readonly ILoggerFactory _logger = LoggerFactory.Create(l => l.AddConsole());

        public DbSet<Departamento> Departamentos { get; set; }
        public DbSet<Funcionario> Funcionarios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //Pooling=True -> permite que o programa reutilize conexões de banco de dados existentes em vez de abrir novas conexões toda vez que uma consulta é executada.
            const string connectionString = "Server=localhost\\SQLEXPRESS;Database=DbEFCoreAdvanced;Trusted_Connection=True;Pooling=True;";

            optionsBuilder
                .UseSqlServer(connectionString)
                .EnableSensitiveDataLogging()
                .UseLoggerFactory(_logger);

            //SplitQuery global no sistema
            //.UseSqlServer(connectionString, a => a.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery))

            //necessário o package 'Microsoft.EntityFrameworkCore.Proxies'
            //.UseLazyLoadingProxies()
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Para SQL Server - Configurando as tabelas do BD para não serem 'Case Sensitive (CI)' e também não
            //sensíveis a acentos (AI)
            modelBuilder.UseCollation("SQL_Latin1_General_CP1_CI_AI");

            //Configurando essa coluna específica para ser 'Case Sensitive' (CS) e sensível a acentos (AS)
            modelBuilder.Entity<Departamento>().Property(d => d.Descricao).UseCollation("SQL_Latin1_General_CP1_CS_AS");

            //Aplica um filtro global que sempre traz apenas os Departamento que não foram excluídos
            //modelBuilder.Entity<Departamento>().HasQueryFilter(d => !d.Excluido);
        }


    }
}
