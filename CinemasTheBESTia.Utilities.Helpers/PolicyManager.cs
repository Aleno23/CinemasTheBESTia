using CinemasTheBESTia.Utilities.Abstractions.Interfaces;
using Polly;
using Polly.Fallback;
using Polly.Wrap;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CinemasTheBESTia.Utilities.Helpers
{
    public class PolicyManager : IPolicyManager
    {

        public Task<T> GetFallbackPolicy<T>(Func<CancellationToken, Task<T>> fallBackAction, Func<Task<T>> action)
        {
            return Policy<T>
                .Handle<Exception>()
                 .FallbackAsync(fallBackAction)
                 .ExecuteAsync(action);
        }

    }
}
