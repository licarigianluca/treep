using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class S
{
    public C c { get; private set; }
    public S1 s1 { get; private set; }

    public S(C c, S1 s1)
    {
        this.c = c;
        this.s1 = s1;
    }
}