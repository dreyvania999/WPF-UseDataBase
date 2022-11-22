using System;
using System.Drawing;
using System.IO;
using System.Windows.Media.Imaging;

namespace WpfApp1.Classes
{
    /// <summary>
    /// Класс для работы с изображениями
    /// </summary>
    internal class Images
    {
        /// <summary>
        /// Преобразует байтовый массив в изображение
        /// </summary>
        /// <param name="photo">Байтовый массив (объект типа Photos)</param>
        /// <returns>Объект типа BitmapImage (изображение)</returns>
        public static BitmapImage GetBitmapImage(Table_Employee_Photo photo)
        {
            if (photo != null)
            {
                byte[] array = photo.binary_photo;
                BitmapImage image = new BitmapImage();

                using (MemoryStream ms = new MemoryStream(array))
                {
                    image.BeginInit();
                    image.StreamSource = ms;
                    image.CacheOption = BitmapCacheOption.OnLoad;
                    image.EndInit();
                }

                return image;
            }

            return new BitmapImage(new Uri("\\Resources\\yuterra.png", UriKind.RelativeOrAbsolute));
        }

        /// <summary>
        /// Добавляет фото в базу данных
        /// </summary>
        /// <param name="path">Путь к фото</param>
        /// <param name="id">Код </param>
        /// <returns>true, если добавление прошло удачно, иначе - false</returns>
        public static bool AddPhoto(string path, int id)
        {
            try
            {
                Table_Employee_Photo photo = new Table_Employee_Photo();
                photo.id_staff = id;

                Image sdi = Image.FromFile(path);
                ImageConverter ic = new ImageConverter();

                byte[] array = (byte[])ic.ConvertTo(sdi, typeof(byte[]));
                photo.binary_photo = array;

                DBaseClass.BD.Table_Employee_Photo.Add(photo);
                DBaseClass.BD.SaveChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}