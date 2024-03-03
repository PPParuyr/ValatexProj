namespace ValetaxProj.Exceptions
{
    public class SecureException : Exception
    {
        public override string StackTrace { get;}
        public SecureException(string message, string stackTrace) : base(message)
        {
            StackTrace = stackTrace;
        }
    }
}
