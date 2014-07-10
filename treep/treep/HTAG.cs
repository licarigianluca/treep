using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


   public class HTAG
    {
       
       public Atomic<char> htag { get; private set; }
       
       public HTAG()
       {
           this.htag = new Atomic<char>('#');
       }
    }

