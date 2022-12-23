using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace WpfApp1.Pages
{
    /// <summary>
    /// Логика взаимодействия для Reclam.xaml
    /// </summary>
    public partial class Reclam : Page
    {
        public Reclam()
        {
            InitializeComponent();

            DoubleAnimation tbFontAnimation = new DoubleAnimation
            {
                From = 20,
                To = 30,
                Duration = TimeSpan.FromSeconds(1),
                RepeatBehavior = RepeatBehavior.Forever,
                AutoReverse = true
            };
            textblocAnim.BeginAnimation(FontSizeProperty, tbFontAnimation);

            DoubleAnimation imageWidthAnimation = new DoubleAnimation
            {
                From = 200,
                To = 300,
                Duration = TimeSpan.FromSeconds(1),
                RepeatBehavior = RepeatBehavior.Forever,
                AutoReverse = true
            };
            imageAnim.BeginAnimation(WidthProperty, imageWidthAnimation);

            DoubleAnimation btnWidthAnimation = new DoubleAnimation
            {
                From = 200,
                To = 300,
                Duration = TimeSpan.FromSeconds(1),
                RepeatBehavior = RepeatBehavior.Forever,
                AutoReverse = true
            };
            buttonAnim.BeginAnimation(WidthProperty, btnWidthAnimation);

            DoubleAnimation btnHeightAnimation = new DoubleAnimation
            {
                From = 50,
                To = 70,
                Duration = TimeSpan.FromSeconds(1),
                RepeatBehavior = RepeatBehavior.Forever,
                AutoReverse = true
            };
            buttonAnim.BeginAnimation(HeightProperty, btnHeightAnimation);

            Color Cstart = Color.FromRgb(255, 0, 0);
            buttonAnim.Background = new SolidColorBrush(Cstart);

            ColorAnimation btnBackgroundAnimation = new ColorAnimation
            {
                From = Color.FromRgb(248, 168, 27),
                To = Color.FromRgb(0, 175, 176),
                Duration = TimeSpan.FromSeconds(2),
                RepeatBehavior = RepeatBehavior.Forever,
                AutoReverse = true
            };
            buttonAnim.Background.BeginAnimation(SolidColorBrush.ColorProperty, btnBackgroundAnimation);



        }

        private void imageAnim_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            imageAnimSecond.Visibility = Visibility.Visible;
            DoubleAnimation imageWidthAnimation = new DoubleAnimation
            {
                From = 100,
                To = 200,
                Duration = TimeSpan.FromSeconds(1),
                RepeatBehavior = RepeatBehavior.Forever,
                AutoReverse = true
            };
            imageAnimSecond.BeginAnimation(WidthProperty, imageWidthAnimation);

           
            DoubleAnimation rotateAnimation = new DoubleAnimation()
            {
                From = 0,
                To = 360,
                Duration = new Duration(TimeSpan.FromSeconds(5.0)),
                RepeatBehavior = new RepeatBehavior(TimeSpan.FromSeconds(10.0)),
                AutoReverse = true
            };
            DoubleAnimation opacityAnimation = new DoubleAnimation()
            {
                From = 1.0,
                To = 0.0,
                BeginTime = TimeSpan.FromSeconds(5.0),
                Duration = new Duration(TimeSpan.FromSeconds(5.0)),
                RepeatBehavior = RepeatBehavior.Forever,
                AutoReverse = true
            };
            Storyboard.SetTarget(rotateAnimation, imageAnimSecond);
            imageAnimSecond.BeginAnimation(WidthProperty, opacityAnimation);
            imageAnimSecond.BeginAnimation(WidthProperty, rotateAnimation);



        }
    }
}
