using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class Expr
{
    public string type { get; private set; }
    public Atomic<char> openPar { get; private set; }
    public Expr e { get; private set; }
    public Atomic<char> closePar { get; private set; }
    public Htag htag { get; private set; }
    public TupleAccess ta { get; private set;}
    public string id { get; private set; }
    public int intValue { get; private set; }
    public double doubleValue { get; private set; }
    public string stringValue { get; private set; }
    public bool boolValue { get; private set; }
    public Tree tree { get; private set; }
    public Tupla tuple { get; private set; }
    public FunctionHead fh { get; private set; }
    public Mul mul { get; private set; }
    public Sum sum { get; private set; }

    public Expr(Expr e, Htag htag, TupleAccess ta)
    {
        this.openPar = new Atomic<char>('(');
        this.e = e;
        this.closePar = new Atomic<char>(')');
        this.htag = htag;
        this.ta = ta;
    }

    public Expr(string id, Htag htag, TupleAccess ta)
    {
        this.id = id;
        this.htag = htag;
        this.ta = ta;
    }

    public Expr(int intValue)
    {
        this.intValue = intValue;
        this.type = "INTEGER";
    }

    public Expr(double doubleValue)
    {
        this.doubleValue = doubleValue;
        this.type = "DOUBLE";
    }

    public Expr(string stringValue)
    {
        this.stringValue = stringValue;
        this.type = "STRING";
    }

    public Expr(bool boolValue)
    {
        this.boolValue = boolValue;
        this.type = "BOOL";
    }

    public Expr(Tree tree)
    {
        this.tree = tree;
        this.type = "TREE";
    }

    public Expr(Tupla tuple)
    {
        this.tuple = tuple;
        this.type = "TUPLE";
    }

    public Expr(FunctionHead fh)
    {
        this.fh = fh;
    }

    public Expr(Mul mul, Sum sum)
    {
        this.mul = mul;
        this.sum = sum;
    }
}

