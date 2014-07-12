using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class ElseIf
{
    public Atomic<string> comCode { get; private set; }
    public Block b { get; private set; }

    public ElseIf(Block b)
    {
        this.comCode = new Atomic<string>("else");
        this.b = b;
    }
}

