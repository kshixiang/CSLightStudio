﻿using System;
using System.Collections.Generic;
using System.Text;
namespace CSLE
{
    public partial class CLS_Expression_Compiler : ICLS_Expression_Compiler
    {
        public ICLS_Expression Compiler_Expression_Define(IList<Token> tlist, ICLS_Environment content, int pos, int posend)
        {
            CLS_Expression_Define define = new CLS_Expression_Define(pos, posend, tlist[pos].line, tlist[posend].line);
            if (tlist[pos].text == "bool")
            {
                define.value_type = typeof(bool);
            }
            else
            {
                ICLS_Type type =    content.GetTypeByKeyword(tlist[pos].text);
                define.value_type = type.type;
            }
            define.value_name = tlist[pos+1].text;
            return define;
        }

        public ICLS_Expression Compiler_Expression_DefineArray(IList<Token> tlist, ICLS_Environment content, int pos, int posend)
        {
            CLS_Expression_Define define = new CLS_Expression_Define(pos, posend, tlist[pos].line, tlist[posend].line);
            {
                ICLS_Type type = content.GetTypeByKeyword(tlist[pos].text+"[]");
                define.value_type = type.type;
            }
            define.value_name = tlist[pos + 3].text;
            return define;
        }

        public ICLS_Expression Compiler_Expression_DefineAndSet(IList<Token> tlist, ICLS_Environment content, int pos, int posend)
        {
            int expbegin =pos+3;
            int bdep;
            int expend = FindCodeAny(tlist, ref expbegin, out bdep);
            if(expend!=posend)
            {
                expend = posend;
            }
            ICLS_Expression v;
            bool succ = Compiler_Expression(tlist,content, expbegin, expend, out v);
            if(succ&&v!=null)
            {
                CLS_Expression_Define define = new CLS_Expression_Define(pos, posend, tlist[pos].line, tlist[posend].line);
                if (tlist[pos].text == "bool")
                {
                    define.value_type = typeof(bool);
                }
                else
                {
                    ICLS_Type type = content.GetTypeByKeyword(tlist[pos].text);
                    define.value_type = type.type;
                }
                define.value_name = tlist[pos + 1].text;
                define.listParam.Add(v);
                return define;
            }
            LogError(tlist,"不正确的定义表达式:" , pos,posend);
            return null;
        }
        public ICLS_Expression Compiler_Expression_DefineAndSetArray(IList<Token> tlist, ICLS_Environment content, int pos, int posend)
        {
            int expbegin = pos + 5;
            int bdep;
            int expend = FindCodeAny(tlist, ref expbegin, out bdep);
            if (expend != posend)
            {
                expend = posend;
            }
            ICLS_Expression v;
            bool succ = Compiler_Expression(tlist, content, expbegin, expend, out v);
            if (succ && v != null)
            {
                CLS_Expression_Define define = new CLS_Expression_Define(pos, posend, tlist[pos].line, tlist[posend].line);
                {
                    ICLS_Type type = content.GetTypeByKeyword(tlist[pos].text+"[]");
                    define.value_type = type.type;
                }
                define.value_name = tlist[pos + 3].text;
                define.listParam.Add(v);
                return define;
            }
            LogError(tlist, "不正确的定义表达式:", pos, posend);
            return null;
        }
        public ICLS_Expression Compiler_Expression_Set(IList<Token> tlist, ICLS_Environment content, int pos, int posend)
        {
            int expbegin = pos + 2;
            int bdep;
            int expend = FindCodeAny(tlist, ref expbegin, out bdep);
            if (expend != posend)
            {
              
                expend = posend;
            }
            ICLS_Expression v;
            bool succ = Compiler_Expression(tlist,content, expbegin, expend, out v);
            if (succ && v != null)
            {
                CLS_Expression_SetValue define = new CLS_Expression_SetValue(pos, expend, tlist[pos].line, tlist[expend].line);
                define.value_name = tlist[pos].text;
                define.listParam.Add(v);
                return define;
            }
            LogError(tlist,"不正确的定义表达式:" ,pos,posend);
            return null;
        }
 
    }
}