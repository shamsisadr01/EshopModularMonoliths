namespace Shared.DDD
{
    public class Entity<T> : IEntity<T>
    {
        public T Id { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string? CreateBy { get; set; }

        public DateTime? LastModified { get; set; }

        public string? LastModifiedBy { get; set; }
    }
}
