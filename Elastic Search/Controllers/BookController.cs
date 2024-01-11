using Elastic_Search.Models;
using Microsoft.AspNetCore.Mvc;
using Nest;

namespace Elastic_Search.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController : ControllerBase
    {
        private readonly IElasticClient _elasticClient;

        public BookController(IElasticClient elasticClient)
        {
            _elasticClient = elasticClient;
        }

        [HttpGet(Name = "GetBook")]
        public async Task<IActionResult> Get(string filterText)
        {
            var result = await _elasticClient.SearchAsync<Book>(
                s => s.Query(
                    q => q.QueryString(
                        d => d.Query('*' + filterText + '*')
                        )
                    ).Size(1000)
                );
            return Ok(result.Documents.ToList());
        }
        [HttpPost(Name = "AddBook")]
        public async Task<IActionResult> Post(Book book)
        {
            await _elasticClient.IndexDocumentAsync(book);
            return Ok();
        }
    }
}
