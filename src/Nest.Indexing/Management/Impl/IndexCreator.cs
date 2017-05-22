using System;
using System.Threading.Tasks;
using Nest.Indexing.Settings;

namespace Nest.Indexing.Management.Impl
{
    public abstract class IndexCreator : IIndexCreator
    {
        private readonly IElasticClient _client;       
        private readonly IIndexSettingCreator _settings;

        protected IndexCreator(IElasticClient client, IIndexSettingCreator settings)
        {
            _client = client;
            _settings = settings;
        }

        private IIndexCreationResult CreateResultCore(ICreateIndexResponse response, Exception ex, string indexName)
        {
            return CreateResult((ex == null && response.IsValid), response, ex, indexName);
        }

        protected virtual IIndexCreationResult CreateResult( bool success, ICreateIndexResponse response, Exception ex, string indexName)
        {
            return new IndexCreationResult(success, response, ex, indexName);
        }

        public IIndexCreationResult Create()
        {
            var indexName = CreateIndexName(_client.ConnectionSettings.DefaultIndex);

            try
            {
                var indexResponse = _client.CreateIndex(indexName, i =>
                {
                    i.Index(indexName);
                    i.InitializeUsing(_settings.Create());                    
                    return i;
                });
               
                return CreateResultCore( indexResponse, null, indexName.Name);
            }
            catch (Exception ex)
            {                
                return CreateResultCore( null, ex, null);
            }
        }

        private IndexName CreateIndexName(string alias)
        {
            var indexName = new IndexName
            {
                Name = $"{alias}-{DateTime.UtcNow:yyyyMMddHHmmss}",

            };
            
            return indexName;
        }

        public async Task<IIndexCreationResult> CreateAsync()
        {
            var indexName = CreateIndexName(_client.ConnectionSettings.DefaultIndex);       

            try
            {
                var indexResponse = await _client.CreateIndexAsync(indexName, i =>
                {
                    i.Index(indexName);
                    i.InitializeUsing(_settings.Create());                    
                    return i;
                });
                                
                return CreateResultCore(indexResponse, null, indexName.Name);
            }
            catch (Exception ex)
            {                
                return CreateResultCore(null, ex, null);
            }
        }
    }
}
