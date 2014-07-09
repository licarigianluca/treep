using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class TP
{
    public Atomic<char> a1{ get; private set; }
    public E e { get; private set; }
    public TP1 tp1 { get; private set; }
    public Atomic<char> a2 { get; private set; }

    public TP(E e,TP1 tp1)
    {
        this.a1 = new Atomic<char>('<');
        this.e = e;
        this.tp1 = tp1;
        this.a2 = new Atomic<char>('>');
    }
}

