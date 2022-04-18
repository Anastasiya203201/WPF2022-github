using System;
using System.Collections.Generic;

namespace backend_improved {
    static class Constans {
        public const double g = 9.80665; //Ускорение свободного падения.
        public const double m = 1;      //Масса тела.
    }
    class Parabola {
      
        static Tuple<List<double>, List<double>, List<double>, List<double>, List<double>> trajectory() {
            double x_0 = Convert.ToDouble(Console.ReadLine());   //Начальное положение тела по оси Ox.
            double y_0 = Convert.ToDouble(Console.ReadLine());   //Начальное положение тела по оси Oy.
            double alpha = Convert.ToDouble(Console.ReadLine()); //Угол, под которым будет брошено тело.
            double v_0 = Convert.ToDouble(Console.ReadLine());   //Начальная скорость.
            List<double> path_x = new List<double>();
            List<double> path_y = new List<double>();
            List<double> steps = new List<double>();
            List<double> v_x_list = new List<double>();               //Проекция скорости на Ox. 
            List<double> v_y_list = new List<double>();               //Проекция скорости на Oy.
            v_x_list[0] = x_0;
            v_y_list[0] = y_0;

            for (int i = 0; i <= 100; ++i) {
                for (double t = 0; t <= 1; t += 0.01) {
                    double wind_resistance = t / 2;
                     
                    v_x_list[i + 1] = v_x_list[i] - 0.01 * wind_resistance * v_x_list[i] / Constans.m;
                    v_y_list[i + 1] = v_y_list[i] - 0.01 * (Constans.g + wind_resistance * v_y_list[i] / Constans.m);
                    path_x[i + 1] = path_x[i] + 0.01 * v_x_list[i];
                    path_y[i + 1] = path_y[i] + 0.01 * v_y_list[i];
                    steps.Add(t); 
                }
            }
            //**********FOR_DEBAG****************
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

            //***********************************
            return Tuple.Create(path_x, path_y, v_x_list, v_y_list, steps);
        }
                static void Main(string[] args) {
                    trajectory();
                }
    }
}
