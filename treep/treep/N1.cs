using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class N1
{
    public Atomic<char> comma { get; private set; }
    public N n { get; private set; }

    public N1(N n)
    {
        this.comma = new Atomic<char>(',');
        this.n = n;
    }
}

