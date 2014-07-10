using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class X
{
    public E e { get; private set; }
    public Y y { get; private set; }
    public Atomic<char> assign { get; private set; }
    public AR ar { get; private set; }
    public B b { get; private set; }

    public X(E e, Y y)
    {
        this.e = e;
        this.assign = new Atomic<char>('=');
        this.y = y;

    }
    public X(AR ar, B b)
    {
        this.ar = ar;
        this.b = b;
    }
}