namespace OtmApi.Utils.Exceptions;
[Serializable]
public class ConflitctException : Exception
{
    public ConflitctException() : base("This Tournament already has a Team with this name")
    {
    }
}
