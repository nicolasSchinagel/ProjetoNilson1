using AplicacaoBanco.Controllers;
using AplicacaoBanco.Models;
using AplicacaoBanco.Repository.Contract;
using MySql.Data.MySqlClient;

namespace AplicacaoBanco.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly string _conexaoMySQL;
        public UsuarioRepository(IConfiguration conf)
        {
            _conexaoMySQL = conf.GetConnectionString("ConexaoMySQL");
        }
        public void Atualizar(Usuario usuario)
        {
            throw new NotImplementedException();
        }

        public void Cadastrar(Usuario usuario)
        {
            using(var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("insert into usuario(nomeUsu, Cargo, DataNasc)" + "values (@nomeUsu, @Cargo, @DataNasc)", conexao);
                cmd.Parameters.Add("@nomeUsu", MySqlDbType.VarChar).Value = usuario.nomeUsu;
                cmd.Parameters.Add("@Cargo", MySqlDbType.VarChar).Value = usuario.Cargo;
                cmd.Parameters.Add("@DAtaNasc", MySqlDbType.VarChar).Value = usuario.DataNasc;
                cmd.ExecuteNonQuery();
                conexao.Close();
            }
        }

        public void Cadastrar(UsuarioController usuario)
        {
            throw new NotImplementedException();
        }

        public void Excluir(int Id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Usuario> ObterTodosUsuarios()
        {
            throw new NotImplementedException();
        }

        public Usuario ObterUsuario(int Id)
        {
            throw new NotImplementedException();
        }
    }
}
