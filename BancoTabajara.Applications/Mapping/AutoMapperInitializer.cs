using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancoTabajara.Applications.Mapping
{
    public class AutoMapperInitializer
    {
        public static void Initialize()
        {
            Mapper.Initialize(config =>
            {
                config.AddProfiles(typeof(AutoMapperInitializer));
            });
        }

        public static void Reset() => Mapper.Reset();
    }
}
