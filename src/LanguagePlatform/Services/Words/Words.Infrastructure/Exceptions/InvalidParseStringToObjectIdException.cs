namespace Words.Infrastructure.Exceptions
{
    public class InvalidParseStringToObjectIdException : Exception
    {
        public InvalidParseStringToObjectIdException(string repositoryName) : base($"Can't parse string to object Id in repository: {repositoryName}")
        {
        }
    }
}
