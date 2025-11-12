using CadastroClienteApi.Models;
using CadastroClienteApi.Services;
using Microsoft.AspNetCore.Mvc;
namespace CadastroClienteApi.Controllers 
{
    // Este é o controlador MVC (para as Views)
    public class ClientesController : Controller
    {
        private readonly ClienteService _clienteService;
        public ClientesController(ClienteService clienteService)
        {
            _clienteService = clienteService;
        }
        // ===============================================
        // AÇÃO: INDEX (LISTAR TODOS)
        // ===============================================
       // GET: Clientes 
        public async Task<IActionResult> Index()
        {
        var clientes = await _clienteService.GetAsync();
        return View(clientes); // Envia a lista para a View Index.cshtml
        }
      // ===============================================
      // AÇÃO: DETAILS (VISUALIZAR DETALHES)
      // ===============================================
      // GET: Clientes/Details/5 (Visualizar Detalhes)
        public async Task<IActionResult> Details(string id)
        {
        if (string.IsNullOrEmpty(id))
        {
            return NotFound();
        }
        // Busca o cliente pelo ID (usando o Service)
        var cliente = await _clienteService.GetAsync(id);
        if (cliente is null)
        {
            return NotFound();
        }
        // Retorna a View de Detalhes
        return View(cliente);
        }
        // ===============================================
        // AÇÃO: CREATE (CRIAR NOVO)
        // ===============================================
        // GET: Clientes/Create (MOSTRAR FORMULÁRIO)
        public IActionResult Create()
        {
            return View();
        }
        // POST: Clientes/Create (CADASTRAR DADOS)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nome,CPF,Email,Telefone,Endereco")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                await _clienteService.CreateAsync(cliente);
                return RedirectToAction(nameof(Index)); // Volta para a lista
            }
            return View(cliente);
        }
        // ===============================================
        // AÇÃO: EDIT (ATUALIZAR)
        // ===============================================
        // GET: Clientes/Edit/{id} (MOSTRAR FORMULÁRIO PRÉ-PREENCHIDO)
        public async Task<IActionResult> Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }
            var cliente = await _clienteService.GetAsync(id);
            if (cliente is null)
            {
                return NotFound();
            }            
            return View(cliente);
        }
        // POST: Clientes/Edit/{id} (SALVAR ALTERAÇÕES)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Nome,CPF,Email,Telefone,Endereco")] Cliente clienteAtualizado)
        {
            if (id != clienteAtualizado.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    // O clienteAtualizado já contém o Id nativo do MongoDB (24 caracteres)
                    await _clienteService.UpdateAsync(id, clienteAtualizado);
                }
                catch (Exception)
                {
                    // Verifica se o erro foi por cliente não encontrado (ID inexistente)
                    if (await _clienteService.GetAsync(id) is null)
                    {
                        return NotFound();
                    }
                    throw; 
                }
                return RedirectToAction(nameof(Index));
            }
            return View(clienteAtualizado);
        }
        // ===============================================
        // AÇÃO: DELETE (EXCLUIR)
        // ===============================================
        // GET: Clientes/Delete/{id} (MOSTRAR PÁGINA DE CONFIRMAÇÃO)
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }
            var cliente = await _clienteService.GetAsync(id);            
            if (cliente is null)
            {
                return NotFound();
            }            
            return View(cliente);
        }
        // POST: Clientes/Delete/{id} (CONFIRMAR EXCLUSÃO)
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var cliente = await _clienteService.GetAsync(id);
            if (cliente is not null)
            {
                // Remove o cliente do MongoDB usando o Id
                await _clienteService.RemoveAsync(id);
            }            
            return RedirectToAction(nameof(Index));
        }
    }
}