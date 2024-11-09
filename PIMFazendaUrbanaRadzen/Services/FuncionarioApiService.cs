namespace PIMFazendaUrbanaRadzen.Services
{
    public class FuncionarioApiService<T>
    {
        private readonly HttpClient _httpClient;
        private readonly string _endpointUrl;

        public FuncionarioApiService(HttpClient httpClient, string endpointUrl)
        {
            _httpClient = httpClient;
            _endpointUrl = endpointUrl;
        }

        public async Task<List<T>> GetFuncionariosFiltradosAsync(string search)
        {
            try
            {
                Console.WriteLine($"Chamando API em: {_endpointUrl}/filtrados-funcionario?search={Uri.EscapeDataString(search)}");
                return await _httpClient.GetFromJsonAsync<List<T>>($"{_endpointUrl}/filtrados-funcionario?search={Uri.EscapeDataString(search)}");
            }
            catch (HttpRequestException httpEx)
            {
                Console.WriteLine($"Erro de requisição: {httpEx.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao chamar API: {ex.Message}");
                throw;
            }
        }

        public async Task<List<T>> GetAllAsync()
        {
            try
            {
                Console.WriteLine($"Chamando API em: {_endpointUrl}/ativos-funcionario");
                return await _httpClient.GetFromJsonAsync<List<T>>($"{_endpointUrl}/ativos-funcionario");
            }
            catch (HttpRequestException httpEx)
            {
                Console.WriteLine($"Erro de requisição: {httpEx.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao chamar API: {ex.Message}");
                throw;
            }
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<T>($"{_endpointUrl}/{id}");
        }

        public async Task<HttpResponseMessage> CreateAsync(T entity)
        {
            return await _httpClient.PostAsJsonAsync($"{_endpointUrl}/cadastrar-funcionario", entity);
        }

        public async Task<HttpResponseMessage> UpdateAsync(T entity)
        {
            return await _httpClient.PutAsJsonAsync($"{_endpointUrl}/alterar-funcionario", entity);
        }

        public async Task<HttpResponseMessage> DeleteAsync(int id)
        {
            return await _httpClient.DeleteAsync($"{_endpointUrl}/excluir-funcionario/{id}");
        }


    }
}
