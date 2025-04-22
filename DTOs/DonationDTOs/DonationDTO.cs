namespace Reunite.DTOs.DonationDTOs
{
    public class DonationDTO
    {
        public string Email { get; set; } = null!;
        public decimal Amount { get; set; }
        public string Currency { get; set; } = "egp";
    }
}