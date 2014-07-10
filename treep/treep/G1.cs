using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class G1
{
    public E e { get; private set; }
    public Atomic<string> op { get; private set; }

    public G1(E e, Atomic<string> op)
    {
        this.e = e;
        this.op = op;
    }
}

