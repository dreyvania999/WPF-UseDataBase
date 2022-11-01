using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using WpfApp1.Classes;
using WpfApp1.Pages;

namespace WpfApp1
{

    //MixGol
    //Satana121251@

    //Admin
    //Admin@113

    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            FrameClass.MainFrame = fMain;
            FrameClass.MainFrame.Navigate(new ActivatedPage());
            DBaseClass.BD = new Entities1();
        }


        public static bool PasswordCheck(PasswordBox PB)
        {
            PB.ToolTip = "";
            Regex presenceALL = new Regex(@"^(?=.*[a-z]){3,}(?=.*[A-Z])(?=.*\d){2,}(?=.*[@$!#№%*?&=+_-])[0-9A-Za-z@$!%*?&=+_-]{8,}$");
            Regex presenceNum = new Regex("(?=.*[0-9].*[0-9])");
            Regex presenceLowerCase = new Regex("(?=.*[a-z].*[a-z].*[a-z])");
            Regex presenceHieghtCase = new Regex("(?=.*[A-Z])");
            Regex presenceSize = new Regex(@"^.{8,}$");
            Regex presenceSymbol = new Regex("(?=.*[@$!#№%*?&=+_-])");
            if (presenceALL.IsMatch(PB.Password.ToString()))
            {
                PB.ToolTip = ("Пароль подходит под все требования.");
            }
            else
            {
                if (!presenceNum.IsMatch(PB.Password.ToString()))
                {
                    PB.ToolTip += ("Введите 2 цифры.");
                }
                if (!presenceSize.IsMatch(PB.Password.ToString()))
                {
                    PB.ToolTip += ("Длина пароля<8 символов.");
                }
                if (!presenceLowerCase.IsMatch(PB.Password.ToString()))
                {
                    PB.ToolTip += ("Недостаточно строчных букв.");
                }
                if (!presenceHieghtCase.IsMatch(PB.Password.ToString()))
                {
                    PB.ToolTip += ("Недостаточно прописных букв.");
                }
                if (!presenceSymbol.IsMatch(PB.Password.ToString()))
                {
                    PB.ToolTip += ("Введите символ.");
                }
                return false;
            }

            return true;
        }


    }
}
