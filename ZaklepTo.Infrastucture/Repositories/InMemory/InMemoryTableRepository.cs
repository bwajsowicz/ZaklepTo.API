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
        private static readonly ISet<Table> _tables = new HashSet<Table>
        {
            new Table(4, (10, 10)),
            new Table(2, (50, 35)),
            new Table(1, (16, 42))
        };

        public async Task AddAsync(Table table)
            => await Task.FromResult(_tables.Add(table));

        public async Task DeleteAsync(Guid id)
        {
            var table = await GetAsync(id);
            _tables.Remove(table);
            await Task.CompletedTask;           
        }

        public async Task<IEnumerable<Table>> GetAllAsync()
            => await Task.FromResult(_tables);

        public async Task<Table> GetAsync(Guid id)
            => await Task.FromResult(_tables.SingleOrDefault(x => x.Id == id));

        public async Task UpdateAsync(Table table)
        {
            var oldTable = await GetAsync(table.Id);
            _tables.Remove(oldTable);
            _tables.Add(table);
            await Task.CompletedTask;
        }
    }
}
