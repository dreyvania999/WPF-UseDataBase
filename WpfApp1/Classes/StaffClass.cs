namespace WpfApp1.Classes
{
    internal class StaffClass
    {
        public static Table_Staff CurrentStaffEmploe;

        public static string StaffEmploeFullName => CurrentStaffEmploe.name + " " + CurrentStaffEmploe.surname + " " + CurrentStaffEmploe.patronymic;
    }
}
