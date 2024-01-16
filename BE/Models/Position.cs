namespace BE.Models
{
    public class Position : BaseNameEntity
    {
        public ICollection<Employee>? Employees { get; set; }
    }
}
