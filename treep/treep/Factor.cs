using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Factor
{
    public string type{ get; private set; }
    public int intValue { get; private set; }
    public double doubleValue { get; private set; }
    public bool boolValue { get; private set; }
    public string id { get; private set; }
    public string stringValue { get; private set; }
    public Atomic<char> openPar { get; private set; }
    public Atomic<char> closePar { get; private set; }
    public Sum sum { get; private set; }
    public Mul mul { get; private set; }
    public FunctionHead fh { get; private set; }
    public Atomic<char> nullValue { get; private set; }

    public Factor(int intValue)
    {
        this.intValue = intValue;
        this.type = "INTEGER";
    }

    public Factor(double doubleValue)
    {
        this.doubleValue = doubleValue;
        this.type = "DOUBLE";
    }

    public Factor(bool boolValue)
    {
        this.boolValue = boolValue;
        this.type = "BOOL";
    }

    public Factor(string value, string type)
    {
        if (type == "STRING")
        {
            this.stringValue = value;
            this.type = type;
        }
        else
        {
            this.id = value;
            this.type = type;
        }
    }

    public Factor(Mul mul, Sum sum)
    {
        this.openPar = new Atomic<char>('(');
        this.mul = mul;
        this.sum = sum;
        this.openPar = new Atomic<char>(')');
    }

    public Factor(FunctionHead fh)
    {
        this.fh = fh;
    }

    public Factor(Atomic<string> nullValue)
    {
        this.nullValue=nullValue;
    }
}
