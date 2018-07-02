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
        class Position
        {
            public double X { get; set; }
            public double Y { get; set; }
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
            Circle circleK = new Circle()
            {
                a = 5,
                b = 2,
                r = 7
            };
            Circle circleN = new Circle()
            {
                a = 5,
                b = 3,
                r = 8
            };
            Intersect_point(circleK, circleN);
            Console.WriteLine(Intersect_Area_of_2_Circles(circleK, circleN));

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
        static IList<double> Intersect_point(Circle c1, Circle c2) //Circle circle_K, Circle circle_N
        {
            var list = new List<double>();

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

            list.Add(point1_x);
            list.Add(point1_y);
            list.Add(point2_x);
            list.Add(point2_y);

            Console.WriteLine("x1: {0},y1: {1}\nx2: {2},y2: {3}",point1_x,point1_y,point2_x,point2_y);

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
    }
}
