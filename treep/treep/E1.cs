using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class E1
{
    public T t{ get; private set; }
    public E1 e1 { get; private set; }
    public Atomic<char> op { get; private set; }

    public E1(T t, E1 e1, char op)
    {
        this.op = new Atomic<char>(op);
        this.t = t;
        this.e1 = e1;
    }
}