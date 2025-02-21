namespace GameMasterDomain.Exceptions
{
    public class ErrorObject
    {
        /// <summary>
        /// Explain error.
        /// </summary>
        /// <example>Occur some error</example>
        public string Details { get; set; } = string.Empty;

        /// <summary>
        /// A code of error can help to solve the problem.
        /// </summary>
        /// <example>DB500</example>
        public string ErrorCode { get; set; } = string.Empty;
    }
}