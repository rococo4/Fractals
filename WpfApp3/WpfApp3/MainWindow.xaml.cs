using System;
using FractalsLibrary;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using Microsoft.Win32;
using System.IO;
using System.Windows.Media.Imaging;

/// <summary>
/// Пространство имен.
/// </summary>
namespace WpfApp3
{
    /// <summary>
    /// Главное окно.
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Индекатор для выбранной кнопки.
        /// </summary>
        private int whichButtonIsChoosen = 0;
        /// <summary>
        /// Главное окно.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
           
            canvas.Width = 707;
            canvas.Height = 340;
            canvas.HorizontalAlignment = HorizontalAlignment.Center;
            canvas.VerticalAlignment = VerticalAlignment.Center;
            canvas.LayoutTransform = scaleTransform;
        }
        /// <summary>
        /// Изменение масштаба.
        /// </summary>
        public ScaleTransform scaleTransform = new();

        /// <summary>
        /// Изменение значения любого слайдера.
        /// </summary>
        /// <param name="sender"> Издатель.</param>
        /// <param name="e"> Информация о событии.</param>
        private void LevelSliderValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            
            if (canvas != null)
            {
                canvas.Children.Clear();
                switch (whichButtonIsChoosen)
                {
                    case 0:
                        var tree = new TreeFractal(KoefSlider.Value, (int)RightAngleSlider.Value, (int)LeftAngleSlider.Value);
                        tree.DrawFractal(canvas, 0, canvas.ActualHeight / 4, (int)LevelSlider.Value - 1, canvas.ActualWidth / 2, canvas.ActualHeight);
                        break;
                    case 4:
                        var cantor = new ManyCantor((int)KoefSlider.Value);
                        cantor.DrawFractal(canvas, 0,canvas.ActualHeight/7 , (int)LevelSlider.Value - 1, 50, canvas.ActualWidth - 50);
                        break;
                    case 2:
                        var carpet = new Carpet();
                        carpet.DrawFractal(canvas, canvas.ActualWidth * 5 / 14, canvas.ActualHeight * 0.1, (int)LevelSlider.Value,5*canvas.ActualHeight / 6 , 5 * canvas.ActualHeight / 6);
                        break;
                    case 3:
                        var triangle = new Triangle();
                        triangle.DrawTriangle(canvas,canvas.ActualWidth / 3, 2 * canvas.ActualWidth / 3 ,2*canvas.ActualHeight / 3, 2*canvas.ActualHeight / 3, canvas.ActualWidth / 2,canvas.ActualHeight / 3,(int)LevelSlider.Value);
                        break;
                    case 1:
                        var curve = new CurveCox();
                        curve.DrawFractal(canvas, canvas.ActualWidth / 4, canvas.ActualHeight / 2, (int)LevelSlider.Value - 1, canvas.ActualWidth -  canvas.ActualWidth / 4, canvas.ActualHeight / 2);
                        break;
                }
            }
        }

        /// <summary>
        /// Кнопка для дерева.
        /// </summary>
        /// <param name="sender"> Издатель.</param>
        /// <param name="e"> Информация о событии.</param>
        private void TreeButtonClick(object sender, RoutedEventArgs e)
        {
            KoefSlider.Minimum = 1.4;
            KoefSlider.Maximum = 2.5;
            KoefSlider.TickFrequency = 0.1;
            KoefLabel.Content = "Коэффицент";
            RightAngleLabel.Visibility = Visibility.Visible;
            LeftAngleLabel.Visibility = Visibility.Visible;
            KoefLabel.Visibility = Visibility.Visible;
            KoefSlider.Value = 1.4;
            whichButtonIsChoosen = 0;
            LevelSlider.Value = 1;
            LevelSlider.Maximum = 10;
            LeftAngleSlider.Visibility = Visibility.Visible;
            RightAngleSlider.Visibility = Visibility.Visible;
            KoefSlider.Visibility = Visibility.Visible;
            KoefText.Visibility = Visibility.Visible;
            RightAngleText.Visibility = Visibility.Visible;
            LeftAngleText.Visibility = Visibility.Visible;
            canvas.Children.Clear();
        }

        /// <summary>
        /// Кнопка для кривой Коха.
        /// </summary>
        /// <param name="sender"> Издатель.</param>
        /// <param name="e"> Информация о событии.</param>
        private void CurveButtonClick(object sender, RoutedEventArgs e)
        {
            RightAngleLabel.Visibility = Visibility.Collapsed;
            LeftAngleLabel.Visibility = Visibility.Collapsed;
            KoefLabel.Visibility = Visibility.Collapsed;
            whichButtonIsChoosen = 1;
            LevelSlider.Value = 1;
            LevelSlider.Maximum = 6;
            LeftAngleSlider.Visibility = Visibility.Collapsed;
            RightAngleSlider.Visibility = Visibility.Collapsed;
            KoefSlider.Visibility = Visibility.Collapsed;
            KoefText.Visibility = Visibility.Collapsed;
            RightAngleText.Visibility = Visibility.Collapsed;
            LeftAngleText.Visibility = Visibility.Collapsed;
            canvas.Children.Clear();
        }

        /// <summary>
        /// Кнопка для ковра Серпинского.
        /// </summary>
        /// <param name="sender"> Издатель.</param>
        /// <param name="e"> Информация о событии.</param>
        private void CarpetButtonClick(object sender, RoutedEventArgs e)
        {
            RightAngleLabel.Visibility = Visibility.Collapsed;
            LeftAngleLabel.Visibility = Visibility.Collapsed;
            KoefLabel.Visibility = Visibility.Collapsed;
            canvas.Children.Clear();
            whichButtonIsChoosen = 2;
            LevelSlider.Value = 1;
            LevelSlider.Maximum = 5;
            LeftAngleSlider.Visibility = Visibility.Collapsed;
            RightAngleSlider.Visibility = Visibility.Collapsed;
            KoefSlider.Visibility = Visibility.Collapsed;
            KoefText.Visibility = Visibility.Collapsed;
            RightAngleText.Visibility = Visibility.Collapsed;
            LeftAngleText.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// Кнопка для треугольника Серпинского.
        /// </summary>
        /// <param name="sender"> Издатель.</param>
        /// <param name="e"> Информация о событии.</param>
        private void TriangleButtonClick(object sender, RoutedEventArgs e)
        {
            RightAngleLabel.Visibility = Visibility.Collapsed;
            LeftAngleLabel.Visibility = Visibility.Collapsed;
            KoefLabel.Visibility = Visibility.Collapsed;
            canvas.Children.Clear();
            whichButtonIsChoosen = 3;
            LevelSlider.Value = 1;
            LevelSlider.Maximum = 7;
            LeftAngleSlider.Visibility = Visibility.Collapsed;
            RightAngleSlider.Visibility = Visibility.Collapsed;
            KoefSlider.Visibility = Visibility.Collapsed;
            KoefText.Visibility = Visibility.Collapsed;
            RightAngleText.Visibility = Visibility.Collapsed;
            LeftAngleText.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// Кнопка для множества Кантора.
        /// </summary>
        /// <param name="sender"> Издатель.</param>
        /// <param name="e"> Информация о событии.</param>
        private void CantorButtonClick(object sender, RoutedEventArgs e)
        {
            RightAngleLabel.Visibility = Visibility.Collapsed;
            LeftAngleLabel.Visibility = Visibility.Collapsed;
            KoefLabel.Visibility = Visibility.Visible;
            LevelSlider.Value = 1;
            whichButtonIsChoosen = 4;
            LevelSlider.Maximum = 7;
            KoefSlider.Value = 10;
            KoefLabel.Content = "Расстояние";
            KoefSlider.Minimum = 10;
            KoefSlider.Maximum = 50;
            KoefSlider.TickFrequency = 10;
            LeftAngleSlider.Visibility = Visibility.Collapsed;
            RightAngleSlider.Visibility = Visibility.Collapsed;
            KoefSlider.Visibility = Visibility.Visible;
            KoefText.Visibility = Visibility.Visible;
            RightAngleText.Visibility = Visibility.Collapsed;
            LeftAngleText.Visibility = Visibility.Collapsed;
            canvas.Children.Clear();

        }

        /// <summary>
        /// Кнопка для сохранения.
        /// </summary>
        /// <param name="sender"> Издатель.</param>
        /// <param name="e"> Информация о событии.</param>
        private void btnSaveFileClick(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Image (.png)|*.png";
           
            if (saveFileDialog.ShowDialog() == true)
            {
                RenderTargetBitmap bmp = new RenderTargetBitmap((int)canvas.ActualWidth, (int)canvas.ActualHeight, 96d, 96d, PixelFormats.Pbgra32);
                canvas.Measure(new Size((int)canvas.ActualWidth, (int)canvas.ActualHeight));
                canvas.Arrange(new Rect(new Size((int)canvas.ActualWidth, (int)canvas.ActualHeight)));
                bmp.Render(canvas);
                PngBitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(bmp));
                using (FileStream file = File.Create(saveFileDialog.FileName))
                {
                    encoder.Save(file);
                }
            }    
                
        }

        /// <summary>
        /// Кнопка для увелечения в 2 раза.
        /// </summary>
        /// <param name="sender"> Издатель.</param>
        /// <param name="e"> Информация о событии.</param>
        private void X2Zoom(object sender, RoutedEventArgs e)
        {
            scaleTransform.ScaleX = 2;
            scaleTransform.ScaleY = 2;
        }

        /// <summary>
        /// Кнопка для увелечения в 3 раза.
        /// </summary>
        /// <param name="sender"> Издатель.</param>
        /// <param name="e"> Информация о событии.</param>
        private void X3Zoom(object sender, RoutedEventArgs e)
        {
            scaleTransform.ScaleX = 3;
            scaleTransform.ScaleY = 3;
        }

        /// <summary>
        /// Кнопка для увелечения в 5 раз.
        /// </summary>
        /// <param name="sender"> Издатель.</param>
        /// <param name="e"> Информация о событии.</param>
        private void  X5Zoom(object sender, RoutedEventArgs e)
        {
            scaleTransform.ScaleX = 5;
            scaleTransform.ScaleY = 5;
        }

        /// <summary>
        /// Кнопка для сброса увелечения.
        /// </summary>
        /// <param name="sender"> Издатель.</param>
        /// <param name="e"> Информация о событии.</param>
        private void Reset(object sender, RoutedEventArgs e)
        {
            scaleTransform.ScaleX = 1;
            scaleTransform.ScaleY = 1;
        }
    }  
}

