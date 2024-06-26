namespace OtmApi.Utils.Exceptions;

[Serializable]
public class MatchNotFoundException : Exception
{
    public MatchNotFoundException(int matchId) : base($"match with id {matchId} not found.")
    {
    }
}
