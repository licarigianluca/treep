using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class P
{
    public S s{get; private set;}
    public Atomic<string> eof { get; private set; }

    public P(S s)
    {
        this.s = s;
        this.eof = new Atomic<string>("EOF");
    }
}