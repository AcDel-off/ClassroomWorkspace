﻿using System.Collections.Generic;

namespace ClassRoomWPF_DB_TP.Models
{
    public class Teacher : Person
    {
        public override string RoleName => "Учитель";
        public string SubjectName { get; set; } = "";
    }
}
