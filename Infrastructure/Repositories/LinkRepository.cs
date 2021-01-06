using MyHN.Domain;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MyHN.Infrastructure.Repositories
{
    public class LinkRepository : ILinkRepository
    {
        private readonly MyHNDbContext _context;

        public LinkRepository(MyHNDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Link link)
        {
            await _context.Links.AddAsync(link);
            await _context.SaveChangesAsync();
        }

        public Link GetById(Guid id)
        {
            return _context.Links.SingleOrDefault(o => o.Id == id);
        }

        public async Task UpdateAsync(Link link)
        {
            await _context.SaveChangesAsync();
        }
    }
}
