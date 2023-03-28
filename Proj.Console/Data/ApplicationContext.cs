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
                //necessário o package 'Microsoft.EntityFrameworkCore.Proxies'
                .UseLazyLoadingProxies()
                //Registra o log das operaçoes do EFCore no nosso logger
                .UseLoggerFactory(_logger);
        }
    }
}
