using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Overlapping_Area_Calculation
{
    class Program
    {
        class Circle
        {
            public double a { get; set; }
            public double b { get; set; }
            public double r { get; set; }
        }
        class Point
        {
            public double X { get; set; }
            public double Y { get; set; }
            //public double X2 { get; set; }
            //public double Y2 { get; set; }
        }
            
        static void Main(string[] args)
        {
            //circlr data read and print in console
            /*var cirlces = Read_Circle();
                        foreach(var circle in cirlces)
            {
                var newLine = string.Format("{0},{1},{2}",circle.X,circle.Y,circle.R);
                Console.WriteLine(newLine);
            }*/
            Circle c1 = new Circle()
            {
                a = -2,
                b = 3.5,
                r = 1
            };
            Circle c2 = new Circle()
            {
                a = 3,
                b = 2,
                r = 6
            };
            Circle c3 = new Circle()
            {
                a = -6,
                b = 4,
                r = 4
            };
            var circles = Read_Circle();

            /*for(int i=0;i< circles.Count();i++)
            {
                double area = 0;
                for(int j=i+1;j<circles.Count();j++)
                {
                    //Intersect_point(circles[i], circles[j]);
                    //area += Intersect_Area_of_2_Circles(circles[i], circles[j]);
                }
                //Console.WriteLine(area);
            }*/
            //Intersect_point_(c1,c2);
            //var points = Intersect_points_of_2_Circles(c1, c2);
            // Console.WriteLine("x1: {0},y1: {1}\nx2: {2},y2: {3}", points.X1, points.Y1, points.X2, points.Y2);
            //foreach(var n in points)
            // {
            //     Console.WriteLine("{0},{1}", n.X,n.Y);
            // }

            var points = Point_Inside_Circle(c1, c2, c3);
            Console.WriteLine(points);

            Console.WriteLine("\n\nProgram runned successfully...");
            Console.ReadLine();
        }

        static IList<Circle> Read_Circle(bool hasheaders = true)
        {
            var list = new List<Circle>();
            var path = "../../../DATA/circle_data.csv";

            foreach(var line in File.ReadLines(path).Skip(hasheaders ? 1:0))
            {
                var data = line.Split(',');

                Circle circle = new Circle()
                {
                    a = double.Parse(data[0]),
                    b = double.Parse(data[1]),
                    r = double.Parse(data[2]),
                };
                list.Add(circle);
            }

            return list;
        }

        //getting intersection points
        static IList<Point> Intersect_points_of_2_Circles(Circle c1, Circle c2) //Circle circle_K, Circle circle_N
        {
            var list = new List<Point>();

            double x1 = c1.a;
            double y1 = c1.b;
            double x2 = c2.a;
            double y2 = c2.b;
            double r1 = c1.r;
            double r2 = c2.r;

            //double R = Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));
            //double point1_x = .5 * (x1 + x2) + (((Math.Pow(r1, 2) - Math.Pow(r2, 2)) * (x2 - x1)) / (2 * Math.Pow(R, 2))) + .5 * Math.Sqrt((2 * (Math.Pow(r1, 2) + Math.Pow(r2, 2)) / Math.Pow(R, 2)) - (Math.Pow(Math.Pow(r1, 2) - Math.Pow(r2, 2), 2) / Math.Pow(R, 4))) - (y2 - y1);
            //double point2_x = .5 * (x1 + x2) + (((Math.Pow(r1, 2) - Math.Pow(r2, 2)) * (x2 - x1)) / (2 * Math.Pow(R, 2))) - .5 * Math.Sqrt((2 * (Math.Pow(r1, 2) + Math.Pow(r2, 2)) / Math.Pow(R, 2)) - (Math.Pow(Math.Pow(r1, 2) - Math.Pow(r2, 2), 2) / Math.Pow(R, 4))) - (y2 - y1);
            //double point1_y = .5 * (y1 + y2) + (((Math.Pow(r1, 2) - Math.Pow(r2, 2)) * (y2 - y1)) / (2 * Math.Pow(R, 2))) + .5 * Math.Sqrt((2 * (Math.Pow(r1, 2) + Math.Pow(r2, 2)) / Math.Pow(R, 2)) - (Math.Pow(Math.Pow(r1, 2) - Math.Pow(r2, 2), 2) / Math.Pow(R, 4))) - (x2 - x1);
            // double point2_y = .5 * (y1 + y2) + (((Math.Pow(r1, 2) - Math.Pow(r2, 2)) * (y2 - y1)) / (2 * Math.Pow(R, 2))) - .5 * Math.Sqrt((2 * (Math.Pow(r1, 2) + Math.Pow(r2, 2)) / Math.Pow(R, 2)) - (Math.Pow(Math.Pow(r1, 2) - Math.Pow(r2, 2), 2) / Math.Pow(R, 4))) - (x2 - x1);

            //Heron's Formula
            var D = Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));
            var a = D + r1 + r2;
            var b = D + r1 - r2;
            var c = D - r1 + r2;
            var d = -D + r1 + r2;
            var area = Math.Sqrt(a * b * c * d) / 4;
            //Console.WriteLine(area);

            var val_1 = (x1 + x2) / 2 + (x2 - x1) * (r1 * r1 - r2 * r2) / (2 * D * D);
            var val_2 = 2 * (y1 - y2) * area / (D * D);

            var point1_x = val_1 + val_2;
            var point2_x = val_1 - val_2;

            var val_3 = (y1 + y2) / 2 + (y2 - y1) * (r1 * r1 - r2 * r2) / (2 * D * D);
            var val_4 = 2 * (x1 - x2) * area / (D * D);

            var point1_y = val_3 - val_4;
            var point2_y = val_3 + val_4;

            //varifing intersection points
            var test = Math.Abs((point1_x - x1) * (point1_x - x1) + (point1_y - y1) * (point1_y - y1) - r1 * r1);
            if(test >0.00005)
            {
                var temp = point1_y;
                point1_y = point2_y;
                point2_y = temp;
            }

            if(!Double.IsNaN(point1_x) && !Double.IsNaN(point1_y))
            {
                //list.Add(point1_x);
                //list.Add(point1_y);
                Point point1 = new Point()
                {
                    X = point1_x,
                    Y = point1_y,
                };
                list.Add(point1);
            }
            if (!Double.IsNaN(point2_x) && !Double.IsNaN(point2_y) && point1_x != point2_x && point1_y != point2_y)
            {
                //list.Add(point2_x);
                //list.Add(point2_y);
                Point point2 = new Point()
                {
                    X = point2_x,
                    Y = point2_y,
                };
                list.Add(point2);
            }
            // Console.WriteLine("x1: {0},y1: {1}\nx2: {2},y2: {3}",point1_x,point1_y,point2_x,point2_y);
            //return new Point()
            //{
            //    X1 = point1_x,
            //    Y1 = point1_y,
            //    X2 = point2_x,
            //    Y2 = point2_y
            //};

            return list;
        }

        //getting intersecion area of two circle
        static double Intersect_Area_of_2_Circles(Circle c1, Circle c2)
        {
            var pi = Math.PI;
            var a = c1.a;
            var b = c1.b;
            var r1 = c1.r;
            var c = c2.a;
            var d = c2.b;
            var r2 = c2.r;

            var D = Math.Sqrt((c - a) * (c - a) + (d - b) * (d - b));

            if(D < Math.Abs(r1-r2))
            {
                if(r1 >r2)
                {
                    return pi * r2 * r2;
                }
                else
                {
                    return pi * r1 * r1;
                }
            }
            else if (D > r1 + r2)
            {
                return 0;
            }
            else if(D == r1+r2)
            {
                return 0;
            }
            else
            {
                var aa = (r1*r1 - r2*r2 + D*D) / (2*D);
                var bb = (r2*r2 - r1*r1 + D*D) / (2*D);
                var theta_1 = 2 * Math.Acos(aa / r1);
                var theta_2 = 2 * Math.Acos(bb / r2);

                double area_1 = 0;
                double area_2 = 0;

                if(theta_1 > pi)
                {
                    area_1 = r2 * r2 * (theta_2 + Math.Sin((2 * pi - theta_2))) / 2;
                }
                else
                {
                    area_1 = r2 * r2 * (theta_2 - Math.Sin(theta_2)) / 2;
                }

                if(theta_2 > pi)
                {
                    area_2 = r1 * r1 * (theta_1 + Math.Sin((2 * pi - theta_1))) / 2;
                }
                else
                {
                    area_2 = r1 * r1 * (theta_1 - Math.Sin(theta_1)) / 2;
                }

                return area_1 + area_2;
            }

            
        }

        static double D(double x1, double y1, double x2,  double y2)
        {
            return Math.Sqrt(Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2));
        }

        static int Point_Inside_Circle(Circle c1, Circle c2, Circle c3)
        {
            var p_12 = Intersect_points_of_2_Circles(c1, c2); //betweeen circle 1 and 2
            var p_13 = Intersect_points_of_2_Circles(c1, c3); //betweeen circle 1 and 3
            var p_23 = Intersect_points_of_2_Circles(c2, c3); //betweeen circle 2 and 3

            //var d1 = Math.Sqrt((p_12.X1 - c3.a) * (p_12.X1 - c3.a) + (p_12.Y1 - c3.b) * (p_12.Y1 - c3.b));
            //var d2 = Math.Sqrt((p_12.X2 - c3.a) * (p_12.X2 - c3.a) + (p_12.Y2 - c3.b) * (p_12.Y2 - c3.b));

            //var d3 = Math.Sqrt((p_13.X1 - c2.a) * (p_13.X1 - c2.a) + (p_13.Y1 - c2.b) * (p_13.Y1 - c2.b));
            //var d4 = Math.Sqrt((p_13.X2 - c2.a) * (p_13.X2 - c2.a) + (p_13.Y2 - c2.b) * (p_13.Y2 - c2.b));

            //var d5 = Math.Sqrt((p_23.X1 - c1.a) * (p_23.X1 - c1.a) + (p_23.Y1 - c1.b) * (p_23.Y1 - c1.b));
            //var d6 = Math.Sqrt((p_23.X2 - c1.a) * (p_23.X2 - c1.a) + (p_23.Y2 - c1.b) * (p_23.Y2 - c1.b));

            //if (c3.r > d1 || c3.r > d2 || c2.r > d3 || c2.r > d4 || c1.r > d5 || c1.r > d6)
            //{
            //    return true;
            //}
            //else
            //{
            //    return false;
            //}
            int points = 0;

            foreach(var n in p_12)
            {
                if (c3.r > D(c3.a, c3.b, n.X, n.Y))
                    points++;
            }
            foreach(var n in p_13)
            {
                if (c2.r > D(c2.a, c2.b, n.X, n.Y))
                    points++;
            }
            foreach (var n in p_23)
            {
                if (c1.r > D(c1.a, c1.b, n.X, n.Y))
                    points++;

            }

            return points;
        }

        static bool Circle_inside_Circle(Circle c1, Circle c2, Circle c3)
        {
            var D_12 = Math.Sqrt(Math.Pow(c1.a - c2.a, 2) + Math.Pow(c1.b - c2.b, 2));
            var D_13 = Math.Sqrt(Math.Pow(c1.a - c3.a, 2) + Math.Pow(c1.b - c3.b, 2));
            var D_23 = Math.Sqrt(Math.Pow(c2.a - c3.a, 2) + Math.Pow(c2.b - c3.b, 2));

            if(Math.Abs(c1.r-c2.r) > D_12 || Math.Abs(c1.r - c3.r) > D_13 || Math.Abs(c2.r - c3.r) > D_23) //|| Math.Abs(c1.r - c3.r) > D_13 || && Math.Abs(c2.r - c3.r) > D_23
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        static bool Inside_Circle(Circle c1, Circle c2)
        {
            var D_12 = Math.Sqrt(Math.Pow(c1.a - c2.a, 2) + Math.Pow(c1.b - c2.b, 2));
           // var D_13 = Math.Sqrt(Math.Pow(c1.a - c3.a, 2) + Math.Pow(c1.b - c3.b, 2));
            //var D_23 = Math.Sqrt(Math.Pow(c2.a - c3.a, 2) + Math.Pow(c2.b - c3.b, 2));

            if (Math.Abs(c1.r - c2.r) > D_12 ) //|| Math.Abs(c1.r - c3.r) > D_13 || && Math.Abs(c2.r - c3.r) > D_23
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //inersect area calculation of 3 circles
        static double Intersect_Area_of_3_Circles(Circle c1, Circle c2, Circle c3) //c1 is focal circle
        {
            var x1 = c1.a;
            var y1 = c1.b;
            var r1 = c1.r;
            var x2 = c2.a;
            var y2 = c2.b;
            var r2 = c2.r;
            var x3 = c3.a;
            var y3 = c3.b;
            var r3 = c3.r;

            var D_12 = Math.Sqrt((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2));
            var D_13 = Math.Sqrt((x1 - x3) * (x1 - x3) + (y1 - y3) * (y1 - y3));
            var D_23 = Math.Sqrt((x2 - x3) * (x2 - x3) + (y2 - y3) * (y2 - y3));

            var intersect_pints_12 = Intersect_points_of_2_Circles(c1, c2);
            var intersect_point_13 = Intersect_points_of_2_Circles(c1, c3);
            var intersect_point_23 = Intersect_points_of_2_Circles(c2, c3);
            

            double intersect_area_12 = Intersect_Area_of_2_Circles(c1, c2);
            double intersect_area_13 = Intersect_Area_of_2_Circles(c1, c3);
            double intersect_area_23 = Intersect_Area_of_2_Circles(c2, c3);

            double overlapped_area = intersect_area_12 + intersect_area_13;

            if (Circle_inside_Circle(c1,c2,c3))
            { 
                if(c1.r < c2.r && c1.r < c3.r && Inside_Circle(c1,c2) && Inside_Circle(c1,c3))
                {
                    overlapped_area -= Math.PI * Math.Pow(c1.r, 2);
                }
                else if (c2.r < c1.r && c2.r < c3.r && Inside_Circle(c2, c1) && Inside_Circle(c2, c3))
                {
                    overlapped_area -= Math.PI * Math.Pow(c2.r, 2);
                }
                else if (c3.r < c1.r && c3.r < c2.r && Inside_Circle(c3, c1) && Inside_Circle(c3, c2))
                {
                    overlapped_area -= Math.PI * Math.Pow(c3.r, 2);
                }
            }
            //else if (Point_Inside_Circle(c1, c1, c3)) //for fig 6 and 8
            //{

            //}









            return overlapped_area;
        }
    }



}
