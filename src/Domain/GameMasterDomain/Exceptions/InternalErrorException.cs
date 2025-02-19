namespace GameMasterDomain.Exceptions
{
    public class InternalErrorException : Exception
    {
        public ErrorObject Error { get; set; }

        public InternalErrorException(Exception innerException, ErrorObject error):base(error.Details, innerException)
        {
            Error = error;
        }
    }
}
