﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class Block
{
    public Atomic<char> openCurly { get; private set; }
    public StatementList sl { get; private set; }
    public Atomic<char> closeCurly { get; private set; }
    public Statement s { get; private set; }
    public StatementTail st { get; private set; }
    
    public Block(StatementList sl)
    {
        this.openCurly = new Atomic<char>('{');
        this.sl = sl;
        this.closeCurly = new Atomic<char>('}');
    }

    public Block(Statement s, StatementTail st)
    {
        this.s = s;
        this.st = st;
    }
}

