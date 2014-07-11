using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


   public class GuardTail
    {
       public Atomic<string> op{get; private set;}
       public Expr e {get;private set}

       public GuardTail(String op, Expr e)
       {
           this.op = new Atomic<string>(op);
           this.e=e;
       }
   }

