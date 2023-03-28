using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Proj.Console.Data;
using Proj.Console.Domain.Models;
using Proj.Console.Utils;

namespace Proj.Console
{
    public class CarregamentoDeDados
    {
        private static JsonHelper JsonHelper => new();


        //Add dados ao DB
        public static void CriarSetupDb(ApplicationContext db)
        {
            //Cria o DB se não existir
            if (db.Database.EnsureCreated())
            {
                if (!db.Departamentos.Any())
                {
                    db.Departamentos.AddRange(
                        new Departamento
                        {
                            Descricao = "Departamento 01",
                            Ativo = true,
                            Excluido = true,
                            Funcionarios = new List<Funcionario>
                            {
                                new Funcionario
                                {
                                    Nome = "Rafael Almeida",
                                    CPF = "99999999911",
                                    RG= "2100062"
                                },
                                new Funcionario
                                {
                                    Nome = "Pamela Pires",
                                    CPF = "123235342",
                                    RG= "125323"
                                }
                            }
                        },
                        new Departamento
                        {
                            Descricao = "Departamento 02",
                            Ativo = true,
                            Excluido = false,
                            Funcionarios = new List<Funcionario>
                            {
                                new Funcionario
                                {
                                    Nome = "Bruno Brito",
                                    CPF = "88888888811",
                                    RG= "3100062"
                                },
                                new Funcionario
                                {
                                    Nome = "Eduardo Pires",
                                    CPF = "77777777711",
                                    RG= "1100062"
                                },
                                new Funcionario
                                {
                                    Nome = "Antoni Doiso",
                                    CPF = "2828282888",
                                    RG= "98737373"
                                }
                            }
                        });

                    db.SaveChanges();
                    db.ChangeTracker.Clear();
                }
            }
        }

        //Cria/Apaga o DB de forma agilizada, sem migrations, de acordo com os Modelos
        public static void EnsureCreatedAndDeleted()
        {
            using var db = new ApplicationContext();
            //Apaga todo o DB se ele existir
            //db.Database.EnsureDeleted();
            //Cria o DB se ele não existir
            db.Database.EnsureCreated();

        }

        //Como criar as tabelas para mais de 2 contextos para o mesmo DB
        public static void GapDoEnsureCreatedAndDeleted()
        {
            //Em caso de 2+ contextos no mesmo DB, deve-se forçar a criação dos outros

            using var db1 = new ApplicationContext();
            using var db2 = new OutroApplicationContext();

            db1.Database.EnsureCreated();

            //Forçando a criação das tabelas do 2° contexto    
            var dbCreatorOfdb2 = db2.GetService<IRelationalDatabaseCreator>();

            dbCreatorOfdb2.CreateTables();
        }

        //Traz todas as infos de uma só vez do BD
        public static void CarregamentoAdiantado()
        {
            using var db = new ApplicationContext();
            CriarSetupDb(db);

            var departamentos = db.Departamentos
                .Include(p => p.Funcionarios)
                .ToList();

            foreach (var departamento in departamentos)
            {
                System.Console.WriteLine("---------------------------------------");
                System.Console.WriteLine($"Departamento: {departamento.Descricao}");

                if (departamento.Funcionarios?.Any() ?? false)
                {
                    foreach (var funcionario in departamento.Funcionarios)
                    {
                        System.Console.WriteLine($"\t{JsonHelper.ObjectToJson(funcionario)}");
                    }
                }
                else
                {
                    System.Console.WriteLine($"\tNenhum funcionario encontrado!");
                }
            }
        }

        //Traz as infos sob demanda
        public static void CarregamentoExplicito()
        {
            using var db = new ApplicationContext();
            CriarSetupDb(db);

            var departamentos = db.Departamentos
                //.Include(p => p.Funcionarios)
                .ToList();

            foreach (var departamento in departamentos)
            {

                if (departamento.Id == 2)
                {
                    //Realiza o include apenas sob demanda e apenas para as entidades desejadas
                    //Não permite filtros
                    //db.Entry(departamento).Collection(d => d.Funcionarios).Load();
                    db.Entry(departamento).Collection(d => d.Funcionarios).Query().Where(f => f.CPF.Contains("8")).ToList();
                }

                System.Console.WriteLine("---------------------------------------");
                System.Console.WriteLine($"Departamento: {departamento.Descricao}");

                if (departamento.Funcionarios?.Any() ?? false)
                {
                    foreach (var funcionario in departamento.Funcionarios)
                    {
                        System.Console.WriteLine($"\t{JsonHelper.ObjectToJson(funcionario)}");
                    }
                }
                else
                {
                    System.Console.WriteLine($"\tNenhum funcionario encontrado!");
                }
            }
        }

        ///Traz as infos sob somente se solicitado pelo código.
        ///Não recomendado o uso, pois abre e fecha a conexão para cada loop.
        ///Há outras 2 formas de usar. Checar o curso 'Dominando o EF Core'.
        public static void CarregamentoLento()
        {
            using var db = new ApplicationContext();
            CriarSetupDb(db);

            //Desativa manualmento o carregamento lento
            //db.ChangeTracker.LazyLoadingEnabled = false;

            var departamentos = db.Departamentos
                .ToList();

            foreach (var departamento in departamentos)
            {
                System.Console.WriteLine("---------------------------------------");
                System.Console.WriteLine($"Departamento: {departamento.Descricao}");

                //Nesse momento é que ocorrerá o 'inner join' em Funcionário, pois a prop de navegação está sendo acessada.
                if (departamento.Funcionarios?.Any() ?? false)
                {
                    foreach (var funcionario in departamento.Funcionarios)
                    {
                        System.Console.WriteLine($"\t{JsonHelper.ObjectToJson(funcionario)}");
                    }
                }
                else
                {
                    System.Console.WriteLine($"\tNenhum funcionario encontrado!");
                }
            }
        }


    }
}
