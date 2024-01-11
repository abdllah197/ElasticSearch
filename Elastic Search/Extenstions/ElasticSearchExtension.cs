using Elastic_Search.Models;
using Nest;

namespace Elastic_Search.Extensions
{
    public static class ElasticSearchExtension
    {
        public static void AddElasticSearch(this IServiceCollection services, IConfiguration configuration)
        {
            // Config
            var url = configuration["ElasticSearch:Uri"];
            var defaultIndex = configuration["ElasticSearch:index"];
            if(url is not null && defaultIndex is not null)
            {
                var setting = new ConnectionSettings(new Uri(url)).PrettyJson().DefaultIndex(defaultIndex);

                AddDefaultMapping(setting);
                var client = new ElasticClient(setting);
                services.AddSingleton<IElasticClient>(client);
                CreateIndex(client, defaultIndex);
            }            
        }
        private static void AddDefaultMapping(ConnectionSettings settings)
        {
            settings.DefaultMappingFor<Book>(p =>
            p.Ignore(x => x.Id)
             .Ignore(x => x.Price));
        }
        private static void CreateIndex(IElasticClient client, string indexName)
        {
            client.Indices.Create(indexName, i => i.Map<Book>(x => x.AutoMap()));
        }
    }
}
