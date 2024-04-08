namespace ManagerUsersGroups.Repository.Exceptions
{
    public class RepositoryNotAccessException : RepositoryException
    {
        public RepositoryNotAccessException() { }

        public RepositoryNotAccessException(string message) : base(message) { }

        public RepositoryNotAccessException(string message, Exception innerException) : base(message, innerException) { }
    }
}
