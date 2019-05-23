using CinemasTheBESTia.Entities.Payment;
using CinemasTheBESTia.Utilities.Abstractions.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CinemasTheBESTia.Booking.Application.Core
{
    public class PaymentService : IPaymentService
    {
        private readonly IAPIClient _apiClient;
        private readonly PaymentSettings _paymentSettings;

        public PaymentService(IAPIClient aPIClient, PaymentSettings paymentSettings)
        {
            _apiClient = aPIClient;
            _paymentSettings = paymentSettings;
        }

        public string Pay(string user, double total)
        {
            return _apiClient.Get(new Uri($"{_paymentSettings.Url}/{user}/{total}"));
        }
    }
}
