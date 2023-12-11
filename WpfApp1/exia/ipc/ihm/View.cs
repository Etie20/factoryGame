using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace WpfApp1.exia.ipc.ihm;

public class View : Window
{
    public StackPanel ContentPane { get; set; }
    public GamePanel GamePanel { get; set; }
    public Label LabelCoins { get; set; }
    public TextBox TextArea { get; set; }

    public View()
    {
        this.Title = "Exia - Prosit IPC";
        this.Width = 615;
        this.Height = 483;

        ContentPane = new StackPanel();
        this.Content = ContentPane;

        GamePanel = new GamePanel();
        GamePanel.Background = Brushes.Transparent;

        LabelCoins = new Label();
        LabelCoins.Content = "0";
        LabelCoins.Background = new ImageBrush(new BitmapImage(new Uri("/exia/ipc/ihm/res/coins.png", UriKind.Relative)));

        TextArea = new TextBox();
        TextArea.Foreground = Brushes.Red;

        ContentPane.Children.Add(GamePanel);
        ContentPane.Children.Add(LabelCoins);
        ContentPane.Children.Add(TextArea);
    }
}
