using System.Collections.Generic;

namespace ClassRoomWPF_DB_TP.Models
{
    public class Student : Person
    {
        public override string RoleName => "Ученик";
        public List<Grade> Grades { get; set; } = new();
    }
}
