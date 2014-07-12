using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Assignment
{
    public LHandValue lhv { get; private set; }
    public AssignmentTail at { get; private set; }

    public Assignment(LHandValue lhv, AssignmentTail at)
    {
        this.lhv = lhv;
        this.at = at;
    }
}

