// $ANTLR 3.2 Sep 23, 2009 12:02:23 C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g 2011-05-02 20:29:23

// The variable 'variable' is assigned but its value is never used.
#pragma warning disable 168, 219
// Unreachable code detected.
#pragma warning disable 162


using System;
using Antlr.Runtime;
using IList 		= System.Collections.IList;
using ArrayList 	= System.Collections.ArrayList;
using Stack 		= Antlr.Runtime.Collections.StackList;


public partial class BIPASMLexer : Lexer {
    public const int XORI = 53;
    public const int ADDI = 33;
    public const int CHAR = 61;
    public const int HLT = 46;
    public const int SUB = 34;
    public const int SLL = 54;
    public const int NOT = 47;
    public const int AND = 48;
    public const int ID = 68;
    public const int EOF = -1;
    public const int ORI = 51;
    public const int WORD = 70;
    public const int HEX = 64;
    public const int RETURN = 58;
    public const int COMENTARIOLINHA = 71;
    public const int BGT = 42;
    public const int JMP = 45;
    public const int BGE = 40;
    public const int BLE = 39;
    public const int INTS = 63;
    public const int ADD = 32;
    public const int STO = 38;
    public const int D = 7;
    public const int E = 8;
    public const int F = 9;
    public const int G = 10;
    public const int XOR = 52;
    public const int A = 4;
    public const int B = 5;
    public const int SRL = 55;
    public const int C = 6;
    public const int L = 15;
    public const int M = 16;
    public const int N = 17;
    public const int O = 18;
    public const int H = 11;
    public const int I = 12;
    public const int BLT = 41;
    public const int J = 13;
    public const int K = 14;
    public const int SECTEXT = 30;
    public const int U = 24;
    public const int T = 23;
    public const int W = 26;
    public const int V = 25;
    public const int LDI = 37;
    public const int Q = 20;
    public const int INT = 62;
    public const int P = 19;
    public const int S = 22;
    public const int R = 21;
    public const int SECDATA = 31;
    public const int STOV = 56;
    public const int REG = 67;
    public const int LDV = 57;
    public const int Y = 28;
    public const int X = 27;
    public const int SUBI = 35;
    public const int Z = 29;
    public const int ANDI = 49;
    public const int WS = 69;
    public const int T__72 = 72;
    public const int OR = 50;
    public const int CIF = 66;
    public const int CALL = 60;
    public const int BEQ = 43;
    public const int BIN = 65;
    public const int T__73 = 73;
    public const int LD = 36;
    public const int BNE = 44;
    public const int RETINT = 59;

    // delegates
    // delegators

    public BIPASMLexer() 
    {
		InitializeCyclicDFAs();
    }
    public BIPASMLexer(ICharStream input)
		: this(input, null) {
    }
    public BIPASMLexer(ICharStream input, RecognizerSharedState state)
		: base(input, state) {
		InitializeCyclicDFAs(); 

    }
    
    override public string GrammarFileName
    {
    	get { return "C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g";} 
    }

