using AplicacaoBanco.Models;

namespace AplicacaoBanco.Repository.Contract
{
    public interface IUsuarioRepository
    {
        //CRUD
        IEnumerable<Usuario> ObterTodosUsuarios();
        void Cadastrar(Usuario usuario);
        void Atualizar(Usuario usuario);
        Usuario ObterUsuario(int Id);
        void Excluir(int Id);
    }
}
