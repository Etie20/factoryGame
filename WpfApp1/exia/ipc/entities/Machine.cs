using System.Windows;

namespace WpfApp1.exia.ipc.entities;

public abstract class Machine : Node
{
    protected Machine(string name, Point indicatorLocation, Point inputLocation, Point outputLocation) : 
        base(name, indicatorLocation, inputLocation, outputLocation)
    {
    }
    
    public void run () {}

    public Node GetOutputNode()
    {
        return new List<Node>(this.routes.Keys).First();
    }
}