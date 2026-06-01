using MongoDB.Driver;
using MonsterApi.Models;

namespace MonsterApi.Repositories
{
    public class MonsterRepository
    {
        private readonly IMongoCollection<Monster> _monsterCollection;

        public MonsterRepository(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("MongoConnection");
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("MonsterDb");
            _monsterCollection = database.GetCollection<Monster>("Monsters");
        }
        // Obter todos os genéricos
        public async Task<List<Monster>> ObterTodosMonsterAsync() =>
            await _monsterCollection.Find(_ => true).ToListAsync();
        // Criar Generico

        public async Task CriarMonsterAsync(Monster novoMonster) =>
            await _monsterCollection.InsertOneAsync(novoMonster);
        // Criar Varios
        public async Task CriarVariosAsync(List<Monster> monsters) =>
            await _monsterCollection.InsertManyAsync(monsters);
        // Deletar
        public async Task DeletarAsync(string id)
        {
            var filtro = Builders<Monster>.Filter.Eq(monster => monster.Id, id);
            await _monsterCollection.DeleteOneAsync(filtro);
        }
        // Alterar Status (Patch) (Atenção com os parâmetros)
        public async Task AlterarStatusAsync(string id, bool cancelado)
        {
            var filtro = Builders<Monster>.Filter.Eq(monster => monster.Id, id);

            var atualizacao = Builders<Monster>.Update.Set(monster => monster.Cancelado, cancelado);

            await _monsterCollection.UpdateOneAsync(filtro, atualizacao);
        }
        public async Task AlterarNotaAsync(string id, int notaPietro)
        {
            var filtro = Builders<Monster>.Filter.Eq(monster => monster.Id, id);

            var atualizacao = Builders<Monster>.Update.Set(monster => monster.NotaPietro, notaPietro);

            await _monsterCollection.UpdateOneAsync(filtro, atualizacao);
        }
        public async Task AtualizarTotalAsync(string id, Monster monsterAtualizado)
        {
            var filtro = Builders<Monster>.Filter.Eq(tarefa => tarefa.Id, id);

            monsterAtualizado.Id = id;

            await _monsterCollection.ReplaceOneAsync(filtro, monsterAtualizado);
        }
    }
}
