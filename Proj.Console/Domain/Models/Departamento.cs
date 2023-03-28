namespace Proj.Console.Domain.Models
{
    public class Departamento
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public bool Ativo { get; set; }
        public bool Excluido { get; set; }
        public ICollection<Funcionario> Funcionarios { get; set; }

        //'virtual' serve para que o carregamendo 'LazyLoad' funcione
        //public virtual ICollection<Funcionario> Funcionarios { get; set; }
    }
}
