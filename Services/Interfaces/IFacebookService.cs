using Reunite.DTOs.QueryDTOs;
namespace Reunite.Services.Interfaces;

public interface IFacebookService
{ 
    Task<string> ParentPostToFacebook(ParentSearchDTO facebookPostDto,string queryId);
    Task<string> FinderPostToFacebook(FinderSearchDTO facebookPostDto,string queryId);

}