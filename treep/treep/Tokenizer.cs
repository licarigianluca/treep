using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public enum type
{
    PLUS, TIMES, ID, INVALID_TOKEN, CLOSE_PAR, OPEN_PAR, EOF, INTEGER,
    OBELUS, MINUS, INCR, SEMICOLON, OPEN_CURLY, CLOSE_CURLY,
    IF, ELSE, FOR, OPEN_TUPLE, CLOSE_TUPLE, EQUAL, DISEQUAL, RETURN, COMMA, HASHTAG, AT,
    OPEN_SQUARE, CLOSE_SQUARE, BOOL, HEIGHT, EMPTY, TYPE, BRANCH, PRINT, DOUBLE, NULL, STRING,
    LT, LE, GT, GE, ASSIGN
};

public class Tokenizer
{
    private bool ignoreBlanks = true;
    private int idx;
    private String s;
    private LinkedList<Token> tokenList;
    private Dictionary<String, int> keyword = new Dictionary<string, int>(){
        {"true",(int)type.BOOL},
        {"false",(int)type.BOOL},
        {"if",(int)type.IF},
        {"else",(int)type.ELSE},
        {"for",(int)type.FOR},
        {"return", (int)type.RETURN},
        {"height",(int)type.HEIGHT},
        {"empty",(int)type.EMPTY},
        {"type",(int)type.TYPE},
        {"branchingfactor",(int)type.BRANCH},
        {"print",(int)type.PRINT},
        {"null", (int)type.NULL},
        {"lt",(int)type.LT},
        {"le",(int)type.LE},
        {"gt",(int)type.GT},
        {"ge",(int)type.GE},
    };
    public Tokenizer(String expr)
    {
        this.s = expr;
        this.idx = 0;
        this.tokenList = new LinkedList<Token>();
    }

    public Token nextToken()
    {

        String lexeme;
        Token t;

        if (idx >= s.Length)
        {
            idx = 0;
            t = new Token("EOF".ToString(), (int)type.EOF);
        }
        else if (s[idx] == ',')
        {
            t = new Token(s[idx++].ToString(), (int)type.COMMA);
        }
        else if (s[idx] == '#')
        {
            t = new Token(s[idx++].ToString(), (int)type.HASHTAG);
        }
        else if (s[idx] == '@')
        {
            t = new Token(s[idx++].ToString(), (int)type.AT);
        }
        else if (s[idx] == '[')
        {
            t = new Token(s[idx++].ToString(), (int)type.OPEN_SQUARE);
        }
        else if (s[idx] == ']')
        {
            t = new Token(s[idx++].ToString(), (int)type.CLOSE_SQUARE);
        }
        else if (s[idx] == '+')
        {
            if (s[idx + 1] == '=')
            {
                string str = s[idx].ToString() + s[idx + 1].ToString();
                t = new Token(str, (int)type.INCR);
                idx += 2;
                ignoreBlanks = true;
            }
            else
            {
                //ignoreBlanks = false;
                t = new Token(s[idx++].ToString(), (int)type.PLUS);
            }
        }
        else if (s[idx] == '-')
        {
            t = new Token(s[idx++].ToString(), (int)type.MINUS);
        }
        else if (s[idx] == '*')
        {
            t = new Token(s[idx++].ToString(), (int)type.TIMES);
        }
        else if (s[idx] == '/')
        {
            t = new Token(s[idx++].ToString(), (int)type.OBELUS);
        }
        else if (s[idx] == '(')
        {
            t = new Token(s[idx++].ToString(), (int)type.OPEN_PAR);
        }
        else if (s[idx] == ')')
        {
            t = new Token(s[idx++].ToString(), (int)type.CLOSE_PAR);
        }
        else if (s[idx] == ';')
        {
            t = new Token(s[idx++].ToString(), (int)type.SEMICOLON);
        }
        else if (s[idx] == '}')
        {
            t = new Token(s[idx++].ToString(), (int)type.CLOSE_CURLY);
        }
        else if (s[idx] == '{')
        {
            t = new Token(s[idx++].ToString(), (int)type.OPEN_CURLY);
        }
        else if (s[idx] == '<')
        {
            t = new Token(s[idx++].ToString(), (int)type.OPEN_TUPLE);
        }
        else if (s[idx] == '>')
        {
            t = new Token(s[idx++].ToString(), (int)type.CLOSE_TUPLE);
        }
        else if (s[idx] == '=')
        {
            if (s[idx + 1] == '=')
            {
                string str = s[idx].ToString() + s[idx + 1].ToString();
                t = new Token(str, (int)type.EQUAL);
                idx += 2;
                ignoreBlanks = true;
            }
            else
            {
                //ignoreBlanks = false;
                t = new Token(s[idx++].ToString(), (int)type.ASSIGN);
            }
        }
        else if (s[idx] == '{')
        {
            t = new Token(s[idx++].ToString(), (int)type.OPEN_CURLY);
        }
        else if (s[idx] == '!')
        {
            if (s[idx + 1] == '=')
            {
                string str = s[idx].ToString() + s[idx + 1].ToString();
                t = new Token(str, (int)type.DISEQUAL);
                idx += 2;
                ignoreBlanks = true;
            }
            else
            {
                ignoreBlanks = false;
                t = new Token(s[idx++].ToString(), (int)type.INVALID_TOKEN);
            }
        }
        else if (s[idx] == '"')
        {

            lexeme = s[idx++].ToString();
            while (idx < s.Length && (isChar(s[idx]) || isDigit(s[idx]) || isBlank(s[idx])) && s[idx] != '"')
            {
                lexeme += s[idx++];
            }
            lexeme += s[idx++];
            t = new Token(lexeme.ToString(), (int)type.STRING);


        }
        else if (isDigit(s[idx]))
        {
            int dot = 0;
            lexeme = s[idx++].ToString();
            while (idx < s.Length && isDigit(s[idx]))
            {
                lexeme += s[idx++];
            }
            if (idx < s.Length - 1 && s[idx] == '.')
            {
                dot++;
                lexeme += s[idx++];
            }
            while (idx < s.Length && isDigit(s[idx]))
            {

                lexeme += s[idx++];
            }
            if (dot == 0)
            {
                t = new Token(lexeme.ToString(), (int)type.INTEGER);
            }
            else if (dot == 1)
            {
                t = new Token(lexeme.ToString(), (int)type.DOUBLE);
            }
            else
            {
                t = new Token(lexeme.ToString(), (int)type.INVALID_TOKEN);
            }
        }
        else if (isChar(s[idx]))
        {
            lexeme = s[idx++].ToString();
            while (idx < s.Length && isChar(s[idx]))
            {
                lexeme += s[idx++];
            }
            if (keyword.ContainsKey(lexeme))
            {
                t = new Token(lexeme.ToString(), keyword[lexeme]);
            }
            else
            {
                t = new Token(lexeme.ToString(), (int)type.ID);
            }
        }
        else
        {
            //if ((s[idx] == ' ' || s[idx] == '\n') && ignoreBlanks)
            if (isBlank(s[idx]) && ignoreBlanks)
            {
                idx++;
                t = nextToken();
            }
            else
            {
                t = new Token(s[idx++].ToString(), (int)type.INVALID_TOKEN);
            }
        }

        return t;
    }


    private bool isDigit(char c)
    {
        return (c >= '0' && c <= '9');
    }

    private bool isChar(char c)
    {
        return (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z');
    }

    private bool isBlank(char c)
    {
        return (c != ' ') || (c != '\r') || (c != '\n');
    }
}
