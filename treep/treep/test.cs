﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class Test
{

    static void Main(String[] args)
    {
        Console.WriteLine("Start parsing...");
        string text = System.IO.File.ReadAllText(@"C:\Users\gianlu\Source\Repos\treep\treep\treep\esempio.txt");
        //string text = "ciao \"ciao   .!= pippo\" true false = !=  + +=  @ # height empty type branchingfactor 123456789 123456.789456 18.1";
        //string text = "<true,false,\"ciao mondo!\",<2,1>>";
        //string text = "x=true;";
        showToken(text);
        parseExpr(text);
    }

    private static void parseExpr(String text)
    {

        Parser p = new Parser();
        Program program = p.parse<Program>(text);
        Console.WriteLine("Finish parsing");
    }
    private static void showToken(String text)
    {
        Tokenizer t = new Tokenizer(text);
        Token lookahead = t.nextToken();
        while (lookahead.Type != (int)type.EOF)
        {
            Console.WriteLine(lookahead.Value + "\t\t\t\t" + converter(lookahead.Type));
            lookahead = t.nextToken();
        }
        Console.WriteLine(lookahead.Value + "\t\t\t\t" + converter(lookahead.Type));
    }

    public static String converter(int type)
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
            case 17: return "OPEN_TUPLE";
            case 18: return "CLOSE_TUPLE";
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


