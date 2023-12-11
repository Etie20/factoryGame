using System.Windows;

namespace WpfApp1.exia.ipc.entities;

public class InputDock : Node
{
    private List<Product> stock = new List<Product>();

    private bool _working = false;

    private Product.TypeP product;
    
    public InputDock(Product.TypeP type, Point indicatorLocation, Point outputLocation) :
        base("Q" + type, indicatorLocation, new Point(), outputLocation)
    {
        this.product = type;
    }

    public int getAvailableProductsCount()
    {
        return this.stock.Count;
    }

    public Product accept()
    {
        if (this._working)
        {
            throw new Exception("Accès concurrent sur le dock " + this.GetName() + " : 5 secondes de pénalité");
        } else if (this.stock.Count < 1)
        {
            throw new Exception("Plus de produits sur le quai " + this.GetName());
        } else
        {
            this._working = true;

            try
            {
                Thread.Sleep(200);
            }
            catch (ThreadInterruptedException var2)
            {
                return null;
            }

            Product p = this.stock[0];
            this.stock.RemoveAt(0);
            this._working = false;
            this.notifyChange(this.stock.Count);
            return p;
        }
    }

    public bool isCurrentlyPickingUp()
    {
        return this._working;
    }

    public void run()
    {
        while (Thread.CurrentThread.IsAlive)
        {
            Random rnd = new Random();
            int prod = 1 + rnd.Next(3);

            while (prod-- > 0)
            {
                this.stock.Add(new Product(this.product));
            }
            
            this.notifyChange(this.stock.Count);

            try
            {
                Random rnd2 = new Random();
                Thread.Sleep((2 + rnd2.Next(4)) * 900);
            }
            catch (Exception e)
            {
                return;
            }
        }
    }

    public bool isProductAvailable()
    {
        return this.stock.Any();
    }

    public void addProduct(Product p)
    {
        this.stock.Add(p);
    }

    public void addIndicatorListener(IndicatorListener l)
    {
        base.addIndicatorListener(l);
        l.notifyChange(this.stock.Count);
    }

    public void addProducts(int i)
    {
        while (i-- > 0)
        {
            this.addProduct(new Product(this.product));
        }
    }
    
}