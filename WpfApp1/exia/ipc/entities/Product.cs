namespace WpfApp1.exia.ipc.entities;

public class Product
{
    private int _step = 0;
    private TypeP _type;
    private bool _finished;
    public static int X = 1;
    public static int Y = 2;
    public static int Z1 = 4;
    public static int Z2 = 8;
    public static int Z3 = 16;

    public Product(TypeP type)
    {
        this._type = type;
    }

    public void addOperation(int value)
    {
        if ((this._step & value) == value)
        {
            throw new Exception("Le produit " + this._type + " est déjà sur la machine " + this.machineName(value));
        }
        else
        {
           this._step |= value;   
        }
    }
    
    public TypeP GetType(){return this._type;}

    public bool isFinished()
    {
        return this._finished && this._step >= 31;
        
    }

    public void makeFinshed()
    {
        if (this._finished)
        {
            throw new Exception();
        }  else if (this._step < 31)
        {
            throw new Exception("Le produit " + this._type + " est marqué comme terminé , mais il n'est pas passé par toutes les étapes.");
        }
        else
        {
            this._finished = true;
        }
        
        
    }

    public string machineName(int value)
    {
        if (value == 1)
        {
            return "X";
        }
        else if (value == 2)
        {
            return "Y";
        }
        else if (value == 4)
        {
            return "Za";
        }
        else if (value == 8)
        {
            return "Zb";
        }
        else
        {
            return value == 16 ? "Zc" : "?";
        }
    }

    public enum TypeP
    {
        M1,
        M2,
        M3,
        M4
        
    }
    
    
}