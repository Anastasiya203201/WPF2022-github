using System;
using System.Collections.Generic;

namespace backend_improved {
    static class Constans {
        public const double g = 9.80665; //Ускорение свободного падения.
        public const double m = 1;      //Масса тела.
    }
    class Parabola {

        public List<double> path_x = new List<double>();
        public List<double> path_y = new List<double>();
        public List<double> steps = new List<double>();
        public List<double> v_x_list = new List<double>();               //Проекция скорости на Ox. 
        public List<double> v_y_list = new List<double>();               //Проекция скорости на Oy.

      public  Parabola(double x, double y, double a, double v) {
            path_x.Add(x);   //Начальное положение тела по оси Ox.
            path_y.Add( y);   //Начальное положение тела по оси Oy.
            double alpha = a; //Угол, под которым будет брошено тело.
            double v_0 = v;   //Начальная скорость.
            a = a/180 * Math.PI;
            v_x_list.Add(v*Math.Cos(a));
            v_y_list.Add(v*Math.Sin(a));

            int i = 0;
            for (double t = 0; t <= 1; t += 0.01) {
                double wind_resistance = t / 2;
                v_x_list.Add(v_x_list[i] - 0.01 * wind_resistance * v_x_list[i] / Constans.m);
                v_y_list.Add(v_y_list[i] - 0.01 * (Constans.g + wind_resistance * v_y_list[i] / Constans.m));
                path_x.Add(path_x[i] + 0.01 * v_x_list[i]);
                path_y.Add(path_y[i] + 0.01 * v_y_list[i]);
                steps.Add(t);
                if (v_y_list[i] < 0)
                    break;
                ++i;
            }
                
        /*        //**********FOR_DEBAG****************
                foreach (var el in path_x)
                    Console.WriteLine(el);
                Console.WriteLine("*************************");
                foreach (var el in path_y)
                    Console.WriteLine(el);
                Console.WriteLine("*************************");
                foreach (var el in steps)
                    Console.WriteLine(el);
                Console.WriteLine("*************************");
                foreach (var el in v_x_list)
                    Console.WriteLine(el);
                Console.WriteLine("*************************");
                foreach (var el in v_y_list)
                    Console.WriteLine(el);
                Console.WriteLine("*************************");

                //***********************************             */
            
      }
    }
}

