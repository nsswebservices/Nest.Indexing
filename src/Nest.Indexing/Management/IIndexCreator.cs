using System.Threading.Tasks;

namespace Nest.Indexing.Management
{
    public interface IIndexCreator
    {
        IIndexCreationResult Create();
        Task<IIndexCreationResult> CreateAsync();
    }
}
