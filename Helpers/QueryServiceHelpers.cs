using Reunite.DTOs.QueryDTOs;
using System.Net.Http.Headers;

namespace Reunite.Helpers
{
    public static class QueryServiceHelpers
    {

        public static MultipartFormDataContent CreateMultipartFormData(SearchDTO searchDTO)
        {
            var content = new MultipartFormDataContent();

            var imageContent = new StreamContent(searchDTO.Image.OpenReadStream());
            imageContent.Headers.ContentType = MediaTypeHeaderValue.Parse(searchDTO.Image.ContentType);
            content.Add(imageContent, "image", searchDTO.Image.FileName);

            content.Add(new StringContent(searchDTO.IsParent.ToString()), "isParent");

            return content;
        }

    }
}