using BibliotecaApi.Domain.Entities;

namespace BibliotecaApi.Domain.Services
{
    public class AtrasoService
    {
        public void AtualizarUsuario(UsuariosEntity usuario, List<EmprestimoEntity> emprestimos)
        {
            var possuiAtraso = emprestimos.Any(e => e.EstaAtrasado());

            usuario.AtualizarAtraso(possuiAtraso);
        }
    }
}
