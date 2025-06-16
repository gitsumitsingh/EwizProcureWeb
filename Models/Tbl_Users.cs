namespace EwizProcureWeb.Models
{
    public class Tbl_Users
    {
        public int UserId { get; set; }
        public string EmailId { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public bool Gender { get; set; }
        public System.DateTime CreatedDateUtc { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public System.DateTime LastModifiedDateUtc { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
    }
}
