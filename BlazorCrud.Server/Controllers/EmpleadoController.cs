using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BlazorCrud.Server.Models;
using BlazorCrud.Shared;
using Microsoft.EntityFrameworkCore;

namespace BlazorCrud.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadoController : ControllerBase
    {
        private readonly DbcrudBlazorContext _dbcontext;

        public EmpleadoController(DbcrudBlazorContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<ActionResult> Lista()
        {
            var responseApi = new ResponseAPI<List<EmpleadoDTO>>();
            var listaEmpleadoDTO = new List<EmpleadoDTO>();
            try
            {
                foreach (var item in await _dbcontext.Empleados.Include(d=>d.IdDepartamentoNavigation).ToListAsync())
                {
                    listaEmpleadoDTO.Add(new EmpleadoDTO
                    {
                        IdEmpleado = item.IdEmpleado,
                        NombreCompleto = item.NombreCompleto,
                        IdDepartamento = item.IdDepartamento,
                        Sueldo = item.Sueldo,
                        FechaContrato = item.FechaContrato,
                        Departamento = new DepartamentoDTO
                        {
                            IdDepartamento = item.IdDepartamentoNavigation.IdDepartamento,
                            Nombre = item.IdDepartamentoNavigation.Nombre
                        }
                    });
                }
                responseApi.EsCorrecto = true;
                responseApi.valor = listaEmpleadoDTO;



            }
            catch (Exception ex)
            {

                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.Message;
            }
            return Ok(responseApi);

        }



        [HttpPost]
        [Route("Guardar")]
        public async Task<ActionResult> Guardar(EmpleadoDTO empleado)
        {
            var responseApi = new ResponseAPI<List<int>>();
           
            try
            {

                var dbempleado = new Empleado
                {
                    IdEmpleado = empleado.IdEmpleado,
                    NombreCompleto = empleado.NombreCompleto,
                    IdDepartamento = empleado.IdDepartamento,
                    Sueldo = empleado.Sueldo,
                    FechaContrato = empleado.FechaContrato
                };
                _dbcontext.Empleados.Add(dbempleado);
                await _dbcontext.SaveChangesAsync();

                if (dbempleado.IdEmpleado != 0)
                {

                    responseApi.EsCorrecto = true;
                    responseApi.Mensaje =dbempleado.IdEmpleado.ToString();
                }
                else
                {

                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "No se guardo el Empleado";
                }

         
            }
            catch (Exception ex)
            {

                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.Message;
            }
            return Ok(responseApi);

        }





        [HttpGet]
        [Route("Buscar/{id}")]
        public async Task<ActionResult> Buscar(int id)
        {
            var responseApi = new ResponseAPI<List<EmpleadoDTO>>();
            var EmpleadoDTO = new EmpleadoDTO();
            try
            {

                var dbEmpleado = await _dbcontext.Empleados.FirstOrDefaultAsync(x => x.IdEmpleado == id);

                if (dbEmpleado != null)
                {
                    EmpleadoDTO.IdEmpleado = dbEmpleado.IdEmpleado;
                    EmpleadoDTO.NombreCompleto = dbEmpleado.NombreCompleto;
                    EmpleadoDTO.IdDepartamento = dbEmpleado.IdDepartamento;
                    EmpleadoDTO.Sueldo = dbEmpleado.Sueldo;
                    EmpleadoDTO.FechaContrato = dbEmpleado.FechaContrato;

                    responseApi.EsCorrecto = true;
                    responseApi.valor.Add(EmpleadoDTO);
                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "No se encontro el Empleado";
                }





            }
            catch (Exception ex)
            {

                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.Message;
            }
            return Ok(responseApi);

        }



        [HttpPost]
        [Route("Editar/{id}")]
        public async Task<ActionResult> Editar(EmpleadoDTO empleado, int id)
        {
            var responseApi = new ResponseAPI<List<int>>();

            try
            {

                var dbempleado = await _dbcontext.Empleados.FirstOrDefaultAsync(x => x.IdEmpleado == id);
              
                if (dbempleado.IdEmpleado != null)
                {
                    dbempleado.NombreCompleto = empleado.NombreCompleto;
                    dbempleado.IdDepartamento = empleado.IdDepartamento;
                    dbempleado.Sueldo = empleado.Sueldo;
                    dbempleado.FechaContrato = empleado.FechaContrato;
                    _dbcontext.Empleados.Update(dbempleado);
                    await _dbcontext.SaveChangesAsync();

                    responseApi.EsCorrecto = true;
                    responseApi.Mensaje = dbempleado.IdEmpleado.ToString();
                }
                else
                {

                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "Empleado no encontrado";
                }


            }
            catch (Exception ex)
            {

                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.Message;
            }
            return Ok(responseApi);

        }


        [HttpDelete]
        [Route("Eliminar/{id}")]
        public async Task<ActionResult> Eliminar(int id)
        {
            var responseApi = new ResponseAPI<List<int>>();

            try
            {

                var dbempleado = await _dbcontext.Empleados.FirstOrDefaultAsync(x => x.IdEmpleado == id);

                if (dbempleado.IdEmpleado != null)
                {
                    
                    _dbcontext.Empleados.Remove(dbempleado);
                    await _dbcontext.SaveChangesAsync();

                    responseApi.EsCorrecto = true;
                    
                }
                else
                {

                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "Empleado no encontrado";
                }


            }
            catch (Exception ex)
            {

                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.Message;
            }
            return Ok(responseApi);

        }
    }
}

