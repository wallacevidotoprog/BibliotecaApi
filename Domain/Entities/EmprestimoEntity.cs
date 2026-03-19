using BibliotecaApi.Domain.Exceptions;

namespace BibliotecaApi.Domain.Entities
{
    public class EmprestimoEntity : DefaultEntity
    {
        public int IdUsuario { get; private set; }
        public virtual UsuariosEntity? Usuario { get; private set; }
        public int IdLivro { get; private set; }
        public virtual LivroEntity? Livro { get; private set; }
        public DateTime DataEmprestimo { get; private set; }
        public DateTime DataPrevistaDevolucao { get; private set; }
        public DateTime? DataDevolucao { get; private set; }
        public Decimal Valor { get; private set; }
        public Decimal Multa { get; private set; }
        public Decimal Total { get; private set; }

        public EmprestimoEntity() { }

        public void Cadastrar(int idUsuario, int idLivro, DateTime dataPrevista)
        {
            if (idUsuario <= 0)
                throw new DomainException("Usuário inválido.");

            if (idLivro <= 0)
                throw new DomainException("Livro inválido.");

            if (dataPrevista <= DateTime.Now)
                throw new DomainException("A data prevista de devolução deve ser futura.");

            IdUsuario = idUsuario;
            IdLivro = idLivro;
            DataEmprestimo = DateTime.Now;
            DataPrevistaDevolucao = dataPrevista;
            DataDevolucao = null;
            Valor = 5.00m;
        }

        public void RegistrarDevolucao()
        {
            DataDevolucao = DateTime.Now;
            Multa = CalcularMulta();
            Total = Valor + Multa;
        }

        private decimal CalcularMulta()
        {
            if (DataDevolucao == null)
                throw new DomainException("Empréstimo ainda não devolvido.");

            TimeSpan atraso = DataDevolucao.Value.Date - DataPrevistaDevolucao.Date;
            int diasAtraso = atraso.Days;

            if (diasAtraso <= 0)
                return 0m;

            decimal valorMulta = 0m;

            if (diasAtraso <= 3)
            {
                valorMulta = diasAtraso * 2.00m;
            }
            else
            {
                valorMulta = (3 * 2.00m) + ((diasAtraso - 3) * 3.50m);
            }

            return Math.Min(valorMulta, 50.00m);
        }

        public bool EstaAtrasado()
        {
            return DataDevolucao == null
                && DataPrevistaDevolucao < DateTime.Now;
        }
    }
}
