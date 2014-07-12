using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Program
{
    public StatementList sl { get; private set; }
    public Atomic<string> eof { get; private set; }

    public Program(StatementList sl)
    {
        this.sl = sl;
        this.eof = new Atomic<string>("EOF");
    }
}