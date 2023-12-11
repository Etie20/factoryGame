using System.Collections;
using System.Windows;
using WpfApp1.exia.ipc.fail;
using WpfApp1.exia.ipc.ihm;

namespace WpfApp1.exia.ipc.entities;

public class PrositIPC
{
    private static List<Node> jobs;
    public static IStep1Strategy Step1 = new WrongStep1();
    public static IStep2Strategy Step2 = new WrongStep2();
    public static IStep3Strategy Step3 = new WrongStep3();
    private static ViewController ctrl;
    private static OutputDock _outputDock;
    private static int _audioSize;

    private static int _score = 0;

    static PrositIPC()
    {
        
        jobs = new List<Node>();
        InputDock q1 = new InputDock(Product.TypeP.M1, new Point(50, 38), new Point(78, 63));
        InputDock q2 = new InputDock(Product.TypeP.M2, new Point(50, 160), new Point(78, 130));
        q1.addProducts(3);
        q2.addProducts(2);
        MachineX m1 = new MachineX(1, q1, q2);
        MachineX m2 = new MachineX(2, q1, q2);
        MachineX m3 = new MachineX(3, q1, q2);
        q1.addRoute(m1, new Point[]{new Point(78, 95), new Point(143, 95), new Point(143, 65)});
        q2.addRoute(m1, new Point[]{new Point(78, 95), new Point(143, 95), new Point(143, 65)});
        q1.addRoute(m2, new Point[]{new Point(78, 95), new Point(143, 95)});
        q2.addRoute(m2, new Point[]{new Point(78, 95), new Point(143, 95)});
        q1.addRoute(m3, new Point[]{new Point(78, 95), new Point(143, 95), new Point(143, 125)});
        q2.addRoute(m3, new Point[]{new Point(78, 95), new Point(143, 95), new Point(143, 125)});
        MachineY m4 = new MachineY();
        m1.addRoute(m4, new Point[]{new Point(238, 67), new Point(238, 95)});
        m2.addRoute(m4, new Point[]{new Point(230, 95), new Point(238, 95)});
        m3.addRoute(m4, new Point[]{new Point(238, 125), new Point(238, 95)});
        MachineZ m7 = new MachineZ(2);
        MachineZ m6 = new MachineZ(1, m7);
        MachineZ m5 = new MachineZ(0, m6);
        MachineZ m10 = new MachineZ(5);
        MachineZ m9 = new MachineZ(4, m10);
        MachineZ m8 = new MachineZ(3, m9);
        m4.addRoute(m5, new Point[]{new Point(363, 95)});
        m4.addRoute(m8, new Point[]{new Point(363, 95)});
        _outputDock = new OutputDock();
        m5.addRoute(_outputDock, new Point[]{new Point(420, 100)});
        m8.addRoute(_outputDock, new Point[]{new Point(420, 100)});
    }
    
    private PrositIPC(){}

    public static void start()
    {
        
    }
    public static Thread moveAsync(Product p, Node from, Node to)
    {
        Thread t = new Thread(new ThreadStart(run));

        void run()
        {
            PrositIPC.move(p, from, to);
        }
        t.Start();
        return t;
    }
    
    static void register(Node job)
    {
        jobs.Add(job);
    }
}