﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class TP1
{
    public Atomic<char> comma { get; private set; }
    public Z z { get; private set; }

    public TP1(Z z)
    {
        this.z = z;
        this.comma = new Atomic<char>(',');
    }

}

