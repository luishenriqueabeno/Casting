using Casting.Domain;

namespace Casting.Domain.Contracts.Tasks
{
    public interface IServicoDeAutenticacao
    {
        bool Autenticar(string login, string senha);
        bool AutenticarPorId(int id);
        Usuario UsuarioAutenticado();
        void Sair();
        Usuario RecuperarSenha(string email);
        int CodigoDeUsuarioAutenticado();
    }
}