namespace ValetaxProj.Exceptions
{
    public class ErrorResponse
    {
        public Guid Id { get; set; }
        public string Type { get; set; }
        public ErrorMessage Data { get; set; }
    }
    public class ErrorMessage
    {
        public string Message { get; set; }
    }
}
