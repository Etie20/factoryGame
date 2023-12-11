using System.Windows;

namespace WpfApp1.exia.ipc.entities;

public class MachineX : Machine 
{
    private int _tempsTraitement = 4;
    private InputDock _q1;
    private InputDock _q2;

    public MachineX(int id, InputDock q1, InputDock q2) :
        base("X" + id, new Point(215, 30 + id * 31), new Point(160, 35 + id * 31), new Point(222, 37 + id * 31))
    {
        this._q1 = q1;
        this._q2 = q2;
    }
    
    public Product executeWork()
    {
        try
        {
            Random rnd = new Random();
            Thread.Sleep(this._tempsTraitement * 500 + rnd.Next(800));
        }
        catch (Exception e)
        {
            return null;
        }

        return new Product(Product.TypeP.M3);
    }
    
    public void run()
    {
        while (Thread.CurrentThread.IsAlive)
        {
            try
            {
                Product p1 = PrositIPC.Step1.onMachineRequest(this._q1, this);
                Product p2 = PrositIPC.Step1.onMachineRequest(this._q2, this);
                Thread t1 = PrositIPC.moveAsync(p1, this._q1, this);
                PrositIPC.move(p2, this._q2, this);
                t1.Join();

                if (p1 == null)
                {
                    throw new Exception(this._q1 + "la méthode onMachineRequest() a renvoyé un NULL");
                }

                if (p2 == null)
                {
                    throw new Exception(this._q2 + "la méthode onMachineRequest() a renvoyé un NULL");
                }

                this.notifyChange(1);
                Product p3 = this.executeWork();
                p3.addOperation(1);
                MachineY next = (MachineY)this.GetOutputNode();
                PrositIPC.Step2.onMachineRequest(this, next);
                new Thread(new ThreadStart(run)).Start();

                void run()
                {
                    try
                    {
                        this.notifyChange(0);
                        PrositIPC.move(p3, this, next);
                        PrositIPC.Step2.onMachineExecute(this, next);
                    }
                    catch (Exception e)
                    {
                        PrositIPC.handleError(e);
                    }
                }
            }
            catch (Exception e)
            {
                try
                {
                    PrositIPC.handleError(e);
                    Thread.Sleep(5000);
                }
                catch (Exception exception)
                {
                    return;
                }
            }
        }
    }
    
    
}