using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


   public class B
    {
       public S s { get; private set; }
       public Atomic<char> openCurly { get; private set; }
       public Atomic<char> closeCurly { get; private set; }
       
       public B(S s)
       {
           this.openCurly = new Atomic<char>('{');
           this.s = s;
           this.closeCurly = new Atomic<char>('}');
       }
    }

