namespace ManagerUsersGroups.Repository.Exceptions
{
    public class RepositoryNotExistEntityException : RepositoryException
    {
        public string Sid { get; set; }

        public RepositoryNotExistEntityException() { }

        public RepositoryNotExistEntityException(string message) : base(message) { }

        public RepositoryNotExistEntityException(string sid, string message) : base(message) 
        {
            Sid = sid;
        }

        public RepositoryNotExistEntityException(string message, Exception innerException) : base(message, innerException) { }

        public RepositoryNotExistEntityException(string sid, string message, Exception innerException) : base(message, innerException) 
        {
            Sid = sid;
        }
    }
}
