namespace Proj.Console.Domain.Models
{
    public class Departamento
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public bool Ativo { get; set; }
        public ICollection<Funcionario> Funcionarios { get; set; }
    }
}
