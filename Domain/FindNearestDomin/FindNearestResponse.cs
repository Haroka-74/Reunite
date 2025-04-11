namespace Reunite.Domain;

public class FindNearestResponse
{
    public FindNearestErrorResponse? Error { get; set; }
    public FindNearestSuccessResponse? Success { get; set; }
    public bool Ok => Success!=null;
}