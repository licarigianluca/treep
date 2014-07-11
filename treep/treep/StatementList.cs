using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



public class StatementList
{
    public Statement s { get; private set; }
    public StatementTail st { get; private set; }

    public StatementList(Statement s, StatementTail st)
    {
        this.s = s;
        this.st = st;
    }
}