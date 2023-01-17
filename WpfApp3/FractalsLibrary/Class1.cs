using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

/// <summary>
/// Пространство имен с фракталами.
/// </summary>
namespace FractalsLibrary
{
    /// <summary>
    /// Абстрактные класс для всех фракталов.
    /// </summary>
    public abstract class Fractal
    {
        /// <summary>
        /// Глубина рекурсии, не используется. Потому что проще передавать парметры сразу в функцию.
        /// </summary>
        public static double Level { get; set; }

        /// <summary>
        /// Функция для рисования фракталов.
        /// </summary>
        /// <param name="canvas">Канвас.</param>
        /// <param name="length">Длина.</param>
        /// <param name="angle">Угол.</param>
        /// <param name="level">Глубина рекурсии.</param>
        /// <param name="xStart">Начало X.</param>
        /// <param name="yStart">Начало Y.</param>
        public abstract void DrawFractal(Canvas canvas, double length, double angle, int level, double xStart, double yStart);
    }

    /// <summary>
    /// Фрактальное дерево.
    /// </summary>
    public class TreeFractal : Fractal
    {
        /// <summary>
        /// Коэффицент.
        /// </summary>
        private double coef;

        /// <summary>
        /// Правый угол.
        /// </summary>
        private double rightAngle;

        /// <summary>
        /// Левый угол.
        /// </summary>
        private double leftAngle;


        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="coef"> Коэффицент.</param>
        /// <param name="rightAngle"> Правый угол.</param>
        /// <param name="leftAngle"> Левый угол.</param>
        public TreeFractal(double coef, double rightAngle, double leftAngle)
        {
            this.coef = coef;
            this.rightAngle = rightAngle;
            this.leftAngle = leftAngle;
        }

        /// <summary>
        /// Построение дерева.
        /// </summary>
        /// <param name="canvas"> Канвас.</param>
        /// <param name="angle"> Угол.</param>
        /// <param name="length"> Длина.</param>
        /// <param name="level"> Глубина рекурсии.</param>
        /// <param name="xStart"> Начало X.</param>
        /// <param name="yStart"> Начало Y.</param>
        public override void DrawFractal(Canvas canvas, double angle, double length, int level, double xStart, double yStart)
        {

            var line = new Line();
            double xEnd = xStart + length * Math.Sin(angle * Math.PI / 180);
            double yEnd = yStart - length * Math.Cos(angle * Math.PI / 180);
            line.X1 = xStart;
            line.X2 = xEnd;
            line.Y1 = yStart;
            line.Y2 = yEnd;
            line.Stroke = Brushes.Black;
            line.StrokeThickness = 1;
            canvas.Children.Add(line);
            if (length >= 0 && level > 0)
            {
                DrawFractal(canvas, angle - leftAngle, length / coef, level - 1, xEnd, yEnd);
                DrawFractal(canvas, angle + rightAngle, length / coef, level - 1, xEnd, yEnd);
            }
        }

    }

    /// <summary>
    /// Множество Кантора.
    /// </summary>
    public class ManyCantor : Fractal
    {
        /// <summary>
        /// Расстояние.
        /// </summary>
        private double distance { get; set; }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="distance"> Расстояние.</param>
        public ManyCantor(double distance)
        {
            this.distance = distance;
        }

        /// <summary>
        /// Отрисовака множества Кантора.
        /// </summary>
        /// <param name="canvas"> Канвас.</param>
        /// <param name="length"> Длина</param>
        /// <param name="yStart"> Y начала.</param>
        /// <param name="level"> Глубина рекурссии.</param>
        /// <param name="xStart"> X начала.</param>
        /// <param name="xEnd"> X конца.</param>
        public override void DrawFractal(Canvas canvas, double length, double yStart, int level, double xStart, double xEnd)
        {

            double xLeft1 = (xEnd - xStart) / 3 + xStart;

            var line1 = new Line();
            line1.X1 = xStart;
            line1.Y1 = yStart;
            line1.X2 = xEnd;
            line1.Y2 = yStart;
            line1.StrokeThickness = 10;
            line1.Stroke = Brushes.Black;
            canvas.Children.Add(line1);

            double xLeft2 = 2 * (xEnd - xStart) / 3 + xStart;
            if (level > 0)
            {
                DrawFractal(canvas, 0, yStart + distance, level - 1, xStart, xLeft1);
                DrawFractal(canvas, 0, yStart + distance, level - 1, xLeft2, xEnd);
            }
            else
            {
                return;
            }
        }
    }

