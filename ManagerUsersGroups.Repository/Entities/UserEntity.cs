namespace ManagerUsersGroups.Repository.Entities
{
    public class UserEntity : BaseEntity, IEquatable<UserEntity>
    {
        public string DisplayName { get; set; }

        public string Login { get; set; }

        public string Email { get; set; }

        public string HomeMDB { get; set; }

        public override int GetHashCode() =>
            DisplayName.GetHashCode() * 
            Login.GetHashCode() ^ 
            Email.GetHashCode() * 
            HomeMDB.GetHashCode() ^ 
            base.GetHashCode();

        public override bool Equals(object obj) =>
            obj is UserEntity ue 
            && DisplayName == ue.DisplayName 
            && Login == ue.Login 
            && Email == ue.Email
            && HomeMDB == ue.HomeMDB
            && base.Equals(obj);

        public bool Equals(UserEntity other) =>
            Equals(other);
    }
}
