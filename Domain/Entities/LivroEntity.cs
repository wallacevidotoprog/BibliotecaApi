using BibliotecaApi.Domain.ValueObjects;

namespace BibliotecaApi.Domain.Entities
{
    public class LivroEntity: DefaultEntity
    {
        public string Titulo { get; private set; }
        public string Autor { get; private set; }
        public ISBN ISBN { get; private set; }

        public LivroEntity() { }

        public void Cadastrar(int? id, string titulo, string autor, string isbn)
        {
            
            if (id != null)
            {
                if (string.IsNullOrEmpty(id.ToString()))
                    throw new ArgumentException("Id não pode ser vazio.");
                if (id <= 0)
                    throw new ArgumentException("Id deve ser maior que zero.");
                Id = id.GetValueOrDefault();
            }

            if (string.IsNullOrWhiteSpace(titulo))
                throw new ArgumentException("Título não pode ser vazio.");
            if (string.IsNullOrWhiteSpace(autor))
                throw new ArgumentException("Autor não pode ser vazio.");

            Titulo = titulo;
            Autor = autor;
            ISBN = ISBN.Criar(isbn);
        }
    }
}
