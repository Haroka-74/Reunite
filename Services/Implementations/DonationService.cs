using Reunite.DTOs.DonationDTOs;
using Reunite.Helpers;
using Reunite.Services.Interfaces;
using Stripe;
using Stripe.Checkout;

namespace Reunite.Services.Implementations
{
    public class DonationService : IDonationService
    {

        private readonly IConfiguration configuration;

        public DonationService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string CheckoutSession(DonationDTO donationDTO)
        {
            StripeConfiguration.ApiKey = configuration["Stripe:SecretKey"]!;

            long amount = CurrencyHelper.ConvertToMinorUnit(donationDTO.Amount, donationDTO.Currency);

            var options = new SessionCreateOptions
            {
                CustomerEmail = donationDTO.Email,
                PaymentMethodTypes = new List<string> { "card" },
                LineItems = new List<SessionLineItemOptions>
                {
                    new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            Currency = donationDTO.Currency.ToLower(),
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = "Help Reunite Lost Children",
                                Description = "Your donation helps bring missing children back to their families",
                                Images = new List<string> { "https://drive.google.com/uc?export=view&id=1eNn53lJkXQVIACv8FMwFTH2gNdkmnhWb" }
                            },
                            UnitAmount = amount,
                        },
                        Quantity = 1,
                    },
                },
                Mode = "payment",
                SuccessUrl = "https://reunitech.me/donation?status=success",
                CancelUrl = "https://reunitech.me/donation?status=cancel",
                SubmitType = "donate",
                PaymentIntentData = new SessionPaymentIntentDataOptions
                {
                    Description = $"Donation to Reunite - {donationDTO.Email}",
                },
            };

            var service = new SessionService();
            var session = service.Create(options);
            return session.Url;
        }

    }
}