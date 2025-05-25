namespace ClassRoomWPF_DB_TP.Models
{
    public abstract class Person
    {
        public int Id { get; set; }
        public string FullName { get; set; } = "";
        public string Login { get; set; } = "";
        public string Password { get; set; } = "";

        public abstract string RoleName { get; }
    }
}
