namespace OtmApi.Utils.Exceptions;

[Serializable]
public class NotFoundException : Exception
{
    public NotFoundException() : base()
    {
    }
    public NotFoundException(string type, int id) : base($"{type} with id {id} not found")
    {
    }
    public NotFoundException(string type, decimal id) : base($"{type} with id {id} not found")
    {
    }
}
