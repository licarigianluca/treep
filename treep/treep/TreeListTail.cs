using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class TreeListTail
{
    public Atomic<char> comma { get; private set; }
    public TreeListHead tlh { get; private set; }

    public TreeListTail(TreeListHead tlh)
    {
        this.comma = new Atomic<char>(',');
        this.tlh = tlh;
    }
}
