using AplicacaoBanco.Repository.Contract;
using Microsoft.AspNetCore.Mvc;

namespace AplicacaoBanco.Controllers
{
    public class UsuarioController : Controller
    {
        private IUsuarioRepository _usuarioRepository;
        public UsuarioController(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        [HttpGet]
        public IActionResult CadastrarUsuario()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CadastrarUsuario(UsuarioController usuario)
        {
            if(ModelState.IsValid)
            {
                _usuarioRepository.Cadastrar(usuario);
            }
            return View();
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
