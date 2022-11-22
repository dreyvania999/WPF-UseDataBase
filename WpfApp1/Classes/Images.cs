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
        public class ImageEmploe
        {
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

                return new BitmapImage(new Uri("\\Resources\\Staff_1.png", UriKind.RelativeOrAbsolute));
            }


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

        public class ImageChemicals
        {
            public static BitmapImage GetBitmapImage(Table_Chemicals_Photo photo)
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

                return new BitmapImage(new Uri("\\Resources\\Ariel.png", UriKind.RelativeOrAbsolute));
            }


            public static bool AddPhoto(string path, int id)
            {
                try
                {
                    Table_Chemicals_Photo photo = new Table_Chemicals_Photo();
                    photo.id_chemicals = id;

                    Image sdi = Image.FromFile(path);
                    ImageConverter ic = new ImageConverter();

                    byte[] array = (byte[])ic.ConvertTo(sdi, typeof(byte[]));
                    photo.binary_photo = array;

                    DBaseClass.BD.Table_Chemicals_Photo.Add(photo);
                    DBaseClass.BD.SaveChanges();

                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        public class ImageHoushold
        {
            public static BitmapImage GetBitmapImage(Table_Houshold_Photo photo)
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

                return new BitmapImage(new Uri("\\Resources\\Gubka.png", UriKind.RelativeOrAbsolute));
            }


            public static bool AddPhoto(string path, int id)
            {
                try
                {
                    Table_Houshold_Photo photo = new Table_Houshold_Photo();
                    photo.id_houshold = id;

                    Image sdi = Image.FromFile(path);
                    ImageConverter ic = new ImageConverter();

                    byte[] array = (byte[])ic.ConvertTo(sdi, typeof(byte[]));
                    photo.binary_photo = array;

                    DBaseClass.BD.Table_Houshold_Photo.Add(photo);
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
}