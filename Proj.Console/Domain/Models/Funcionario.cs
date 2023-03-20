using Newtonsoft.Json;

namespace Proj.Console.Domain.Models
{
    public class Funcionario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string RG { get; set; }
        public int DepartamentoId { get; set; }
        [JsonIgnore]
        public Departamento Departamento { get; set; }
    }
}
