using ClassRoomWPF_DB_TP.Models;

namespace ClassRoomWPF_DB_TP;

public static class UserSession
{
    public static Person CurrentUser { get; set; } = null!;
}
