using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class F
{
    public int intValue { get; private set; }
    public double doubleValue { get; private set; }
    public bool boolValue { get; private set; }
    public string stringValue { get; private set; }
    public TP tp{ get; private set; }
    public TR tr { get; private set; }
    public E e { get; private set; }
    public string type { get; private set; }

    public Atomic<Char> par1 { get; private set; }
    public Atomic<Char> par2 { get; private set; }

    public F(E e)
    {
        this.par1 = new Atomic<Char>('(');
        this.e = e;
        this.par2 = new Atomic<Char>(')');
    }

    public F(int intValue)
    {
        this.intValue = intValue;
        this.type = "INTEGER";
        
    }

    public F(double doubleValue)
    {
        this.doubleValue = doubleValue;
        this.type = "DOUBLE";
        
    }

    public F(bool boolValue)
    {
        this.boolValue = boolValue;
        this.type = "BOOL";
    }

    public F(String stringValue,String type)
    {
        this.stringValue = stringValue;
        this.type = type; ;
    }

    public F(TP tp)
    {
        this.tp = tp;
    }

    public F(TR tr)
    {
        this.tr = tr;
    }

}