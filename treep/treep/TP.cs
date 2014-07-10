using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class TP
{
    public Atomic<char> a1{ get; private set; }
    public Z z { get; private set; }
    public Atomic<char> a2 { get; private set; }

    public TP(Z z)
    {
        this.a1 = new Atomic<char>('<');
        this.z = z;
        this.a2 = new Atomic<char>('>');
    }
}

