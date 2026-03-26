using AplicacaoBanco.Controllers;
using AplicacaoBanco.Models;
using AplicacaoBanco.Repository.Contract;
using MySql.Data.MySqlClient;
using System.Data;

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
            using(var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("call spAtualizarUsuario(@nomeUsu, @Cargo, @DataNasc, @CEP, @Estado, @Cidade, @Bairro, @Logradouro, @Complemento, @Numero, @IdUsu)", conexao);

                cmd.Parameters.Add("@nomeUsu", MySqlDbType.VarChar).Value = usuario.nomeUsu;
                cmd.Parameters.Add("@Cargo", MySqlDbType.VarChar).Value = usuario.Cargo;
                cmd.Parameters.Add("@DataNasc", MySqlDbType.VarChar).Value = usuario.DataNasc.ToString("yyyy/MM/dd");
                cmd.Parameters.Add("@IdUsu", MySqlDbType.VarChar).Value = usuario.IdUsu;
                cmd.Parameters.Add("@CEP", MySqlDbType.VarChar).Value = usuario.CEP;
                cmd.Parameters.Add("@Estado", MySqlDbType.VarChar).Value = usuario.Estado;
                cmd.Parameters.Add("@Cidade", MySqlDbType.VarChar).Value = usuario.Cidade;
                cmd.Parameters.Add("@Bairro", MySqlDbType.VarChar).Value = usuario.Bairro;
                cmd.Parameters.Add("@Logradouro", MySqlDbType.VarChar).Value = usuario.Logradouro;
                cmd.Parameters.Add("@Complemento", MySqlDbType.VarChar).Value = usuario.Complemento;
                cmd.Parameters.Add("@Numero", MySqlDbType.Int32).Value = usuario.Numero;
                cmd.ExecuteNonQuery();
                conexao.Close();
            }
        }

        public void Cadastrar(Usuario usuario)
        {
            using(var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("call spCriarUsuario(@nomeUsu, @Cargo, @DataNasc, @CEP, @Estado, @Cidade, @Bairro, @Logradouro, @Complemento, @Numero)", conexao);
                cmd.Parameters.Add("@nomeUsu", MySqlDbType.VarChar).Value = usuario.nomeUsu;
                cmd.Parameters.Add("@Cargo", MySqlDbType.VarChar).Value = usuario.Cargo;
                cmd.Parameters.Add("@DataNasc", MySqlDbType.VarChar).Value = usuario.DataNasc.ToString("yyyy/MM/dd");
                cmd.Parameters.Add("@CEP", MySqlDbType.VarChar).Value = usuario.CEP;
                cmd.Parameters.Add("@Estado", MySqlDbType.VarChar).Value = usuario.Estado;
                cmd.Parameters.Add("@Cidade", MySqlDbType.VarChar).Value = usuario.Cidade;
                cmd.Parameters.Add("@Bairro", MySqlDbType.VarChar).Value = usuario.Bairro;
                cmd.Parameters.Add("@Logradouro", MySqlDbType.VarChar).Value = usuario.Logradouro;
                cmd.Parameters.Add("@Complemento", MySqlDbType.VarChar).Value = usuario.Complemento;
                cmd.Parameters.Add("@Numero", MySqlDbType.Int32).Value = usuario.Numero;
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
            using(var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("call spDeletarUsuario(@IdUsu)", conexao);
                cmd.Parameters.AddWithValue("@IdUsu", Id);
                int i = cmd.ExecuteNonQuery();
                conexao.Close();
            }
        }

        public IEnumerable<Usuario> ObterTodosUsuarios()
        {
            List<Usuario> UsuarioList = new List<Usuario>();
            using(var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("Select * from ViewUsu", conexao);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                conexao.Clone();

                foreach(DataRow dr in dt.Rows)
                {
                    UsuarioList.Add(
                        new Usuario
                        {
                            IdUsu = Convert.ToInt32(dr["Id"]),
                            nomeUsu = (string)dr["Nome"],
                            Cargo = (string)dr["Cargo"],
                            DataNasc = Convert.ToDateTime(dr["Data de Nascimento"]),
                            CEP = (string)(dr["CEP"]),
                            Estado = (string)(dr["Estado"]),
                            Cidade = (string)(dr["Cidade"]),
                            Bairro = (string)(dr["Bairro"]),
                            Logradouro = (string)(dr["Logradouro"]),
                            Complemento = Convert.ToString(dr["Complemento"]),
                            Numero = Convert.ToInt32(dr["Numero"])
                        });
                }
                return UsuarioList;
            }
        }

        public Usuario ObterUsuario(int Id)
        {
            using(var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("Select * from ViewUsu where Id=@IdUsu", conexao);

                cmd.Parameters.AddWithValue("@IdUsu", Id);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                MySqlDataReader dr;

                Usuario usuario = new Usuario();
                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {
                    usuario.IdUsu = Convert.ToInt32(dr["Id"]);
                    usuario.nomeUsu = (string)(dr["Nome"]);
                    usuario.Cargo = (string)(dr["Cargo"]);
                    usuario.DataNasc = Convert.ToDateTime(dr["Data de Nascimento"]);
                    usuario.CEP = (string)(dr["CEP"]);
                    usuario.Estado = (string)(dr["Estado"]);
                    usuario.Cidade = (string)(dr["Cidade"]);
                    usuario.Bairro = (string)(dr["Bairro"]);
                    usuario.Logradouro = (string)(dr["Logradouro"]);
                    if (dr["Complemento"] != DBNull.Value)
                    {
                        usuario.Complemento = dr["Complemento"].ToString();
                    }
                    
                        usuario.Numero = Convert.ToInt32(dr["Numero"]);
                }
                return usuario;
            }
        }
    }
}