    /// <summary>
    /// Кривая Коха.
    /// </summary>
    public class CurveCox : Fractal
    {
        /// <summary>
        /// Отрисовака кривой Коха.
        /// </summary>
        /// <param name="canvas"> Канвас.</param>
        /// <param name="xStart"> X начала.</param>
        /// <param name="yStart"> Y начала.</param>
        /// <param name="level"> Глубина рекурсии.</param>
        /// <param name="xEnd"> Х конца.</param>
        /// <param name="yEnd"> Y конца.</param>
        public override void DrawFractal(Canvas canvas, double xStart, double yStart, int level, double xEnd, double yEnd)
        {


            double xFirstSide = xStart + (xEnd - xStart) / 3;
            double yFirstSide = yStart + (yEnd - yStart) / 3;
            double xSecondSide = xStart + 2 * (xEnd - xStart) / 3;
            double ySecondSide = yStart + 2 * (yEnd - yStart) / 3;
            double xTop = xFirstSide + (xSecondSide - xFirstSide) / 2 + Math.Sqrt(3) / 2 * (ySecondSide - yFirstSide);
            double yTop = yFirstSide - (xSecondSide - xFirstSide) * Math.Sqrt(3) / 2 + (ySecondSide - yFirstSide) / 2;

            Line startLine = new Line();
            startLine.X1 = xStart;
            startLine.X2 = xFirstSide;
            startLine.Y1 = yStart;
            startLine.Y2 = yFirstSide;
            startLine.Stroke = Brushes.Black;

            Line leftSide = new Line();
            leftSide.X1 = xFirstSide;
            leftSide.X2 = xTop;
            leftSide.Y1 = yFirstSide;
            leftSide.Y2 = yTop;
            leftSide.Stroke = Brushes.Black;

            Line rightSide = new Line();
            rightSide.X1 = xTop;
            rightSide.X2 = xSecondSide;
            rightSide.Y1 = yTop;
            rightSide.Y2 = ySecondSide;
            rightSide.Stroke = Brushes.Black;

            Line endLine = new Line();
            endLine.X1 = xSecondSide;
            endLine.X2 = xEnd;
            endLine.Y1 = ySecondSide;
            endLine.Y2 = yEnd;
            endLine.Stroke = Brushes.Black;

            if (level == 0)
            {


                canvas.Children.Add(startLine);
                canvas.Children.Add(leftSide);
                canvas.Children.Add(rightSide);
                canvas.Children.Add(endLine);


            }
            else
            {
                DrawFractal(canvas, xStart, yStart, level - 1, xFirstSide, yFirstSide);
                DrawFractal(canvas, xFirstSide, yFirstSide, level - 1, xTop, yTop);
                DrawFractal(canvas, xTop, yTop, level - 1, xSecondSide, ySecondSide);
                DrawFractal(canvas, xSecondSide, ySecondSide, level - 1, xEnd, yEnd);
                return;
            }

        }
    }

    /// <summary>
    /// Ковер Серпинского.
    /// </summary>
    public class Carpet : Fractal
    {
        /// <summary>
        /// Отрисовка ковра Серпинского.
        /// </summary>
        /// <param name="canvas"> Канвас.</param>
        /// <param name="x"> X.</param>
        /// <param name="y"> Y.</param>
        /// <param name="sizeX"> Ширина.</param>
        /// <param name="sizeY"> Длина.</param>
        /// <param name="level"> Глубина рекурсии.</param>
        public void DrawCarpet(Canvas canvas, double x, double y, double sizeX, double sizeY, int level)
        {

            if (level > 0)
            {
                double x0 = x, y0 = y, width = sizeX / 3.0, height = sizeY / 3.0;
                double x1 = x0 + width, y1 = y0 + height, x2 = x0 + width * 2.0, y2 = y0 + height * 2.0;

                DrawCarpet(canvas, x0, y0, width, height, level - 1);
                DrawCarpet(canvas, x1, y0, width, height, level - 1);
                DrawCarpet(canvas, x2, y0, width, height, level - 1);
                DrawCarpet(canvas, x0, y1, width, height, level - 1);
                DrawCarpet(canvas, x2, y1, width, height, level - 1);
                DrawCarpet(canvas, x0, y2, width, height, level - 1);
                DrawCarpet(canvas, x1, y2, width, height, level - 1);
                DrawCarpet(canvas, x2, y2, width, height, level - 1);

            }
            else
            {
                Rectangle rectangle = new Rectangle();
                Canvas.SetLeft(rectangle, x);
                Canvas.SetTop(rectangle, y);
                rectangle.Width = sizeX;
                rectangle.Height = sizeY;
                rectangle.Fill = Brushes.Black;
                canvas.Children.Add(rectangle);
            }

        }

