namespace ManagerUsersGroups.Repository.Entities
{
    public class GroupEntity : BaseEntity, IEquatable<GroupEntity>
    {
        public override bool Equals(object obj) =>
            obj is GroupEntity && 
            Equals(obj);

        public bool Equals(GroupEntity other) =>
            base.Equals(other);

        public override int GetHashCode() =>
            base.GetHashCode();
    }
}
