using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class TR
{
    public E e { get; private set; }
    public Atomic<char> openSquare { get; private set; }
    public N n { get; private set; }
    public Atomic<char> closeSquare { get; private set; }
    
    public TR(E e,N n)
    {
        this.e=e;
        this.openSquare = new Atomic<char>('[');
        this.n = n;
        this.closeSquare = new Atomic<char>(']');
    }
}

