using BibliotecaApi.Domain.Enums;
using BibliotecaApi.Domain.ValueObjects;

namespace BibliotecaApi.Domain.Entities
{
    public class UsuariosEntity : DefaultEntity
    {
        public string Nome { get; private set; }
        public CPF CPF { get; private set; }
        public Email Email { get; private set; }
        public NivelAcesso NivelAcesso { get; private set; }
        public string SenhaHash { get; private set; }

        public bool PossuiAtrasoAtivo { get; private set; } = false;

        public bool Ativo { get; private set; }


        public UsuariosEntity() { }

        public void Cadastrar(string nome, string cpf, string email, string senha, NivelAcesso nivelAcesso = NivelAcesso.User)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new ArgumentException("Nome não pode ser vazio.");

            if (string.IsNullOrWhiteSpace(senha))
                throw new ArgumentException("Senha não pode ser vazia.");

            
            Nome = nome;
            CPF = CPF.Criar(cpf);
            Email = Email.Criar(email);
            SenhaHash = senha; // fazer hash da senha aqui
            NivelAcesso = NivelAcesso.User;
            Ativo = true;
        }

        public void Ativar()
        {
            Ativo = true;
        }

        public void Desativar()
        {
            Ativo = false;
        }

        public void AtualizarAtraso(bool possuiAtraso)
        {
            PossuiAtrasoAtivo = possuiAtraso;
        }
    }
}
