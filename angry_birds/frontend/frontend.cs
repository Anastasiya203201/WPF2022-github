﻿using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Controls;
using backend_improved;


class Frontend : Window { 
    [STAThread]
    public static void Main() {
        Application app = new Application();
        app.Run(new Frontend());
    }

    public Frontend() {
        Title = "Angry Birds";
        SizeToContent = SizeToContent.WidthAndHeight;
        StackPanel stack = new StackPanel();
        Content = stack;
        Button btn = new Button();

        Label l_x = new Label();
        stack.Children.Add(l_x);
        l_x.Content = "координата по x";
        TextBox coordinate_x = new TextBox();
        coordinate_x.Width = 300;
        stack.Children.Add(coordinate_x);

        Label l_y = new Label();
        stack.Children.Add(l_y);
        l_y.Content = "координата по y";
        TextBox coordinate_y = new TextBox();
        coordinate_y.Width = 300;
        stack.Children.Add(coordinate_y);

        Label st_s = new Label();
        stack.Children.Add(st_s);
        st_s.Content = "начальная скорость";
        TextBox starting_speed = new TextBox();
        starting_speed.Width = 300;
        stack.Children.Add(starting_speed);

        Label ang = new Label();
        stack.Children.Add(ang);
        ang.Content = "угол бросания";
        TextBox angle = new TextBox();
        angle.Width = 300;
        stack.Children.Add(angle);
        btn.Content = "Start";
        stack.Children.Add(btn);
        btn.Click += ButtonOnClick;

        void ButtonOnClick (object sender, RoutedEventArgs args) {
            btn = args.Source as Button;
            if (btn != null)
            {

                Parabola p = new Parabola(
                    Convert.ToDouble(coordinate_x.Text),
                    Convert.ToDouble(coordinate_y.Text),
                    Convert.ToDouble(angle.Text),
                    Convert.ToDouble(starting_speed.Text));

                MessageBox.Show("Тело упало: x = " + p.path_x[p.path_x.Count-1] + "y = " + p.path_y[p.path_y.Count-1]);
            }
        }
    }

}

