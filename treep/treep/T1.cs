using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class T1
{
    public F f { get; private set; }
    public T1 t1 { get; private set; }
    public Atomic<char> op { get; private set; }

    public T1(F f, T1 t1, char op)
    {
        this.op = new Atomic<char>(op);
        this.f = f;
        this.t1 = t1;
    }
}