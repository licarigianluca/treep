using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



public class Parser
{
    private Tokenizer t;
    Token lookahead;

    public Parser()
    {

    }

    public T parse<T>(String s)
    {
        t = new Tokenizer(s);
        lookahead = t.nextToken();

        T p = (T)(Object)P();
        Match(type.EOF);
        return p;

    }
    Program P()
    {
        return new Program(S());
    }
    //S -> C S1 | epsilon
    StatementList S()
    {
        if (lookahead.Type == (int)type.ID | lookahead.Type == (int)type.FOR | lookahead.Type == (int)type.IF | lookahead.Type == (int)type.PRINT | lookahead.Type == (int)type.RETURN)
        {
            return new StatementList(C(), S1());
        }
        else return null;
    }
    //S1 -> ';' S
    StatementTail S1()
    {

        Match(type.SEMICOLON);
        return new StatementTail(S());

    }
    //C ->  ID X                        |				
    //'for' '(' C ';' G ';' C ')' B     | 	
    //'if' '(' G ')' B EIF              |
    //'print' E 
    //'return' E
    C C()
    {
        C c1;
        C c2;
        G g;
        B b;
        EIF eif;

        if (lookahead.Type == (int)type.ID)
        {
            String value = lookahead.Value;
            Match(type.ID);
            return new C(X(), value);
        }
        else if (lookahead.Type == (int)type.FOR)
        {
            Match(type.FOR);
            Match(type.OPEN_PAR);
            c1 = C();
            Match(type.SEMICOLON);
            g = G();
            Match(type.SEMICOLON);
            c2 = C();
            Match(type.CLOSE_PAR);
            b = B();
            return new C(c1, g, c2, b);
        }
        else if (lookahead.Type == (int)type.IF)
        {
            Match(type.IF);
            Match(type.OPEN_PAR);
            g = G();
            Match(type.CLOSE_PAR);
            b = B();
            eif = EIF();
            return new C(g, b, eif);


        }else if (lookahead.Type == (int)type.RETURN)
        {
            Match(type.RETURN);
            return new C(E());
        }
        else
        {
            Match(type.PRINT);
            return new C(E());
        }
    }
    //X -> '=' E Y          | 			
     //    '(' AR ')' B     |
     //    '[' TREE_LIST ']'  ????
    X X()
    {
        AR ar;
        
        if (lookahead.Type == (int)type.ASSIGN)
        {
            Match(type.ASSIGN);
            return new X(E(), Y());
        }
        else //(lookahead.Type == (int)type.OPEN_PAR)
        {
            Match(type.OPEN_PAR);
            ar = AR();
            Match(type.CLOSE_PAR);
            return new X(ar, B());
        }

        
    }
    //B -> '{' S '}' 		
    B B()
    {
        StatementList s;
        if (lookahead.Type == (int)type.OPEN_CURLY)
        {
            Match(type.OPEN_CURLY);
            s = S();
            Match(type.CLOSE_CURLY);
            return new B(s);
        }
        else return new B(S());
    }
    
    //AR -> A | epsilon
    AR AR()
    {
        return new AR(A());
    }
    
    //A -> ID A1 		
    A A()
    {
        String value = lookahead.Value;
        Match(type.ID);
        return new A(value, A1());

    }
    
    //A1 -> ',' A | epsilon
    A1 A1()
    {
        if (lookahead.Type == (int)type.COMMA)
        {
            Match(type.COMMA);
            return new A1(A());
        }
        else return null;
    }

    //Y -> '(' AR ')'  | epsilon
    Y Y()
    {
        AR ar;
        if (lookahead.Type == (int)type.OPEN_PAR)
        {
            Match(type.OPEN_PAR);
            ar = AR();
            Match(type.CLOSE_PAR);
            return new Y(ar);
        }
        else return null;
    }
    //EIF -> 'else' B | epsilon
    EIF EIF()
    {
        
        if (lookahead.Type == (int)type.ELSE)
        {
            Match(type.ELSE);
            return new EIF(B());
        }
        else return null;
    }
    //G -> E C1
    G G()
    {
        return new G(E(), G1());
    }

