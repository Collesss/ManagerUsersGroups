namespace ManagerUsersGroups.Repository.Entities
{
    public class UserEntity : BaseEntity
    {
        public string DisplayName { get; set; }

        public string Login { get; set; }

        public string Email { get; set; }
    }
}
