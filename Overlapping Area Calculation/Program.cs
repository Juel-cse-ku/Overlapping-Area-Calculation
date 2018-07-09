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
            public Circle C1 { get; set; }
            public Circle C2 { get; set; }
        }
            
        static void Main(string[] args)
        {
            Circle c1 = new Circle()
            {
                a = -3,
                b = -1,
                r = 8
            };
            Circle c2 = new Circle()
            {
                a = -1,
                b = 1,
                r = 7
            };
            Circle c3 = new Circle()
            {
                a = 1,
                b = 0,
                r = 5.5
            };

            Console.WriteLine(Intersect_Area_of_3_Circles(c1, c2, c3));
            var circles = Read_Circle();

            double[] shadedArea = new double[circles.Count()];

            for (int n = 0; n < circles.Count(); n++) 
                shadedArea[n] = 0;

            //for (int i = 0; i < circles.Count(); i++)
            //{
            //    for (int j = i + 1; j < circles.Count(); j++)
            //    {
            //        var overlapped_area1 = Intersect_Area_of_2_Circles(circles[i], circles[j]);
                  

            //        shadedArea[i] += overlapped_area1;
            //        shadedArea[j] += overlapped_area1;

            //        Console.WriteLine("{0},{1}",shadedArea[i],shadedArea[j]);
                    
            //        for (int k = j + 1; k < circles.Count(); k++)
            //        {
            //            var overlapped_area2 = Intersect_Area_of_2_Circles(circles[i], circles[k]);

            //            var conflict_area = Intersect_Area_of_3_Circles(circles[i], circles[j], circles[k]);

                        
            //            shadedArea[i] = shadedArea[i] + overlapped_area2 - conflict_area;
            //            // Console.WriteLine(shadedArea[i]);
            //            shadedArea[i] = shadedArea[i] / (3.1416 * circles[i].r * circles[i].r);
            //           // Console.WriteLine(shadedArea[i]);
            //        }
            //    }
            //    Console.WriteLine();
                
            //}

            //for (int i = 0; i < circles.Count(); i++)
            //{
            //    for (int j = i+1; j < circles.Count(); j++) 
            //    {
            //        var overlapped_area = Intersect_Area_of_2_Circles(circles[i], circles[j]);
            //        if (circles[i].r < circles[j].r) 
            //        {
            //            shadedArea[i] += overlapped_area * .09;// overlapped_area / (3.1416 * circles[i].r * circles[i].r);
            //        }
            //        else if(circles[i].r > circles[j].r)
            //        {
            //            shadedArea[j] += overlapped_area * .09;// overlapped_area / (3.1416 * circles[j].r * circles[j].r);
            //        }

            //    }
            //        Console.WriteLine(shadedArea[i] / (3.1416 * circles[i].r * circles[i].r));
            //}

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

            var test = Math.Abs((point1_x - x1) * (point1_x - x1) + (point1_y - y1) * (point1_y - y1) - r1 * r1);
            if(test >0.0000005)
            {
                var temp = point1_y;
                point1_y = point2_y;
                point2_y = temp;
            }

            if(!Double.IsNaN(point1_x) && !Double.IsNaN(point1_y))
            {
                Point point1 = new Point()
                {
                    X = point1_x,
                    Y = point1_y,
                };
                list.Add(point1);
            }
           // Console.WriteLine("{0},{1}", list[0].X, list[0].Y);
            if (!Double.IsNaN(point2_x) && !Double.IsNaN(point2_y) && (point1_x != point2_x || point1_y != point2_y))
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

        //calculating segment area
        static double SegmentArea(Point p1, Point p2, double r)
        {
            var a = D(p1.X, p1.Y, p2.X, p2.Y);
            var area = r * r * Math.Asin(a / (2 * r)) - (a / 4) * Math.Sqrt(4 * r * r - a * a);
            return area;
        }

        static float D(double x1, double y1, double x2,  double y2)
        {
            return (float)Math.Sqrt(Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2));
        }

        static int Point_Inside_Circle(Circle c1, Circle c2, Circle c3)
        {
            var p_12 = Intersect_points_of_2_Circles(c1, c2); //betweeen circle 1 and 2
            var p_13 = Intersect_points_of_2_Circles(c1, c3); //betweeen circle 1 and 3
            var p_23 = Intersect_points_of_2_Circles(c2, c3); //betweeen circle 2 and 3
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

        //finding common points position of 3 circles
        static IList<Point> Common_Circles(Circle c1, Circle c2, Circle c3)
        {
            var list = new List<Point>();

            var p_12 = Intersect_points_of_2_Circles(c1, c2); //betweeen circle 1 and 2
            var p_13 = Intersect_points_of_2_Circles(c1, c3); //betweeen circle 1 and 3
            var p_23 = Intersect_points_of_2_Circles(c2, c3); //betweeen circle 2 and 3

            foreach (var n in p_12)
            {
                if (c3.r > D(c3.a, c3.b, n.X, n.Y))
                {
                    Point point = new Point()
                    {
                        X = n.X,
                        Y = n.Y,
                        C1 = c1,
                        C2 = c2
                    };
                    list.Add(point);
                }
                //Console.WriteLine("1212121212");
            }
            foreach (var n in p_13)
            {
                if (c2.r > D(c2.a, c2.b, n.X, n.Y))
                {
                    //list.Add(c1);
                    //list.Add(c3);
                    Point point = new Point()
                    {
                        X = n.X,
                        Y = n.Y,
                        C1 = c1,
                        C2 = c3
                    };
                    list.Add(point);
                }
               // Console.WriteLine("13131313");
            }
            foreach (var n in p_23)
            {
                if (c1.r > D(c1.a, c1.b, n.X, n.Y))
                {
                    //list.Add(c2);
                    //list.Add(c3);
                    Point point = new Point()
                    {
                        X = n.X,
                        Y = n.Y,
                        C1 = c2,
                        C2 = c3
                    };
                    list.Add(point);
                }
               // Console.WriteLine("23232323");
            }

            return list;
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

        //calculating segment area of 3 points
        static double Segment_Area_3_Points(IList<Point> points)
        {
            double segment_area = 0;
            for (int i = 0; i < points.Count(); i++)
            {
                for(int j=i+1;j<points.Count();j++)
                {
                    var mx = (points[i].X + points[j].X) / 2;
                    var my = (points[i].Y + points[j].Y) / 2;

                    Circle C,C2,C3;
                   

                    if (points[i].C1 == points[j].C1 || points[i].C1 == points[j].C2)
                    {
                        C = points[i].C1;
                    }
                    else //if (points[i].C2 == points[j].C1 || points[i].C2 == points[j].C2)
                    {
                        C = points[i].C2;
                    }
                    double a1 = C.a;
                    double b1 = C.b;
                    double r1 = C.r;

                    var m = (b1 - my) / (a1 - mx);
                    var n = -(mx * (b1 - my)) / (a1 - mx) + my;
                    var sigma = r1 * r1 * (1 + m * m) - (b1 - m * a1 - n) * (b1 - m * a1 - n);

                    var x1 = (a1 + b1 * m - m * n + Math.Sqrt(sigma)) / (1 + m * m);
                    var x2 = (a1 + b1 * m - m * n - Math.Sqrt(sigma)) / (1 + m * m);

                    var y1 = (n + a1 * m + b1 * m * m + m * Math.Sqrt(sigma)) / (1 + m * m);
                    var y2 = (n + a1 * m + b1 * m * m - m * Math.Sqrt(sigma)) / (1 + m * m);

                    if (points[i].C1 != C)
                    {
                        C2 = points[i].C1;
                    }
                    else
                    {
                        C2 = points[i].C2;
                    }

                    if(points[j].C1!=C)
                    {
                        C3 = points[j].C1;
                    }
                    else
                    {
                        C3 = points[j].C2;
                    }

                    var D2 = D(x1, y1, C2.a, C2.b);
                    var D3 = D(x1, y1, C3.a, C3.b);

                    double x, y;

                    if (C2.r > D2 && C3.r > D3)
                    {
                        x = x1;
                        y = y1;
                    }
                    else
                    {
                        x = x2;
                        y = y2;
                    }

                    double theta;
                    if (C.r > D(x, y, mx, my))
                    {
                        theta = 2 * Math.Asin(a1 / (2 * r1));
                    }
                    else
                    {
                        theta = 2 * (Math.PI - Math.Asin(a1 / (2 * r1)));
                    }

                    segment_area += (r1 * r1 / 2)*(theta - Math.Sin(theta));
                }
            }
            return segment_area;
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
            double conflict_area=0;
           // Console.WriteLine(conflict_area);

            //int common_points_3_circles=0;// = Point_Inside_Circle(c1, c1, c3);
            var points = Common_Circles(c1, c2, c3);
            //var points = Point_Inside_Circle(c1, c2, c3);
            //Console.WriteLine(points.Count());

            if (Circle_inside_Circle(c1,c2,c3))
            {
                //Console.WriteLine("************");
                if(c1.r < c2.r && c1.r < c3.r && Inside_Circle(c1,c2) && Inside_Circle(c1,c3))
                {
                    //overlapped_area -= Math.PI * Math.Pow(c1.r, 2);
                    conflict_area = Math.PI * Math.Pow(c1.r, 2);
                   
                }
                else if (c2.r < c1.r && c2.r < c3.r && Inside_Circle(c2, c1) && Inside_Circle(c2, c3))
                {
                    //overlapped_area -= Math.PI * Math.Pow(c2.r, 2);
                    conflict_area = Math.PI * Math.Pow(c2.r, 2);
                }
                else if (c3.r < c1.r && c3.r < c2.r && Inside_Circle(c3, c1) && Inside_Circle(c3, c2))
                {
                    //overlapped_area -= Math.PI * Math.Pow(c3.r, 2);
                    conflict_area = Math.PI * Math.Pow(c3.r, 2);
                }
                //Console.WriteLine(conflict_area);
            }
           // overlapped_area -= conflict_area;

            if (points.Count() >=2) //for fig 7,9,10,11,12
            {
                if (points.Count() == 2)
                {
                    conflict_area = Intersect_Area_of_2_Circles(points[0].C1, points[0].C2);
                    //Console.WriteLine(conflict_area);
                }
                else if (points.Count() == 3)
                {
                    var a = D(points[0].X, points[0].Y, points[1].X, points[1].Y);
                    var b = D(points[1].X, points[1].Y, points[2].X, points[2].Y);
                    var c = D(points[2].X, points[2].Y, points[0].X, points[0].Y);

                    var s = .5 * (a + b + c);
                    // Console.Write("{0},{1},{2},{3},{4},{5}", points[0].X, points[0].Y, points[1].X, points[1].Y, points[2].X, points[2].Y);
                    conflict_area = Math.Sqrt(s * (s - a) * (s - b) * (s - c));
                    Console.WriteLine(Segment_Area_3_Points(points));
                    // Console.WriteLine(conflict_area);
                    //var seg1 = SegmentArea(points[0], points[1], points[0].C1.r==points[1].C1.r?points[);
                    //var seg2 = SegmentArea(points[2], points[3], Math.Max(points[2].C1.r, points[2].C2.r));
                    //var seg3 = SegmentArea(points[0], points[2], Math.Min(points[2].C1.r, points[2].C2.r));
                    //Console.WriteLine(points.Count());
                    //if (D(points[0].X, points[0].Y, c1.a, c1.b) < c1.r)
                    //{
                    //    var d = D(points[1].X, points[1].Y, points[2].X, points[2].Y);
                    //    conflict_area += (c1.r * c1.r) * Math.Asin(d / (2 * c1.r)) - (d * Math.Sqrt(4 * c1.r * c1.r - d * d) / 4);
                    //   // Console.WriteLine("111111111");
                       
                    //}
                    //else if (D(points[0].X, points[0].Y, c2.a, c2.b) < c2.r)
                    //{
                    //    var d = D(points[1].X, points[1].Y, points[2].X, points[2].Y);
                    //    conflict_area += (c2.r * c2.r) * Math.Asin(d / (2 * c2.r)) - (d * Math.Sqrt(4 * c2.r * c2.r - d * d) / 4);
                    //    //Console.WriteLine("22222222222222");
                    //}
                    //else if (D(points[0].X, points[0].Y, c3.a, c3.b) < c3.r)
                    //{
                    //    var d = D(points[1].X, points[1].Y, points[2].X, points[2].Y);
                    //    conflict_area += (c3.r * c3.r) * Math.Asin(d / (2 * c3.r)) - (d * Math.Sqrt(4 * c3.r * c3.r - d * d) / 4);
                    //    //var segA = (c3.r * c3.r) * Math.Asin(d / (2 * c3.r)) - (d * Math.Sqrt(4 * c3.r * c3.r - d * d) / 4);
                    //    // Console.WriteLine(segA);
                    //   // Console.WriteLine("333333333");
                    //    //var seg1 = SegmentArea(points[1], points[2],c3.r);
                    //    //Console.WriteLine(seg1);
                    //}
                    ////overlapped_area -= conflict_area;

                    //if (D(points[1].X, points[1].Y, c1.a, c1.b) < c1.r)
                    //{
                    //    var d = D(points[0].X, points[0].Y, points[2].X, points[2].Y);
                    //    conflict_area += (c1.r * c1.r) * Math.Asin(d / (2 * c1.r)) - (d * Math.Sqrt(4 * c1.r * c1.r - d * d) / 4);
                    //    //Console.WriteLine("111111111");
                    //}
                    //else if (D(points[1].X, points[1].Y, c2.a, c2.b) < c2.r)
                    //{
                    //    var d = D(points[0].X, points[0].Y, points[2].X, points[2].Y);
                    //    conflict_area += (c2.r * c2.r) * Math.Asin(d / (2 * c2.r)) - (d * Math.Sqrt(4 * c2.r * c2.r - d * d) / 4);
                    //   // Console.WriteLine("22222222222222");
                    //}
                    //else if (D(points[1].X, points[1].Y, c3.a, c3.b) < c3.r)
                    //{
                    //    var d = D(points[0].X, points[0].Y, points[2].X, points[2].Y);
                    //    conflict_area += (c3.r * c3.r) * Math.Asin(d / (2 * c3.r)) - (d * Math.Sqrt(4 * c3.r * c3.r - d * d) / 4);
                    //    //Console.WriteLine("3333333333");
                    //}

                    //if (D(points[2].X, points[2].Y, c1.a, c1.b) < c1.r)
                    //{
                    //    var d = D(points[0].X, points[0].Y, points[1].X, points[1].Y);
                    //    conflict_area += (c1.r * c1.r) * Math.Asin(d / (2 * c1.r)) - (d * Math.Sqrt(4 * c1.r * c1.r - d * d) / 4);
                    //   // Console.WriteLine("111111111");
                    //}
                    //else if (D(points[2].X, points[2].Y, c2.a, c2.b) < c2.r)
                    //{
                    //    var d = D(points[0].X, points[0].Y, points[1].X, points[1].Y);
                    //    conflict_area += (c2.r * c2.r) * Math.Asin(d / (2 * c2.r)) - (d * Math.Sqrt(4 * c2.r * c2.r - d * d) / 4);
                    //    //Console.WriteLine("22222222222222");
                    //}
                    //else if (D(points[2].X, points[2].Y, c3.a, c3.b) < c3.r)
                    //{
                    //    var d = D(points[0].X, points[0].Y, points[1].X, points[1].Y);
                    //    conflict_area += (c3.r * c3.r) * Math.Asin(d / (2 * c3.r)) - (d * Math.Sqrt(4 * c3.r * c3.r - d * d) / 4);
                    //    //Console.WriteLine("3333333333");
                    //}

                    //Console.WriteLine(conflict_area);
                }
                else if (points.Count() == 4)
                {
                    //Console.WriteLine("444444444");
                    //Console.WriteLine(points[0].C2.r);
                    //Console.WriteLine(points[0].C1.r);
                    foreach (var n in points)
                    {
                        // Console.WriteLine("{0},{1},", n.X, n.Y);
                        //Console.WriteLine(Math.Min(n.C1.r, n.C1.r));
                    }
                    var seg1 = SegmentArea(points[0], points[1], Math.Max(points[0].C1.r, points[0].C2.r));
                    var seg2 = SegmentArea(points[2], points[3], Math.Max(points[2].C1.r, points[2].C2.r));
                    var seg3 = SegmentArea(points[0], points[2], Math.Min(points[2].C1.r, points[2].C2.r));
                    var seg4 = SegmentArea(points[1], points[3], Math.Min(points[1].C1.r, points[1].C2.r));

                    var p = D(points[0].X, points[0].Y, points[3].X, points[3].Y);
                    var q = D(points[1].X, points[1].Y, points[2].X, points[2].Y);
                    var theta = 2 * Math.Asin(D(points[0].X, points[0].Y, points[2].X, points[2].Y)/(2*Math.Min(points[0].C1.r,points[0].C2.r)));

                    var rectArea = .5 * p * q * Math.Sin(theta);

                    //Console.WriteLine(rectArea + seg1 + seg2 + seg3 + seg4);
                    conflict_area = rectArea + seg1 + seg2 + seg3 + seg4;
                }
            }



                return conflict_area;
        }
    }



}
