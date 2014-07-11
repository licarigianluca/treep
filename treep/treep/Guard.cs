using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

    public class Guard
    {
        public Expr e {get; private set;}
        public GuardTail gt {get; private set;}

        public Guard(Expr e, GuardTail gt)
        {
            this.e = e;
            this.gt = gt;
        }
    }

