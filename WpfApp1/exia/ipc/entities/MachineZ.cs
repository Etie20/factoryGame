using System.Windows;

namespace WpfApp1.exia.ipc.entities;

public class MachineZ : Machine
{
    private Product _job;
    private MachineZ _next;
    private int _type;

    public MachineZ(int id) : this(id, null)
    {
    };

    public MachineZ(int id, MachineZ next) : 
        base("Z" + id, new Point(405, 12 + id * 25 + (id > 2 ? 30 : 0)), new Point(368, 28 + id * 23 + (id > 2 ? 30 : 0)), new Point(415, 28 + id * 23 + (id > 2 ? 30 : 0)))
    {
        this._job = null;
        this._type = id < 3 ? id : id - 3;
        this._next = next;
    }
    

    public void executeJob(Product p)
    {
        if (this._job != null)
        {
            throw new Exception("La machine " + this.GetName() + " est déjà en fonctionnement : le produit est perdu");
        }
        else
        {
            this._job = p;
            this.notifyChange(1);

            try
            {
                if (this._type == 0)
                {
                    Thread.Sleep(300);
                }
                else if (this._type == 1)
                {
                    Thread.Sleep(2100);
                }
                else if (this._type == 2)
                {
                    Thread.Sleep(1500);
                }
            }
            catch (Exception e)
            {
                return;
            }

            if (this._type == 0)
            {
                p.addOperation(4);
            } else if (this._type == 1)
            {
                p.addOperation(8);
            } else if (this._type == 2)
            {
                p.addOperation(16);
            }
            else
            {
                Console.WriteLine("Erreur interne");
            }
            
            this.notifyChange(0);
            this._job = null;
        }
    }

    public bool isMachineAvailable()
    {
        return this._job == null;
    }

    public bool isChainAvailable()
    {
        bool ready = this._job == null;
        if (this._next != null)
        {
            ready = ready && this._next.isMachineAvailable();
        }

        return ready;
    }

    public MachineZ getNextMachine()
    {
        return this._next;
    }
}