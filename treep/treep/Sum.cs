using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Sum
{
    public Factor f1 { get; private set; }
    public Atomic<string> op { get; private set; }
    public Factor f2 { get; private set; }
    public SumTail st { get; private set; }

    public Sum(Factor f1, string op, Factor f2, SumTail st)
    {
        this.f1 = f1;
        this.op = new Atomic<string>(op);
        this.f2 = f2;
        this.st = st;
    }
}

