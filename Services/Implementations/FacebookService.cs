using System.Text.Json;
using Microsoft.Net.Http.Headers;
using Reunite.DTOs;
using Reunite.DTOs.FacebookDTOs;
using Reunite.DTOs.QueryDTOs;
using Reunite.DTOs.SearchDTOs;
using Reunite.Models;
using Reunite.Repositories.Interfaces;
using Reunite.Services.Interfaces;
using MediaTypeHeaderValue = System.Net.Http.Headers.MediaTypeHeaderValue;

namespace Reunite.Services.Implementations;

public class FacebookService(HttpClient httpClient, IConfiguration configuration,IFacebookRepository facebookRepository) : IFacebookService
{
    
    public async Task<string> ParentPostToFacebook(ParentSearchDTO facebookPostDto,string queryId)
    {
        string url = $"https://graph.facebook.com/{configuration["Facebook:PageId"]}/photos";

        var form = new MultipartFormDataContent();

        var imageStream = facebookPostDto.Image.OpenReadStream();
        var imageContent = new StreamContent(imageStream);
        imageContent.Headers.ContentType = new MediaTypeHeaderValue(facebookPostDto.Image.ContentType);
        form.Add(imageContent, "source", facebookPostDto.Image.FileName);
        string caption = $"Please help us find {facebookPostDto.ChildName}\nAge: {facebookPostDto.ChildAge} " +
                         $"\nIf you have seen this child or have any information, please leave a comment.";
        form.Add(new StringContent(caption), "caption");
        form.Add(new StringContent(configuration["Facebook:PageAccessToken"]), "access_token");


        var response = await httpClient.PostAsync(url, form);
        var responseContent = await response.Content.ReadAsStringAsync();

        var json = JsonDocument.Parse(responseContent);
        var post = json.RootElement.GetProperty("post_id").GetString();
        var parts = post.Split('_');
        string pageId = parts[0];
        string postId = parts[1];
        string postUrl = $"https://www.facebook.com/{pageId}/posts/{postId}";
        await facebookRepository.AddFacebookPostAsync(new FacebookPost()
        {
            Link = postUrl, Id = Guid.NewGuid().ToString(), QueryId = queryId
        });
        return postUrl;
    }

    public async Task<string> FinderPostToFacebook(FinderSearchDTO facebookPostDto,string queryId)
    {
        string url;

        url = $"https://graph.facebook.com/{configuration["Facebook:PageId"]}/photos";

        var form = new MultipartFormDataContent();

        var imageStream = facebookPostDto.Image.OpenReadStream();
        var imageContent = new StreamContent(imageStream);
        imageContent.Headers.ContentType = new MediaTypeHeaderValue(facebookPostDto.Image.ContentType);
        form.Add(imageContent, "source", facebookPostDto.Image.FileName);
        string caption =
            $"We found this child and are trying to help reunite them with their family.\nUnfortunately, we do not have any information — only this photo.\nIf you recognize this child or have any information about their family, please leave a comment. ";
        form.Add(new StringContent(caption), "caption");
        form.Add(new StringContent(configuration["Facebook:PageAccessToken"]), "access_token");


        var response = await httpClient.PostAsync(url, form);
        var responseContent = await response.Content.ReadAsStringAsync();

        var json = JsonDocument.Parse(responseContent);
        var post = json.RootElement.GetProperty("post_id").GetString();
        var parts = post.Split('_');
        string pageId = parts[0];
        string postId = parts[1];
        string postUrl = $"https://www.facebook.com/{pageId}/posts/{postId}";
        await facebookRepository.AddFacebookPostAsync(new FacebookPost()
        {
            Link = postUrl, Id = Guid.NewGuid().ToString(), QueryId = queryId
        });
        return postUrl;
    }
}