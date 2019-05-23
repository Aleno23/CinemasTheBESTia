using System;
using System.Collections.Generic;
using System.Text;

namespace CinemasTheBESTia.Utilities.Abstractions.Interfaces
{
    public interface IPaymentService
    {

        string Pay(string user, double total);
    }
}
