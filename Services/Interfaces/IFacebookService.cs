using Reunite.DTOs.QueryDTOs;
using Reunite.DTOs.SearchDTOs;
namespace Reunite.Services.Interfaces;

public interface IFacebookService
{ 
    Task<string> ParentPostToFacebook(ParentSearchDTO facebookPostDto,string queryId);
    Task<string> FinderPostToFacebook(FinderSearchDTO facebookPostDto,string queryId);

}