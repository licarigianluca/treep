using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class T
{
    public F f { get; private set; }
    public T1 t1 { get; private set; }


    public T(F f, T1 t1)
    {
        this.f = f;
        this.t1 = t1;
    }
}