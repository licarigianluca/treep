using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



public class Parser
{
    private Tokenizer t;
    Token[] lookahead;

    public Parser()
    {

    }

    public T parse<T>(String s)
    {
        t = new Tokenizer(s);
        lookahead = new Token[2] ;
        lookahead[0] = t.nextToken();
        lookahead[1] = t.nextToken();
       

        T p = (T)(Object)Program();
        Match(type.EOF);
        return p;

    }

    //Program -> StatementList EOF
    Program Program()
    {
        return new Program(StatementList());
    }
    
    //StatementList -> Statement StatementTail
    StatementList StatementList()
    {
        return new StatementList(Statement(), StatementTail());
    }

    //StatementTail -> ';' StatementList | epsilon		
    StatementTail StatementTail()
    {
        if (lookahead[0].Type == (int)type.SEMICOLON)
        {
            Match(type.SEMICOLON);
            return new StatementTail(StatementList());
        }
        else return null;
    }

    //Statement -> Assignment					                            |
    //             FunctionHead FunctionTail				                |
    //            'for' '(' Assignment ';' Guard ';' Assignment ')' Block   | 	
	//	          'if' '(' Guard ')' Block ElseIf			                |
	//            'return' Expr					                            |
	//            'print'  Expr
    Statement Statement()
    {
        Assignment as1;
        Assignment as2;
        Guard g;
        Block b;
        ElseIf ei;

        if (lookahead[0].Type == (int)type.ID)
        {
            if (lookahead[1].Type == (int)type.OPEN_PAR)
            {
                return new Statement(FunctionHead(), FunctionTail());
            }
            else
            {
                return new Statement(Assignment());
            }
        }
        else if (lookahead[0].Type == (int)type.FOR)
        {
            Match(type.FOR);
            Match(type.OPEN_PAR);
            as1 = Assignment();
            Match(type.SEMICOLON);
            g = Guard();
            Match(type.SEMICOLON);
            as2 = Assignment();
            Match(type.CLOSE_PAR);
            b = Block();
            return new Statement(as1, g, as2, b);
        }
        else if (lookahead[0].Type == (int)type.IF)
        {
            Match(type.IF);
            Match(type.OPEN_PAR);
            g = Guard();
            Match(type.CLOSE_PAR);
            b = Block();
            ei = ElseIf();
            return new Statement(g,b,ei);
        }
        else if (lookahead[0].Type == (int)type.RETURN)
        {
            Match(type.RETURN);
            return new Statement("return",Expr());
        }
        else
        {
            Match(type.PRINT);
            return new Statement("print", Expr());
        }
    }

    //ElseIf -> 'else' Block | epsilon	
    ElseIf ElseIf()
    {
        if (lookahead[0].Type == (int)type.ELSE)
        {
            Match(type.ELSE);
            return new ElseIf(Block());
        }
        else return null;
    }

    //Block -> '{' StatementList '}' | Statement ';'	
    Block Block()
    {
        StatementList sl;
        Statement s;
        if (lookahead[0].Type == (int)type.OPEN_CURLY)
        {
            Match(type.OPEN_CURLY);
            sl = StatementList();
            Match(type.CLOSE_CURLY);
            return new Block(sl);
        }
        else
        {
            s = Statement();
            Match(type.SEMICOLON);
            return new Block(s);
        }
    }
    //Assignment -> LHandValue '=' Expr	    |
	//              LHandValue '+=' Expr	|
	//              LHandValue '-=' Expr	|
	//              LHandValue '*=' Expr	|
	//              LHandValue '/=' Expr	|
    
    Assignment Assignment()
    {
        return new Assignment(LHandValue(), lookahead[1].Value, Expr());
    }
    //FunctionHead -> Id '(' ArgumentList ')' 	
    FunctionHead FunctionHead()
    {
        ArgumentList al;

        String id = lookahead[0].Value;
        Match(type.ID);
        Match(type.OPEN_PAR);
        al = ArgumentList();
        Match(type.CLOSE_PAR);
        return new FunctionHead(id, al);
    }

    //ArgumentList -> Id ArgumentTail
    ArgumentList ArgumentList()
    {
        String id = lookahead[0].Value;
        Match(type.ID);
        return new ArgumentList(id, ArgumentTail());
    }

