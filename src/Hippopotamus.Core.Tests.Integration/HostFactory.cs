using Hippopotamus.Core.Tests.WebApp;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using System;

namespace Hippopotamus.Core.Tests.Integration
{
    public class HostFactory
    {
        public IWebHost Create()
        {
            var builder = WebHost.CreateDefaultBuilder(Array.Empty<string>());
            builder.UseStartup<Startup>();
            var host = builder.Build();
            host.Start();

            return host;
        }
    }
}
