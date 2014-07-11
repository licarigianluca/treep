using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class ArgumentTail
{
    public Atomic<char> comma { get; private set; }
    public ArgumentList al { get; private set; }

    public ArgumentTail(ArgumentList al)
    {
        this.comma = new Atomic<char>(',');
        this.al = al;
    }
}

