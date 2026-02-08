namespace Shared.DDD
{
    public interface IEntity
    {
        public DateTime? CreatedDate { get; set; }

        public string? CreateBy {  get; set; }

        public DateTime? LastModified { get; set; }

        public string? LastModifiedBy { get;set; }
    }

    public interface IEntity<T> : IEntity
    {
        public T Id { get; set; }
    }
}
