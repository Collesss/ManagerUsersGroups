namespace ManagerUsersGroups.Repository.Entities
{
    public class BaseEntity
    {
        public string SID { get; set; }

        public string DistinguishedName { get; set; }

        //public string CanonicalName { get; set; }

        public string CommonName { get; set; }
    }
}
