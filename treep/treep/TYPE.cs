using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



public class TYPE
{
    public int intValue { get; private set; }
    public double doubleValue { get; private set; }
    public bool boolValue { get; private set; }
    public string stringValue { get; private set; }
    public string idValue { get; private set; }
    public TP tp{ get; private set; }
    public E e { get; private set; }
    public string type { get; private set; }
    public Atomic<Char> par1 { get; private set; }
    public Atomic<Char> par2 { get; private set; }
    public HTAG htag { get; private set; }

    public TYPE(E e ,HTAG htag)
    {
        this.par1 = new Atomic<Char>('(');
        this.e = e;
        this.par2 = new Atomic<Char>(')');
        this.htag = htag;
    }

    public TYPE(String idValue, HTAG htag)
    {
        this.idValue = idValue;
        this.htag = htag;
    }

    public TYPE(int intValue)
    {
        this.intValue = intValue;
        this.type = "INTEGER";
        
    }

    public TYPE(double doubleValue)
    {
        this.doubleValue = doubleValue;
        this.type = "DOUBLE";
        
    }

    public TYPE(bool boolValue)
    {
        this.boolValue = boolValue;
        this.type = "BOOL";
    }

    public TYPE(String stringValue)
    {
        this.stringValue = stringValue;
        this.type = "STRING" ;
    }

    public TYPE(TP tp)
    {
        this.tp = tp;
    }

    
}