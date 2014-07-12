using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



public class Mul
{
    public Factor f1 { get; private set; }
    public Atomic<string> op { get; private set; }
    public Factor f2 { get; private set; }
    public MulTail mt { get; private set; }

    public Mul(Factor f1, string op, Factor f2, MulTail mt)
    {
        this.f1 = f1;
        this.op = new Atomic<string>(op);
        this.f2 = f2;
        this.mt = mt;
    }

}
