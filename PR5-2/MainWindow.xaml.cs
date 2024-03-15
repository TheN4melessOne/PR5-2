using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
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
using static System.Net.Mime.MediaTypeNames;

namespace PR5_2
{
    public partial class MainWindow : Window
    {
        const int countDot = 70;
        List<double> dataList = new List<double>();
        DrawingGroup drawingGroup = new DrawingGroup();
        public MainWindow()
        {
            InitializeComponent();
            DataFill();
            Execute();
            Image.Source = new DrawingImage(drawingGroup);
        }

        void DataFill()
        {
            for (int i = 0; i < countDot - 1; i++)
            {
                double angle = (-Math.PI / 6) + (Math.PI / countDot) * i;//отметки на оси абсцисс - мера угла в радианах
                //был взят отрезок PI/6...5PI/6, т.к. далее функция начинает стремительно расти
                dataList.Add(Math.Pow((angle - 1), 3) + Math.Cos(2 * Math.Pow(angle, 3)));
            }
        }

        private void BackgroundFun()
        {
            GeometryDrawing geometryDrawing = new GeometryDrawing();

            RectangleGeometry rectangleGeometry = new RectangleGeometry();
            rectangleGeometry.Rect = new Rect(0, 0, 1, 1);
            geometryDrawing.Geometry = rectangleGeometry;

            geometryDrawing.Brush = Brushes.AliceBlue;
            geometryDrawing.Pen = new Pen(Brushes.Coral, 0.005);

            drawingGroup.Children.Add(geometryDrawing);
        }

        private void GridFun()
        {
            GeometryGroup geometryGroup = new GeometryGroup();
            for (int i = 1; i < 10; i++)
            {
                LineGeometry line = new LineGeometry(new Point(1.0, i * 0.1),
                    new Point(0, i * 0.1));
                geometryGroup.Children.Add(line);
            }

            GeometryDrawing geometryDrawing = new GeometryDrawing();
            geometryDrawing.Geometry = geometryGroup;

            geometryDrawing.Pen = new Pen(Brushes.Gray, 0.003);
            geometryDrawing.Brush = Brushes.Beige;

            drawingGroup.Children.Add(geometryDrawing);
        }

        private void SinFun()
        {
            GeometryGroup geometryGroup = new GeometryGroup();
            for (int i = 0; i < dataList.Count - 1; i++)
            {
                LineGeometry line = new LineGeometry(new Point((double)i / countDot,
                    0.5 - (dataList[i] / 10)),
                    new Point((double)(i + 1) / countDot,
                    0.5 - (dataList[i + 1] / 10)));
                geometryGroup.Children.Add(line);
            }

            GeometryDrawing geometryDrawing = new GeometryDrawing();
            geometryDrawing.Geometry = geometryGroup;

            geometryDrawing.Pen = new Pen(Brushes.Blue, 0.005);
            drawingGroup.Children.Add(geometryDrawing);
        }

        private void MakerFun()
        {
            GeometryGroup geometryGroup = new GeometryGroup();
            for (int i = 0; i < 10; i++)
            {
                FormattedText formattedText = new FormattedText(
                    String.Format("{0,7:F}", 5 - i),
                    CultureInfo.InvariantCulture, FlowDirection.LeftToRight,
                    new Typeface("Verdana"), 0.05, Brushes.Black);
                formattedText.SetFontWeight(FontWeights.Bold);

                Geometry geometry = formattedText.BuildGeometry(
                    new Point(-0.2, i * 0.1 - 0.03));
                geometryGroup.Children.Add(geometry);
            }

            GeometryDrawing geometryDrawing = new GeometryDrawing();
            geometryDrawing.Geometry = geometryGroup;
            geometryDrawing.Pen = new Pen(Brushes.Gray, 0.003);
            geometryDrawing.Brush = Brushes.LightGray;
            drawingGroup.Children.Add(geometryDrawing);
        }

        void Execute()
        {
            BackgroundFun();
            GridFun();
            SinFun();
            MakerFun();
        }
    }
}