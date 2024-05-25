namespace Login.API.Behaviours
{
    public class ResultException : Exception
    {
        public ResultException(string message)
            : base(message)
        {
        }
    }
}
