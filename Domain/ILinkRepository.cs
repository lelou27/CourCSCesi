using System;
using System.Threading.Tasks;

namespace MyHN.Domain
{
    public interface ILinkRepository
    {
        /// <summary>
        /// Persiste une entité lien
        /// </summary>
        /// <param name="link"></param>
        /// <returns></returns>
        Task AddAsync(Link link);
        /// <summary>
        /// Récupère un lien
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Link GetById(Guid id);

        Task UpdateAsync(Link link);
    }
}
