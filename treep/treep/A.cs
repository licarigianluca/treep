using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class A
{
    public string id { get; private set; }
    public A1 a1 { get; private set; }

    public A(string id, A1 a1)
    {
        this.id = id;
        this.a1 = a1;
    }
}

