using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Materias.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Materias.Controllers
{
    [Route("api/materias")]
    [ApiController]
    public class MateriaController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<MateriaController> _logger;

        public MateriaController(ApplicationDbContext context, ILogger<MateriaController> logger)
        {
            _context = context;
            _logger = logger;
        }
        
        [HttpGet]
        public async Task<ActionResult<List<Materia>>> Get()
        {
            _logger.LogWarning("Se obtuvieron todas las materias");
            return await _context.Materias.ToListAsync();
        }
        
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Materia>> GetOneById(int id)
        {
            _logger.LogWarning($"Se busco una materia por el id: {id}");
            return await _context.Materias
                .Include(b => b.Requisito)
                .FirstOrDefaultAsync(a => a.Id == id);
        }
        
        //Crear una materia
        [HttpPost]
        public async Task<ActionResult> Post(Materia modelo)
        {
            _logger.LogWarning($"Se creo una nueva materia con el nombre: {modelo.Nombre}");
            _context.Add(modelo);
            await _context.SaveChangesAsync();
            return Ok();
        }
        
        //Actualizar una materia
        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, Materia modelo)
        {
            var materia = await _context.Materias.FirstOrDefaultAsync(a => a.Id == id);
            if (materia is null)
            {
                return NotFound("Materia no encontrada");
            }
            
            _logger.LogWarning($"Se actualizo la materia con el id: {id}");
            
            materia.Codigo = modelo.Codigo;
            materia.Nombre = modelo.Nombre;
            materia.RequisitoId = modelo.RequisitoId;
            _context.Update(materia);
            await _context.SaveChangesAsync();
            
            return Ok();
        }
        
        //Eliminar una materia
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var materia = await _context.Materias.FirstOrDefaultAsync(a => a.Id == id);
            if (materia is null)
            {
                return NotFound("Materia no encontrada");
            }
            
            _logger.LogWarning($"Se elimino la materia con el id: {id}");
            
            _context.Remove(materia);
            await _context.SaveChangesAsync();
            
            return Ok();
        }
    }
}
