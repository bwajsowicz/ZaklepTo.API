using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using ZaklepTo.Core.Repositories;
using System.Threading.Tasks;
using ZaklepTo.Core.Domain;

namespace ZaklepTo.Infrastucture.Repositories.InMemory
{
    class InMemoryTableRepository : ITableRepository
    {
        private ISet<Table> _tables = new HashSet<Table>
        {
            new Table(Guid.NewGuid(), 4, (10, 10)),
            new Table(Guid.NewGuid(), 2, (50, 35)),
            new Table(Guid.NewGuid(), 1, (16, 42))
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
