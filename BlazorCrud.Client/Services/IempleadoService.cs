using BlazorCrud.Shared;

namespace BlazorCrud.Client.Services
{
    public interface IempleadoService
    {
        Task <List<EmpleadoDTO>> Listaempleados();
        Task<EmpleadoDTO> GetbyId(int Id);
        Task<int> Guardar(EmpleadoDTO Empleado);
        Task<int> Editar(EmpleadoDTO Empleado);
        Task<bool> Eliminar(int Id);
    }
}
