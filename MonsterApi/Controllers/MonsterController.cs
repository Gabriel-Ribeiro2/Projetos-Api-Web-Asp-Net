using Microsoft.AspNetCore.Mvc;
using MonsterApi.Models;
using MonsterApi.Repositories;

namespace MonsterApi.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class MonsterController : Controller
    {
        private readonly MonsterRepository _repository;

        public MonsterController(MonsterRepository repository)
        {
            _repository = repository;
        }

        // Get (Buscar)
        [HttpGet]
        public async Task<ActionResult<List<Monster>>> Get()
        {
            var monster = await _repository.ObterTodosMonsterAsync();
            return Ok(monster);
        }

        // Post (Criar)
        [HttpPost]
        public async Task<IActionResult> Post(Monster novoMonster)
        {
            await _repository.CriarMonsterAsync(novoMonster);
            return CreatedAtAction(nameof(Get), new { id = novoMonster.Id }, novoMonster);
        }

        // Post (Criar Vários)
        [HttpPost("many")]
        public async Task<IActionResult> PostMany(List<Monster> monsters)
        {
            await _repository.CriarVariosAsync(monsters);
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
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchStatus(string id, [FromBody] bool cancelado)
        {
            
            await _repository.AlterarStatusAsync(id, cancelado);

            //Retorna 204 NoContent (Sucesso, sem conteúdo para devolver)
            return NoContent();
        }

        // Patch (Atualizar Notas)
        [HttpPatch("{id}/notas")]
        public async Task<IActionResult> PatchNotas(string id, [FromBody] int notaPietro)
        {
            if (notaPietro < 0 || notaPietro > 10)
            {
                return BadRequest("A nota deve estar entre 0 e 10 Pietro.");
            }
            await _repository.AlterarNotaAsync(id, notaPietro);

            //Retorna 204 NoContent (Sucesso, sem conteúdo para devolver)
            return NoContent();
        }
        // Put (Atualizar tudo)
        [HttpPut("{id}/status")]
        public async Task<IActionResult> Put(string id, [FromBody] Monster monsterAtualizada)
        {
            
            await _repository.AtualizarTotalAsync(id, monsterAtualizada);

           
            return NoContent();
        }

    }
}