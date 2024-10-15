using Dima.Core.Handlers;
using Dima.Core.Requests.Account;
using Dima.Core.Responses;
using System.Net.Http.Json;
using System.Text;

namespace Dima.Pwa.Handlers;

public class AccountHandler(IHttpClientFactory _httpClientFactory) : IAccountHandler
{
    private readonly HttpClient _httpClient = _httpClientFactory.CreateClient(Configuration.HttpClientName);

    public async Task<Response<string>> LoginAsync(LoginRequest request)
    {
        var result = await _httpClient.PostAsJsonAsync("v1/identity/login?useCookies=true", request);

        return result.IsSuccessStatusCode
            ? new Response<string>(null, 200, "Login realizado com sucesso")
            : new Response<string>(null, 400, "Usuário ou senha inválido");
    }

    public async Task<Response<string>> RegisterAsync(RegisterRequest request)
    {
        var result = await _httpClient.PostAsJsonAsync("v1/identity/register", request);

        return result.IsSuccessStatusCode
            ? new Response<string>(null, 200, "Cadastro realizado com sucesso")
            : new Response<string>(null, 400, "Não foi possível realizar o seu cadastro");
    }

    public async Task LogoutAsync()
    {
        var emptyContent = new StringContent("{}", Encoding.UTF8, "application/json");
        await _httpClient.PostAsJsonAsync("v1/identity/logout", emptyContent);
    }
}
