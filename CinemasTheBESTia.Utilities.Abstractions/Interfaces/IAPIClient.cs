using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CinemasTheBESTia.Utilities.Abstractions.Interfaces
{
    public interface IAPIClient
    {
        Task<T> GetAsync<T>(Uri requestUrl);
        Task<T> GetAsync<T>(Uri requestUrl, string tokenName);
    }
}
