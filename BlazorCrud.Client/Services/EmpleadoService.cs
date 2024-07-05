using BlazorCrud.Shared;
using System.Net.Http.Json;

namespace BlazorCrud.Client.Services
{
   
    public class EmpleadoService: IempleadoService
    {
        private readonly HttpClient _http;

        public EmpleadoService(HttpClient httpClient)
        {
            _http = httpClient;
        }

   
        public async Task<int> Editar(EmpleadoDTO Empleado)
        {
            var result = await _http.PutAsJsonAsync($"api/Empleado/Editar/{Empleado.IdEmpleado}", Empleado);
            var response = await result.Content.ReadFromJsonAsync<ResponseAPI<int>>();
            if (response!.EsCorrecto)
            {
                return response!.valor;
            }
            else
            {
                throw new Exception(response.Mensaje);
            }
        }

        public async Task<bool> Eliminar(int Id)
        {
            var result = await _http.DeleteAsync($"api/Empleado/Eliminar/{Id}");
            var response = await result.Content.ReadFromJsonAsync<ResponseAPI<int>>();
            if (response!.EsCorrecto)
            {
                return response.EsCorrecto;
            }
            else
            {
                throw new Exception(response.Mensaje);
            }
        }

        public async Task<EmpleadoDTO> GetbyId(int Id)
        {
            var result = await _http.GetFromJsonAsync<ResponseAPI<EmpleadoDTO>>($"api/Empleado/{Id}");
            if (result!.EsCorrecto)
            {
                return result!.valor;
            }
            else
            {
                throw new Exception(result.Mensaje);
            }
        }

        public async Task<int> Guardar(EmpleadoDTO Empleado)
        {

            var result = await _http.PostAsJsonAsync("api/Empleado/Guardar", Empleado);
            var response = await result.Content.ReadFromJsonAsync<ResponseAPI<int>>();
            if (response!.EsCorrecto)
            {
                return response!.valor;
            }
            else
            {
                throw new Exception(response.Mensaje);
            }
        }
        public async Task<List<EmpleadoDTO>> Listaempleados()
        {
            var result = await _http.GetFromJsonAsync<ResponseAPI<List<EmpleadoDTO>>>("api/Empleado/Lista");
            if (result!.EsCorrecto)
            {
                return result!.valor;
            }
            else
            {
                throw new Exception(result.Mensaje);
            }
        }

        
    }
}
