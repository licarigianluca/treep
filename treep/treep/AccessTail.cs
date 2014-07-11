using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class AccessTail
{
    public int index { get; private set; }
    public string id { get; private set; }
    public AccessTail at { get; private set; }
    public Atomic<char> op { get; private set; }

    public AccessTail(int index, AccessTail at)
    {
        this.op = new Atomic<char>('@');
        this.index = index;
        this.at = at;
    }

    public AccessTail(string id, AccessTail at)
    {
        this.op = new Atomic<char>('@');
        this.id = id;
        this.at = at;
    }
}

