using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class TAIL
{

    public Atomic<char> openSquare { get; private set; }
    public N n { get; private set; }
    public Atomic<char> closeSquare { get; private set; }
    public HTAG htag { get; private set; }

    public TAIL(N n)
    {

        this.openSquare = new Atomic<char>('[');
        this.n = n;
        this.closeSquare = new Atomic<char>(']');
    }
}

