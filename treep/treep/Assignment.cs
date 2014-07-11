using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Assignment
{
    public LHandValue lhv { get; private set; }
    public Expr e { get; private set; }
    public Atomic<string> op { get; private set; }

    public Assignment(LHandValue lhv, String op, Expr e)
    {
        this.lhv = lhv;
        this.op = new Atomic<string>(op);
        this.e = e;
    }
}

