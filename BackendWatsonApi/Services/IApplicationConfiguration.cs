using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendWatsonApi.Services
{
    public interface IApplicationConfiguration
    {
        string WatsonApiKey { get; set; }
    }
}
