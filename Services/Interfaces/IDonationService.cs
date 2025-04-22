using Reunite.DTOs.DonationDTOs;

namespace Reunite.Services.Interfaces
{
    public interface IDonationService
    {
        string CheckoutSession(DonationDTO donationDTO);
    }
}