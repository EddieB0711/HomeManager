using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeManager.Infrastructure.Extensions;
using Newtonsoft.Json;

namespace HomeManager.Infrastructure.Services.Repositories.Json
{
    public class JsonRepository<TEntity> : IJsonRepository<TEntity>
    {
        private readonly IJsonAdapter _context;

        public JsonRepository(IJsonAdapter context)
        {
            context.NullGuard();

            _context = context;
        }

        public async Task AddAsync(TEntity entity)
        {
            var list = await GetAllAsync().ConfigureAwait(false);
            list.Add(entity);

            await SaveTemporaryChanges(list);
        }

        public async Task DeleteAsync(Func<TEntity, bool> predicate)
        {
            var list = new List<TEntity>(await GetAllAsync().ConfigureAwait(false));
            list.RemoveAll(e => predicate(e));

            await SaveTemporaryChanges(list);
        }

        public async Task<IList<TEntity>> GetAllAsync()
        {
            if (!_context.VerifyFileExists()) return new List<TEntity>();

            var fileContent = _context.ReadTextAsync().ConfigureAwait(false);
            var entities = JsonConvert.DeserializeObject<List<TEntity>>(await fileContent) ?? new List<TEntity>();

            return entities.Where(e => e != null).ToList();
        }

        private async Task SaveTemporaryChanges(IList<TEntity> list)
        {
            var fileContent = JsonConvert.SerializeObject(list);
            await _context.WriteTextAsync(fileContent).ConfigureAwait(false);
        }
    }
}