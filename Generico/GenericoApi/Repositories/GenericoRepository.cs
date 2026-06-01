using GenericoApi.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace GenericoApi.Repositories
{
    public class GenericoRepository
    {
        private readonly IMongoCollection<Generico> _genericoCollection;

        public GenericoRepository(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("MongoConnection");
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("GenericoDatabase");
            _genericoCollection = database.GetCollection<Generico>("Genericos");
        }

        // Obter todos os genéricos
        public async Task<List<Generico>> ObterTodosGenericoAsync() =>
            await _genericoCollection.Find(_ => true).ToListAsync();

        // Criar Generico
        public async Task CriarGenericoAsync(Generico novoGenerico) =>
            await _genericoCollection.InsertOneAsync(novoGenerico);

        // Criar Varios
        public async Task CriarVariosAsync(List<Generico> genericos) =>
            await _genericoCollection.InsertManyAsync(genericos);

        // Deletar
        public async Task DeletarAsync(string id)
        {
            var filtro = Builders<Generico>.Filter.Eq(generico => generico.Id, id);
            await _genericoCollection.DeleteOneAsync(filtro);
        }

        // Alterar Status (Patch) (Atenção com os parâmetros)
        public async Task AlterarStatusAsync(string id, bool cancelado)
        {
            var filtro = Builders<Generico>.Filter.Eq(generico => generico.Id, id);

            var atualizacao = Builders<Generico>.Update.Set(generico => generico.Cancelado, cancelado);

            await _genericoCollection.UpdateOneAsync(filtro, atualizacao);
        }

        // Alterar Tudo (Put)
        public async Task AtualizarTotalAsync(string id, Generico genericoAtualizado)
        {
            var filtro = Builders<Generico>.Filter.Eq(generico => generico.Id, id);

            genericoAtualizado.Id = id;

            await _genericoCollection.ReplaceOneAsync(filtro, genericoAtualizado);
        }



    }
}
