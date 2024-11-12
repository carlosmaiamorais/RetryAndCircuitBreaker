using RetryPattern.Models;

namespace RetryPattern.Services
{
    public interface IClienteAppService
    {
        Task<List<Cliente>> ObterTodos();
    }

    public class ClienteAppService : IClienteAppService
    {

        private readonly HttpClient _httpClient;

        public ClienteAppService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Cliente>> ObterTodos()
        {
            try
            {
                using (var resposta = await _httpClient.GetAsync("http://10.255.255.1"))
                {
                    var resultado = await resposta.Content.ReadAsStringAsync();

                    // converteria em list

                    return new List<Cliente>();
                }
            }
            catch (Exception)
            {

                throw;
            }    
        }
    }
}
