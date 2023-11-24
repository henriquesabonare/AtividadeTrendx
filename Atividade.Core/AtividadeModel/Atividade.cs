namespace Atividade.Core.AtividadeModel
{
    public class Atividade: Entity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public bool Completed { get; set; }
        public DateTime InsertionDate { get; set; }
    }
}
