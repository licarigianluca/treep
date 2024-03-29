﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class TupleAccess
{
    public int index { get; private set; }
    public string id { get; private set; }

    public TupleAccess ta { get; private set; }

    public TupleAccess(int index, TupleAccess ta)
    {
        this.index = index;
        this.ta = ta;
    }

    public TupleAccess(string id, TupleAccess ta)
    {
        this.id = id;
        this.ta = ta;
    }
}

