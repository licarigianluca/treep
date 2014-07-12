using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Statement
{
    public Assignment as1 { get; private set; }
    public Assignment as2 { get; private set; }
    public FunctionHead fh { get; private set; }
    public FunctionTail ft { get; private set; }
    public Atomic<string> comCode { get; private set; }
    public Atomic<char> semicolon1 { get; private set; }
    public Atomic<char> semicolon2 { get; private set; }
    public Atomic<char> openPar { get; private set; }
    public Atomic<char> closePar { get; private set; }
    public Guard g { get; private set; }
    public Block b { get; private set; }
    public ElseIf ei { get; private set; }
    public Expr e { get; private set; }

    public Statement(Assignment assignment)
    {
        this.as1 = assignment;
    }

    public Statement(FunctionHead fh, FunctionTail ft)
    {
        this.fh = fh;
        this.ft = ft;
    }

    public Statement(Assignment as1, Guard g, Assignment as2, Block b)
    {
        this.comCode = new Atomic<string>("for");
        this.openPar = new Atomic<char>('(');
        this.as1 = as1;
        this.semicolon1 = new Atomic<char>(';');
        this.g = g;
        this.semicolon2 = new Atomic<char>(';');
        this.as2 = as2;
        this.closePar = new Atomic<char>(')');
        this.b = b;
    }

    public Statement(Guard g, Block b, ElseIf ei)
    {
        this.comCode = new Atomic<string>("if");
        this.openPar = new Atomic<char>('(');
        this.g = g;
        this.closePar = new Atomic<char>(')');
        this.b = b;
        this.ei = ei;
    }

    public Statement(String comCode, Expr e)
    {
        this.comCode = new Atomic<string>(comCode);
        this.e = e;
    }

}

