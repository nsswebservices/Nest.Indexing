using System;

namespace Nest.Indexing.Management
{
    public interface IIndexCreationResult
    {
       
        bool Success { get; }
        ICreateIndexResponse IndexResponse { get; }
        Exception Exception { get; }

        string IndexName { get; }
    }
}
