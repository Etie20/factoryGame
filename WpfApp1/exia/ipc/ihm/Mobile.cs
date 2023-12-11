using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace WpfApp1.exia.ipc.ihm;

public class Mobile : Label
{
   private BitmapImage img;
   private List<Point> route;
   private Point next;
   private DispatcherTimer timer;

   public Mobile(GamePanel panel)
   {
       this.Width = 45;
       this.Height = 34;
       this.HorizontalAlignment = HorizontalAlignment.Left;
       this.VerticalAlignment = VerticalAlignment.Top;
       this.Margin = new Thickness(panel.ApplyRatio(new Point(472, 185)).X, panel.ApplyRatio(new Point(472, 185)).Y, 0, 0);
       this.Content = new ImageBrush(new BitmapImage(new Uri("/exia/ipc/ihm/res/Truck.png", UriKind.Relative)));
       this.route = new List<Point>();
       this.route.Add(panel.ApplyRatio(new Point(472, 185)));
       this.route.Add(panel.ApplyRatio(new Point(472, 230)));
       this.route.Add(panel.ApplyRatio(new Point(-80, 230)));
       this.next = this.route[0];
       this.route.RemoveAt(0);
       this.timer = new DispatcherTimer();
       this.timer.Interval = TimeSpan.FromMilliseconds(4000);
       this.timer.Tick += Timer_Tick;
       this.timer.Start();
   }

   private void Timer_Tick(object sender, EventArgs e)
   {
       if (Math.Abs(this.Margin.Left - next.X) < 14)
       {
           this.Margin = new Thickness(next.X, this.Margin.Top, 0, 0);
       }
       else
       {
           if (this.Margin.Left > next.X)
           {
               this.Margin = new Thickness(this.Margin.Left - 14, this.Margin.Top, 0, 0);
           }
           else if (this.Margin.Left < next.X)
           {
               this.Margin = new Thickness(this.Margin.Left + 14, this.Margin.Top, 0, 0);
           }
       }

       if (Math.Abs(this.Margin.Top - next.Y) < 14)
       {
           this.Margin = new Thickness(this.Margin.Left, next.Y, 0, 0);
       }
       else
       {
           if (this.Margin.Top > next.Y)
           {
               this.Margin = new Thickness(this.Margin.Left, this.Margin.Top - 14, 0, 0);
           }
           else if (this.Margin.Top < next.Y)
           {
               this.Margin = new Thickness(this.Margin.Left, this.Margin.Top + 14, 0, 0);
           }
       }

       if (this.route.Count < 1)
       {
           this.timer.Stop();
       }
       else
       {
           this.next = this.route[0];
           this.route.RemoveAt(0);
       }
   }
}
