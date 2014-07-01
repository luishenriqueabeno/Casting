using SharpArch.Domain.PersistenceSupport;

namespace Casting.Domain.Contracts.Tasks
{
    public interface IUsuarios : IRepository<Usuario>
    {
        Usuario ObterPorLogin(string usuario, string senha);
        Usuario ObterPorId(int id);
        bool CpfExiste(string cpf);
        void SaveUsuario(Usuario usuario);
    }
}