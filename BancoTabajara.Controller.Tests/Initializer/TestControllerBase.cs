using BancoTabajara.Applications.Mapping;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BancoTabajara.Controller.Tests.Initializer
{
    [TestFixture]
    public class TestControllerBase
    {
        [OneTimeSetUp]
        public void InitializeOnceTime()
        {
            AutoMapperInitializer.Reset();
            AutoMapperInitializer.Initialize();
          
        }

        public HttpRequestMessage GetUri(string Uri)
        {
            return new HttpRequestMessage()
            {
                RequestUri = new Uri(Uri)
            };
        }

    }
}