using GenericoApi.Models;
using GenericoApi.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace GenericoApi.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class GenericoController : Controller
    {
        private readonly GenericoRepository _repository;

        public GenericoController(GenericoRepository repository)
        {
            _repository = repository;
        }

        // Get (Buscar)
        [HttpGet]
        public async Task<ActionResult<List<Generico>>> Get()
        {
            var generico = await _repository.ObterTodosGenericoAsync();
            return Ok(generico);
        }

        // Post (Criar)
        [HttpPost]
        public async Task<IActionResult> Post(Generico novoGenerico)
        {
            await _repository.CriarGenericoAsync(novoGenerico);
            return CreatedAtAction(nameof(Get), new { id = novoGenerico.Id }, novoGenerico);
        }

        // Post (Criar Vários)
        [HttpPost("many")]
        public async Task<IActionResult> PostMany(List<Generico> genericos)
        {
            await _repository.CriarVariosAsync(genericos);
            return Ok();
        }

        // Delete (Deletar Vários)
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _repository.DeletarAsync(id);
            return NoContent();
        }

        // Patch (Atualizar Status)
        [HttpPatch("{id}/status")]
        public async Task<IActionResult> PatchStatus(string id, [FromBody] bool cancelado)
        {

            await _repository.AlterarStatusAsync(id, cancelado);

            //Retorna 204 NoContent (Sucesso, sem conteúdo para devolver)
            return NoContent();
        }

        // Put (Atualizar tudo)
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] Generico genericoAtualizado)
        {

            await _repository.AtualizarTotalAsync(id, genericoAtualizado);


            return NoContent();
        }
    }
}
