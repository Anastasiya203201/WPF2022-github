using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Forms;
using backend_improved;
using System.Windows.Threading;
using System.Windows.Media.Imaging;

class AngryBirds : Window {
    [STAThread]
    public static void Main() {
        System.Windows.Application app = new System.Windows.Application();
        app.Run(new AngryBirds());
    }

    public AngryBirds() {
        Title = "Angry Birds";
        SizeToContent = SizeToContent.WidthAndHeight;
        StackPanel stack = new StackPanel();
        Content = stack;
        System.Windows.Controls.Button btn = new System.Windows.Controls.Button();

        System.Windows.Controls.Label l_x = new System.Windows.Controls.Label();
        stack.Children.Add(l_x);
        l_x.Content = "координата по x";
        System.Windows.Controls.TextBox coordinate_x = new System.Windows.Controls.TextBox();
        coordinate_x.Text = "0";
        coordinate_x.Width = 300;
        stack.Children.Add(coordinate_x);

        System.Windows.Controls.Label l_y = new System.Windows.Controls.Label();
        stack.Children.Add(l_y);
        l_y.Content = "координата по y";
        System.Windows.Controls.TextBox coordinate_y = new System.Windows.Controls.TextBox();
        coordinate_y.Text = "0";
        coordinate_y.Width = 300;
        stack.Children.Add(coordinate_y);

        System.Windows.Controls.Label st_s = new System.Windows.Controls.Label();
        stack.Children.Add(st_s);
        st_s.Content = "начальная скорость";
        System.Windows.Controls.TextBox starting_speed = new System.Windows.Controls.TextBox();
        starting_speed.Text = "10";
        starting_speed.Width = 300;
        stack.Children.Add(starting_speed);

        System.Windows.Controls.Label ang = new System.Windows.Controls.Label();
        stack.Children.Add(ang);
        ang.Content = "угол бросания";
        System.Windows.Controls.TextBox angle = new System.Windows.Controls.TextBox();
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
            btn = args.Source as System.Windows.Controls.Button;
            if (btn != null) {
                System.Windows.MessageBox.Show("Тело упало:    x = " + p.path_x[p.path_x.Count-1] + "    " +
                                                               "y = " + p.path_y[p.path_y.Count-1]);
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

        OpenFileDialog dialog = new OpenFileDialog();
        dialog.Filter = "Images |*.png;*.jpg";
        if (Convert.ToBoolean(dialog.ShowDialog())) {
            ImageBrush brush = new ImageBrush();
            brush.ImageSource = new BitmapImage(new Uri(dialog.FileName, UriKind.Absolute));
            Data.win.Background = brush;
        }
        Content = Data.win;
    }
}  

