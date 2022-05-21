using compilerProject2.Models;
using Microsoft.AspNetCore.Mvc;

namespace compilerProject2.Controllers
{
    public class ScannerController : Controller
    {
        Dictionary<string, int> convertCharsToInt = new Dictionary<string, int>();
        Dictionary<string, string> KeyWords = new Dictionary<string, string>();
        Dictionary<int, string> tokensType = new Dictionary<int, string>();
        DataContext db = new DataContext();
        List<KeyWord> keyWordsList;
        List<SymbolsHasSameId> SymbolsHasSameId ;
        public List<string> TokensMessages { get; set; } = new List<string>();
        public List<string> Tokens { get; set; } = new List<string>();
        int [,] t ;
        Dictionary<int, bool> Accept = new Dictionary<int, bool>();
        string newLine = '\n'.ToString();
        string tap = '\t'.ToString();
        public IActionResult Index()
        {
            return View(TokensMessages);
        }
        public void readKeyWordsFromDB()
        {
            keyWordsList = db.KeyWords.ToList();
            SymbolsHasSameId = db.SymbolsHasSameIds.ToList();
            foreach (var keyWord in keyWordsList)
            {
                string key = keyWord.KeyWord1;
                string value = keyWord.ReturnToken;
                KeyWords[key] = value;
            }
            //return View (KeyWords);
        }
        public void stateDefinition ()
        {
            tokensType[39] = "Symbol";
            tokensType[40] = "Identifer";
            tokensType[41] = "Digit";
            tokensType[42] = "Comment";
            tokensType[43] = "Not Matched";
        }
        public void ConvertCharsToInt ()
        {
            for (char c = 'a'; c <= 'z'; c++) convertCharsToInt[c.ToString()] = 0;
            for (char c = 'A'; c <= 'Z'; c++) convertCharsToInt[c.ToString()] = 0;
            for (char c = '0'; c <= '9'; c++) convertCharsToInt[c.ToString()] = 1;
            //if ( @ / ^ / $ / # / ~ / { / } / [ / ]  /+ / ‘ /” )
            List <CharsId> chars = db.CharsIds.ToList();
            List<SymbolsHasSameId> symbolsHasSameId = db.SymbolsHasSameIds.ToList ();  
            int id = 2 ;
            foreach (CharsId charr in chars)
            {
                string ch = charr.Char ;
                id = charr.CharId ;
                convertCharsToInt[ch] = id;
            }
            convertCharsToInt[newLine] = id+1;
            convertCharsToInt[tap.ToString()] = id+2;
            foreach (var symbol in symbolsHasSameId)
            {
                convertCharsToInt[symbol.Symbol] = id + 3;
            }



        }
        public void preCompile ()
        {
            
            readKeyWordsFromDB();
            ConvertCharsToInt();
            createTransitionTable();
            createAcepptArray();
            stateDefinition();
        }
        public void createTransitionTable ()
        {
            int numberOfChars = convertCharsToInt.Count() + 20;
            int dfaStates = 60;
            t = new int[dfaStates, numberOfChars];
            for (int i = 0; i < dfaStates; i++)
                for (int j = 0; j < numberOfChars; j++)
                    t[i, j] = 44;
            doTransitionTableForD0();
            doTransitionTableForD1();
            doTransitionTableForD3();
            doTransitionTableForD4();
            doTransitionTableForD7();
            doTransitionTableForD9();
            doTransitionTableForD10();
            doTransitionTableForD11();
            doTransitionTableForD12();
            doTransitionTableForD20();
            doTransitionTableForD23();
            doTransitionTableForD26();
            doTransitionTableForD28();
            doTransitionTableForD31();
            doTransitionTableForD32();
            doTransitionTableForD33();
            doTransitionTableForD38();
            doTransitionTableForDError44Error();
        }
        public void doTransitionTableForDError44Error ()
        {
            for (char c = 'a'; c <= 'z'; c++) t[44, convertCharsToInt[c.ToString()]] = 44;
            for (char c = 'A'; c <= 'Z'; c++) t[44, convertCharsToInt[c.ToString()]] = 44;
            for (char c = '0'; c <= '9'; c++) t[44, convertCharsToInt[c.ToString()]] = 44;

            List<SymbolsHasSameId> symbolsHasSameId = db.SymbolsHasSameIds.ToList();

            foreach (var symbol in symbolsHasSameId)
            {
                t[44, convertCharsToInt[symbol.Symbol]] = 44;
            }
            t[44, convertCharsToInt["<"]] = 44;
            t[44, convertCharsToInt[">"]] = 44;
            t[44, convertCharsToInt["/"]] = 44;
            t[44, convertCharsToInt["="]] = 44;
            t[44, convertCharsToInt["!"]] = 44;
            t[44, convertCharsToInt["-"]] = 44;
            t[44, convertCharsToInt["*"]] = 44;
            t[44, convertCharsToInt["|"]] = 44;
            t[44, convertCharsToInt["&"]] = 44;
            t[44, convertCharsToInt[";"]] = 44;
            t[44, convertCharsToInt["|"]] = 44;
            t[44, convertCharsToInt[newLine]] = 43;
            t[44, convertCharsToInt[tap]] = 43;
            t[44, convertCharsToInt[" "]] = 43;
        }
        public void doTransitionTableForD0 ()
        {
            //                              state  ch                               next state
            for (char c = 'a'; c <= 'z'; c++) t[0, convertCharsToInt[c.ToString()]] = 3;
            for (char c = 'A'; c <= 'Z'; c++) t[0, convertCharsToInt[c.ToString()]] = 3;
            for (char c = '0'; c <= '9'; c++) t[0, convertCharsToInt[c.ToString()]] = 4;

            List< SymbolsHasSameId > symbolsHasSameId = db.SymbolsHasSameIds.ToList();

            foreach (var symbol in symbolsHasSameId)
            {
                t[0, convertCharsToInt[symbol.Symbol] ] = 1 ;
            }
            t[0, convertCharsToInt["<"]] = 7 ;
            t[0, convertCharsToInt[">"]] = 23 ;
            t[0, convertCharsToInt["/"]] = 1 ;
            t[0, convertCharsToInt["="]] = 23 ;
            t[0, convertCharsToInt["!"]] = 20 ;
            t[0, convertCharsToInt["-"]] = 12 ;
            t[0, convertCharsToInt["*"]] = 31 ;
            t[0, convertCharsToInt["|"]] = 26 ;
            t[0, convertCharsToInt["&"]] = 28 ;
            t[0, convertCharsToInt[";"]] = 39 ;
            t[0, convertCharsToInt["|"]] = 26 ;
            t[0, convertCharsToInt[newLine]] = 39 ;
            t[0, convertCharsToInt[tap]] = 39 ;
            t[0, convertCharsToInt[" "]] = 39 ;

        }
        public void doTransitionTableForD1()
        {
            t[1, convertCharsToInt[" "]] = 39;
            t[1, convertCharsToInt[tap]] = 39;
            t[1, convertCharsToInt[newLine]] = 39;
        }
        public void doTransitionTableForD3()
        {
            for (char c = 'a'; c <= 'z'; c++) t[3, convertCharsToInt[c.ToString()]] = 3;
            for (char c = 'A'; c <= 'Z'; c++) t[3, convertCharsToInt[c.ToString()]] = 3;
            for (char c = '0'; c <= '9'; c++) t[3, convertCharsToInt[c.ToString()]] = 3;

            t[3, convertCharsToInt[" "]] = 40;
            t[3, convertCharsToInt[tap]] = 40;
            t[3, convertCharsToInt[newLine]] = 40;
        }
        public void doTransitionTableForD4()
        {
            for (char c = '0'; c <= '9'; c++) t[4, convertCharsToInt[c.ToString()]] = 4; 

            t[4, convertCharsToInt[" "]] = 41;
            t[4, convertCharsToInt[tap]] = 41;
            t[4, convertCharsToInt[newLine]] = 41;
        }
        public void doTransitionTableForD7()
        {
            t[7, convertCharsToInt["/"]] = 9;
            t[7, convertCharsToInt["="]] = 1;

            t[7, convertCharsToInt[" "]] = 39;
            t[7, convertCharsToInt[tap]] = 39;
            t[7, convertCharsToInt[newLine]] = 39;
        }
        public void doTransitionTableForD9()
        {
            //                              state  ch                               next state
            for (char c = 'a'; c <= 'z'; c++) t[9, convertCharsToInt[c.ToString()]] = 9;
            for (char c = 'A'; c <= 'Z'; c++) t[9, convertCharsToInt[c.ToString()]] = 9;
            for (char c = '0'; c <= '9'; c++) t[9, convertCharsToInt[c.ToString()]] = 9;
            foreach (var symbolHasSameId in SymbolsHasSameId)
            {
                t[9, convertCharsToInt[symbolHasSameId.Symbol]] = 9;
            }
            t[9, convertCharsToInt["<"]] = 9;
            t[9, convertCharsToInt[">"]] = 9;
            t[9, convertCharsToInt["/"]] = 10;
            t[9, convertCharsToInt["="]] = 9;
            t[9, convertCharsToInt["!"]] = 9;
            t[9, convertCharsToInt["-"]] = 9;
            t[9, convertCharsToInt["*"]] = 9;
            t[9, convertCharsToInt["|"]] = 9;
            t[9, convertCharsToInt["&"]] = 9;
            t[9, convertCharsToInt[";"]] = 9;
            t[9, convertCharsToInt["|"]] = 9;
            t[9, convertCharsToInt[newLine]] = 9;
            t[9, convertCharsToInt[tap]] = 9;
            t[9, convertCharsToInt[" "]] = 9;
        }
        public void doTransitionTableForD10()
        {
            //                              state  ch                               next state
            for (char c = 'a'; c <= 'z'; c++) t[10, convertCharsToInt[c.ToString()]] = 9;
            for (char c = 'A'; c <= 'Z'; c++) t[10, convertCharsToInt[c.ToString()]] = 9;
            for (char c = '0'; c <= '9'; c++) t[10, convertCharsToInt[c.ToString()]] = 9;
            foreach (var symbolHasSameId in SymbolsHasSameId)
            {
                t[10, convertCharsToInt[symbolHasSameId.Symbol]] = 9;
            }
            t[10, convertCharsToInt["<"]] = 9;
            t[10, convertCharsToInt[">"]] = 11;
            t[10, convertCharsToInt["/"]] = 10;
            t[10, convertCharsToInt["="]] = 9;
            t[10, convertCharsToInt["!"]] = 9;
            t[10, convertCharsToInt["-"]] = 9;
            t[10, convertCharsToInt["*"]] = 9;
            t[10, convertCharsToInt["|"]] = 9;
            t[10, convertCharsToInt["&"]] = 9;
            t[10, convertCharsToInt[";"]] = 9;
            t[10, convertCharsToInt["|"]] = 9;
            t[10, convertCharsToInt[newLine]] = 9;
            t[10, convertCharsToInt[tap]] = 9;
            t[10, convertCharsToInt[" "]] = 9;
        }
        public void doTransitionTableForD11()
        {
            t[11, convertCharsToInt[" "]] = 42;
            t[11, convertCharsToInt[tap]] = 42;
            t[11, convertCharsToInt[newLine]] = 42;
        }
        public void doTransitionTableForD12()
        {
            t[12, convertCharsToInt[">"]] = 38;

            t[12, convertCharsToInt[" "]] = 39;
            t[12, convertCharsToInt[tap]] = 39;
            t[12, convertCharsToInt[newLine]] = 39;
        }
        public void doTransitionTableForD20()
        {
            t[20, convertCharsToInt["="]] = 1;
        }
        public void doTransitionTableForD23()
        {
            t[23, convertCharsToInt["="]] = 1;

            t[23, convertCharsToInt[" "]] = 39;
            t[23, convertCharsToInt[tap]] = 39;
            t[23, convertCharsToInt[newLine]] = 39;
        }
        public void doTransitionTableForD26()
        {
            t[26, convertCharsToInt["|"]] = 1;
        }
        public void doTransitionTableForD28()
        {
            t[28, convertCharsToInt["&"]] = 1;
        }
        public void doTransitionTableForD32()
        {
            t[32, convertCharsToInt["*"]] = 33;
        }
        public void doTransitionTableForD31()
        {
            t[31, convertCharsToInt["*"]] = 32;

            t[31, convertCharsToInt[" "]] = 39;
            t[31, convertCharsToInt[tap]] = 39;
            t[31, convertCharsToInt[newLine]] = 39;
        }
        public void doTransitionTableForD33()
        {
            //                              state  ch                               next state
            for (char c = 'a'; c <= 'z'; c++) t[33, convertCharsToInt[c.ToString()]] = 33;
            for (char c = 'A'; c <= 'Z'; c++) t[33, convertCharsToInt[c.ToString()]] = 33;
            for (char c = '0'; c <= '9'; c++) t[33, convertCharsToInt[c.ToString()]] = 33;
            foreach (var symbolHasSameId in SymbolsHasSameId)
            {
                t[33, convertCharsToInt[symbolHasSameId.Symbol]] = 33;
            }

            t[33, convertCharsToInt["<"]] = 33;
            t[33, convertCharsToInt[">"]] = 33;
            t[33, convertCharsToInt["/"]] = 33;
            t[33, convertCharsToInt["="]] = 33;
            t[33, convertCharsToInt["!"]] = 33;
            t[33, convertCharsToInt["-"]] = 33;
            t[33, convertCharsToInt["*"]] = 33;
            t[33, convertCharsToInt["|"]] = 33;
            t[33, convertCharsToInt["&"]] = 33;
            t[33, convertCharsToInt[";"]] = 33;
            t[33, convertCharsToInt["|"]] = 33;
            t[33, convertCharsToInt[newLine]] = 42;
            t[33, convertCharsToInt[tap]] = 33;
            t[33, convertCharsToInt[" "]] = 33;
        }
        public void doTransitionTableForD38()
        {
            for (char c = 'a'; c <= 'z'; c++) t[38, convertCharsToInt[c.ToString()]] = 3;
            for (char c = 'A'; c <= 'Z'; c++) t[38, convertCharsToInt[c.ToString()]] = 3;
        }
        public void createAcepptArray()
        {
            int sizeOfAccept = 60; 
            int sizeOfNotAcceptState = 38; 
            int sizeOfAcceptState = 45;
            for (int i = 0; i < sizeOfAccept; i++) Accept[i] = false;
            for (int i = sizeOfNotAcceptState+1; i < sizeOfAcceptState; i++) Accept[i] = true;
            Accept[44] = false ; // for error
        }
        /*[HttpGet]
        public ActionResult readTokens()
        {
            Tokens.Add("dola");
            return RedirectToAction("Index");
            return View();
        }*/
        public ActionResult readTokens (string code )
        {
            //string code = "@ ha 55 \n 9gg </ jhdhdhnv/> jfahafh ; *** jsjsid sq \n $ ";
            code += " ";
            preCompile();
            /*System.Text.RegularExpressions.Regex regex =
        new System.Text.RegularExpressions.Regex(@"(<br />|<br/>|</ br>|</br>)");
            code = regex.Replace(code, @"\n");*/
            List<int> states = new List<int>();
            // note you must add at the end of code
            //string code= "";
            TokensMessages.Add(code);
            int lineNumber = 1;
            int state = 0;
            int newState;
            states.Add(state);
            states.Add(convertCharsToInt[newLine.ToString()]);
            for (int i =  0;code!=null&& i<code.Length ;  )
            {
                state = 0;
                string token = "";
                while( i < code.Length && !Accept[state]  )
                {
                    if (code[i] == '\n'|| code[i] == ';')
                    {
                        lineNumber++;
                    }
                    newState = t[state, convertCharsToInt[code[i].ToString()]] ;
                    state = newState ;
                    token += code[i];
                    states.Add(newState);
                    //states.Add(convertCharsToInt[code[i].ToString()]);
                    i++;
                }
                TokensMessages.Add(token);
                if (token.Equals("; "))
                    Tokens.Add(";"); // to add semi colon
                token = token.Remove(token.Length - 1);
               
                if (state == -1 )
                {
                    string tokenMessage =  token + " in line " + lineNumber + " is " + tokensType[state];
                    if (!token.Equals(""))
                    {
                        TokensMessages.Add(tokenMessage);
                        Tokens.Add(tokensType[state]);
                    }
                }
                else if (Accept[state])
                {
                    string tokenMessage = "";
                    if (KeyWords.ContainsKey(token))
                     tokenMessage = token + " in line " + lineNumber + " is " + KeyWords[token];
                    else tokenMessage = token + " in line " + lineNumber + " is " + tokensType[state];
                    if (!token.Equals(""))
                    {
                        TokensMessages.Add(tokenMessage);
                        Tokens.Add(tokensType[state]);
                    }
                }

            }
    
            return View(TokensMessages);
        }
        public bool IsDelimiter (string c)
        {
             

            if (c.Equals(" ") || c.Equals(@"\n") || c.Equals(@"\t"))
                return true;
            return false;
        }



    }
}