    // $ANTLR start "T__72"
    public void mT__72() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = T__72;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:7:7: ( ':' )
            // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:7:9: ':'
            {
            	Match(':'); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "T__72"

    // $ANTLR start "T__73"
    public void mT__73() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = T__73;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:8:7: ( ',' )
            // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:8:9: ','
            {
            	Match(','); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "T__73"

    // $ANTLR start "A"
    public void mA() // throws RecognitionException [2]
    {
    		try
    		{
            // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:12:11: ( ( 'a' | 'A' ) )
            // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:12:12: ( 'a' | 'A' )
            {
            	if ( input.LA(1) == 'A' || input.LA(1) == 'a' ) 
            	{
            	    input.Consume();

            	}
            	else 
            	{
            	    MismatchedSetException mse = new MismatchedSetException(null,input);
            	    Recover(mse);
            	    throw mse;}


            }

        }
        finally 
    	{
        }
    }
    // $ANTLR end "A"

    // $ANTLR start "B"
    public void mB() // throws RecognitionException [2]
    {
    		try
    		{
            // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:13:11: ( ( 'b' | 'B' ) )
            // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:13:12: ( 'b' | 'B' )
            {
            	if ( input.LA(1) == 'B' || input.LA(1) == 'b' ) 
            	{
            	    input.Consume();

            	}
            	else 
            	{
            	    MismatchedSetException mse = new MismatchedSetException(null,input);
            	    Recover(mse);
            	    throw mse;}


            }

        }
        finally 
    	{
        }
    }
    // $ANTLR end "B"

    // $ANTLR start "C"
    public void mC() // throws RecognitionException [2]
    {
    		try
    		{
            // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:14:11: ( ( 'c' | 'C' ) )
            // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:14:12: ( 'c' | 'C' )
            {
            	if ( input.LA(1) == 'C' || input.LA(1) == 'c' ) 
            	{
            	    input.Consume();

            	}
            	else 
            	{
            	    MismatchedSetException mse = new MismatchedSetException(null,input);
            	    Recover(mse);
            	    throw mse;}


            }

        }
        finally 
    	{
        }
    }
    // $ANTLR end "C"

    // $ANTLR start "D"
    public void mD() // throws RecognitionException [2]
    {
    		try
    		{
            // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:15:11: ( ( 'd' | 'D' ) )
            // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:15:12: ( 'd' | 'D' )
            {
            	if ( input.LA(1) == 'D' || input.LA(1) == 'd' ) 
            	{
            	    input.Consume();

            	}
            	else 
            	{
            	    MismatchedSetException mse = new MismatchedSetException(null,input);
            	    Recover(mse);
            	    throw mse;}


            }

        }
        finally 
    	{
        }
    }
    // $ANTLR end "D"

    // $ANTLR start "E"
    public void mE() // throws RecognitionException [2]
    {
    		try
    		{
            // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:16:11: ( ( 'e' | 'E' ) )
            // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:16:12: ( 'e' | 'E' )
            {
            	if ( input.LA(1) == 'E' || input.LA(1) == 'e' ) 
            	{
            	    input.Consume();

            	}
            	else 
            	{
            	    MismatchedSetException mse = new MismatchedSetException(null,input);
            	    Recover(mse);
            	    throw mse;}


            }

        }
        finally 
    	{
        }
    }
    // $ANTLR end "E"

    // $ANTLR start "F"
    public void mF() // throws RecognitionException [2]
    {
    		try
    		{
            // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:17:11: ( ( 'f' | 'F' ) )
            // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:17:12: ( 'f' | 'F' )
            {
            	if ( input.LA(1) == 'F' || input.LA(1) == 'f' ) 
            	{
            	    input.Consume();

            	}
            	else 
            	{
            	    MismatchedSetException mse = new MismatchedSetException(null,input);
            	    Recover(mse);
            	    throw mse;}


            }

        }
        finally 
    	{
        }
    }
    // $ANTLR end "F"

    // $ANTLR start "G"
    public void mG() // throws RecognitionException [2]
    {
    		try
    		{
            // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:18:11: ( ( 'g' | 'G' ) )
            // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:18:12: ( 'g' | 'G' )
            {
            	if ( input.LA(1) == 'G' || input.LA(1) == 'g' ) 
            	{
            	    input.Consume();

            	}
            	else 
            	{
            	    MismatchedSetException mse = new MismatchedSetException(null,input);
            	    Recover(mse);
            	    throw mse;}


            }

        }
        finally 
    	{
        }
    }
    // $ANTLR end "G"

    // $ANTLR start "H"
    public void mH() // throws RecognitionException [2]
    {
    		try
    		{
            // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:19:11: ( ( 'h' | 'H' ) )
            // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:19:12: ( 'h' | 'H' )
            {
            	if ( input.LA(1) == 'H' || input.LA(1) == 'h' ) 
            	{
            	    input.Consume();

            	}
            	else 
            	{
            	    MismatchedSetException mse = new MismatchedSetException(null,input);
            	    Recover(mse);
            	    throw mse;}


            }

        }
        finally 
    	{
        }
    }
    // $ANTLR end "H"

    // $ANTLR start "I"
    public void mI() // throws RecognitionException [2]
    {
    		try
    		{
            // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:20:11: ( ( 'i' | 'I' ) )
            // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:20:12: ( 'i' | 'I' )
            {
            	if ( input.LA(1) == 'I' || input.LA(1) == 'i' ) 
            	{
            	    input.Consume();

            	}
            	else 
            	{
            	    MismatchedSetException mse = new MismatchedSetException(null,input);
            	    Recover(mse);
            	    throw mse;}


            }

        }
        finally 
    	{
        }
    }
    // $ANTLR end "I"

    // $ANTLR start "J"
    public void mJ() // throws RecognitionException [2]
    {
    		try
    		{
            // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:21:11: ( ( 'j' | 'J' ) )
            // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:21:12: ( 'j' | 'J' )
            {
            	if ( input.LA(1) == 'J' || input.LA(1) == 'j' ) 
            	{
            	    input.Consume();

            	}
            	else 
            	{
            	    MismatchedSetException mse = new MismatchedSetException(null,input);
            	    Recover(mse);
            	    throw mse;}


            }

        }
        finally 
    	{
        }
    }
    // $ANTLR end "J"

    // $ANTLR start "K"
    public void mK() // throws RecognitionException [2]
    {
    		try
    		{
            // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:22:11: ( ( 'k' | 'K' ) )
            // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:22:12: ( 'k' | 'K' )
            {
            	if ( input.LA(1) == 'K' || input.LA(1) == 'k' ) 
            	{
            	    input.Consume();

            	}
            	else 
            	{
            	    MismatchedSetException mse = new MismatchedSetException(null,input);
            	    Recover(mse);
            	    throw mse;}


            }

        }
        finally 
    	{
        }
    }
    // $ANTLR end "K"

    // $ANTLR start "L"
    public void mL() // throws RecognitionException [2]
    {
    		try
    		{
            // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:23:11: ( ( 'l' | 'L' ) )
            // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:23:12: ( 'l' | 'L' )
            {
            	if ( input.LA(1) == 'L' || input.LA(1) == 'l' ) 
            	{
            	    input.Consume();

            	}
            	else 
            	{
            	    MismatchedSetException mse = new MismatchedSetException(null,input);
            	    Recover(mse);
            	    throw mse;}


            }

        }
        finally 
    	{
        }
    }
    // $ANTLR end "L"

    // $ANTLR start "M"
    public void mM() // throws RecognitionException [2]
    {
    		try
    		{
            // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:24:11: ( ( 'm' | 'M' ) )
            // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:24:12: ( 'm' | 'M' )
            {
            	if ( input.LA(1) == 'M' || input.LA(1) == 'm' ) 
            	{
            	    input.Consume();

            	}
            	else 
            	{
            	    MismatchedSetException mse = new MismatchedSetException(null,input);
            	    Recover(mse);
            	    throw mse;}


            }

        }
        finally 
    	{
        }
    }
    // $ANTLR end "M"

    // $ANTLR start "N"
    public void mN() // throws RecognitionException [2]
    {
    		try
    		{
            // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:25:11: ( ( 'n' | 'N' ) )
            // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:25:12: ( 'n' | 'N' )
            {
            	if ( input.LA(1) == 'N' || input.LA(1) == 'n' ) 
            	{
            	    input.Consume();

            	}
            	else 
            	{
            	    MismatchedSetException mse = new MismatchedSetException(null,input);
            	    Recover(mse);
            	    throw mse;}


            }

        }
        finally 
    	{
        }
    }
    // $ANTLR end "N"

    // $ANTLR start "O"
    public void mO() // throws RecognitionException [2]
    {
    		try
    		{
            // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:26:11: ( ( 'o' | 'O' ) )
            // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:26:12: ( 'o' | 'O' )
            {
            	if ( input.LA(1) == 'O' || input.LA(1) == 'o' ) 
            	{
            	    input.Consume();

            	}
            	else 
            	{
            	    MismatchedSetException mse = new MismatchedSetException(null,input);
            	    Recover(mse);
            	    throw mse;}


            }

        }
        finally 
    	{
        }
    }
    // $ANTLR end "O"

    // $ANTLR start "P"
    public void mP() // throws RecognitionException [2]
    {
    		try
    		{
            // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:27:11: ( ( 'p' | 'P' ) )
            // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:27:12: ( 'p' | 'P' )
            {
            	if ( input.LA(1) == 'P' || input.LA(1) == 'p' ) 
            	{
            	    input.Consume();

            	}
            	else 
            	{
            	    MismatchedSetException mse = new MismatchedSetException(null,input);
            	    Recover(mse);
            	    throw mse;}


            }

        }
        finally 
    	{
        }
    }
    // $ANTLR end "P"

    // $ANTLR start "Q"
    public void mQ() // throws RecognitionException [2]
    {
    		try
    		{
            // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:28:11: ( ( 'q' | 'Q' ) )
            // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:28:12: ( 'q' | 'Q' )
            {
            	if ( input.LA(1) == 'Q' || input.LA(1) == 'q' ) 
            	{
            	    input.Consume();

            	}
            	else 
            	{
            	    MismatchedSetException mse = new MismatchedSetException(null,input);
            	    Recover(mse);
            	    throw mse;}


            }

        }
        finally 
    	{
        }
    }
    // $ANTLR end "Q"

    // $ANTLR start "R"
    public void mR() // throws RecognitionException [2]
    {
    		try
    		{
            // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:29:11: ( ( 'r' | 'R' ) )
            // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:29:12: ( 'r' | 'R' )
            {
            	if ( input.LA(1) == 'R' || input.LA(1) == 'r' ) 
            	{
            	    input.Consume();

            	}
            	else 
            	{
            	    MismatchedSetException mse = new MismatchedSetException(null,input);
            	    Recover(mse);
            	    throw mse;}


            }

        }
        finally 
    	{
        }
    }
    // $ANTLR end "R"

    // $ANTLR start "S"
    public void mS() // throws RecognitionException [2]
    {
    		try
    		{
            // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:30:11: ( ( 's' | 'S' ) )
            // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:30:12: ( 's' | 'S' )
            {
            	if ( input.LA(1) == 'S' || input.LA(1) == 's' ) 
            	{
            	    input.Consume();

            	}
            	else 
            	{
            	    MismatchedSetException mse = new MismatchedSetException(null,input);
            	    Recover(mse);
            	    throw mse;}


            }

        }
        finally 
    	{
        }
    }
    // $ANTLR end "S"

    // $ANTLR start "T"
    public void mT() // throws RecognitionException [2]
    {
    		try
    		{
            // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:31:11: ( ( 't' | 'T' ) )
            // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:31:12: ( 't' | 'T' )
            {
            	if ( input.LA(1) == 'T' || input.LA(1) == 't' ) 
            	{
            	    input.Consume();

            	}
            	else 
            	{
            	    MismatchedSetException mse = new MismatchedSetException(null,input);
            	    Recover(mse);
            	    throw mse;}


            }

        }
        finally 
    	{
        }
    }
    // $ANTLR end "T"

    // $ANTLR start "U"
    public void mU() // throws RecognitionException [2]
    {
    		try
    		{
            // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:32:11: ( ( 'u' | 'U' ) )
            // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:32:12: ( 'u' | 'U' )
            {
            	if ( input.LA(1) == 'U' || input.LA(1) == 'u' ) 
            	{
            	    input.Consume();

            	}
            	else 
            	{
            	    MismatchedSetException mse = new MismatchedSetException(null,input);
            	    Recover(mse);
            	    throw mse;}


            }

        }
        finally 
    	{
        }
    }
    // $ANTLR end "U"

    // $ANTLR start "V"
    public void mV() // throws RecognitionException [2]
    {
    		try
    		{
            // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:33:11: ( ( 'v' | 'V' ) )
            // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:33:12: ( 'v' | 'V' )
            {
            	if ( input.LA(1) == 'V' || input.LA(1) == 'v' ) 
            	{
            	    input.Consume();

            	}
            	else 
            	{
            	    MismatchedSetException mse = new MismatchedSetException(null,input);
            	    Recover(mse);
            	    throw mse;}


            }

        }
        finally 
    	{
        }
    }
    // $ANTLR end "V"

    // $ANTLR start "W"
    public void mW() // throws RecognitionException [2]
    {
    		try
    		{
            // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:34:11: ( ( 'w' | 'W' ) )
            // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:34:12: ( 'w' | 'W' )
            {
            	if ( input.LA(1) == 'W' || input.LA(1) == 'w' ) 
            	{
            	    input.Consume();

            	}
            	else 
            	{
            	    MismatchedSetException mse = new MismatchedSetException(null,input);
            	    Recover(mse);
            	    throw mse;}


            }

        }
        finally 
    	{
        }
    }
    // $ANTLR end "W"

    // $ANTLR start "X"
    public void mX() // throws RecognitionException [2]
    {
    		try
    		{
            // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:35:11: ( ( 'x' | 'X' ) )
            // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:35:12: ( 'x' | 'X' )
            {
            	if ( input.LA(1) == 'X' || input.LA(1) == 'x' ) 
            	{
            	    input.Consume();

            	}
            	else 
            	{
            	    MismatchedSetException mse = new MismatchedSetException(null,input);
            	    Recover(mse);
            	    throw mse;}


            }

        }
        finally 
    	{
        }
    }
    // $ANTLR end "X"

    // $ANTLR start "Y"
    public void mY() // throws RecognitionException [2]
    {
    		try
    		{
            // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:36:11: ( ( 'y' | 'Y' ) )
            // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:36:12: ( 'y' | 'Y' )
            {
            	if ( input.LA(1) == 'Y' || input.LA(1) == 'y' ) 
            	{
            	    input.Consume();

            	}
            	else 
            	{
            	    MismatchedSetException mse = new MismatchedSetException(null,input);
            	    Recover(mse);
            	    throw mse;}


            }

        }
        finally 
    	{
        }
    }
    // $ANTLR end "Y"

    // $ANTLR start "Z"
    public void mZ() // throws RecognitionException [2]
    {
    		try
    		{
            // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:37:11: ( ( 'z' | 'Z' ) )
            // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:37:12: ( 'z' | 'Z' )
            {
            	if ( input.LA(1) == 'Z' || input.LA(1) == 'z' ) 
            	{
            	    input.Consume();

            	}
            	else 
            	{
            	    MismatchedSetException mse = new MismatchedSetException(null,input);
            	    Recover(mse);
            	    throw mse;}


            }

        }
        finally 
    	{
        }
    }
    // $ANTLR end "Z"

    // $ANTLR start "SECTEXT"
    public void mSECTEXT() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = SECTEXT;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:40:8: ( '.' T E X T )
            // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:40:11: '.' T E X T
            {
            	Match('.'); 
            	mT(); 
            	mE(); 
            	mX(); 
            	mT(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "SECTEXT"

    // $ANTLR start "SECDATA"
    public void mSECDATA() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = SECDATA;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:41:8: ( '.' D A T A )
            // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:41:11: '.' D A T A
            {
            	Match('.'); 
            	mD(); 
            	mA(); 
            	mT(); 
            	mA(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "SECDATA"

    // $ANTLR start "ADD"
    public void mADD() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = ADD;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:43:4: ( A D D )
            // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:43:7: A D D
            {
            	mA(); 
            	mD(); 
            	mD(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "ADD"

    // $ANTLR start "ADDI"
    public void mADDI() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = ADDI;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:44:5: ( A D D I )
            // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:44:8: A D D I
            {
            	mA(); 
            	mD(); 
            	mD(); 
            	mI(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "ADDI"

    // $ANTLR start "SUB"
    public void mSUB() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = SUB;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:45:4: ( S U B )
            // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:45:7: S U B
            {
            	mS(); 
            	mU(); 
            	mB(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "SUB"

    // $ANTLR start "SUBI"
    public void mSUBI() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = SUBI;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:46:5: ( S U B I )
            // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:46:8: S U B I
            {
            	mS(); 
            	mU(); 
            	mB(); 
            	mI(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "SUBI"

    // $ANTLR start "LD"
    public void mLD() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = LD;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:47:3: ( L D )
            // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:47:6: L D
            {
            	mL(); 
            	mD(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "LD"

    // $ANTLR start "LDI"
    public void mLDI() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = LDI;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:48:4: ( L D I )
            // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:48:7: L D I
            {
            	mL(); 
            	mD(); 
            	mI(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "LDI"

    // $ANTLR start "STO"
    public void mSTO() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = STO;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:49:4: ( S T O )
            // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:49:7: S T O
            {
            	mS(); 
            	mT(); 
            	mO(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "STO"

    // $ANTLR start "BLE"
    public void mBLE() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = BLE;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:51:4: ( B L E )
            // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:51:7: B L E
            {
            	mB(); 
            	mL(); 
            	mE(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "BLE"

    // $ANTLR start "BGE"
    public void mBGE() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = BGE;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:52:4: ( B G E )
            // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:52:7: B G E
            {
            	mB(); 
            	mG(); 
            	mE(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "BGE"

    // $ANTLR start "BLT"
    public void mBLT() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = BLT;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:53:4: ( B L T )
            // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:53:7: B L T
            {
            	mB(); 
            	mL(); 
            	mT(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "BLT"

    // $ANTLR start "BGT"
    public void mBGT() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = BGT;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:54:4: ( B G T )
            // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:54:7: B G T
            {
            	mB(); 
            	mG(); 
            	mT(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "BGT"

    // $ANTLR start "BEQ"
    public void mBEQ() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = BEQ;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:55:4: ( B E Q )
            // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:55:7: B E Q
            {
            	mB(); 
            	mE(); 
            	mQ(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "BEQ"

    // $ANTLR start "BNE"
    public void mBNE() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = BNE;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:56:4: ( B N E )
            // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:56:7: B N E
            {
            	mB(); 
            	mN(); 
            	mE(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "BNE"

    // $ANTLR start "JMP"
    public void mJMP() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = JMP;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:57:4: ( J M P )
            // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:57:7: J M P
            {
            	mJ(); 
            	mM(); 
            	mP(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "JMP"

    // $ANTLR start "HLT"
    public void mHLT() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = HLT;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:58:4: ( H L T )
            // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:58:7: H L T
            {
            	mH(); 
            	mL(); 
            	mT(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "HLT"

    // $ANTLR start "NOT"
    public void mNOT() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = NOT;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:59:5: ( N O T )
            // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:59:7: N O T
            {
            	mN(); 
            	mO(); 
            	mT(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "NOT"

    // $ANTLR start "AND"
    public void mAND() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = AND;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:60:5: ( A N D )
            // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:60:7: A N D
            {
            	mA(); 
            	mN(); 
            	mD(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "AND"

    // $ANTLR start "ANDI"
    public void mANDI() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = ANDI;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:61:6: ( A N D I )
            // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:61:8: A N D I
            {
            	mA(); 
            	mN(); 
            	mD(); 
            	mI(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "ANDI"

    // $ANTLR start "OR"
    public void mOR() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = OR;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:62:4: ( O R )
            // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:62:6: O R
            {
            	mO(); 
            	mR(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "OR"

    // $ANTLR start "ORI"
    public void mORI() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = ORI;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:63:5: ( O R I )
            // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:63:7: O R I
            {
            	mO(); 
            	mR(); 
            	mI(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "ORI"

    // $ANTLR start "XOR"
    public void mXOR() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = XOR;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:64:5: ( X O R )
            // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:64:7: X O R
            {
            	mX(); 
            	mO(); 
            	mR(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "XOR"

    // $ANTLR start "XORI"
    public void mXORI() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = XORI;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:65:6: ( X O R I )
            // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:65:8: X O R I
            {
            	mX(); 
            	mO(); 
            	mR(); 
            	mI(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "XORI"

    // $ANTLR start "SLL"
    public void mSLL() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = SLL;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:66:5: ( S L L )
            // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:66:7: S L L
            {
            	mS(); 
            	mL(); 
            	mL(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "SLL"

    // $ANTLR start "SRL"
    public void mSRL() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = SRL;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:67:5: ( S R L )
            // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:67:7: S R L
            {
            	mS(); 
            	mR(); 
            	mL(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "SRL"

    // $ANTLR start "STOV"
    public void mSTOV() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = STOV;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:68:6: ( S T O V )
            // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:68:8: S T O V
            {
            	mS(); 
            	mT(); 
            	mO(); 
            	mV(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "STOV"

    // $ANTLR start "LDV"
    public void mLDV() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = LDV;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:69:5: ( L D V )
            // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:69:7: L D V
            {
            	mL(); 
            	mD(); 
            	mV(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "LDV"

    // $ANTLR start "RETURN"
    public void mRETURN() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = RETURN;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:70:9: ( R E T U R N )
            // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:70:11: R E T U R N
            {
            	mR(); 
            	mE(); 
            	mT(); 
            	mU(); 
            	mR(); 
            	mN(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "RETURN"

    // $ANTLR start "RETINT"
    public void mRETINT() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = RETINT;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:71:8: ( R E T I N T )
            // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:71:10: R E T I N T
            {
            	mR(); 
            	mE(); 
            	mT(); 
            	mI(); 
            	mN(); 
            	mT(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "RETINT"

    // $ANTLR start "CALL"
    public void mCALL() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = CALL;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:72:6: ( C A L L )
            // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:72:8: C A L L
            {
            	mC(); 
            	mA(); 
            	mL(); 
            	mL(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "CALL"

    // $ANTLR start "CHAR"
    public void mCHAR() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = CHAR;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:74:6: ( ( '\\'' ) ~ ( '\\'' ) ( '\\'' ) )
            // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:74:8: ( '\\'' ) ~ ( '\\'' ) ( '\\'' )
            {
            	// C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:74:8: ( '\\'' )
            	// C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:74:9: '\\''
            	{
            		Match('\''); 

            	}

            	if ( (input.LA(1) >= '\u0000' && input.LA(1) <= '&') || (input.LA(1) >= '(' && input.LA(1) <= '\uFFFF') ) 
            	{
            	    input.Consume();

            	}
            	else 
            	{
            	    MismatchedSetException mse = new MismatchedSetException(null,input);
            	    Recover(mse);
            	    throw mse;}

            	// C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:74:21: ( '\\'' )
            	// C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:74:22: '\\''
            	{
            		Match('\''); 

            	}


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "CHAR"

    // $ANTLR start "INT"
    public void mINT() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = INT;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:75:6: ( ( '0' .. '9' )+ )
            // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:75:8: ( '0' .. '9' )+
            {
            	// C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:75:8: ( '0' .. '9' )+
            	int cnt1 = 0;
            	do 
            	{
            	    int alt1 = 2;
            	    int LA1_0 = input.LA(1);

            	    if ( ((LA1_0 >= '0' && LA1_0 <= '9')) )
            	    {
            	        alt1 = 1;
            	    }


            	    switch (alt1) 
            		{
            			case 1 :
            			    // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:75:9: '0' .. '9'
            			    {
            			    	MatchRange('0','9'); 

            			    }
            			    break;

            			default:
            			    if ( cnt1 >= 1 ) goto loop1;
            		            EarlyExitException eee1 =
            		                new EarlyExitException(1, input);
            		            throw eee1;
            	    }
            	    cnt1++;
            	} while (true);

            	loop1:
            		;	// Stops C# compiler whining that label 'loop1' has no statements


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "INT"

    // $ANTLR start "INTS"
    public void mINTS() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = INTS;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:76:7: ( ( '-' ) ( '0' .. '9' )+ )
            // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:76:9: ( '-' ) ( '0' .. '9' )+
            {
            	// C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:76:9: ( '-' )
            	// C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:76:10: '-'
            	{
            		Match('-'); 

            	}

            	// C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:76:14: ( '0' .. '9' )+
            	int cnt2 = 0;
            	do 
            	{
            	    int alt2 = 2;
            	    int LA2_0 = input.LA(1);

            	    if ( ((LA2_0 >= '0' && LA2_0 <= '9')) )
            	    {
            	        alt2 = 1;
            	    }


            	    switch (alt2) 
            		{
            			case 1 :
            			    // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:76:15: '0' .. '9'
            			    {
            			    	MatchRange('0','9'); 

            			    }
            			    break;

            			default:
            			    if ( cnt2 >= 1 ) goto loop2;
            		            EarlyExitException eee2 =
            		                new EarlyExitException(2, input);
            		            throw eee2;
            	    }
            	    cnt2++;
            	} while (true);

            	loop2:
            		;	// Stops C# compiler whining that label 'loop2' has no statements


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "INTS"

    // $ANTLR start "HEX"
    public void mHEX() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = HEX;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:77:5: ( '0x' ( ( '0' .. '9' ) | ( 'A' .. 'F' ) | ( 'a' .. 'f' ) )+ )
            // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:77:7: '0x' ( ( '0' .. '9' ) | ( 'A' .. 'F' ) | ( 'a' .. 'f' ) )+
            {
            	Match("0x"); 

            	// C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:77:11: ( ( '0' .. '9' ) | ( 'A' .. 'F' ) | ( 'a' .. 'f' ) )+
            	int cnt3 = 0;
            	do 
            	{
            	    int alt3 = 4;
            	    switch ( input.LA(1) ) 
            	    {
            	    case '0':
            	    case '1':
            	    case '2':
            	    case '3':
            	    case '4':
            	    case '5':
            	    case '6':
            	    case '7':
            	    case '8':
            	    case '9':
            	    	{
            	        alt3 = 1;
            	        }
            	        break;
            	    case 'A':
            	    case 'B':
            	    case 'C':
            	    case 'D':
            	    case 'E':
            	    case 'F':
            	    	{
            	        alt3 = 2;
            	        }
            	        break;
            	    case 'a':
            	    case 'b':
            	    case 'c':
            	    case 'd':
            	    case 'e':
            	    case 'f':
            	    	{
            	        alt3 = 3;
            	        }
            	        break;

            	    }

            	    switch (alt3) 
            		{
            			case 1 :
            			    // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:77:12: ( '0' .. '9' )
            			    {
            			    	// C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:77:12: ( '0' .. '9' )
            			    	// C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:77:13: '0' .. '9'
            			    	{
            			    		MatchRange('0','9'); 

            			    	}


            			    }
            			    break;
            			case 2 :
            			    // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:77:23: ( 'A' .. 'F' )
            			    {
            			    	// C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:77:23: ( 'A' .. 'F' )
            			    	// C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:77:24: 'A' .. 'F'
            			    	{
            			    		MatchRange('A','F'); 

            			    	}


            			    }
            			    break;
            			case 3 :
            			    // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:77:34: ( 'a' .. 'f' )
            			    {
            			    	// C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:77:34: ( 'a' .. 'f' )
            			    	// C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:77:35: 'a' .. 'f'
            			    	{
            			    		MatchRange('a','f'); 

            			    	}


            			    }
            			    break;

            			default:
            			    if ( cnt3 >= 1 ) goto loop3;
            		            EarlyExitException eee3 =
            		                new EarlyExitException(3, input);
            		            throw eee3;
            	    }
            	    cnt3++;
            	} while (true);

            	loop3:
            		;	// Stops C# compiler whining that label 'loop3' has no statements


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "HEX"

    // $ANTLR start "BIN"
    public void mBIN() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = BIN;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:78:5: ( '0b' ( '0' | '1' )+ )
            // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:78:7: '0b' ( '0' | '1' )+
            {
            	Match("0b"); 

            	// C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:78:11: ( '0' | '1' )+
            	int cnt4 = 0;
            	do 
            	{
            	    int alt4 = 2;
            	    int LA4_0 = input.LA(1);

            	    if ( ((LA4_0 >= '0' && LA4_0 <= '1')) )
            	    {
            	        alt4 = 1;
            	    }


            	    switch (alt4) 
            		{
            			case 1 :
            			    // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:
            			    {
            			    	if ( (input.LA(1) >= '0' && input.LA(1) <= '1') ) 
            			    	{
            			    	    input.Consume();

            			    	}
            			    	else 
            			    	{
            			    	    MismatchedSetException mse = new MismatchedSetException(null,input);
            			    	    Recover(mse);
            			    	    throw mse;}


            			    }
            			    break;

            			default:
            			    if ( cnt4 >= 1 ) goto loop4;
            		            EarlyExitException eee4 =
            		                new EarlyExitException(4, input);
            		            throw eee4;
            	    }
            	    cnt4++;
            	} while (true);

            	loop4:
            		;	// Stops C# compiler whining that label 'loop4' has no statements


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "BIN"

    // $ANTLR start "CIF"
    public void mCIF() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = CIF;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:79:7: ( '$' ( '0' .. '9' )+ )
            // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:79:9: '$' ( '0' .. '9' )+
            {
            	Match('$'); 
            	// C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:79:12: ( '0' .. '9' )+
            	int cnt5 = 0;
            	do 
            	{
            	    int alt5 = 2;
            	    int LA5_0 = input.LA(1);

            	    if ( ((LA5_0 >= '0' && LA5_0 <= '9')) )
            	    {
            	        alt5 = 1;
            	    }


            	    switch (alt5) 
            		{
            			case 1 :
            			    // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:79:13: '0' .. '9'
            			    {
            			    	MatchRange('0','9'); 

            			    }
            			    break;

            			default:
            			    if ( cnt5 >= 1 ) goto loop5;
            		            EarlyExitException eee5 =
            		                new EarlyExitException(5, input);
            		            throw eee5;
            	    }
            	    cnt5++;
            	} while (true);

            	loop5:
            		;	// Stops C# compiler whining that label 'loop5' has no statements


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "CIF"

    // $ANTLR start "REG"
    public void mREG() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = REG;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:80:5: ( '$' ( 'port0_dir' | 'port1_dir' | 'port0_data' | 'port1_data' | 'tmr0_config' | 'tmr0_value' | 'int_config' | 'int_status' | 'mcu_config' | 'indr' | 'status' | 'uart_data' | 'uart_status' | 'in_port' | 'out_port' ) )
            // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:80:7: '$' ( 'port0_dir' | 'port1_dir' | 'port0_data' | 'port1_data' | 'tmr0_config' | 'tmr0_value' | 'int_config' | 'int_status' | 'mcu_config' | 'indr' | 'status' | 'uart_data' | 'uart_status' | 'in_port' | 'out_port' )
            {
            	Match('$'); 
            	// C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:80:10: ( 'port0_dir' | 'port1_dir' | 'port0_data' | 'port1_data' | 'tmr0_config' | 'tmr0_value' | 'int_config' | 'int_status' | 'mcu_config' | 'indr' | 'status' | 'uart_data' | 'uart_status' | 'in_port' | 'out_port' )
            	int alt6 = 15;
            	alt6 = dfa6.Predict(input);
            	switch (alt6) 
            	{
            	    case 1 :
            	        // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:80:11: 'port0_dir'
            	        {
            	        	Match("port0_dir"); 


            	        }
            	        break;
            	    case 2 :
            	        // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:80:25: 'port1_dir'
            	        {
            	        	Match("port1_dir"); 


            	        }
            	        break;
            	    case 3 :
            	        // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:80:39: 'port0_data'
            	        {
            	        	Match("port0_data"); 


            	        }
            	        break;
            	    case 4 :
            	        // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:80:54: 'port1_data'
            	        {
            	        	Match("port1_data"); 


            	        }
            	        break;
            	    case 5 :
            	        // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:80:69: 'tmr0_config'
            	        {
            	        	Match("tmr0_config"); 


            	        }
            	        break;
            	    case 6 :
            	        // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:80:85: 'tmr0_value'
            	        {
            	        	Match("tmr0_value"); 


            	        }
            	        break;
            	    case 7 :
            	        // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:80:100: 'int_config'
            	        {
            	        	Match("int_config"); 


            	        }
            	        break;
            	    case 8 :
            	        // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:80:115: 'int_status'
            	        {
            	        	Match("int_status"); 


            	        }
            	        break;
            	    case 9 :
            	        // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:81:29: 'mcu_config'
            	        {
            	        	Match("mcu_config"); 


            	        }
            	        break;
            	    case 10 :
            	        // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:81:44: 'indr'
            	        {
            	        	Match("indr"); 


            	        }
            	        break;
            	    case 11 :
            	        // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:81:53: 'status'
            	        {
            	        	Match("status"); 


            	        }
            	        break;
            	    case 12 :
            	        // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:81:64: 'uart_data'
            	        {
            	        	Match("uart_data"); 


            	        }
            	        break;
            	    case 13 :
            	        // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:81:78: 'uart_status'
            	        {
            	        	Match("uart_status"); 


            	        }
            	        break;
            	    case 14 :
            	        // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:81:94: 'in_port'
            	        {
            	        	Match("in_port"); 


            	        }
            	        break;
            	    case 15 :
            	        // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:81:106: 'out_port'
            	        {
            	        	Match("out_port"); 


            	        }
            	        break;

            	}


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "REG"

    // $ANTLR start "ID"
    public void mID() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = ID;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:83:9: ( ( ( 'a' .. 'z' ) | ( 'A' .. 'Z' ) | '_' ) ( ( 'a' .. 'z' ) | ( 'A' .. 'Z' ) | '_' | ( '0' .. '9' ) )* )
            // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:83:11: ( ( 'a' .. 'z' ) | ( 'A' .. 'Z' ) | '_' ) ( ( 'a' .. 'z' ) | ( 'A' .. 'Z' ) | '_' | ( '0' .. '9' ) )*
            {
            	// C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:83:11: ( ( 'a' .. 'z' ) | ( 'A' .. 'Z' ) | '_' )
            	int alt7 = 3;
            	switch ( input.LA(1) ) 
            	{
            	case 'a':
            	case 'b':
            	case 'c':
            	case 'd':
            	case 'e':
            	case 'f':
            	case 'g':
            	case 'h':
            	case 'i':
            	case 'j':
            	case 'k':
            	case 'l':
            	case 'm':
            	case 'n':
            	case 'o':
            	case 'p':
            	case 'q':
            	case 'r':
            	case 's':
            	case 't':
            	case 'u':
            	case 'v':
            	case 'w':
            	case 'x':
            	case 'y':
            	case 'z':
            		{
            	    alt7 = 1;
            	    }
            	    break;
            	case 'A':
            	case 'B':
            	case 'C':
            	case 'D':
            	case 'E':
            	case 'F':
            	case 'G':
            	case 'H':
            	case 'I':
            	case 'J':
            	case 'K':
            	case 'L':
            	case 'M':
            	case 'N':
            	case 'O':
            	case 'P':
            	case 'Q':
            	case 'R':
            	case 'S':
            	case 'T':
            	case 'U':
            	case 'V':
            	case 'W':
            	case 'X':
            	case 'Y':
            	case 'Z':
            		{
            	    alt7 = 2;
            	    }
            	    break;
            	case '_':
            		{
            	    alt7 = 3;
            	    }
            	    break;
            		default:
            		    NoViableAltException nvae_d7s0 =
            		        new NoViableAltException("", 7, 0, input);

            		    throw nvae_d7s0;
            	}

            	switch (alt7) 
            	{
            	    case 1 :
            	        // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:83:12: ( 'a' .. 'z' )
            	        {
            	        	// C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:83:12: ( 'a' .. 'z' )
            	        	// C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:83:13: 'a' .. 'z'
            	        	{
            	        		MatchRange('a','z'); 

            	        	}


            	        }
            	        break;
            	    case 2 :
            	        // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:83:23: ( 'A' .. 'Z' )
            	        {
            	        	// C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:83:23: ( 'A' .. 'Z' )
            	        	// C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:83:24: 'A' .. 'Z'
            	        	{
            	        		MatchRange('A','Z'); 

            	        	}


            	        }
            	        break;
            	    case 3 :
            	        // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:83:34: '_'
            	        {
            	        	Match('_'); 

            	        }
            	        break;

            	}

            	// C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:83:39: ( ( 'a' .. 'z' ) | ( 'A' .. 'Z' ) | '_' | ( '0' .. '9' ) )*
            	do 
            	{
            	    int alt8 = 5;
            	    switch ( input.LA(1) ) 
            	    {
            	    case 'a':
            	    case 'b':
            	    case 'c':
            	    case 'd':
            	    case 'e':
            	    case 'f':
            	    case 'g':
            	    case 'h':
            	    case 'i':
            	    case 'j':
            	    case 'k':
            	    case 'l':
            	    case 'm':
            	    case 'n':
            	    case 'o':
            	    case 'p':
            	    case 'q':
            	    case 'r':
            	    case 's':
            	    case 't':
            	    case 'u':
            	    case 'v':
            	    case 'w':
            	    case 'x':
            	    case 'y':
            	    case 'z':
            	    	{
            	        alt8 = 1;
            	        }
            	        break;
            	    case 'A':
            	    case 'B':
            	    case 'C':
            	    case 'D':
            	    case 'E':
            	    case 'F':
            	    case 'G':
            	    case 'H':
            	    case 'I':
            	    case 'J':
            	    case 'K':
            	    case 'L':
            	    case 'M':
            	    case 'N':
            	    case 'O':
            	    case 'P':
            	    case 'Q':
            	    case 'R':
            	    case 'S':
            	    case 'T':
            	    case 'U':
            	    case 'V':
            	    case 'W':
            	    case 'X':
            	    case 'Y':
            	    case 'Z':
            	    	{
            	        alt8 = 2;
            	        }
            	        break;
            	    case '_':
            	    	{
            	        alt8 = 3;
            	        }
            	        break;
            	    case '0':
            	    case '1':
            	    case '2':
            	    case '3':
            	    case '4':
            	    case '5':
            	    case '6':
            	    case '7':
            	    case '8':
            	    case '9':
            	    	{
            	        alt8 = 4;
            	        }
            	        break;

            	    }

            	    switch (alt8) 
            		{
            			case 1 :
            			    // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:83:40: ( 'a' .. 'z' )
            			    {
            			    	// C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:83:40: ( 'a' .. 'z' )
            			    	// C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:83:41: 'a' .. 'z'
            			    	{
            			    		MatchRange('a','z'); 

            			    	}


            			    }
            			    break;
            			case 2 :
            			    // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:83:51: ( 'A' .. 'Z' )
            			    {
            			    	// C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:83:51: ( 'A' .. 'Z' )
            			    	// C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:83:52: 'A' .. 'Z'
            			    	{
            			    		MatchRange('A','Z'); 

            			    	}


            			    }
            			    break;
            			case 3 :
            			    // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:83:62: '_'
            			    {
            			    	Match('_'); 

            			    }
            			    break;
            			case 4 :
            			    // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:83:67: ( '0' .. '9' )
            			    {
            			    	// C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:83:67: ( '0' .. '9' )
            			    	// C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:83:68: '0' .. '9'
            			    	{
            			    		MatchRange('0','9'); 

            			    	}


            			    }
            			    break;

            			default:
            			    goto loop8;
            	    }
            	} while (true);

            	loop8:
            		;	// Stops C# compiler whining that label 'loop8' has no statements


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "ID"

    // $ANTLR start "WS"
    public void mWS() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = WS;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:86:4: ( ( ' ' | '\\t' | '\\n' | '\\r' )+ )
            // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:86:6: ( ' ' | '\\t' | '\\n' | '\\r' )+
            {
            	// C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:86:6: ( ' ' | '\\t' | '\\n' | '\\r' )+
            	int cnt9 = 0;
            	do 
            	{
            	    int alt9 = 2;
            	    int LA9_0 = input.LA(1);

            	    if ( ((LA9_0 >= '\t' && LA9_0 <= '\n') || LA9_0 == '\r' || LA9_0 == ' ') )
            	    {
            	        alt9 = 1;
            	    }


            	    switch (alt9) 
            		{
            			case 1 :
            			    // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:
            			    {
            			    	if ( (input.LA(1) >= '\t' && input.LA(1) <= '\n') || input.LA(1) == '\r' || input.LA(1) == ' ' ) 
            			    	{
            			    	    input.Consume();

            			    	}
            			    	else 
            			    	{
            			    	    MismatchedSetException mse = new MismatchedSetException(null,input);
            			    	    Recover(mse);
            			    	    throw mse;}


            			    }
            			    break;

            			default:
            			    if ( cnt9 >= 1 ) goto loop9;
            		            EarlyExitException eee9 =
            		                new EarlyExitException(9, input);
            		            throw eee9;
            	    }
            	    cnt9++;
            	} while (true);

            	loop9:
            		;	// Stops C# compiler whining that label 'loop9' has no statements

            	_channel=HIDDEN;

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "WS"

    // $ANTLR start "WORD"
    public void mWORD() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = WORD;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:88:7: ( '.word' )
            // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:88:10: '.word'
            {
            	Match(".word"); 

            	_channel=HIDDEN;

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "WORD"

    // $ANTLR start "COMENTARIOLINHA"
    public void mCOMENTARIOLINHA() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = COMENTARIOLINHA;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:90:16: ( '#' (~ ( '\\n' | '\\r' ) )* ( '\\r' )? '\\n' )
            // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:90:17: '#' (~ ( '\\n' | '\\r' ) )* ( '\\r' )? '\\n'
            {
            	Match('#'); 
            	// C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:90:21: (~ ( '\\n' | '\\r' ) )*
            	do 
            	{
            	    int alt10 = 2;
            	    int LA10_0 = input.LA(1);

            	    if ( ((LA10_0 >= '\u0000' && LA10_0 <= '\t') || (LA10_0 >= '\u000B' && LA10_0 <= '\f') || (LA10_0 >= '\u000E' && LA10_0 <= '\uFFFF')) )
            	    {
            	        alt10 = 1;
            	    }


            	    switch (alt10) 
            		{
            			case 1 :
            			    // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:90:21: ~ ( '\\n' | '\\r' )
            			    {
            			    	if ( (input.LA(1) >= '\u0000' && input.LA(1) <= '\t') || (input.LA(1) >= '\u000B' && input.LA(1) <= '\f') || (input.LA(1) >= '\u000E' && input.LA(1) <= '\uFFFF') ) 
            			    	{
            			    	    input.Consume();

            			    	}
            			    	else 
            			    	{
            			    	    MismatchedSetException mse = new MismatchedSetException(null,input);
            			    	    Recover(mse);
            			    	    throw mse;}


            			    }
            			    break;

            			default:
            			    goto loop10;
            	    }
            	} while (true);

            	loop10:
            		;	// Stops C# compiler whining that label 'loop10' has no statements

            	// C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:90:35: ( '\\r' )?
            	int alt11 = 2;
            	int LA11_0 = input.LA(1);

            	if ( (LA11_0 == '\r') )
            	{
            	    alt11 = 1;
            	}
            	switch (alt11) 
            	{
            	    case 1 :
            	        // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:90:35: '\\r'
            	        {
            	        	Match('\r'); 

            	        }
            	        break;

            	}

            	Match('\n'); 
            	_channel=HIDDEN;

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "COMENTARIOLINHA"

    override public void mTokens() // throws RecognitionException 
    {
        // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:1:8: ( T__72 | T__73 | SECTEXT | SECDATA | ADD | ADDI | SUB | SUBI | LD | LDI | STO | BLE | BGE | BLT | BGT | BEQ | BNE | JMP | HLT | NOT | AND | ANDI | OR | ORI | XOR | XORI | SLL | SRL | STOV | LDV | RETURN | RETINT | CALL | CHAR | INT | INTS | HEX | BIN | CIF | REG | ID | WS | WORD | COMENTARIOLINHA )
        int alt12 = 44;
        alt12 = dfa12.Predict(input);
        switch (alt12) 
        {
            case 1 :
                // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:1:10: T__72
                {
                	mT__72(); 

                }
                break;
            case 2 :
                // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:1:16: T__73
                {
                	mT__73(); 

                }
                break;
            case 3 :
                // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:1:22: SECTEXT
                {
                	mSECTEXT(); 

                }
                break;
            case 4 :
                // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:1:30: SECDATA
                {
                	mSECDATA(); 

                }
                break;
            case 5 :
                // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:1:38: ADD
                {
                	mADD(); 

                }
                break;
            case 6 :
                // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:1:42: ADDI
                {
                	mADDI(); 

                }
                break;
            case 7 :
                // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:1:47: SUB
                {
                	mSUB(); 

                }
                break;
            case 8 :
                // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:1:51: SUBI
                {
                	mSUBI(); 

                }
                break;
            case 9 :
                // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:1:56: LD
                {
                	mLD(); 

                }
                break;
            case 10 :
                // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:1:59: LDI
                {
                	mLDI(); 

                }
                break;
            case 11 :
                // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:1:63: STO
                {
                	mSTO(); 

                }
                break;
            case 12 :
                // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:1:67: BLE
                {
                	mBLE(); 

                }
                break;
            case 13 :
                // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:1:71: BGE
                {
                	mBGE(); 

                }
                break;
            case 14 :
                // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:1:75: BLT
                {
                	mBLT(); 

                }
                break;
            case 15 :
                // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:1:79: BGT
                {
                	mBGT(); 

                }
                break;
            case 16 :
                // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:1:83: BEQ
                {
                	mBEQ(); 

                }
                break;
            case 17 :
                // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:1:87: BNE
                {
                	mBNE(); 

                }
                break;
            case 18 :
                // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:1:91: JMP
                {
                	mJMP(); 

                }
                break;
            case 19 :
                // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:1:95: HLT
                {
                	mHLT(); 

                }
                break;
            case 20 :
                // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:1:99: NOT
                {
                	mNOT(); 

                }
                break;
            case 21 :
                // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:1:103: AND
                {
                	mAND(); 

                }
                break;
            case 22 :
                // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:1:107: ANDI
                {
                	mANDI(); 

                }
                break;
            case 23 :
                // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:1:112: OR
                {
                	mOR(); 

                }
                break;
            case 24 :
                // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:1:115: ORI
                {
                	mORI(); 

                }
                break;
            case 25 :
                // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:1:119: XOR
                {
                	mXOR(); 

                }
                break;
            case 26 :
                // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:1:123: XORI
                {
                	mXORI(); 

                }
                break;
            case 27 :
                // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:1:128: SLL
                {
                	mSLL(); 

                }
                break;
            case 28 :
                // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:1:132: SRL
                {
                	mSRL(); 

                }
                break;
            case 29 :
                // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:1:136: STOV
                {
                	mSTOV(); 

                }
                break;
            case 30 :
                // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:1:141: LDV
                {
                	mLDV(); 

                }
                break;
            case 31 :
                // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:1:145: RETURN
                {
                	mRETURN(); 

                }
                break;
            case 32 :
                // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:1:152: RETINT
                {
                	mRETINT(); 

                }
                break;
            case 33 :
                // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:1:159: CALL
                {
                	mCALL(); 

                }
                break;
            case 34 :
                // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:1:164: CHAR
                {
                	mCHAR(); 

                }
                break;
            case 35 :
                // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:1:169: INT
                {
                	mINT(); 

                }
                break;
            case 36 :
                // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:1:173: INTS
                {
                	mINTS(); 

                }
                break;
            case 37 :
                // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:1:178: HEX
                {
                	mHEX(); 

                }
                break;
            case 38 :
                // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:1:182: BIN
                {
                	mBIN(); 

                }
                break;
            case 39 :
                // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:1:186: CIF
                {
                	mCIF(); 

                }
                break;
            case 40 :
                // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:1:190: REG
                {
                	mREG(); 

                }
                break;
            case 41 :
                // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:1:194: ID
                {
                	mID(); 

                }
                break;
            case 42 :
                // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:1:197: WS
                {
                	mWS(); 

                }
                break;
            case 43 :
                // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:1:200: WORD
                {
                	mWORD(); 

                }
                break;
            case 44 :
                // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:1:205: COMENTARIOLINHA
                {
                	mCOMENTARIOLINHA(); 

                }
                break;

        }

    }


    protected DFA6 dfa6;
    protected DFA12 dfa12;
	private void InitializeCyclicDFAs()
	{
	    this.dfa6 = new DFA6(this);
	    this.dfa12 = new DFA12(this);
	}

    const string DFA6_eotS =
        "\x28\uffff";
    const string DFA6_eofS =
        "\x28\uffff";
    const string DFA6_minS =
        "\x01\x69\x01\x6f\x01\x6d\x01\x6e\x02\uffff\x01\x61\x01\uffff\x02"+
        "\x72\x01\x5f\x01\x72\x01\x74\x01\x30\x01\x5f\x02\uffff\x01\x74\x01"+
        "\x30\x01\x5f\x01\x63\x03\x5f\x01\x63\x02\uffff\x03\x64\x04\uffff"+
        "\x02\x61\x04\uffff";
    const string DFA6_maxS =
        "\x01\x75\x01\x6f\x01\x6d\x01\x6e\x02\uffff\x01\x61\x01\uffff\x02"+
        "\x72\x01\x74\x01\x72\x01\x74\x01\x30\x01\x5f\x02\uffff\x01\x74\x01"+
        "\x31\x01\x5f\x01\x73\x03\x5f\x01\x76\x02\uffff\x01\x73\x02\x64\x04"+
        "\uffff\x02\x69\x04\uffff";
    const string DFA6_acceptS =
        "\x04\uffff\x01\x09\x01\x0b\x01\uffff\x01\x0f\x07\uffff\x01\x0a"+
        "\x01\x0e\x08\uffff\x01\x07\x01\x08\x03\uffff\x01\x05\x01\x06\x01"+
        "\x0c\x01\x0d\x02\uffff\x01\x01\x01\x03\x01\x02\x01\x04";
    const string DFA6_specialS =
        "\x28\uffff}>";
    static readonly string[] DFA6_transitionS = {
            "\x01\x03\x03\uffff\x01\x04\x01\uffff\x01\x07\x01\x01\x02\uffff"+
            "\x01\x05\x01\x02\x01\x06",
            "\x01\x08",
            "\x01\x09",
            "\x01\x0a",
            "",
            "",
            "\x01\x0b",
            "",
            "\x01\x0c",
            "\x01\x0d",
            "\x01\x10\x04\uffff\x01\x0f\x0f\uffff\x01\x0e",
            "\x01\x11",
            "\x01\x12",
            "\x01\x13",
            "\x01\x14",
            "",
            "",
            "\x01\x15",
            "\x01\x16\x01\x17",
            "\x01\x18",
            "\x01\x19\x0f\uffff\x01\x1a",
            "\x01\x1b",
            "\x01\x1c",
            "\x01\x1d",
            "\x01\x1e\x12\uffff\x01\x1f",
            "",
            "",
            "\x01\x20\x0e\uffff\x01\x21",
            "\x01\x22",
            "\x01\x23",
            "",
            "",
            "",
            "",
            "\x01\x25\x07\uffff\x01\x24",
            "\x01\x27\x07\uffff\x01\x26",
            "",
            "",
            "",
            ""
    };

    static readonly short[] DFA6_eot = DFA.UnpackEncodedString(DFA6_eotS);
    static readonly short[] DFA6_eof = DFA.UnpackEncodedString(DFA6_eofS);
    static readonly char[] DFA6_min = DFA.UnpackEncodedStringToUnsignedChars(DFA6_minS);
    static readonly char[] DFA6_max = DFA.UnpackEncodedStringToUnsignedChars(DFA6_maxS);
    static readonly short[] DFA6_accept = DFA.UnpackEncodedString(DFA6_acceptS);
    static readonly short[] DFA6_special = DFA.UnpackEncodedString(DFA6_specialS);
    static readonly short[][] DFA6_transition = DFA.UnpackEncodedStringArray(DFA6_transitionS);

    protected class DFA6 : DFA
    {
        public DFA6(BaseRecognizer recognizer)
        {
            this.recognizer = recognizer;
            this.decisionNumber = 6;
            this.eot = DFA6_eot;
            this.eof = DFA6_eof;
            this.min = DFA6_min;
            this.max = DFA6_max;
            this.accept = DFA6_accept;
            this.special = DFA6_special;
            this.transition = DFA6_transition;

        }

        override public string Description
        {
            get { return "80:10: ( 'port0_dir' | 'port1_dir' | 'port0_data' | 'port1_data' | 'tmr0_config' | 'tmr0_value' | 'int_config' | 'int_status' | 'mcu_config' | 'indr' | 'status' | 'uart_data' | 'uart_status' | 'in_port' | 'out_port' )"; }
        }

    }

    const string DFA12_eotS =
        "\x04\uffff\x0b\x1f\x01\uffff\x01\x12\x03\uffff\x0b\x1f\x06\uffff"+
        "\x0c\x1f\x02\x59\x0e\x1f\x02\x70\x06\x1f\x04\uffff\x02\x79\x02\x7c"+
        "\x02\x7f\x02\u0082\x02\u0083\x02\u0086\x01\uffff\x02\u0087\x02\u0088"+
        "\x01\u0089\x01\u008a\x01\u0089\x01\u008a\x02\u008b\x02\u008c\x02"+
        "\u008d\x02\u008e\x02\u008f\x02\u0090\x02\u0091\x01\uffff\x02\u0092"+
        "\x02\u0093\x04\x1f\x01\uffff\x02\u009c\x01\uffff\x02\u009d\x01\uffff"+
        "\x02\u009e\x02\uffff\x02\u009f\x0e\uffff\x02\u00a0\x04\x1f\x02\u00a5"+
        "\x05\uffff\x04\x1f\x01\uffff\x02\u00aa\x02\u00ab\x02\uffff";
    const string DFA12_eofS =
        "\u00ac\uffff";
    const string DFA12_minS =
        "\x01\x09\x02\uffff\x02\x44\x01\x4c\x01\x44\x01\x45\x01\x4d\x01"+
        "\x4c\x01\x4f\x01\x52\x01\x4f\x01\x45\x01\x41\x01\uffff\x01\x62\x02"+
        "\uffff\x01\x30\x01\x44\x01\x4c\x01\x44\x01\x45\x01\x4d\x01\x4c\x01"+
        "\x4f\x01\x52\x01\x4f\x01\x45\x01\x41\x06\uffff\x04\x44\x01\x4f\x01"+
        "\x4c\x01\x42\x01\x4f\x01\x4c\x01\x42\x02\x4c\x02\x30\x02\x45\x01"+
        "\x51\x02\x45\x01\x51\x02\x45\x02\x50\x04\x54\x02\x30\x02\x52\x02"+
        "\x54\x02\x4c\x04\uffff\x0c\x30\x01\uffff\x16\x30\x01\uffff\x04\x30"+
        "\x02\x49\x02\x4c\x01\uffff\x02\x30\x01\uffff\x02\x30\x01\uffff\x02"+
        "\x30\x02\uffff\x02\x30\x0e\uffff\x02\x30\x01\x4e\x01\x52\x01\x4e"+
        "\x01\x52\x02\x30\x05\uffff\x02\x54\x02\x4e\x01\uffff\x04\x30\x02"+
        "\uffff";
    const string DFA12_maxS =
        "\x01\x7a\x02\uffff\x01\x77\x01\x6e\x01\x75\x01\x64\x01\x6e\x01"+
        "\x6d\x01\x6c\x01\x6f\x01\x72\x01\x6f\x01\x65\x01\x61\x01\uffff\x01"+
        "\x78\x02\uffff\x01\x75\x01\x6e\x01\x75\x01\x64\x01\x6e\x01\x6d\x01"+
        "\x6c\x01\x6f\x01\x72\x01\x6f\x01\x65\x01\x61\x06\uffff\x04\x64\x01"+
        "\x6f\x01\x6c\x01\x62\x01\x6f\x01\x6c\x01\x62\x02\x6c\x02\x7a\x01"+
        "\x74\x01\x65\x01\x71\x01\x74\x01\x65\x01\x71\x02\x74\x02\x70\x04"+
        "\x74\x02\x7a\x02\x72\x02\x74\x02\x6c\x04\uffff\x0c\x7a\x01\uffff"+
        "\x16\x7a\x01\uffff\x04\x7a\x02\x75\x02\x6c\x01\uffff\x02\x7a\x01"+
        "\uffff\x02\x7a\x01\uffff\x02\x7a\x02\uffff\x02\x7a\x0e\uffff\x02"+
        "\x7a\x01\x6e\x01\x72\x01\x6e\x01\x72\x02\x7a\x05\uffff\x02\x74\x02"+
        "\x6e\x01\uffff\x04\x7a\x02\uffff";
    const string DFA12_acceptS =
        "\x01\uffff\x01\x01\x01\x02\x0c\uffff\x01\x22\x01\uffff\x01\x24"+
        "\x01\x23\x0c\uffff\x01\x29\x01\x2a\x01\x2c\x01\x2b\x01\x04\x01\x03"+
        "\x24\uffff\x01\x25\x01\x26\x01\x27\x01\x28\x0c\uffff\x01\x09\x16"+
        "\uffff\x01\x17\x08\uffff\x01\x05\x02\uffff\x01\x15\x02\uffff\x01"+
        "\x0b\x02\uffff\x01\x1b\x01\x07\x02\uffff\x01\x1c\x01\x1e\x01\x0a"+
        "\x01\x0e\x01\x0c\x01\x11\x01\x10\x01\x0d\x01\x0f\x01\x12\x01\x13"+
        "\x01\x14\x01\x18\x01\x19\x08\uffff\x01\x06\x01\x16\x01\x1d\x01\x08"+
        "\x01\x1a\x04\uffff\x01\x21\x04\uffff\x01\x20\x01\x1f";
    const string DFA12_specialS =
        "\u00ac\uffff}>";
    static readonly string[] DFA12_transitionS = {
            "\x02\x20\x02\uffff\x01\x20\x12\uffff\x01\x20\x02\uffff\x01"+
            "\x21\x01\x13\x02\uffff\x01\x0f\x04\uffff\x01\x02\x01\x11\x01"+
            "\x03\x01\uffff\x01\x10\x09\x12\x01\x01\x06\uffff\x01\x14\x01"+
            "\x17\x01\x1e\x04\x1f\x01\x19\x01\x1f\x01\x18\x01\x1f\x01\x16"+
            "\x01\x1f\x01\x1a\x01\x1b\x02\x1f\x01\x1d\x01\x15\x04\x1f\x01"+
            "\x1c\x02\x1f\x04\uffff\x01\x1f\x01\uffff\x01\x04\x01\x07\x01"+
            "\x0e\x04\x1f\x01\x09\x01\x1f\x01\x08\x01\x1f\x01\x06\x01\x1f"+
            "\x01\x0a\x01\x0b\x02\x1f\x01\x0d\x01\x05\x04\x1f\x01\x0c\x02"+
            "\x1f",
            "",
            "",
            "\x01\x23\x0f\uffff\x01\x24\x0f\uffff\x01\x23\x0f\uffff\x01"+
            "\x24\x02\uffff\x01\x22",
            "\x01\x26\x09\uffff\x01\x28\x15\uffff\x01\x25\x09\uffff\x01"+
            "\x27",
            "\x01\x2d\x05\uffff\x01\x30\x01\uffff\x01\x2c\x01\x2e\x16\uffff"+
            "\x01\x2a\x05\uffff\x01\x2f\x01\uffff\x01\x29\x01\x2b",
            "\x01\x32\x1f\uffff\x01\x31",
            "\x01\x38\x01\uffff\x01\x3a\x04\uffff\x01\x36\x01\uffff\x01"+
            "\x37\x16\uffff\x01\x35\x01\uffff\x01\x39\x04\uffff\x01\x33\x01"+
            "\uffff\x01\x34",
            "\x01\x3c\x1f\uffff\x01\x3b",
            "\x01\x3e\x1f\uffff\x01\x3d",
            "\x01\x40\x1f\uffff\x01\x3f",
            "\x01\x42\x1f\uffff\x01\x41",
            "\x01\x44\x1f\uffff\x01\x43",
            "\x01\x46\x1f\uffff\x01\x45",
            "\x01\x48\x1f\uffff\x01\x47",
            "",
            "\x01\x4a\x15\uffff\x01\x49",
            "",
            "",
            "\x0a\x4b\x2f\uffff\x01\x4c\x03\uffff\x01\x4c\x01\uffff\x02"+
            "\x4c\x02\uffff\x03\x4c",
            "\x01\x26\x09\uffff\x01\x28\x15\uffff\x01\x25\x09\uffff\x01"+
            "\x27",
            "\x01\x2d\x05\uffff\x01\x30\x01\uffff\x01\x2c\x01\x2e\x16\uffff"+
            "\x01\x2a\x05\uffff\x01\x2f\x01\uffff\x01\x29\x01\x2b",
            "\x01\x32\x1f\uffff\x01\x31",
            "\x01\x38\x01\uffff\x01\x3a\x04\uffff\x01\x36\x01\uffff\x01"+
            "\x37\x16\uffff\x01\x35\x01\uffff\x01\x39\x04\uffff\x01\x33\x01"+
            "\uffff\x01\x34",
            "\x01\x3c\x1f\uffff\x01\x3b",
            "\x01\x3e\x1f\uffff\x01\x3d",
            "\x01\x40\x1f\uffff\x01\x3f",
            "\x01\x42\x1f\uffff\x01\x41",
            "\x01\x44\x1f\uffff\x01\x43",
            "\x01\x46\x1f\uffff\x01\x45",
            "\x01\x48\x1f\uffff\x01\x47",
            "",
            "",
            "",
            "",
            "",
            "",
            "\x01\x4e\x1f\uffff\x01\x4d",
            "\x01\x4e\x1f\uffff\x01\x4d",
            "\x01\x50\x1f\uffff\x01\x4f",
            "\x01\x50\x1f\uffff\x01\x4f",
            "\x01\x52\x1f\uffff\x01\x51",
            "\x01\x54\x1f\uffff\x01\x53",
            "\x01\x56\x1f\uffff\x01\x55",
            "\x01\x52\x1f\uffff\x01\x51",
            "\x01\x54\x1f\uffff\x01\x53",
            "\x01\x56\x1f\uffff\x01\x55",
            "\x01\x58\x1f\uffff\x01\x57",
            "\x01\x58\x1f\uffff\x01\x57",
            "\x0a\x1f\x07\uffff\x08\x1f\x01\x5d\x0c\x1f\x01\x5b\x04\x1f"+
            "\x04\uffff\x01\x1f\x01\uffff\x08\x1f\x01\x5c\x0c\x1f\x01\x5a"+
            "\x04\x1f",
            "\x0a\x1f\x07\uffff\x08\x1f\x01\x5d\x0c\x1f\x01\x5b\x04\x1f"+
            "\x04\uffff\x01\x1f\x01\uffff\x08\x1f\x01\x5c\x0c\x1f\x01\x5a"+
            "\x04\x1f",
            "\x01\x61\x0e\uffff\x01\x60\x10\uffff\x01\x5f\x0e\uffff\x01"+
            "\x5e",
            "\x01\x63\x1f\uffff\x01\x62",
            "\x01\x65\x1f\uffff\x01\x64",
            "\x01\x61\x0e\uffff\x01\x60\x10\uffff\x01\x5f\x0e\uffff\x01"+
            "\x5e",
            "\x01\x63\x1f\uffff\x01\x62",
            "\x01\x65\x1f\uffff\x01\x64",
            "\x01\x67\x0e\uffff\x01\x69\x10\uffff\x01\x66\x0e\uffff\x01"+
            "\x68",
            "\x01\x67\x0e\uffff\x01\x69\x10\uffff\x01\x66\x0e\uffff\x01"+
            "\x68",
            "\x01\x6b\x1f\uffff\x01\x6a",
            "\x01\x6b\x1f\uffff\x01\x6a",
            "\x01\x6d\x1f\uffff\x01\x6c",
            "\x01\x6d\x1f\uffff\x01\x6c",
            "\x01\x6f\x1f\uffff\x01\x6e",
            "\x01\x6f\x1f\uffff\x01\x6e",
            "\x0a\x1f\x07\uffff\x08\x1f\x01\x72\x11\x1f\x04\uffff\x01\x1f"+
            "\x01\uffff\x08\x1f\x01\x71\x11\x1f",
            "\x0a\x1f\x07\uffff\x08\x1f\x01\x72\x11\x1f\x04\uffff\x01\x1f"+
            "\x01\uffff\x08\x1f\x01\x71\x11\x1f",
            "\x01\x74\x1f\uffff\x01\x73",
            "\x01\x74\x1f\uffff\x01\x73",
            "\x01\x76\x1f\uffff\x01\x75",
            "\x01\x76\x1f\uffff\x01\x75",
            "\x01\x78\x1f\uffff\x01\x77",
            "\x01\x78\x1f\uffff\x01\x77",
            "",
            "",
            "",
            "",
            "\x0a\x1f\x07\uffff\x08\x1f\x01\x7b\x11\x1f\x04\uffff\x01\x1f"+
            "\x01\uffff\x08\x1f\x01\x7a\x11\x1f",
            "\x0a\x1f\x07\uffff\x08\x1f\x01\x7b\x11\x1f\x04\uffff\x01\x1f"+
            "\x01\uffff\x08\x1f\x01\x7a\x11\x1f",
            "\x0a\x1f\x07\uffff\x08\x1f\x01\x7e\x11\x1f\x04\uffff\x01\x1f"+
            "\x01\uffff\x08\x1f\x01\x7d\x11\x1f",
            "\x0a\x1f\x07\uffff\x08\x1f\x01\x7e\x11\x1f\x04\uffff\x01\x1f"+
            "\x01\uffff\x08\x1f\x01\x7d\x11\x1f",
            "\x0a\x1f\x07\uffff\x15\x1f\x01\u0081\x04\x1f\x04\uffff\x01"+
            "\x1f\x01\uffff\x15\x1f\x01\u0080\x04\x1f",
            "\x0a\x1f\x07\uffff\x15\x1f\x01\u0081\x04\x1f\x04\uffff\x01"+
            "\x1f\x01\uffff\x15\x1f\x01\u0080\x04\x1f",
            "\x0a\x1f\x07\uffff\x1a\x1f\x04\uffff\x01\x1f\x01\uffff\x1a"+
            "\x1f",
            "\x0a\x1f\x07\uffff\x1a\x1f\x04\uffff\x01\x1f\x01\uffff\x1a"+
            "\x1f",
            "\x0a\x1f\x07\uffff\x08\x1f\x01\u0085\x11\x1f\x04\uffff\x01"+
            "\x1f\x01\uffff\x08\x1f\x01\u0084\x11\x1f",
            "\x0a\x1f\x07\uffff\x08\x1f\x01\u0085\x11\x1f\x04\uffff\x01"+
            "\x1f\x01\uffff\x08\x1f\x01\u0084\x11\x1f",
            "\x0a\x1f\x07\uffff\x1a\x1f\x04\uffff\x01\x1f\x01\uffff\x1a"+
            "\x1f",
            "\x0a\x1f\x07\uffff\x1a\x1f\x04\uffff\x01\x1f\x01\uffff\x1a"+
            "\x1f",
            "",
            "\x0a\x1f\x07\uffff\x1a\x1f\x04\uffff\x01\x1f\x01\uffff\x1a"+
            "\x1f",
            "\x0a\x1f\x07\uffff\x1a\x1f\x04\uffff\x01\x1f\x01\uffff\x1a"+
            "\x1f",
            "\x0a\x1f\x07\uffff\x1a\x1f\x04\uffff\x01\x1f\x01\uffff\x1a"+
            "\x1f",
            "\x0a\x1f\x07\uffff\x1a\x1f\x04\uffff\x01\x1f\x01\uffff\x1a"+
            "\x1f",
            "\x0a\x1f\x07\uffff\x1a\x1f\x04\uffff\x01\x1f\x01\uffff\x1a"+
            "\x1f",
            "\x0a\x1f\x07\uffff\x1a\x1f\x04\uffff\x01\x1f\x01\uffff\x1a"+
            "\x1f",
            "\x0a\x1f\x07\uffff\x1a\x1f\x04\uffff\x01\x1f\x01\uffff\x1a"+
            "\x1f",
            "\x0a\x1f\x07\uffff\x1a\x1f\x04\uffff\x01\x1f\x01\uffff\x1a"+
            "\x1f",
            "\x0a\x1f\x07\uffff\x1a\x1f\x04\uffff\x01\x1f\x01\uffff\x1a"+
            "\x1f",
            "\x0a\x1f\x07\uffff\x1a\x1f\x04\uffff\x01\x1f\x01\uffff\x1a"+
            "\x1f",
            "\x0a\x1f\x07\uffff\x1a\x1f\x04\uffff\x01\x1f\x01\uffff\x1a"+
            "\x1f",
            "\x0a\x1f\x07\uffff\x1a\x1f\x04\uffff\x01\x1f\x01\uffff\x1a"+
            "\x1f",
            "\x0a\x1f\x07\uffff\x1a\x1f\x04\uffff\x01\x1f\x01\uffff\x1a"+
            "\x1f",
            "\x0a\x1f\x07\uffff\x1a\x1f\x04\uffff\x01\x1f\x01\uffff\x1a"+
            "\x1f",
            "\x0a\x1f\x07\uffff\x1a\x1f\x04\uffff\x01\x1f\x01\uffff\x1a"+
            "\x1f",
            "\x0a\x1f\x07\uffff\x1a\x1f\x04\uffff\x01\x1f\x01\uffff\x1a"+
            "\x1f",
            "\x0a\x1f\x07\uffff\x1a\x1f\x04\uffff\x01\x1f\x01\uffff\x1a"+
            "\x1f",
            "\x0a\x1f\x07\uffff\x1a\x1f\x04\uffff\x01\x1f\x01\uffff\x1a"+
            "\x1f",
            "\x0a\x1f\x07\uffff\x1a\x1f\x04\uffff\x01\x1f\x01\uffff\x1a"+
            "\x1f",
            "\x0a\x1f\x07\uffff\x1a\x1f\x04\uffff\x01\x1f\x01\uffff\x1a"+
            "\x1f",
            "\x0a\x1f\x07\uffff\x1a\x1f\x04\uffff\x01\x1f\x01\uffff\x1a"+
            "\x1f",
            "\x0a\x1f\x07\uffff\x1a\x1f\x04\uffff\x01\x1f\x01\uffff\x1a"+
            "\x1f",
            "",
            "\x0a\x1f\x07\uffff\x1a\x1f\x04\uffff\x01\x1f\x01\uffff\x1a"+
            "\x1f",
            "\x0a\x1f\x07\uffff\x1a\x1f\x04\uffff\x01\x1f\x01\uffff\x1a"+
            "\x1f",
            "\x0a\x1f\x07\uffff\x08\x1f\x01\u0095\x11\x1f\x04\uffff\x01"+
            "\x1f\x01\uffff\x08\x1f\x01\u0094\x11\x1f",
            "\x0a\x1f\x07\uffff\x08\x1f\x01\u0095\x11\x1f\x04\uffff\x01"+
            "\x1f\x01\uffff\x08\x1f\x01\u0094\x11\x1f",
            "\x01\u0098\x0b\uffff\x01\u0099\x13\uffff\x01\u0096\x0b\uffff"+
            "\x01\u0097",
            "\x01\u0098\x0b\uffff\x01\u0099\x13\uffff\x01\u0096\x0b\uffff"+
            "\x01\u0097",
            "\x01\u009b\x1f\uffff\x01\u009a",
            "\x01\u009b\x1f\uffff\x01\u009a",
            "",
            "\x0a\x1f\x07\uffff\x1a\x1f\x04\uffff\x01\x1f\x01\uffff\x1a"+
            "\x1f",
            "\x0a\x1f\x07\uffff\x1a\x1f\x04\uffff\x01\x1f\x01\uffff\x1a"+
            "\x1f",
            "",
            "\x0a\x1f\x07\uffff\x1a\x1f\x04\uffff\x01\x1f\x01\uffff\x1a"+
            "\x1f",
            "\x0a\x1f\x07\uffff\x1a\x1f\x04\uffff\x01\x1f\x01\uffff\x1a"+
            "\x1f",
            "",
            "\x0a\x1f\x07\uffff\x1a\x1f\x04\uffff\x01\x1f\x01\uffff\x1a"+
            "\x1f",
            "\x0a\x1f\x07\uffff\x1a\x1f\x04\uffff\x01\x1f\x01\uffff\x1a"+
            "\x1f",
            "",
            "",
            "\x0a\x1f\x07\uffff\x1a\x1f\x04\uffff\x01\x1f\x01\uffff\x1a"+
            "\x1f",
            "\x0a\x1f\x07\uffff\x1a\x1f\x04\uffff\x01\x1f\x01\uffff\x1a"+
            "\x1f",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "\x0a\x1f\x07\uffff\x1a\x1f\x04\uffff\x01\x1f\x01\uffff\x1a"+
            "\x1f",
            "\x0a\x1f\x07\uffff\x1a\x1f\x04\uffff\x01\x1f\x01\uffff\x1a"+
            "\x1f",
            "\x01\u00a2\x1f\uffff\x01\u00a1",
            "\x01\u00a4\x1f\uffff\x01\u00a3",
            "\x01\u00a2\x1f\uffff\x01\u00a1",
            "\x01\u00a4\x1f\uffff\x01\u00a3",
            "\x0a\x1f\x07\uffff\x1a\x1f\x04\uffff\x01\x1f\x01\uffff\x1a"+
            "\x1f",
            "\x0a\x1f\x07\uffff\x1a\x1f\x04\uffff\x01\x1f\x01\uffff\x1a"+
            "\x1f",
            "",
            "",
            "",
            "",
            "",
            "\x01\u00a7\x1f\uffff\x01\u00a6",
            "\x01\u00a7\x1f\uffff\x01\u00a6",
            "\x01\u00a9\x1f\uffff\x01\u00a8",
            "\x01\u00a9\x1f\uffff\x01\u00a8",
            "",
            "\x0a\x1f\x07\uffff\x1a\x1f\x04\uffff\x01\x1f\x01\uffff\x1a"+
            "\x1f",
            "\x0a\x1f\x07\uffff\x1a\x1f\x04\uffff\x01\x1f\x01\uffff\x1a"+
            "\x1f",
            "\x0a\x1f\x07\uffff\x1a\x1f\x04\uffff\x01\x1f\x01\uffff\x1a"+
            "\x1f",
            "\x0a\x1f\x07\uffff\x1a\x1f\x04\uffff\x01\x1f\x01\uffff\x1a"+
            "\x1f",
            "",
            ""
    };

    static readonly short[] DFA12_eot = DFA.UnpackEncodedString(DFA12_eotS);
    static readonly short[] DFA12_eof = DFA.UnpackEncodedString(DFA12_eofS);
    static readonly char[] DFA12_min = DFA.UnpackEncodedStringToUnsignedChars(DFA12_minS);
    static readonly char[] DFA12_max = DFA.UnpackEncodedStringToUnsignedChars(DFA12_maxS);
    static readonly short[] DFA12_accept = DFA.UnpackEncodedString(DFA12_acceptS);
    static readonly short[] DFA12_special = DFA.UnpackEncodedString(DFA12_specialS);
    static readonly short[][] DFA12_transition = DFA.UnpackEncodedStringArray(DFA12_transitionS);

    protected class DFA12 : DFA
    {
        public DFA12(BaseRecognizer recognizer)
        {
            this.recognizer = recognizer;
            this.decisionNumber = 12;
            this.eot = DFA12_eot;
            this.eof = DFA12_eof;
            this.min = DFA12_min;
            this.max = DFA12_max;
            this.accept = DFA12_accept;
            this.special = DFA12_special;
            this.transition = DFA12_transition;

        }

        override public string Description
        {
            get { return "1:1: Tokens : ( T__72 | T__73 | SECTEXT | SECDATA | ADD | ADDI | SUB | SUBI | LD | LDI | STO | BLE | BGE | BLT | BGT | BEQ | BNE | JMP | HLT | NOT | AND | ANDI | OR | ORI | XOR | XORI | SLL | SRL | STOV | LDV | RETURN | RETINT | CALL | CHAR | INT | INTS | HEX | BIN | CIF | REG | ID | WS | WORD | COMENTARIOLINHA );"; }
        }

    }

 
    
}
