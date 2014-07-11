using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class TreeListHead
{
    public Tree t { get; private set; }
    public TreeListTail tlt { get; private set; }

    public TreeListHead(Tree t, TreeListTail tlt)
    {
        this.t = t;
        this.tlt = tlt;
    }
}

