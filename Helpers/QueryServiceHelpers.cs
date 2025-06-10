using Reunite.DTOs.QueryDTOs;
using System.Net.Http.Headers;

namespace Reunite.Helpers
{
    public static class QueryServiceHelpers
    {

        public static MultipartFormDataContent CreateMultipartFormData(SearchDTO searchDTO, bool isParent)
        {
            var content = new MultipartFormDataContent();

            var imageContent = new StreamContent(searchDTO.Image.OpenReadStream());
            imageContent.Headers.ContentType = MediaTypeHeaderValue.Parse(searchDTO.Image.ContentType);
            content.Add(imageContent, "image", searchDTO.Image.FileName);

            content.Add(new StringContent(isParent.ToString()), "isParent");

            return content;
        }

    }
}