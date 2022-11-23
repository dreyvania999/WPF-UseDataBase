using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Media;
using WpfApp1.Classes;
using MessageBox = System.Windows.MessageBox;

namespace WpfApp1.Pages.PageForUsers
{
    /// <summary>
    /// Логика взаимодействия для AcountPage.xaml
    /// </summary>
    public partial class AcountPage : Page
    {
        private List<Table_Employee_Photo> ListPhoto;
        private int IdCurrentPhoto;
        public AcountPage()
        {
            InitializeComponent();

            tbFullName.Text += StaffClass.StaffEmploeFullName;
            tbBirthdate.Text += StaffClass.CurrentStaffEmploe.date_birth.ToString("D");

            tbLogin.Text += StaffClass.CurrentStaffEmploe.login;
            tbRole.Text += StaffClass.CurrentStaffEmploe.Table_Role.role.ToLower();

            ListPhoto = DBaseClass.BD.Table_Employee_Photo.Where(x => x.id_staff == StaffClass.CurrentStaffEmploe.id_staff).ToList();
            IdCurrentPhoto = ListPhoto.Count - 1;
            Table_Employee_Photo photo;

            if (IdCurrentPhoto != -1)
            {
                photo = ListPhoto[IdCurrentPhoto];
            }
            else
            {
                photo = null;
            }

            imgPhoto.Source = Images.ImageEmploe.GetBitmapImage(photo);
            imgPhoto.Stretch = Stretch.Uniform;


        }

        private void BtnAddPhoto_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.ShowDialog();
            if (ofd.FileName != "")
            {
                if (Images.ImageEmploe.AddPhoto(ofd.FileName, StaffClass.CurrentStaffEmploe.id_staff))
                {
                    FrameClass.MainFrame.Navigate(new AcountPage());
                    MessageBox.Show("Фото успешно добавлено", "Личный кабинет", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Возникла ошибка! Обратитесь к администратору", "Личный кабинет", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        private void BtnDelPhoto_Click(object sender, RoutedEventArgs e)
        {

            if (IdCurrentPhoto == -1)
            {
                MessageBox.Show("Нельзя удалить стандартное изображение!", "Личный кабинет", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            Table_Employee_Photo photo = ListPhoto[IdCurrentPhoto];

            DBaseClass.BD.Table_Employee_Photo.Remove(photo);
            DBaseClass.BD.SaveChanges();

            ListPhoto = DBaseClass.BD.Table_Employee_Photo.Where(x => x.id_staff == StaffClass.CurrentStaffEmploe.id_staff).ToList();

            IdCurrentPhoto--;

            if (IdCurrentPhoto == -1)
            {
                photo = null;
                btnBackPhoto.Visibility = Visibility.Hidden;
            }
            else
            {
                if (IdCurrentPhoto == 0)
                {
                    btnBackPhoto.Visibility = Visibility.Hidden;
                }

                photo = ListPhoto[IdCurrentPhoto];
            }
            imgPhoto.Source = Images.ImageEmploe.GetBitmapImage(photo);
        }

        private void BtnChangePhoto_Click(object sender, RoutedEventArgs e)
        {
            if (btnChangePhoto.Content.ToString() == "Изменить")
            {
                btnChangePhoto.Content = "Сохранить";
                if (IdCurrentPhoto != -1)
                {
                    btnBackPhoto.Visibility = Visibility.Visible;
                }

            }
            else
            {
                btnChangePhoto.Content = "Изменить";
                btnBackPhoto.Visibility = Visibility.Hidden;
                btnNextPhoto.Visibility = Visibility.Hidden;

                if (IdCurrentPhoto != -1)
                {
                    Table_Employee_Photo photo = ListPhoto[IdCurrentPhoto];
                    Table_Employee_Photo newPhoto = new Table_Employee_Photo()
                    {
                        id_staff = photo.id_staff,
                        binary_photo = photo.binary_photo,
                        path_photo = photo.path_photo
                    };
                    DBaseClass.BD.Table_Employee_Photo.Remove(photo);
                    DBaseClass.BD.Table_Employee_Photo.Add(newPhoto);
                    DBaseClass.BD.SaveChanges();

                    FrameClass.MainFrame.Navigate(new AcountPage());
                }
            }
        }

        private void BtnBackPhoto_Click(object sender, RoutedEventArgs e)
        {
            IdCurrentPhoto--;
            Table_Employee_Photo photo = ListPhoto[IdCurrentPhoto];

            if (photo != null)
            {
                imgPhoto.Source = Images.ImageEmploe.GetBitmapImage(photo);
            }

            if (IdCurrentPhoto == 0)
            {
                btnBackPhoto.Visibility = Visibility.Hidden;
            }

            btnNextPhoto.Visibility = Visibility.Visible;
        }

        private void BtnNextPhoto_Click(object sender, RoutedEventArgs e)
        {
            IdCurrentPhoto++;
            Table_Employee_Photo photo = ListPhoto[IdCurrentPhoto];

            if (photo != null)
            {
                imgPhoto.Source = Images.ImageEmploe.GetBitmapImage(photo);
            }

            if (IdCurrentPhoto == ListPhoto.Count - 1)
            {
                btnNextPhoto.Visibility = Visibility.Hidden;
            }

            btnBackPhoto.Visibility = Visibility.Visible;
        }

        private void BtnChangePersonal_Click(object sender, RoutedEventArgs e)
        {
            ChangingPersonalInform acp = new ChangingPersonalInform();
            acp.ShowDialog();
            FrameClass.MainFrame.Navigate(new AcountPage());
        }

        private void BtnChangeAccount_Click(object sender, RoutedEventArgs e)
        {
            ChangingAccount ac = new ChangingAccount();
            ac.ShowDialog();
        }

        private void BtnAddSomePhotos_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = true;

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                bool check = false;

                foreach (string path in ofd.FileNames)
                {
                    if (!Images.ImageEmploe.AddPhoto(path, StaffClass.CurrentStaffEmploe.id_staff))
                    {
                        check = true;
                    }
                }

                FrameClass.MainFrame.Navigate(new AcountPage());

                if (check)
                {
                    MessageBox.Show("Часть фото не удалось загрузить! Обратитесь к администратору", "Личный кабинет", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Фото успешно добавлены", "Личный кабинет", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

    }
}