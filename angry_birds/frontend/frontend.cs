using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Shapes;
using backend_improved;
using System.Windows.Threading;

class AngryBirds : Window {
    [STAThread]
    public static void Main() {
        Application app = new Application();
        app.Run(new AngryBirds());
    }

    public AngryBirds() {
        Title = "Angry Birds";
        SizeToContent = SizeToContent.WidthAndHeight;
        StackPanel stack = new StackPanel();
        Content = stack;
        Button btn = new Button();

        Label l_x = new Label();
        stack.Children.Add(l_x);
        l_x.Content = "координата по x";
        TextBox coordinate_x = new TextBox();
        coordinate_x.Text = "0";
        coordinate_x.Width = 300;
        stack.Children.Add(coordinate_x);

        Label l_y = new Label();
        stack.Children.Add(l_y);
        l_y.Content = "координата по y";
        TextBox coordinate_y = new TextBox();
        coordinate_y.Text = "0";
        coordinate_y.Width = 300;
        stack.Children.Add(coordinate_y);

        Label st_s = new Label();
        stack.Children.Add(st_s);
        st_s.Content = "начальная скорость";
        TextBox starting_speed = new TextBox();
        starting_speed.Text = "10";
        starting_speed.Width = 300;
        stack.Children.Add(starting_speed);

        Label ang = new Label();
        stack.Children.Add(ang);
        ang.Content = "угол бросания";
        TextBox angle = new TextBox();
        angle.Text = "45";
        angle.Width = 300;
        stack.Children.Add(angle);
        btn.Content = "Start";
        stack.Children.Add(btn);

        Parabola p = new Parabola(
                    Convert.ToDouble(coordinate_x.Text),
                    Convert.ToDouble(coordinate_y.Text),
                    Convert.ToDouble(angle.Text),
                    Convert.ToDouble(starting_speed.Text));


        btn.Click += ButtonOnClick;

        Data.tmr = new DispatcherTimer();
        Data.tmr.Interval = TimeSpan.FromSeconds(0.01);
        Data.tmr.Tick += TimerOnTick;

        void ButtonOnClick (object sender, RoutedEventArgs args) {
            btn = args.Source as Button;
            if (btn != null) {
                MessageBox.Show("Тело упало:    x = " + p.path_x[p.path_x.Count-1] + "    " + "y = " + p.path_y[p.path_y.Count-1]);
                Draw z = new Draw();
                z.Show();
                Data.i = 0;
                Data.tmr.Start();
            }
        }

        void TimerOnTick(object sender, EventArgs args) {
            
            if (Data.i < p.path_x.Count) {
                Data.tr.Points.Add(new Point(100 * p.path_x[Data.i], 400 - 100 * p.path_y[Data.i]));
                ++Data.i;
                
            }
            else {
                Data.tmr.Stop();
            }
        }
    }
}

public class Data { 
    public static Polyline tr = new Polyline();
    public static int i = 0;
    public static DispatcherTimer tmr;
    public static Canvas win; 
}
public class Draw : Window {
    public Draw() {
        Data.win = new Canvas();
        Data.win.Width = 1000;
        Data.win.Height = 1000;
     //   win.Background = Brushes.Green;
        Data.tr.Stroke = Brushes.Black;
        Data.tr.StrokeThickness = 4;
        Data.win.Children.Add(Data.tr);        
        Content = Data.win;
    }
}  

