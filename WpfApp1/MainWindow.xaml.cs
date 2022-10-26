using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp1.Classes;
using WpfApp1.Pages;

namespace WpfApp1
{
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
        }

        
private bool PasswordCheck(PasswordBox PB)
        {
            PB.ToolTip = "мпа";
            Regex presenceALL = new Regex(@"^(?=.*[a-z]){3,}(?=.*[A-Z])(?=.*\d){2,}(?=.*[@$!#№%*?&=+_-])[0-9A-Za-z@$!%*?&=+_-]{8,}$");
            Regex presenceNum = new Regex(@"^.*\d.*\d$");
            Regex presenceLowerCase = new Regex(@"^.*[a-z].*[a-z].*[a-z].*$");
            Regex presenceHieghtCase = new Regex(@"^.*[A-Z].*$");
            Regex presenceSize = new Regex(@"^.{8,}$");
            Regex presenceSymbol = new Regex(@"^.*[@$!#№%*?&=+_-].*$");
            if (presenceALL.IsMatch(PB.Password.ToString()))
            {
                PB.ToolTip = (" presenceALL ");
            }
            else
            {
                if (!presenceNum.IsMatch(PB.Password.ToString()))
                {
                    PB.ToolTip += (" presenceNum ");
                }
                if (!presenceSize.IsMatch(PB.Password.ToString()))
                {
                    PB.ToolTip += (" presenceSize ");
                }
                if (!presenceLowerCase.IsMatch(PB.Password.ToString()))
                {
                    PB.ToolTip += (" presenceLowercase ");
                }
                if (!presenceHieghtCase.IsMatch(PB.Password.ToString()))
                {
                    PB.ToolTip += (" presenceHieghtCase ");
                }
                if (!presenceSymbol.IsMatch(PB.Password.ToString()))
                {
                    PB.ToolTip += (" presenceSymbol ");
                }
                return false;
            }
            
            return true;
        }

       
    }
}