    //G1 ->     'lt'  E |
    //          'gt'  E |
    //          '=='  E |
    //          'ge' E | 
    //          'le' E | 
    //          '!=' E 

    G1 G1()
    {
        if (lookahead.Type == (int)type.LT)
        {
            Match(type.LT);
            return new G1(E(), new Atomic<string>("lt"));
        }
        else if (lookahead.Type == (int)type.GT)
        {
            Match(type.GT);
            return new G1(E(), new Atomic<string>("gt"));
        }
        if (lookahead.Type == (int)type.LE)
        {
            Match(type.LT);
            return new G1(E(), new Atomic<string>("lt"));
        }
        else if (lookahead.Type == (int)type.GE)
        {
            Match(type.GT);
            return new G1(E(), new Atomic<string>("ge"));
        }
        else if (lookahead.Type == (int)type.EQUAL)
        {
            Match(type.EQUAL);
            return new G1(E(), new Atomic<string>("=="));
        }
        else
        {
            Match(type.DISEQUAL);
            return new G1(E(), new Atomic<string>("!="));
        }

    }
    //E -> T E1
    E E()
    {
        return new E(T(), E1());
    }
    //E1 -> '+' T E1 | 
    //      '-' T E1 |
    //      '@' T E1 |
    //      '>' T E1 |
    //      epsilon
    E1 E1()
    {


        if (lookahead.Type == (int)type.PLUS)
        {
            Match(type.PLUS);

            return new E1(T(), E1(), '+');
        }
        else if (lookahead.Type == (int)type.MINUS)
        {
            Match(type.MINUS);

            return new E1(T(), E1(), '-');
        }
        else if (lookahead.Type == (int)type.AT)
        {
            Match(type.AT);

            return new E1(T(), E1(), '@');
        }
        else if (lookahead.Type == (int)type.CLOSE_TUPLE)
        {
            Match(type.CLOSE_TUPLE);

            return new E1(T(), E1(), '>');
        }
        else return null;
    }

    //T -> F T1
    T T()
    {
        return new T(F(), T1());
    }

    //T1 -> '*' F T1 |
    //      '/' F T1 |
    //      epsilon
    T1 T1()
    {

        if (lookahead.Type == (int)type.TIMES)
        {
            Match(type.TIMES);
            return new T1(F(), T1(), '*');
        }
        else if (lookahead.Type == (int)type.OBELUS)
        {
            Match(type.OBELUS);
            return new T1(F(), T1(), '/');
        }
        else return null;

    }

    //F-> TYPE TAIL

    F F()
    {
        return new F(TYPE(), TAIL());
    }
    //TYPE -> '(' E ')'   HTAG |
    //      ID        HTAG     |
    //      INTEGER            |
    //      DOUBLE             |
    //	    BOOL               |
    //	    STRING             |
    //	    TUPLE       


    TYPE TYPE()
    {
        E e;
        HTAG htag;
        if (lookahead.Type == (int)type.OPEN_PAR)
        {
            Match(type.OPEN_PAR);
            e = E();
            Match(type.CLOSE_PAR);
            htag = HTAG();
            return new TYPE(e, htag);
        }
        else if (lookahead.Type == (int)type.ID)
        {
            String idValue = lookahead.Value;
            Match(type.ID);
            htag = HTAG();
            return new TYPE(idValue, htag);
        }
        else if (lookahead.Type == (int)type.INTEGER)
        {
            String value = lookahead.Value;
            Match(type.INTEGER);
            return new TYPE(Convert.ToInt32(value));
        }
        else if (lookahead.Type == (int)type.DOUBLE)
        {
            String value = lookahead.Value;
            Match(type.DOUBLE);
            return new TYPE(Convert.ToDouble(value));
        }
        else if (lookahead.Type == (int)type.STRING)
        {
            String value = lookahead.Value;
            Match(type.STRING);
            return new TYPE(value);
        }
        else if (lookahead.Type == (int)type.BOOL)
        {
            String value = lookahead.Value;
            Match(type.BOOL);
            return new TYPE(value);
        }
        else
            return new TYPE(TP());
    }

