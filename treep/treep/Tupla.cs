using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Tupla
{
    public Atomic<char> a1{ get; private set; }
    public DataList dl { get; private set; }
    public Atomic<char> a2 { get; private set; }

    public Tupla(DataList dl)
    {
        this.a1 = new Atomic<char>('<');
        this.dl = dl;
        this.a2 = new Atomic<char>('>');
    }
}

