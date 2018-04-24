using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZaklepTo.Core.Domain;
using ZaklepTo.Core.Repositories;

namespace ZaklepTo.Infrastructure.Repositories.InMemory
{
    public class InMemoryTableRepository : ITableRepository
    {
        private static readonly ISet<Table> Tables = new HashSet<Table>();

        public async Task AddAsync(Table table)
            => await Task.FromResult(Tables.Add(table));

        public async Task DeleteAsync(Guid id)
        {
            var table = await GetAsync(id);
            Tables.Remove(table);
            await Task.CompletedTask;           
        }

        public async Task<IEnumerable<Table>> GetAllAsync()
            => await Task.FromResult(Tables);

        public async Task<Table> GetAsync(Guid id)
            => await Task.FromResult(Tables.SingleOrDefault(x => x.Id == id));

        public async Task UpdateAsync(Table table)
        {
            var oldTable = await GetAsync(table.Id);
            Tables.Remove(oldTable);
            Tables.Add(table);
            await Task.CompletedTask;
        }
    }
}
