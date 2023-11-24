using System;

namespace Atividade.ViewModel
{
    public class AtividadeViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool Completed { get; set; }
        public DateTime InsertionDate { get; set; }
    }
}