    //ArgumentTail -> ',' ArgumentList | epsilon
    ArgumentTail ArgumentTail()
    {
        if (lookahead[0].Type == (int)type.COMMA){
            Match(type.COMMA);
            return new ArgumentTail(ArgumentList());
        }
        else return null;
    }

    //FunctionTail -> Block | epsilon		
    FunctionTail FunctionTail()
    {
        if (lookahead[0].Type == (int)type.OPEN_CURLY |
            lookahead[0].Type == (int)type.ID|
            lookahead[0].Type == (int)type.FOR|
            lookahead[0].Type == (int)type.IF|
            lookahead[0].Type == (int)type.RETURN
            lookahead[0].Type == (int)type.PRINT)
        {
            return new FunctionTail(Block());
        }
        else return null;
    }

    //Guard -> Expr GuardTail	
    Guard Guard()
    {
        return new Guard(Expr(), GuardTail());
    }

    //GuardTail -> 'lt' Expr |																		
	//             'gt' Expr |
	//             '==' Expr |
	//             'ge' Expr |		
    //	           'le' Expr |		
	//             '!=' Expr 

    GuardTail GuardTail()
    {
        if (lookahead[0].Type == (int)type.LT)
        {
            Match(type.LT);
            return new GuardTail("lt", Expr());
        }
        else if (lookahead[0].Type == (int)type.GT)
        {
            Match(type.GT);
            return new GuardTail("gt", Expr());
        }
        else if (lookahead[0].Type == (int)type.LE)
        {
            Match(type.EQUAL);
            return new GuardTail("==", Expr());
        }
        else if (lookahead[0].Type == (int)type.GE)
        {
            Match(type.GE);
            return new GuardTail("ge", Expr());
        }
        else if (lookahead[0].Type == (int)type.EQUAL)
        {
            Match(type.LE);
            return new GuardTail("le", Expr());
        }
        else
        {
            Match(type.DISEQUAL);
            return new GuardTail("!=", Expr());
        }

    }

    //LHandValue ->	Id Htag	TupleAccess		                |
    //               '(' Id AccessTail ')' Htag TupleAccess	
    LHandValue LHandValue()
    {
        AccessTail at;

        if (lookahead[0].Type == (int)type.ID)
        {
            String id = lookahead[0].Value;
            Match(type.ID);
            return new LHandValue(id, Htag(), TupleAccess());
        }
        else
        {
            Match(type.OPEN_PAR);
            String id = lookahead[0].Value;
            Match(type.ID);
            at = AccessTail();
            Match(type.CLOSE_PAR);
            return new LHandValue(id, Htag(), TupleAccess());
        }
    }

    //Htag -> '#' | epsilon
    Htag Htag()
    {
        if (lookahead[0].Type == (int)type.HASHTAG)
        {
            Match(type.HASHTAG);
            return new Htag();
        }
        else return null;
    }

    //AccessTail ->  '@' Integer AccessTail	|																			
	//	             '@' Id AccessTail	    |
	//               epsilon

    AccessTail AccessTail()
    {
        if (lookahead[0].Type == (int)type.AT)
        {
            if (lookahead[1].Type == (int)type.INTEGER)
            {
                int index = Convert.ToInt32(lookahead[1].Value);
                Match(type.INTEGER);
                return new AccessTail(index, AccessTail());
            }
            else
            {
                string id = lookahead[1].Value;
                Match(type.ID);
                return new AccessTail(id, AccessTail());
            }
        }
        else return null;
    }

    //TupleAccess -> '>' Integer TupleAccess	|  epsilon	
    TupleAccess TupleAccess()
    {
        if (lookahead[0].Type == (int)type.CLOSE_TUPLE)
        {
            Match(type.CLOSE_TUPLE);
            int index = Convert.ToInt32(lookahead[0].Value);
            Match(type.INTEGER);
            return new TupleAccess(index, TupleAccess());
        }
        else return null;
    }

    //Expr -> '(' Expr ')' Htag	|																	ok
	//		Id Htag			|
	//		Integer			|
	//		Double			|
	//		String			|
	//		Bool			|
	//		Tree			|
	//		Tupla			|
	//		FunctionHead	|
	//		Mul Sum			
    Expr Expr()
    {

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