using Microsoft.EntityFrameworkCore;
using Proj.Console.Data;

namespace Proj.Console
{
    public class EstadoConexao
    {
        /// <summary>
        /// Compara o tempo de consulta ao BD
        /// </summary>
        public static string GerenciarEstadoDaConexao(bool abrirConexaoManual = false)
        {
            using var db = new ApplicationContext();

            if (abrirConexaoManual)
            {
                var conexao = db.Database.GetDbConnection();
                conexao.Open();
            }

            //inicia uma contagem
            var time = System.Diagnostics.Stopwatch.StartNew();

            for (int i = 0; i < 200; i++)
            {
                db.Departamentos.AsNoTracking().Any();
            }

            time.Stop();

            if(abrirConexaoManual)
                return $"Tempo decorrido: {time.Elapsed} - conexão aberta manualmente";

            return $"Tempo decorrido: {time.Elapsed} - EF Core abrindo e fechado conexão";
        }
    }
}
