using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WepApiCurso.Entidades;
using WepApiCurso.Servicios;

namespace WepApiCurso.Controllers
{
    [ApiController]
    [Route("api/autores")]
    public class AutoresController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IServicio servicio;
        private readonly ServicioTransient servicioTransient;
        private readonly ServicioScoped servicioScoped;
        private readonly ILogger<AutoresController> logger;
        private readonly ServicioSingleton servicioSingleton;

        public AutoresController(ApplicationDbContext context, IServicio servicio,
            ServicioTransient servicioTransient, ServicioSingleton servicioSingleton,
            ServicioScoped servicioScoped, ILogger<AutoresController> logger)
        {
            this.context = context;
            this.servicio = servicio;
            this.servicioTransient = servicioTransient;
            this.servicioScoped = servicioScoped;
            this.logger = logger;
            this.servicioSingleton = servicioSingleton;

        }

        [HttpGet("GUID")]
        public ActionResult ObtenerGuids()
        {
            return Ok(new
            {
                AutoresController_Transient = servicioTransient.Guid,
                ServicioA_Transient = servicio.ObtenerTrasient(),

                AutoresController_Scoped = servicioScoped.Guid,
                ServicioA_Scoped = servicio.ObtenerScoped(),

                AutoresController_Singleton = servicioSingleton.Guid,
                ServicioA_Singleton = servicio.ObtenerSingleton()
            });
        }

        /// Visualizar 
        [HttpGet]
        [HttpGet("/listado")]
        [HttpGet("listado")]// api/autores/listado
        public async Task<ActionResult<List<Autor>>> Get()
        {
            logger.LogInformation("Estamos obteniendo los autores");
            logger.LogError("Mensaje de prueba - error");
            servicio.RealizarTarea();
            return await context.Autores.Include(x => x.Libros).ToListAsync();
        }

        [HttpGet("primero")]// api/autores/primero?nombre=laura&apellido=bernate
        public async Task<ActionResult<Autor>> PrimerAutor([FromHeader] int miValor, [FromQuery] string name)
        {
            return await context.Autores.FirstOrDefaultAsync();
        }

        [HttpGet("{id:int}/{param2?}")]// api/autores/id/parametro opcional
        public async Task<ActionResult<Autor>> Get(int id, string param2)
        {
            var autor = await context.Autores.FirstOrDefaultAsync(x => x.Id == id);
            if (autor == null)
            {
                return NotFound();
            }
            return autor;
        }

        [HttpGet("{name}")]// api/autores/name
        public async Task<ActionResult<Autor>> Get([FromRoute] string name)
        {
            var autor = await context.Autores.FirstOrDefaultAsync(x => x.Name.Contains(name));
            if (autor == null)
            {
                return NotFound();
            }
            return autor;
        }

        /// Crear
        [HttpPost]
        public async Task<ActionResult> Post([FromBody]Autor autor)
        {
            var existeAutorNombreIdentico = await context.Autores.AnyAsync(x => x.Name == autor.Name);

            if (existeAutorNombreIdentico)
            {
                return BadRequest($"Ya existe un autor registrado con el nombre {autor.Name}");
            }

            context.Add(autor);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(Autor autor, int id)
        {
            if (autor.Id != id)
            {
                return BadRequest("El id del autor no coindice con el id de la URL");
            }
            context.Update(autor);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existeIdAutor = await context.Autores.AnyAsync(x => x.Id == id);

            if (!existeIdAutor)
            {
                return NotFound("No existe el id ");
            }

            context.Remove(new Autor() { Id = id});
            await context.SaveChangesAsync();
            return Ok();
        }
    }
}