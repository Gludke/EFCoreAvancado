using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Proj.Console.Data;
using Proj.Console.Domain.Models;
using Proj.Console.Utils;

CarregamentoAdiantado();



//Cria/Apaga o DB de forma agilizada, sem migrations, de acordo com os Modelos
static void EnsureCreatedAndDeleted()
{
    using var db = new ApplicationContext();
    //Cria o DB se ele não existir
    db.Database.EnsureCreated();
    //Apaga todo o DB se ele existir
    //db.Database.EnsureDeleted();
}

//Como criar as tabelas para mais de 2 contextos para o mesmo DB
static void GapDoEnsureCreatedAndDeleted()
{
    //Em caso de 2+ contextos no mesmo DB, deve-se forçar a criação dos outros

    using var db1 = new ApplicationContext();
    using var db2 = new OutroApplicationContext();

    db1.Database.EnsureCreated();

    //Forçando a criação das tabelas do 2° contexto    
    var dbCreatorOfdb2 = db2.GetService<IRelationalDatabaseCreator>();

    dbCreatorOfdb2.CreateTables();
}

//Add dados ao DB
static void CriarSetupDb(ApplicationContext db)
{
    if (!db.Departamentos.Any())
    {
        db.Departamentos.AddRange(
            new Departamento
            {
                Descricao = "Departamento 01",
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

//Traz todas as infos de uma só vez do BD
static void CarregamentoAdiantado()
{
    using var db = new ApplicationContext();
    CriarSetupDb(db);

    var departamentos = db.Departamentos
        .Include(p => p.Funcionarios)
        .ToList();

    foreach (var departamento in departamentos)
    {
        Console.WriteLine("---------------------------------------");
        Console.WriteLine($"Departamento: {departamento.Descricao}");

        if (departamento.Funcionarios?.Any() ?? false)
        {
            foreach (var funcionario in departamento.Funcionarios)
            {
                Console.WriteLine($"\t{JsonHelper.ObjectToJson(funcionario)}");
            }
        }
        else
        {
            Console.WriteLine($"\tNenhum funcionario encontrado!");
        }
    }
}

