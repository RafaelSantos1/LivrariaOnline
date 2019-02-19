using AutoMapper;

namespace LivrariaOnline.Application.Api.AutoMapper
{
    public class AutoMapperConfig
    {
        public static object thisLock = new object();
        private static bool _initialized = false;
        // Centralize automapper initialize

        public static MapperConfiguration RegisterMappings()
        {
            lock (thisLock)
            {
                if (!_initialized)
                {
                    Mapper.Reset();
                    Mapper.Initialize(cfg =>
                    {
                        cfg.AddProfile(new DomainToDtoMappingProfile());
                        cfg.AddProfile(new DtoToDomainMappingProfile());
                    });
                    _initialized = true;
                }
            }

            return new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new DomainToDtoMappingProfile());
                cfg.AddProfile(new DtoToDomainMappingProfile());
            });
        }
    }
}
