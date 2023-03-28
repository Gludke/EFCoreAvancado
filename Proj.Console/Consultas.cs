using Proj.Console.Data;
using Proj.Console.Utils;
using System.Diagnostics;

namespace Proj.Console
{
    public class Consultas
    {
        private static JsonHelper JsonHelper => new();

        public static void FiltroGlobal(ApplicationContext db)
        {
            CarregamentoDeDados.CriarSetupDb(db);

            var depsDb = db.Departamentos.ToList();

            foreach (var dep in depsDb)
            {
                System.Console.WriteLine(JsonHelper.ObjectToJson(dep));
            }
        }
    }
}
