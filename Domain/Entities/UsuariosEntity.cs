using BibliotecaApi.Domain.Enums;
using BibliotecaApi.Domain.Exceptions;
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

        public void Cadastrar(string nome, string cpf, string email, string senhaHash, NivelAcesso nivelAcesso = NivelAcesso.Usuario)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new DomainException("Nome não pode ser vazio.");

            if (string.IsNullOrWhiteSpace(senhaHash))
                throw new DomainException("Senha não pode ser vazia.");

            
            Nome = nome;
            CPF = CPF.Criar(cpf);
            Email = Email.Criar(email);
            SenhaHash = senhaHash;
            NivelAcesso = nivelAcesso;
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
