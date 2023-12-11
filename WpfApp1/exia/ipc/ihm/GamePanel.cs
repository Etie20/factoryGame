using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace WpfApp1.exia.ipc.ihm;

public class GamePanel : Panel
{
    private BitmapImage imgA;

    public GamePanel()
    {
        imgA = new BitmapImage(new Uri("/exia/ipc/ihm/res/BackgroundA.png", UriKind.Relative));
    }

    protected override void OnRender(DrawingContext drawingContext)
    {
        drawingContext.DrawImage(imgA, new Rect(0, 0, this.Width, this.Height));
    }

    public double GetRatioX()
    {
        return (double)this.Width / imgA.Width;
    }

    public double GetRatioY()
    {
        return (double)this.Height / imgA.Height;
    }

    public Size GetMinimumSize()
    {
        return new Size(imgA.Width, imgA.Height);
    }

    public Size GetPreferredSize()
    {
        return new Size(imgA.Width, imgA.Height);
    }

    public Point ApplyRatio(Point location)
    {
        return new Point(location.X * GetRatioX(), location.Y * GetRatioY());
    }

    public List<Point> ApplyRatio(List<Point> route)
    {
        List<Point> outList = new List<Point>();
        foreach (Point p in route)
        {
            outList.Add(ApplyRatio(p));
        }

        return outList;
    }
}
