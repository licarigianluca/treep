using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class C
{
    public String id{get ;private set;}
    public E e{get ;private set;}
    public B b { get; private set; }
    public C c1 { get; private set; }
    public C c2 { get; private set; }
    public G g { get; private set; }
    public X x { get; private set; }
    public EIF eif { get; private set; }
    public Atomic<string> comcode { get; private set; }
    public Atomic<string> assign { get; private set; }

    public String argument { get; private set; }

    public C(X x, String ID)
    {
        this.id = ID;
        this.x = x;
    }

    public C(C c1, G g, C c2, B b)
    {
        
        this.c1 = c1;
        this.c2 = c2;
        this.b = b;
        this.comcode = new Atomic<string>("for");
              
    }

    public C(G g, B b, EIF eif)
    {
        this.g = g;
        this.b = b;
        this.eif = eif;
        this.comcode = new Atomic<string>("if");
    }

    
    public C(E e)
    {
        this.e = e;
        this.comcode = new Atomic<string>("print");
    }
}