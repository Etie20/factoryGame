using System.ComponentModel;
using System.Drawing;

namespace WpfApp1.exia.ipc.ihm;

public class ViewController
{
    public View view;
    private List<Mobile> _moves;

    public ViewController(View v)
    {
        this.view = v;
        this._moves = new List<Mobile>();
    }

    public void run()
    {
        // MessageConsole mc = new MessageConsole(this.view.TextArea);
        // mc.RedirectOut();
        // mc.RedirectErr(Color.Red, null);
        // this.view.Clie += (s, e) =>
        // {
        //     Component[] var5;
        //     int var4 = (var5 = this.view.GamePanel.Controls.To)
        // };

    }

    public List<Mobile> GetMoves()
    {
        return this._moves;
    }
}