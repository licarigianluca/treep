using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class LHandValue
{
    public String id { get; private set; }
    public Htag htag { get; private set; }
    public TupleAccess ta { get; private set; }
    public Atomic<char> openPar { get; private set; }
    public AccessTail at { get; private set; }
    public Atomic<char> closePar { get; private set; }


    public LHandValue(String id, Htag htag, TupleAccess ta)
    {
        this.id = id;
        this.htag = htag;
        this.ta = ta;
    }

    public LHandValue(String id, AccessTail at, Htag htag, TupleAccess ta)
    {
        this.openPar = new Atomic<char>('(');
        this.id = id;
        this.at = at;
        this.closePar = new Atomic<char>(')');
        this.htag = htag;
        this.ta = ta;
    }

}

