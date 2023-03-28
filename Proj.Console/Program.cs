using Proj.Console;
using Proj.Console.Data;

using var db = new ApplicationContext();

CarregamentoDeDados.CriarSetupDb(db);

db.Dispose();





