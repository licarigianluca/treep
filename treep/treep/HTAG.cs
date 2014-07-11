using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Htag
{
    public Atomic<char> htag { get; private set; }

    public Htag()
    {
        this.htag = new Atomic<char>('#');
    }
}

