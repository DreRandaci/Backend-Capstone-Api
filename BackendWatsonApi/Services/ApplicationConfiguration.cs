using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendWatsonApi.Services
{
    public class ApplicationConfiguration : IApplicationConfiguration
    {
        public string WatsonApiKey { get; set; }
    }
}
