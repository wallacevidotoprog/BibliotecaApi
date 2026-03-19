namespace BibliotecaApi.Domain.Entities
{
    public abstract class DefaultEntity
    {
            public int Id { get; set; }
            public DateTime DataCriacao { get; set; }
            public DateTime? DataAtualizacao { get; set; }
    }
}
