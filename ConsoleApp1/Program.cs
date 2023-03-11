
using System;
using System.ComponentModel;
using System.Security.AccessControl;

interface IComparable
{
    public bool Equals(object p);
    public int GetHashCode();
}
class Point:IComparable
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
        return $"({X} : {Y})\n";
    }

    public override bool Equals(object p)
    {
        Point temp = (Point)p;
        if (this.X == temp.X && this.Y == temp.Y)
            return true;
        else
            return false;
    }
    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public static bool operator ==(Point a, Point b)
    {
       return a.Equals(b);
    }

    public static bool operator !=(Point a, Point b)
    {
        return !(a.Equals(b));
    }
    public static Point operator +(Point a, Point b)
    {
        return new Point(a.X + b.X, a.Y + b.Y);
    }
}

class ColoredPoint:Point
{
    private string Color;
    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

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
        return $"({X} : {Y}) Color= {Color}\n";
    }
    public override bool Equals(object p)
    {
        ColoredPoint temp = (ColoredPoint)p;
        if (this.X == temp.X && this.Y == temp.Y&&this.Color == temp.Color)
            return true;
        else
            return false;
    }
    public static bool operator ==(ColoredPoint a, ColoredPoint b)
    {
        return a.Equals(b);
    }

    public static bool operator !=(ColoredPoint a, ColoredPoint b)
    {
        return !(a.Equals(b));
    }
}

class MultiAngle:Point
{
    protected Point[] Points = new Point[100];
    protected int Angles = 0;
    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
    public MultiAngle()
    {
        Points[0] = new Point(0, 0);
        Angles = 1;
    }
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
    public override bool Equals(object p)
    {
        bool Check = true;
        MultiAngle temp = (MultiAngle)p;
        if (Angles == temp.Angles)
        {
            for (int i = 0; i < Angles; i++)
            {
                if (!this.Points[i].Equals(temp.Points[i]))
                    Check = false;
            }
        }
        return Check;
    }
    public static bool operator ==(MultiAngle a, MultiAngle b)
    {
        return a.Equals(b);
    }

    public static bool operator !=(MultiAngle a, MultiAngle b)
    {
        return !(a.Equals(b));
    }

    public void MoveToX(int x)
    {
        for (int i = 0; i < Angles; i++)
            Points[i] = new Point(Points[i].X + x, Points[i].Y);
    }

    public void MoveToY(int y)
    {
        for (int i = 0; i < Angles; i++)
            Points[i] = new Point(Points[i].X, Points[i].Y+y);
    }

    public void Move(int x, int y)
    {
        for (int i = 0; i < Angles; i++)
            Points[i] = new Point(Points[i].X + x, Points[i].Y+y);
    }
}

class ColoredLine:MultiAngle
{
    private string Color;
    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
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
    public override bool Equals(object p)
    {
        bool Check = true;
        ColoredLine temp = (ColoredLine)p;
        if (Angles == temp.Angles)
        {
            for (int i = 0; i < Angles; i++)
            {
                if (!this.Points[i].Equals(temp.Points[i]))
                    Check = false;
            }
        }
        if (this.Color != temp.Color)
            Check = false;
        return Check;
    }
    public static bool operator ==(ColoredLine a, ColoredLine b)
    {
        return a.Equals(b);
    }

    public static bool operator !=(ColoredLine a, ColoredLine b)
    {
        return !(a.Equals(b));
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
        Console.WriteLine(l==l1);
        Console.WriteLine("\n\n\nЦветная линия: ");
        ColoredLine cl = new ColoredLine("White",p,p1);
        Console.WriteLine(cl.ToString());
        Console.WriteLine("\n\n\nЦветная точка:");
        ColoredPoint cp = new ColoredPoint(1,2,"Black");
        Console.WriteLine(cp.ToString());
        Console.WriteLine("\n\n\nМногоугольник:");
        MultiAngle m = new MultiAngle(p, p1, new Point(1, 3));
        Console.WriteLine(m.ToString());
        Console.WriteLine("\n\n\nМногоугольник перемещён:");
        m.Move(2, 2);
        Console.WriteLine(m.ToString());
    }
}
