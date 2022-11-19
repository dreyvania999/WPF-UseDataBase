namespace WpfApp1.Classes
{
    internal class StaffClass
    {
        public static Table_Staff CurrentStaffEmploe;

        public static string StaffEmploeFullName 
        { 
            get 
            {
                return CurrentStaffEmploe.name+" "+CurrentStaffEmploe.surname+" "+CurrentStaffEmploe.patronymic;
            }
        }
    }
}
