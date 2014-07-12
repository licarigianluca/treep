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
        lookahead = new Token[2];
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
            return new Statement(g, Block(), ElseIf());
        }
        else if (lookahead[0].Type == (int)type.RETURN)
        {
            Match(type.RETURN);
            return new Statement("return", Expr());
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

        if (lookahead[0].Type == (int)type.OPEN_CURLY)
        {
            Match(type.OPEN_CURLY);
            sl = StatementList();
            Match(type.CLOSE_CURLY);
            return new Block(sl);
        }
        else
        {
            /*s = Statement();
            Match(type.SEMICOLON);*/
            return new Block(Statement(), StatementTail());
        }
    }
    //Assignment -> LHandValue AssignmentTail
    
    Assignment Assignment()
    {
        return new Assignment(LHandValue(),AssignmentTail());
    }

    //AssignmentTail -> '=' Expr    |
    //                  '+=' Expr	|
    //                  '-=' Expr   |
    //                  '*=' Expr   |
    //                  '/=' Expr
    //DA AGGIUNGERE ALTRI OPERATORI!!!!!!!!
    AssignmentTail AssignmentTail()
    {
        if (lookahead[0].Type == (int)type.INCR)
        {
            Match(type.INCR);
            return new AssignmentTail(lookahead[0].Value, Expr());
        }
        else
        {
            Match(type.ASSIGN);
            return new AssignmentTail(lookahead[0].Value, Expr());
        }
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
        if (lookahead[0].Type == (int)type.COMMA)
        {
            Match(type.COMMA);
            return new ArgumentTail(ArgumentList());
        }
        else return null;
    }

    //FunctionTail -> Block | epsilon		
    FunctionTail FunctionTail()
    {
        if (lookahead[0].Type == (int)type.OPEN_CURLY |
            lookahead[0].Type == (int)type.ID |
            lookahead[0].Type == (int)type.FOR |
            lookahead[0].Type == (int)type.IF |
            lookahead[0].Type == (int)type.RETURN |
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
            Match(type.LE);
            return new GuardTail("le", Expr());
        }
        else if (lookahead[0].Type == (int)type.GE)
        {
            Match(type.GE);
            return new GuardTail("ge", Expr());
        }
        else if (lookahead[0].Type == (int)type.EQUAL)
        {
            Match(type.EQUAL);
            return new GuardTail("==", Expr());
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
    //               '>' Id  TupleAccess
    TupleAccess TupleAccess()
    {
        if (lookahead[0].Type == (int)type.CLOSE_TUPLE)
        {
            
            if (lookahead[1].Type == (int)type.INTEGER)
            {
                Match(type.CLOSE_TUPLE);
                int index = Convert.ToInt32(lookahead[0].Value);
                Match(type.INTEGER);
                return new TupleAccess(index, TupleAccess());
            }
            else
            {
                Match(type.CLOSE_TUPLE);
                string id = lookahead[0].Value;
                Match(type.ID);
                return new TupleAccess(id, TupleAccess());
            }

        }
        else return null;
    }

    //Expr -> '(' Expr ')' Htag	|																	
    //		Id Htag			|
    //		Integer			|
    //		Double			|
    //		String			|
    //		Bool			|
    //		Tree			|
    //		Tupla			|
    //		FunctionHead---	|
    //		Mul Sum			---
    Expr Expr()
    {
        Expr e;

        if (lookahead[0].Type == (int)type.OPEN_PAR)
        {
            Match(type.OPEN_PAR);
            e = Expr();
            Match(type.CLOSE_PAR);
            return new Expr(e, Htag(), TupleAccess());

        }
        else if (lookahead[0].Type == (int)type.ID || lookahead[0].Type == (int)type.INTEGER ||
                lookahead[0].Type == (int)type.DOUBLE || lookahead[0].Type == (int)type.STRING ||
            lookahead[0].Type == (int)type.BOOL)
        {

            if (lookahead[1].Type == (int)type.OPEN_PAR)
            {
                return new Expr(FunctionHead());
            }
            if (lookahead[1].Type == (int)type.TIMES || lookahead[1].Type == (int)type.OBELUS)
            {
                return new Expr(Mul(), Sum());
            }
            if (lookahead[1].Type == (int)type.OPEN_SQUARE)
            {
                return new Expr(Tree());
            }
            if (lookahead[1].Type == (int)type.OPEN_SQUARE)
            {
                String id = lookahead[0].Value;
                Match(type.ID);
                return new Expr(id, Htag(), TupleAccess());
            }
            if (lookahead[0].Type == (int)type.ID)
            {
                String id = lookahead[0].Value;
                Match(type.ID);
                return new Expr(id, Htag(), TupleAccess());
            }
            if (lookahead[0].Type == (int)type.INTEGER)
            {
                int intValue = Convert.ToInt32(lookahead[0].Value);
                Match(type.INTEGER);
                return new Expr(intValue);
            }
            if (lookahead[0].Type == (int)type.DOUBLE)
            {
                double doubleValue = Convert.ToDouble(lookahead[0].Value);
                Match(type.DOUBLE);
                return new Expr(doubleValue);
            }
            if (lookahead[0].Type == (int)type.STRING)
            {
                string stringValue = lookahead[0].Value;
                Match(type.STRING);
                return new Expr(stringValue);
            }
            if (lookahead[0].Type == (int)type.BOOL)
            {
                string boolValue = lookahead[0].Value;
                Match(type.BOOL);
                return new Expr(boolValue.Equals("true"));
            }
        }

        return new Expr(Tupla());

    }

    //Mul -> Factor  '*' Factor MulTail		|
    //       Factor  '/' Factor MulTail		
    Mul Mul()
    {

        return new Mul(Factor(), lookahead[1].Value, Factor(), MulTail());

    }

    //MulTail -> Mul | epsilon
    MulTail MulTail()
    {
        if (lookahead[0].Type == (int)type.ID || lookahead[0].Type == (int)type.INTEGER ||
                lookahead[0].Type == (int)type.DOUBLE || lookahead[0].Type == (int)type.STRING ||
            lookahead[0].Type == (int)type.BOOL)
        {
            return new MulTail(Mul());
        }
        else return null;
    }
    //Sum -> Factor  '+' Factor SumTail		|
    //       Factor  '-' Factor SumTail		
    Sum Sum()
    {
        return new Sum(Factor(), lookahead[1].Value, Factor(), SumTail());

    }

    //SumTail -> Sum | epsilon
    SumTail SumTail()
    {
        if (lookahead[0].Type == (int)type.ID || lookahead[0].Type == (int)type.INTEGER ||
                lookahead[0].Type == (int)type.DOUBLE || lookahead[0].Type == (int)type.STRING ||
            lookahead[0].Type == (int)type.BOOL)
        {
            return new SumTail(Sum());
        }
        else return null;
    }

    //Factor -> Integer 		|													
    //          Double			|
    //          Id				|
    //          String			|
    //          Bool			|	
    //          '(' Mul Sum ')'	|
    //          FunctionHead	|
    //          null
    Factor Factor()
    {
        Mul mul;
        Sum sum;

        if (lookahead[0].Type == (int)type.CLOSE_PAR)
        {
            Match(type.OPEN_PAR);
            mul = Mul();
            sum = Sum();
            Match(type.CLOSE_PAR);
            return new Factor(mul, sum);
        }
        else if (lookahead[0].Type == (int)type.ID)
        {
            if (lookahead[1].Type == (int)type.OPEN_PAR)
            {
                return new Factor(FunctionHead());
            }
            else
            {
                string id = lookahead[1].Value;
                Match(type.ID);
                return new Factor(id, "ID");
            }
        }
        else if (lookahead[0].Type == (int)type.INTEGER)
        {
            int intValue = Convert.ToInt32(lookahead[0].Value);
            Match(type.INTEGER);
            return new Factor(intValue);
        }
        else if (lookahead[0].Type == (int)type.DOUBLE)
        {
            double doubleValue = Convert.ToDouble(lookahead[0].Value);
            Match(type.DOUBLE);
            return new Factor(doubleValue);
        }
        else if (lookahead[0].Type == (int)type.BOOL)
        {
            string boolValue = lookahead[0].Value;
            Match(type.BOOL);
            return new Factor(boolValue.Equals("true"));
        }
        else
        {
            return new Factor(new Atomic<string>("null"));

        }
    }

    //Tree ->	Factor '[' TreeList ']'	|													
    //          Tupla  '[' TreeList ']'	
    Tree Tree()
    {
        Factor f;
        Tupla t;
        TreeList tl;

        if (lookahead[0].Type == (int)type.ID || lookahead[0].Type == (int)type.INTEGER ||
                lookahead[0].Type == (int)type.DOUBLE || lookahead[0].Type == (int)type.STRING ||
            lookahead[0].Type == (int)type.BOOL)
        {
            f = Factor();
            Match(type.OPEN_SQUARE);
            tl = TreeList();
            Match(type.CLOSE_SQUARE);
            return new Tree(f, tl);
        }
        else
        {
            t = Tupla();
            Match(type.OPEN_SQUARE);
            tl = TreeList();
            Match(type.CLOSE_SQUARE);
            return new Tree(t, tl);
        }
    }

    //TreeList -> TreeListHead | epsilon
    TreeList TreeList()
    {
        if (lookahead[0].Type == (int)type.ID || lookahead[0].Type == (int)type.INTEGER ||
                lookahead[0].Type == (int)type.DOUBLE || lookahead[0].Type == (int)type.STRING ||
                lookahead[0].Type == (int)type.BOOL || lookahead[0].Type == (int)type.OPEN_TUPLE)
        {
            return new TreeList(TreeListHead());
        }
        else return null;
    }

    //TreeListHead -> Tree TreeListTail
    TreeListHead TreeListHead()
    {
        return new TreeListHead(Tree(), TreeListTail());
    }

    //TreeListTail -> ',' TreeListHead | epsilon	
    TreeListTail TreeListTail()
    {
        if (lookahead[0].Type == (int)type.COMMA)
        {
            Match(type.COMMA);
            return new TreeListTail(TreeListHead());
        }
        else return null;
    }

    //Tupla -> '<' DataList '>' 
    Tupla Tupla()
    {
        DataList dl;

        Match(type.OPEN_TUPLE);
        dl = DataList();
        Match(type.CLOSE_TUPLE);
        return new Tupla(dl);
    }

    //DataList -> Factor DataTail	
    DataList DataList()
    {
        return new DataList(Factor(), DataTail());
    }

    //DataTail-> ',' DataList | epsilon
    DataTail DataTail()
    {
        if (lookahead[0].Type == (int)type.COMMA)
        {
            Match(type.COMMA);
            return new DataTail(DataList());
        }
        else return null;
    }
    protected void Match(type t)
    {
        Token tmp;
        Debug.Assert(lookahead[0].Type == (int)t, "Syntax error expected " + converter((int)t));
        tmp = lookahead[1];
        lookahead[1] = this.t.nextToken();
        lookahead[0] = tmp;
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