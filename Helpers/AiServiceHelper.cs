using System.Net.Http.Headers;

namespace Reunite.Helpers
{
    public class AiServiceHelper
    {
        public static MultipartFormDataContent CreateMultipartContent(IFormFile image, bool isParent)
        {
            var content = new MultipartFormDataContent();
            var imageContent = new StreamContent(image.OpenReadStream());
            imageContent.Headers.ContentType = MediaTypeHeaderValue.Parse(image.ContentType);
            content.Add(imageContent, "image", image.FileName);
            content.Add(new StringContent(isParent.ToString()), "isParent");
            return content;
        }
    }
}