    //TAIL -> '[' N ']' HTAG | epsilon
    TAIL TAIL()
    {
        N n;
        if (lookahead.Type == (int)type.OPEN_SQUARE)
        {
            Match(type.OPEN_SQUARE);
            n = N();
            Match(type.CLOSE_SQUARE);
            return new TAIL(n);
        }
        else return null;
    }
    //HTAG ->  '#' | epsilon 
    HTAG HTAG()
    {
        if (lookahead.Type == (int)type.HASHTAG)
        {
            Match(type.HASHTAG);
            return new HTAG();
        }
        else return null;
    }
    //N -> E N1 | epsilon
    N N()
    {
        if (lookahead.Type == (int)type.OPEN_PAR | lookahead.Type == (int)type.ID |
            lookahead.Type == (int)type.INTEGER | lookahead.Type == (int)type.DOUBLE |
            lookahead.Type == (int)type.BOOL | lookahead.Type == (int)type.STRING |
            lookahead.Type == (int)type.LT)
        {
            return new N(E(), N1());

        }
        else return null;
    }
    //N1 -> ',' N | epsilon
    N1 N1()
    {
        if (lookahead.Type == (int)type.COMMA)
        {
            Match(type.COMMA);
            return new N1(N());
        }
        else return null;
    }
    //TP -> '<' E TP1 '>'
    Tupla TP()
    {
        Z z;

        Match(type.OPEN_TUPLE);
        z = Z();
        Match(type.CLOSE_TUPLE);
        return new Tupla(z);


    }


    //Z -> E E_LIST 

    Z Z()
    {
        return new Z(E(), ELIST());
    }

    //E_LIST -> ',' Z | epsilon

    ELIST ELIST()
    {
        if (lookahead.Type == (int)type.COMMA)
        {
            Match(type.COMMA);
            return new ELIST(Z());
        }
        else return null;
    }
    protected void Match(type t)
    {
        Debug.Assert(lookahead.Type == (int)t, "Syntax error expected " + converter((int)t));
        lookahead = this.t.nextToken();
    }
    public String converter(int type)
    {

        switch (type)
        {
            case 0: return "PLUS";
            case 1: return "TIMES";
            case 2: return "ID";
            case 3: return "INVALID_TOKEN";
            case 4: return "CLOSE_PAR";
            case 5: return "OPEN_PAR";
            case 6: return "EOF";
            case 7: return "INTEGER";
            case 8: return "OBELUS";
            case 9: return "MINUS";
            case 10: return "INCR";
            case 11: return "SEMICOLON";
            case 12: return "OPEN_CURLY";
            case 13: return "CLOSE_CURLY";
            case 14: return "IF";
            case 15: return "ELSE";
            case 16: return "FOR";
            case 17: return "GT";
            case 18: return "LT";
            case 19: return "EQUAL";
            case 20: return "DISEQUAL";
            case 21: return "RETURN";
            case 22: return "COMMA";
            case 23: return "HASHTAG";
            case 24: return "AT";
            case 25: return "OPEN_SQUARE";
            case 26: return "CLOSE_SQUARE";
            case 27: return "BOOL";
            case 28: return "HEIGHT";
            case 29: return "EMPTY";
            case 30: return "TYPE";
            case 31: return "BRANCH";
            case 32: return "PRINT";
            case 33: return "DOUBLE";
            case 34: return "NULL";
            case 35: return "STRING";
            case 36: return "LT";
            case 37: return "LE";
            case 38: return "GT";
            case 39: return "GE";
            case 40: return "ASSIGN";
            default: return null;
        }

    }
}