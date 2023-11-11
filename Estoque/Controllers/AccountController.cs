using Microsoft.AspNetCore.Mvc;

public class AccountController : Controller
{
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Login(string username, string password)
    {
        // Lógica de autenticação aqui (por exemplo, verificar no banco de dados)

        // Verificar se as credenciais são válidas (usuário "admin" e senha "15963")
        if (username == "admin" && password == "15963")
        {
            // Redirecionar para a página Index de EstoqueController após o login bem-sucedido
            return View("/Views/ItensEstoques/Index");
        }
        else
        {
            // Credenciais inválidas, exibir uma mensagem de erro ou redirecionar de volta para a página de login
            ViewBag.ErrorMessage = "Credenciais inválidas. Tente novamente.";
            return View();
        }
    }
}
