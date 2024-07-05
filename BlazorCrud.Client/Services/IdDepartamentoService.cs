using BlazorCrud.Shared;
namespace BlazorCrud.Client.Services
{
    public interface IdDepartamentoService
    {
        Task<List<DepartamentoDTO>> ListaDepartamentos();
    }
}
