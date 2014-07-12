using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class AssignmentTail
{
    public Atomic<string> op { get; private set; }
    public Expr e { get; private set; }
    public AssignmentTail(string op, Expr e)
    {
        this.op = new Atomic<string>(op);
        this.e = e;
    }
}

