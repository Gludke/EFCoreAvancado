using Microsoft.EntityFrameworkCore;
using Proj.Console.Data;
using Proj.Console.Domain.Models;
using Proj.Console.Utils;
using System.Diagnostics;

namespace Proj.Console
{
    public class Consultas
    {
        private static JsonHelper JsonHelper = new();

        public static void FiltroGlobal(ApplicationContext db)
        {
            CarregamentoDeDados.CriarSetupDb(db);

            //Ignora os filtros globais
            var depsDbIgnorandoFiltroGlobal = db.Departamentos.IgnoreQueryFilters().ToList();
            //var depsDb = db.Departamentos.ToList();

            foreach (var dep in depsDbIgnorandoFiltroGlobal)
            {
                System.Console.WriteLine($"\n{JsonHelper.ObjectToJson(dep)}");
            }
        }

        /// <summary>
        /// Consiste em trazer apenas os dados necessário do DB, garantindo muito mais performance
        /// </summary>
        public static void ConsultaProjetada(ApplicationContext db)
        {
            CarregamentoDeDados.CriarSetupDb(db);

            //O left join ocorre automaticamente
            var depsDb = db.Departamentos
                .Select(d => new { d.Descricao, Funcionario = d.Funcionarios.Select(f => f.Nome) })
                .ToList();

            foreach (var dep in depsDb)
            {
                System.Console.WriteLine($"\n{JsonHelper.ObjectToJson(dep)}");
            }
        }

        /// <summary>
        /// Consiste em usar SQL puro nas consultas
        /// </summary>
        public static void ConsultaParametrizada(ApplicationContext db)
        {
            CarregamentoDeDados.CriarSetupDb(db);

            var id = 0;
            
            //Sql puro + consulta do EF Core
            var depsDb = db.Departamentos
                .FromSqlInterpolated($"select * from Departamentos where Id>{id}")
                .Select(d => new { 
                    d.Descricao, 
                    Funcionario = d.Funcionarios.Where(f => f.Id > 2).Select(f => f.Nome) 
                })
                .ToList();

            foreach (var dep in depsDb)
            {
                System.Console.WriteLine($"\n{JsonHelper.ObjectToJson(dep)}");
            }
        }

    }
}