        /// <summary>
        /// Вызов метода DrawCarpet.
        /// </summary>
        /// <param name="canvas"></param>
        /// <param name="xStart"></param>
        /// <param name="yStart"></param>
        /// <param name="level"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public override void DrawFractal(Canvas canvas, double xStart, double yStart, int level, double width, double height)
        {
            DrawCarpet(canvas, xStart, yStart, width, height, level);
        }
    }

    /// <summary>
    /// Треугольник Серпинского.
    /// </summary>
    public class Triangle : Fractal
    {
        /// <summary>
        /// Отрисовка треугольника.
        /// </summary>
        /// <param name="canvas"> Канвас.</param>
        /// <param name="xLeft"> X у левой стороны.</param>
        /// <param name="xRight"> X у правой стороны.</param>
        /// <param name="yLeft"> Y у левой стороны.</param>
        /// <param name="yRight"> Y у правой стороны.</param>
        /// <param name="xTop"> X вершины.</param>
        /// <param name="yTop"> Y вершины.</param>
        /// <param name="level"> Глубина рекурсии.</param>
        public void DrawTriangle(Canvas canvas, double xLeft, double xRight, double yLeft, double yRight, double xTop, double yTop, int level)
        {
            if (level > 0)
            {
                var bottomLine = new Line();
                bottomLine.X1 = xLeft;
                bottomLine.X2 = xRight;
                bottomLine.Y1 = yLeft;
                bottomLine.Y2 = yRight;
                bottomLine.Stroke = Brushes.DarkOliveGreen;
                bottomLine.StrokeThickness = 2;
                canvas.Children.Add(bottomLine);

                var leftSideLine = new Line();
                leftSideLine.X1 = xLeft;
                leftSideLine.X2 = xTop;
                leftSideLine.Y1 = yLeft;
                leftSideLine.Y2 = yTop;
                leftSideLine.Stroke = Brushes.DarkOliveGreen;
                leftSideLine.StrokeThickness = 2;
                canvas.Children.Add(leftSideLine);

                var rightSideLine = new Line();
                rightSideLine.X1 = xRight;
                rightSideLine.X2 = xTop;
                rightSideLine.Y1 = yRight;
                rightSideLine.Y2 = yTop;
                rightSideLine.Stroke = Brushes.DarkOliveGreen;
                rightSideLine.StrokeThickness = 2;
                canvas.Children.Add(rightSideLine);

                double xMidPoint = (xLeft + xRight) / 2;
                double yMidPoint = (yLeft + yRight) / 2;
                double xLeftPoint = (xTop + xLeft) / 2;
                double yLeftPoint = (yTop + yLeft) / 2;
                double xRightPoint = (xTop + xRight) / 2;
                double yRightPoint = (yTop + yRight) / 2;

                if (level >= 0)
                {
                    DrawTriangle(canvas, xRightPoint, xMidPoint, yRightPoint, yMidPoint, xRight, yRight, level - 1);
                    DrawTriangle(canvas, xTop, xLeftPoint, yTop, yLeftPoint, xRightPoint, yRightPoint, level - 1);
                    DrawTriangle(canvas, xLeftPoint, xLeft, yLeftPoint, yLeft, xMidPoint, yMidPoint, level - 1);
                }
                else
                {
                    return;
                }
            }
        }
        /// <summary>
        /// --- Не отвечает ни за что.
        /// </summary>
        /// <param name="canvas"></param>
        /// <param name="length"></param>
        /// <param name="angle"></param>
        /// <param name="level"></param>
        /// <param name="xStart"></param>
        /// <param name="yStart"></param>
        /// <exception cref="ArgumentException"></exception>
        public override void DrawFractal(Canvas canvas, double length, double angle, int level, double xStart, double yStart)
        {
            throw new ArgumentException("Эта функция ни за что не отвечает.");
        }
    }
}
