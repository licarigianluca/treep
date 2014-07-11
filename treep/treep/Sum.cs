using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Sum
{
    public Factor f { get; private set; }
    public SumTail st { get; private set; }

    public Sum(Factor f, SumTail mt)
    {
        this.f = f;
        this.st = st;
    }
}

