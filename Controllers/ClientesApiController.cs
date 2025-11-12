using CadastroClienteApi.Models;
using CadastroClienteApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace CadastroClienteApi.Controllers
{
    // Indica que é um controlador de API e define a rota base
    [Route("api/[controller]")]
    [ApiController] 
    public class ClientesApiController : ControllerBase
    {
        private readonly ClienteService _clienteService;
        public ClientesApiController(ClienteService clienteService)
        {
            _clienteService = clienteService;
        }
        // GET: api/ClientesApi
        [HttpGet]
        public async Task<List<Cliente>> Get() =>
            await _clienteService.GetAsync();
        // GET: api/ClientesApi/5 (busca por ID)
        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Cliente>> Get(string id)
        {
            var cliente = await _clienteService.GetAsync(id);
            if (cliente is null)
            {
                return NotFound();
            }
            return cliente;
        }
        // POST: api/ClientesApi
        [HttpPost]
        public async Task<IActionResult> Post(Cliente novoCliente)
        {
            await _clienteService.CreateAsync(novoCliente);
            return CreatedAtAction(nameof(Get), new { id = novoCliente.Id }, novoCliente);
        }
        // PUT: api/ClientesApi/{id}
        // Retorna 204 No Content se a atualização for bem-sucedida
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, Cliente clienteAtualizado)
        {
            // 1. Verifica se o cliente existe
            var clienteExistente = await _clienteService.GetAsync(id);
            if (clienteExistente is null)
            {
                // Se não encontrar, retorna 404 Not Found
                return NotFound();
            }
            // 2. Garante que o ID no objeto de atualização seja o ID da URL
            clienteAtualizado.Id = clienteExistente.Id;
            // 3. Executa a atualização no serviço
            await _clienteService.UpdateAsync(id, clienteAtualizado);
            // 4. Retorna 204 No Content (padrão REST para PUT bem-sucedido)
            return NoContent();
        }
        // DELETE: api/ClientesApi/{id}
        // Retorna 204 No Content se a exclusão for bem-sucedida
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            // 1. Verifica se o cliente existe
            var clienteExistente = await _clienteService.GetAsync(id);
            if (clienteExistente is null)
            {
                // Se não encontrar, retorna 404 Not Found
                return NotFound();
            }
            // 2. Executa a exclusão
            await _clienteService.RemoveAsync(id);
            // 3. Retorna 204 No Content
            return NoContent();
        }
    }
}