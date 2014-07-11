using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class Tree
{
    public Factor f { get; private set; }
    public TreeList tl { get; private set; }
    public Atomic<char> openSquare { get; private set; }
    public Atomic<char> closeSquare { get; private set; }
    public tupla t { get; private set; }

    public Tree(Factor f, TreeList tl)
    {
        this.f = f;
        this.openSquare = new Atomic<char>('[');
        this.tl = tl;
        this.closeSquare = new Atomic<char>(']');
    }

    public Tree(Tupla t, TreeList tl)
    {
        this.t = t;
        this.openSquare = new Atomic<char>('[');
        this.tl = tl;
        this.closeSquare = new Atomic<char>(']');
    }
}

