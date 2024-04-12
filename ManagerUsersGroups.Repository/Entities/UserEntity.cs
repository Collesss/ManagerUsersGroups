namespace ManagerUsersGroups.Repository.Entities
{
    public class UserEntity : BaseEntity, IEquatable<UserEntity>
    {
        public string DisplayName { get; set; }

        public string Login { get; set; }

        public string Email { get; set; }

        public override int GetHashCode() =>
            DisplayName.GetHashCode() * Login.GetHashCode() ^ Email.GetHashCode() ;

        public override bool Equals(object obj) =>
            obj is UserEntity ue 
            && DisplayName == ue.DisplayName 
            && Login == ue.Login 
            && Email == ue.Email
            && base.Equals(obj);

        public bool Equals(UserEntity other) =>
            Equals(other);
    }
}
