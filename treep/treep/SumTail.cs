using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class SumTail
{
    public Atomic<string> op { get; private set; }
    public Sum sum { get; private set; }

    public SumTail(String op, Sum sum)
    {
        this.op = new Atomic<string>(op);
        this.sum = sum;
    }
}

