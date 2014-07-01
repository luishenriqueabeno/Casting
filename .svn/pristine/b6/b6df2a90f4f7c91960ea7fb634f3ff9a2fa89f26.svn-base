using System.Linq;
using SharpArch.Domain.PersistenceSupport;
using SharpArch.NHibernate;
using Casting.Domain;
using Casting.Domain.Contracts.Tasks;

namespace Casting.Infrastructure
{
    public class Usuarios : NHibernateRepository<Usuario>, IUsuarios
    {
        private readonly IRepository<Usuario> _usuario;

        public Usuarios(IRepository<Usuario> usuario)
        {
            _usuario = usuario;
        }

        public Usuario ObterPorLogin(string cpf, string senha)
        {
            var user = Session.QueryOver<Usuario>()
                .Where(x => x.Cpf == cpf && x.Senha == senha)
                .SingleOrDefault();

            return user;
        }

        public Usuario ObterPorId(int id)
        {
            var user = Session.QueryOver<Usuario>()
                .Where(x => x.Id == id)
                .SingleOrDefault();

            return user;
        }

        public bool CpfExiste(string cpf)
        {
            var user = Session.QueryOver<Usuario>()
                .Where(x => x.Cpf == cpf)
                .List();

            return user.Any();
        }

        public void SaveUsuario(Usuario usuario)
        {
            _usuario.SaveOrUpdate(usuario);
            _usuario.DbContext.CommitChanges();
        }
    }
}
