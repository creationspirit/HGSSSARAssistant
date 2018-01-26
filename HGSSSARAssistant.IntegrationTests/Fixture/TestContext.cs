using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using Microsoft.AspNetCore.TestHost;
using HGSSSARAssistant.Web;
using Microsoft.AspNetCore.Hosting;

namespace HGSSSARAssistant.IntegrationTests.Fixture
{
    public class TestContext
    {
        public HttpClient Client;
        private TestServer _server;

        public TestContext()
        {
            SetupClient();
        }

        private void SetupClient()
        {
            _server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            Client = _server.CreateClient();
        }
    }
}
