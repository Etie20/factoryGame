using System.Windows;

namespace WpfApp1.exia.ipc.entities;

public class MachineY : Machine
{
    public MachineY() :
        base("Y", new Point(328, 72), new Point(260, 95), new Point(335, 95))
    {}

    public void executeJob()
    {
        if (this.counter >=2)
        {
            throw new Exception("La machine " + this.GetName() + " est déjà en fonctionnement : le produit est perdu");
        }
        else
        {
            this.incrementCounter();

            try
            {
                Thread.Sleep(600);
                Product product = new Product(Product.TypeP.M4);
                product.addOperation(1);
                product.addOperation(2);
                MachineZ machine =
                    PrositIPC.Step3.chooseMachine((MachineZ)this.getRoute(0), (MachineZ)this.getRoute(1));
                if (machine == null)
                {
                    throw new Exception("Un produit " + product.GetType() + "a été perdu : pas de machine Z choisie");
                }

                new Thread(new ThreadStart(run)).Start();

                void run()
                {
                    PrositIPC.move(product, this, machine);

                    try
                    {
                        PrositIPC.Step3.onMachineRequest(product, machine, machine.getNextMachine(),
                            machine.getNextMachine().getNextMachine());
                    }
                    catch (Exception e)
                    {
                        PrositIPC.handleError(e);
                        return;
                    }

                    int i = 0;

                    while (!product.isFinished())
                    {
                        try
                        {
                            Thread.Sleep(500);
                        }
                        catch (Exception e)
                        {
                            return;
                        }

                        if (i++ >= 10)
                        {
                            PrositIPC.handleError(new Exception("Un produit " + product.GetType() + "jamais terminé"));
                            return;
                        }
                    }

                    PrositIPC.move(product, machine, machine.GetOutputNode());
                    machine.GetOutputNode().incrementCounter();
                    PrositIPC.score();
                }

                this.decrementCounter();

            }
            catch (Exception e)
            {
                return;
            }
        }
    }
}