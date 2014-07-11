using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class ArgumentList
{
    public string id { get; private set; }
    public ArgumentTail at { get; private set; }

    public ArgumentList(string id, ArgumentTail at)
    {
        this.id = id;
        this.at = at;
    }
}

