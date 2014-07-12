using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class StatementTail
{
    public StatementList sl { get; private set; }
    public Atomic<char> semicolon { get; private set; }

    public StatementTail(StatementList sl)
    {
        this.semicolon = new Atomic<char>(';');
        this.sl = sl;

    }
}

