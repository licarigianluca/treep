using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



public class Mul
{
    public Factor f { get; private set; }
    public MulTail mt { get; private set; }

    public Mul(Factor f, MulTail mt)
    {
        this.f = f;
        this.mt = mt;
    }

}

