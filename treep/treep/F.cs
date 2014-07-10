using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



public class F
    {
    public TYPE type { get; private set; }
    public TAIL tail { get; private set; }

    public F(TYPE type,TAIL tail)
    {
        this.type=type;
        this.tail=tail;
    }
}
