using System.Collections;
using System.Windows;

namespace WpfApp1.exia.ipc.entities;

public abstract class Node
{
    private List<IndicatorListener> _listeners = new List<IndicatorListener>();
    protected Dictionary<Node, Point[]> routes = new Dictionary<Node, Point[]>();
    private string _name;
    private Point _indicatorLocation;
    private Point _inputLocation;
    private Point _outputLocation;
    public int counter = 0;

    protected Node(string name, Point indicatorLocation, Point inputLocation, Point outputLocation)
    {
        this._name = name;
        this._indicatorLocation = indicatorLocation;
        this._inputLocation = inputLocation;
        this._outputLocation = outputLocation;
    }
    
    protected void addIndicatorListener(IndicatorListener l){this._listeners.Add(l);}

    protected void notifyChange(int value)
    {
        foreach (IndicatorListener i in this._listeners)
        {
            i.notifyChange(value);
        }
    }

    public string GetName()
    {
        return this._name;
    }

    public Point GetIndicatorLocation()
    {
        return this._indicatorLocation;
    }

    public Point GetInputLocation()
    {
        return this._inputLocation;
    }

    public Point GetOutputLocation()
    {
        return this._outputLocation;
    }

    public void addRoute(Node target, params Point[] points)
    {
        this.routes.Add(target, points);
    }

    public Point[] getRoute(Node target)
    {
        Point[] route;
        if (this.routes.TryGetValue(target, out route))
        {
            return route;
        }
        else
        {
            return null;   
        }
    }

    public Node getRoute(int index)
    {
        return new List<Node>(this.routes.Keys).ElementAt(index);
    }

    public void incrementCounter()
    {
        ++this.counter;
        this.notifyChange(this.counter);
    }

    protected void decrementCounter()
    {
        --this.counter;
        this.notifyChange(this.counter);
    }

    protected void resetCounter()
    {
        this.counter = 0;
        this.notifyChange(this.counter);
    }
}