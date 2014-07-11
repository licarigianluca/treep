using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class DataList
{
    public Factor f { get; private set; }
    public DataTail dt { get; private set; }

    public DataList(Factor f, DataTail dt)
    {
        this.f = f;
        this.dt = dt;
    }
}

