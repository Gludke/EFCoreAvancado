using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Proj.Console.Domain.Models;

namespace Proj.Console.Data
{
    public class AplicationContext : DbContext
    {
        //Gera o log das operações no DB. Necessário o package 'Microsoft.Extensions.Logging.Console'
        //Adiciona o log no console da app
        private static readonly ILoggerFactory _logger = LoggerFactory.Create(l => l.AddConsole());

        public DbSet<Departamento> Departamentos { get; set; }
        public DbSet<Funcionario> Funcionarios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            const string connectionString = "Server=localhost\\SQLEXPRESS;Database=DbEFCoreAdvanced;Trusted_Connection=True;";

            optionsBuilder
                .UseSqlServer(connectionString)
                .EnableSensitiveDataLogging()
                //Registra o log das operaçoes do EFCore no nosso logger
                .UseLoggerFactory(_logger);
        }
    }
}
