using Microsoft.AspNetCore.Mvc;
using ProjetoPW.Models;
using ProjetoPW.Repositorio;

namespace ProjetoPW.Controllers
{
    public class ProdutoController : Controller
    {
        /*DECLARANDO UMA VARIÁVEL PRIVADA SOMENTE PARA LEITURA DO TIPO ProdutoRepositorio chamada
        _produtoRepositorio*/

        private readonly ProdutoRepositorio _produtoRepositorio;

        // DEFININDO CONSTRUTOR DA CLASSE USUARIOCONTROLLER QUE VAI RECEBER UMA INSTANCIA DE ProdutoRepositorio
        public ProdutoController(ProdutoRepositorio produtoRepositorio)
        {
            _produtoRepositorio = produtoRepositorio;
        }
        public IActionResult CadastrarProd()
        {

            return View();
        }

        [HttpPost]
        
        public IActionResult Cadastro()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CadastrarProduto(Produto produto)
        {
            // Verifica se o ModelState é válido. O ModelState é considerado válido se não houver erros de validação.
            if (ModelState.IsValid)
            {
                /* Se o modelo for válido:
                 Chama o método AdicionarProduto do _produtoRepositorio, passando o objeto Produto recebido.
                 Isso  salvará as informações do novo usuário no banco de dados.*/

                _produtoRepositorio.AdicionarProduto(produto);

                /* Redireciona o usuário para a action "Login" deste mesmo Controller (LoginController).
                  após um cadastro bem-sucedido, redirecionará à página de login.*/
                return RedirectToAction("Login");
            }

            /* Se o ModelState não for válido (houver erros de validação):
             Retorna a View de Cadastro novamente, passando o objeto Produto com os erros de validação.
             Isso permite que a View exiba os erros para o usuário corrigir o formulário.*/
            return View(produto);
        }
       
    }
}
