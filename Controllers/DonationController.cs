using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Reunite.DTOs.DonationDTOs;
using Reunite.Services.Interfaces;

namespace Reunite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DonationController : ControllerBase
    {

        private readonly IDonationService donationService = null!;

        public DonationController(IDonationService donationService)
        {
            this.donationService = donationService;
        }

        [HttpPost("donate")]
        public IActionResult Donate([FromForm] DonationDTO donationDTO)
        {
            return Ok(new { URL = donationService.CheckoutSession(donationDTO) });
        }

    }
}