using BancoTabajara.Applications.Mapping;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancoTabajara.Applications.Tests.Initializer
{

    [TestFixture]
    public class TestBase
    {
        [OneTimeSetUp]
        public void Initialize()
        {
            AutoMapperInitializer.Reset();
            AutoMapperInitializer.Initialize();
        }
    }

}
