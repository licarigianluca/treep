using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class TP1
{
    public Atomic<char> comma { get; private set; }
    public TP tp { get; private set; }

    public TP1(TP tp)
    {
        this.tp = tp;
        this.comma = new Atomic<char>(',');
    }

}

