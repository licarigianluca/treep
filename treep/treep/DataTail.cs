using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class DataTail
{
    public Atomic<char> comma { get; private set; }
    public DataList dl { get; private set; }

    public DataTail(DataList dl)
    {
        this.comma = new Atomic<char>(',');
        this.dl = dl;
    }
}


