using System.Text.Json;
using Microsoft.Net.Http.Headers;
using Reunite.DTOs;
using Reunite.DTOs.FacebookDTOs;
using Reunite.Services.Interfaces;
using MediaTypeHeaderValue = System.Net.Http.Headers.MediaTypeHeaderValue;

namespace Reunite.Services.Implementations;

public class FacebookService : IFacebookService
{
    private readonly HttpClient httpClient;
    private readonly IConfiguration configuration;

    public FacebookService(HttpClient httpClient, IConfiguration configuration)
    {
        this.httpClient = httpClient;
        this.configuration = configuration;
    }

    public async Task<string> ParentPostToFacebook(ParentSearchDTO facebookPostDto)
    {
        string url;

        url = $"https://graph.facebook.com/{configuration["Facebook:PageId"]}/photos";

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
        var postId = json.RootElement.GetProperty("post_id").GetString();
        return postId;
    }

    public async Task<string> FinderPostToFacebook(FinderSearchDTO facebookPostDto)
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
        var postId = json.RootElement.GetProperty("post_id").GetString();
        return postId;
    }
}