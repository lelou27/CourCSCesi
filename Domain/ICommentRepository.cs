using System;
using System.Threading.Tasks;

namespace MyHN.Domain
{
    public interface ICommentRepository
    {
        Task AddAsync(Comment comment);
        Comment GetById(Guid id);
        Task UpdateAsync(Comment comment);
    }
}
