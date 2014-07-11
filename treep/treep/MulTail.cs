using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class MulTail
{
    public Atomic<string> op { get; private set; }
    public Mul mul { get; private set; }

    public MulTail(String op, Mul mul)
    {
        this.op = new Atomic<string>(op);
        this.mul = mul;
    }

}

