using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Reunite.Services.Interfaces;

namespace Reunite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController(IUserService userService, IChatService chatService) : ControllerBase
    {

        [HttpPost("user-chats")]
        public async Task<IActionResult> UserChats([FromForm] string userId)
        {
            return Ok(await userService.GetUserChatsAsync(userId));
        }

        [HttpPost("chat")]
        public async Task<IActionResult> Chat([FromForm] string userId1, [FromForm] string userId2)
        {
            string chatId = await chatService.OpenChatBetweenUsersAsync(userId1, userId2);
            return Ok(await chatService.GetChatMessages(chatId));
        }

    }
}