using BlazorCrud.Shared;
using System.Net.Http.Json;

namespace BlazorCrud.Client.Services
{
    public class DepartamentoService: IdDepartamentoService
    {

        private readonly HttpClient _http;
        public DepartamentoService(HttpClient httpClient)
        {
            _http = httpClient;
        }

        public async Task<List<DepartamentoDTO>> ListaDepartamentos()
        {
            var result =await  _http.GetFromJsonAsync<ResponseAPI<List<DepartamentoDTO>>>("api/Departamento/Lista");
            if (result!.EsCorrecto)
            {
                return result!.valor;
            }else 
                throw new Exception(result.Mensaje);
            
           
                
        }
    }
}
