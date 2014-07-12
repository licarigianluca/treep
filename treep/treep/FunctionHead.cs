using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class FunctionHead
{
    public string id { get; private set; }
    public Atomic<char> openPar { get; private set; }
    public ArgumentList al { get; private set; }
    public Atomic<char> closePar { get; private set; }

    public FunctionHead(string id, ArgumentList al)
    {
        this.id = id;
        this.openPar = new Atomic<char>('(');
        this.al = al;
        this.closePar = new Atomic<char>(')');
    }
}

