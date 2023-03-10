
using System;
using System.ComponentModel;
using System.Security.AccessControl;

class Point
{
    public int X { get; protected set; }
    public int Y { get; protected set; }

    public Point()
    {
        X = 0;
        Y = 0;
    }
    public Point(int x)
    {
        X = x;
        Y = 0;
    }
    public Point(int x, int y)
    {
        X = x;
        Y = y;
    }

    public override string ToString()
    {
        return $"[{X} : {Y}]\n";
    }

    public bool Equals(Point p)
    {
        if (this.X == p.X && this.Y == p.Y)
            return true;
        else
            return false;
    }
    public static Point operator +(Point a, Point b)
    {
        return new Point(a.X + b.X, a.Y + b.Y);
    }
}

class ColoredPoint:Point
{
    private string Color;

    public ColoredPoint() : base()
    {
        Color = "Black";
    }
    public ColoredPoint(int x) : base(x)
    {
        Color = "Black";
    }
    public ColoredPoint(int x,int y) : base(x,y)
    {
        Color = "Black";
    }
    public ColoredPoint(int x,int y,string col) : base(x,y)
    {
        Color = col;
    }
    public override string ToString()
    {
        return $"[{X} : {Y}] Color= {Color}\n";
    }
    public bool Equals(ColoredPoint p)
    {
        if (this.X == p.X && this.Y == p.Y&&this.Color==p.Color)
            return true;
        else
            return false;
    }
}

class MultiAngle:Point
{
    protected Point[] Points = new Point[100];
    protected int Angles = 0;
    public MultiAngle(params Point [] p)
    {
        Array.Copy(p,Points, p.Length);
        Angles = p.Length;
    }

    public override string ToString()
    {
        string s;
        if (Angles == 2)
            s = "Линия: \n";
        else
            s = $"{Angles}-угольник: \n";
        for (int i = 0; i < Angles; i++)
            s += Points[i].ToString();

       return s;
    }
    public bool Equals(MultiAngle p)
    {
        bool Check = true; 
        for (int i = 0; i < Angles; i++)
        {
            if (!this.Points[i].Equals(p.Points[i]))
                Check = false;
        }
        return Check;
    }
}

class ColoredLine:MultiAngle
{
    private string Color;
    public ColoredLine(params Point[] p)
    {
        Array.Copy(p, Points, p.Length);
        Angles = p.Length;
        Color = "Black";
    }
    public ColoredLine(string col,params Point[] p)
    {
        Array.Copy(p, Points, p.Length);
        Angles = p.Length;
        Color = col;
    }
    public override string ToString()
    {
        string s;
        if (Angles == 2)
            s = "Линия: \n";
        else
            s = $"{Angles}-угольник: \n";
        for (int i = 0; i < Angles; i++)
            s += Points[i].ToString();
        s += "Color= " + Color;
        return s;
    }
    public bool Equals(ColoredLine p)
    {
        bool Check = true;
        for (int i = 0; i < Angles; i++)
        {
            if (!this.Points[i].Equals(p.Points[i]))
                Check = false;
        }
        if (this.Color != p.Color)
            Check = false;

        return Check;
    }
}

class HelloWorld
{
    static void Main()
    {
        Console.WriteLine("\n\n\nСложение точек: ");
        Point p = new Point(1, 1);
        Point p1 = new Point(1, 2);
        Console.WriteLine(p.ToString() +"   +\n"+ p1.ToString());
        Point p2 = p + p1;
        Console.WriteLine("Ответ: "+p2.ToString());
        Console.WriteLine("\n\n\nСравнение линий:");
        MultiAngle l = new MultiAngle(p, p1);
        MultiAngle l1 = new MultiAngle(p, p1);
        Console.WriteLine(l.ToString());
        Console.WriteLine(l1.ToString());
        Console.WriteLine(l.Equals(l1));
        Console.WriteLine("\n\n\nЦветная линия: ");
        ColoredLine cl = new ColoredLine("White",p,p1);
        Console.WriteLine(cl.ToString());
        Console.WriteLine("\n\n\nЦветная точка:");
        ColoredPoint cp = new ColoredPoint(1,2,"Black");
        Console.WriteLine(cp.ToString());
        Console.WriteLine("\n\n\nМногоугольник:");
        MultiAngle m = new MultiAngle(p, p1, new Point(1, 3));
        Console.WriteLine(m.ToString());
    }
}
