using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Reunite.Services.Interfaces;

namespace Reunite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ChatController(IUserService userService, IChatService chatService) : ControllerBase
    {

        [HttpGet("unread-counter")]
        public async Task<IActionResult> UnreadCounter([FromQuery] string userId)
        {
            return Ok(await chatService.GetUnreadMessagesCountAsync(userId));
        }

        [HttpPost("user-chats")]
        public async Task<IActionResult> UserChats([FromForm] string userId)
        {
            return Ok(await userService.GetUserChatsAsync(userId));
        }

        [HttpPost("chat")]
        public async Task<IActionResult> Chat([FromForm] string userId1, [FromForm] string userId2)
        {
            if (userId1 == userId2)
                return BadRequest(new { Message = "You cannot start a chat with yourself." });

            string chatId = await chatService.OpenChatBetweenUsersAsync(userId1, userId2);

            return Ok(await chatService.GetChatMessages(chatId));
        }

    }
}