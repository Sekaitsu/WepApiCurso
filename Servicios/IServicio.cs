using WepApiCurso.Servicios;

namespace WepApiCurso.Servicios
{
    public interface IServicio
    {
        Guid ObtenerScoped();
        Guid ObtenerSingleton();
        Guid ObtenerTrasient();
        void RealizarTarea();
    }
    public class ServicioA : IServicio
    {
        private readonly ILogger<ServicioA> logger;
        private readonly ServicioTransient servicioTransient;
        private readonly ServicioSingleton servicioSingleton;
        private readonly ServicioScoped servicioScoped;

        public ServicioA(ILogger<ServicioA> logger, ServicioTransient servicioTransient,
            ServicioSingleton servicioSingleton, ServicioScoped servicioScoped)
        {
            this.logger = logger;
            this.servicioTransient = servicioTransient;
            this.servicioSingleton = servicioSingleton;
            this.servicioScoped = servicioScoped;
        }

        public Guid ObtenerTrasient() { return servicioTransient.Guid; }
        public Guid ObtenerScoped() { return servicioScoped.Guid; }
        public Guid ObtenerSingleton () { return servicioSingleton.Guid; }

        public void RealizarTarea()
        {
        }
    }
    public class ServicioB : IServicio
    {
        public Guid ObtenerScoped()
        {
            throw new NotImplementedException();
        }

        public Guid ObtenerSingleton()
        {
            throw new NotImplementedException();
        }

        public Guid ObtenerTrasient()
        {
            throw new NotImplementedException();
        }

        public void RealizarTarea()
        {
        }
    }

    public class ServicioTransient
    {
        public Guid Guid = Guid.NewGuid();
    }
    public class ServicioScoped
    {
        public Guid Guid = Guid.NewGuid();
    }
    public class ServicioSingleton
    {
        public Guid Guid = Guid.NewGuid();
    }
}