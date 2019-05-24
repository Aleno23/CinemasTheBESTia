using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CinemasTheBESTia.Entities.Movies;

namespace CinemasTheBESTia.Utilities.Abstractions.Interfaces
{
    public interface IPolicyManager
    {
        Task<T> GetFallbackPolicy<T>(Func<CancellationToken, Task<T>> fallBackAction, Func<Task<T>> action);
    }
}
