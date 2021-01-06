using MyHN.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHN.Infrastructure
{
    public class CommentRepository : ICommentRepository
    {
        private readonly MyHNDbContext _context;

        public CommentRepository(MyHNDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Comment comment)
        {
            await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();
        }

        public Comment GetById(Guid id)
        {
            return _context.Comments.SingleOrDefault(o => o.Id == id);
        }

        public async Task UpdateAsync(Comment comment)
        {
            await _context.SaveChangesAsync();
        }
    }
}
