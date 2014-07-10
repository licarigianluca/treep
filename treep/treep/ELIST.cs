using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class ELIST
{
    public Z z { get; private set; }
    public Atomic<char> comma { get; private set; }

    public ELIST(Z z)
    {
        this.z = z;
        this.comma = new Atomic<char>(',');
    }
}

