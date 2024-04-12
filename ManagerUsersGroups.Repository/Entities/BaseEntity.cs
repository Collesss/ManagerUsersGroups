namespace ManagerUsersGroups.Repository.Entities
{
    public class BaseEntity: IEquatable<BaseEntity>
    {
        public string SID { get; set; }

        public string DistinguishedName { get; set; }

        //public string CanonicalName { get; set; }

        public string CommonName { get; set; }

        public override int GetHashCode() =>
            SID.GetHashCode() * DistinguishedName.GetHashCode() ^ CommonName.GetHashCode();

        public override bool Equals(object obj) =>
            obj is BaseEntity entity && 
            SID == entity.SID &&
            CommonName == entity.CommonName &&
            DistinguishedName == entity.DistinguishedName;

        public bool Equals(BaseEntity other) =>
            Equals(other);
    }
}
