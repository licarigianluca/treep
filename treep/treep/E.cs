using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class E
{
    public T t { get; private set; }
    public E1 e1{get; private set;}

    public E(T t, E1 e1)
    {
        this.t = t;
        this.e1 = e1;
        
    }
}