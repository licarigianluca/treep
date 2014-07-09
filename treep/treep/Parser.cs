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

        T p = (T)(Object)E();
        Match(type.EOF);
        return p;

    }

    //E -> T E1
    E E()
    {
        return new E(T(), E1());
    }
    //E1 -> '+' T E1 | 
    //      '-' T E1 |
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

    //F -> '(' E ')'    |
    //      ID          |
    //      INTEGER     |
    //      DOUBLE      |
    //	    BOOL        |
    //	    STRING      |
    //	    TUPLE       |
    //	    TREE	

    F F()
    {
        E e;

        if (lookahead.Type == (int)type.OPEN_PAR)
        {
            Match(type.OPEN_PAR);
            e = E();
            Match(type.CLOSE_PAR);
            return new F(e);
        }
        else if (lookahead.Type == (int)type.ID)
        {
            String value = lookahead.Value;
            Match(type.ID);
            return new F(value, "ID");
        }
        else if (lookahead.Type == (int)type.INTEGER)
        {
            String value = lookahead.Value;
            Match(type.INTEGER);
            return new F(Convert.ToInt32(value));
        }
        else if (lookahead.Type == (int)type.DOUBLE)
        {
            String value = lookahead.Value;
            Match(type.DOUBLE);
            return new F(Convert.ToDouble(value));
        }
        else if (lookahead.Type == (int)type.STRING)
        {
            String value = lookahead.Value;
            Match(type.STRING);
            return new F(value, "STRING");
        }
        else if (lookahead.Type == (int)type.LT)
        {
            return new F(TP());
        }else if(lookahead.Type == (int)type)

    }

    //TP -> '<' E TP1 '>'

    TP TP()
    {
        E e;
        TP1 tp1;
        Match(type.LT);
        e = E();
        tp1 = TP1();
        Match(type.GT);
        return new TP(e, tp1);


    }

    //TP1 -> ',' TP | epsilon
    TP1 TP1()
    {
        if (lookahead.Type == (int)type.COMMA)
        {
            Match(type.COMMA);
            return new TP1(TP());
        }
        else return null;
    }


    protected void Match(type t)
    {
        Debug.Assert(lookahead.Type == (int)t, "Syntax error");
        lookahead = this.t.nextToken();
    }
}