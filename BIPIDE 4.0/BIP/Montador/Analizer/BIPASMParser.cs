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

using IDictionary	= System.Collections.IDictionary;
using Hashtable 	= System.Collections.Hashtable;
namespace BIP.Montador.Analizer
{
public partial class BIPASMParser : Parser
{
    public static readonly string[] tokenNames = new string[] 
	{
        "<invalid>", 
		"<EOR>", 
		"<DOWN>", 
		"<UP>", 
		"A", 
		"B", 
		"C", 
		"D", 
		"E", 
		"F", 
		"G", 
		"H", 
		"I", 
		"J", 
		"K", 
		"L", 
		"M", 
		"N", 
		"O", 
		"P", 
		"Q", 
		"R", 
		"S", 
		"T", 
		"U", 
		"V", 
		"W", 
		"X", 
		"Y", 
		"Z", 
		"SECTEXT", 
		"SECDATA", 
		"ADD", 
		"ADDI", 
		"SUB", 
		"SUBI", 
		"LD", 
		"LDI", 
		"STO", 
		"BLE", 
		"BGE", 
		"BLT", 
		"BGT", 
		"BEQ", 
		"BNE", 
		"JMP", 
		"HLT", 
		"NOT", 
		"AND", 
		"ANDI", 
		"OR", 
		"ORI", 
		"XOR", 
		"XORI", 
		"SLL", 
		"SRL", 
		"STOV", 
		"LDV", 
		"RETURN", 
		"RETINT", 
		"CALL", 
		"CHAR", 
		"INT", 
		"INTS", 
		"HEX", 
		"BIN", 
		"CIF", 
		"REG", 
		"ID", 
		"WS", 
		"WORD", 
		"COMENTARIOLINHA", 
		"':'", 
		"','"
    };

    public const int XORI = 53;
    public const int ADDI = 33;
    public const int CHAR = 61;
    public const int HLT = 46;
    public const int SUB = 34;
    public const int SLL = 54;
    public const int NOT = 47;
    public const int ID = 68;
    public const int AND = 48;
    public const int EOF = -1;
    public const int ORI = 51;
    public const int WORD = 70;
    public const int HEX = 64;
    public const int COMENTARIOLINHA = 71;
    public const int RETURN = 58;
    public const int JMP = 45;
    public const int BGT = 42;
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
    public const int INT = 62;
    public const int Q = 20;
    public const int LDI = 37;
    public const int P = 19;
    public const int S = 22;
    public const int R = 21;
    public const int SECDATA = 31;
    public const int REG = 67;
    public const int STOV = 56;
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
    public const int BIN = 65;
    public const int BEQ = 43;
    public const int T__73 = 73;
    public const int BNE = 44;
    public const int LD = 36;
    public const int RETINT = 59;

    // delegates
    // delegators



        public BIPASMParser(ITokenStream input)
    		: this(input, new RecognizerSharedState()) {
        }

        public BIPASMParser(ITokenStream input, RecognizerSharedState state)
    		: base(input, state) {
            InitializeCyclicDFAs();
            this.state.ruleMemo = new Hashtable[59+1];
             
             
        }
        

    override public string[] TokenNames {
		get { return BIPASMParser.tokenNames; }
    }

    override public string GrammarFileName {
		get { return "C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g"; }
    }



    // $ANTLR start "programa"
    // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:92:1: programa : sectionData sectionText ;
    public void programa() // throws RecognitionException [1]
    {   
        int programa_StartIndex = input.Index();
        try 
    	{
    	    if ( (state.backtracking > 0) && AlreadyParsedRule(input, 1) ) 
    	    {
    	    	return ; 
    	    }
            // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:92:10: ( sectionData sectionText )
            // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:92:12: sectionData sectionText
            {
            	PushFollow(FOLLOW_sectionData_in_programa1036);
            	sectionData();
            	state.followingStackPointer--;
            	if (state.failed) return ;
            	PushFollow(FOLLOW_sectionText_in_programa1038);
            	sectionText();
            	state.followingStackPointer--;
            	if (state.failed) return ;

            }

        }
         catch (RecognitionException e) { throw e; }    finally 
    	{
            if ( state.backtracking > 0 ) 
            {
            	Memoize(input, 1, programa_StartIndex); 
            }
        }
        return ;
    }
    // $ANTLR end "programa"


    // $ANTLR start "sectionData"
    // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:94:1: sectionData : ( SECDATA lista_decl | );
    public void sectionData() // throws RecognitionException [1]
    {   
        int sectionData_StartIndex = input.Index();
         paraphrases.Push("Erro na seção .data");  
        try 
    	{
    	    if ( (state.backtracking > 0) && AlreadyParsedRule(input, 2) ) 
    	    {
    	    	return ; 
    	    }
            // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:96:3: ( SECDATA lista_decl | )
            int alt1 = 2;
            int LA1_0 = input.LA(1);

            if ( (LA1_0 == SECDATA) )
            {
                alt1 = 1;
            }
            else if ( (LA1_0 == SECTEXT) )
            {
                alt1 = 2;
            }
            else 
            {
                if ( state.backtracking > 0 ) {state.failed = true; return ;}
                NoViableAltException nvae_d1s0 =
                    new NoViableAltException("", 1, 0, input);

                throw nvae_d1s0;
            }
            switch (alt1) 
            {
                case 1 :
                    // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:96:5: SECDATA lista_decl
                    {
                    	Match(input,SECDATA,FOLLOW_SECDATA_in_sectionData1061); if (state.failed) return ;
                    	PushFollow(FOLLOW_lista_decl_in_sectionData1063);
                    	lista_decl();
                    	state.followingStackPointer--;
                    	if (state.failed) return ;

                    }
                    break;
                case 2 :
                    // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:96:26: 
                    {
                    }
                    break;

            }
            if ( (state.backtracking==0) )
            {
               paraphrases.Pop();  
            }
        }
         catch (RecognitionException e) { throw e; }    finally 
    	{
            if ( state.backtracking > 0 ) 
            {
            	Memoize(input, 2, sectionData_StartIndex); 
            }
        }
        return ;
    }
    // $ANTLR end "sectionData"


    // $ANTLR start "lista_decl"
    // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:98:1: lista_decl : ( decl )+ ;
    public void lista_decl() // throws RecognitionException [1]
    {   
        int lista_decl_StartIndex = input.Index();
        try 
    	{
    	    if ( (state.backtracking > 0) && AlreadyParsedRule(input, 3) ) 
    	    {
    	    	return ; 
    	    }
            // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:98:12: ( ( decl )+ )
            // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:98:14: ( decl )+
            {
            	// C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:98:14: ( decl )+
            	int cnt2 = 0;
            	do 
            	{
            	    int alt2 = 2;
            	    int LA2_0 = input.LA(1);

            	    if ( (LA2_0 == ID) )
            	    {
            	        alt2 = 1;
            	    }


            	    switch (alt2) 
            		{
            			case 1 :
            			    // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:0:0: decl
            			    {
            			    	PushFollow(FOLLOW_decl_in_lista_decl1074);
            			    	decl();
            			    	state.followingStackPointer--;
            			    	if (state.failed) return ;

            			    }
            			    break;

            			default:
            			    if ( cnt2 >= 1 ) goto loop2;
            			    if ( state.backtracking > 0 ) {state.failed = true; return ;}
            		            EarlyExitException eee2 =
            		                new EarlyExitException(2, input);
            		            throw eee2;
            	    }
            	    cnt2++;
            	} while (true);

            	loop2:
            		;	// Stops C# compiler whining that label 'loop2' has no statements


            }

        }
         catch (RecognitionException e) { throw e; }    finally 
    	{
            if ( state.backtracking > 0 ) 
            {
            	Memoize(input, 3, lista_decl_StartIndex); 
            }
        }
        return ;
    }
    // $ANTLR end "lista_decl"


    // $ANTLR start "decl"
    // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:100:1: decl : ID ':' valvar ;
    public void decl() // throws RecognitionException [1]
    {   
        int decl_StartIndex = input.Index();
        IToken ID1 = null;
        BIPASMParser.valvar_return valvar2 = default(BIPASMParser.valvar_return);


         paraphrases.Push("Erro na declaração de variável");  
        try 
    	{
    	    if ( (state.backtracking > 0) && AlreadyParsedRule(input, 4) ) 
    	    {
    	    	return ; 
    	    }
            // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:102:3: ( ID ':' valvar )
            // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:102:5: ID ':' valvar
            {
            	ID1=(IToken)Match(input,ID,FOLLOW_ID_in_decl1102); if (state.failed) return ;
            	Match(input,72,FOLLOW_72_in_decl1104); if (state.failed) return ;
            	if ( (state.backtracking==0) )
            	{
            	  LimpaVetor();
            	}
            	PushFollow(FOLLOW_valvar_in_decl1108);
            	valvar2 = valvar();
            	state.followingStackPointer--;
            	if (state.failed) return ;
            	if ( (state.backtracking==0) )
            	{
            	  AddVariavel(((ID1 != null) ? ID1.Text : null), ((valvar2 != null) ? input.ToString((IToken)(valvar2.Start),(IToken)(valvar2.Stop)) : null).Replace(",",""));
            	}

            }

            if ( (state.backtracking==0) )
            {
               paraphrases.Pop();  
            }
        }
         catch (RecognitionException e) { throw e; }    finally 
    	{
            if ( state.backtracking > 0 ) 
            {
            	Memoize(input, 4, decl_StartIndex); 
            }
        }
        return ;
    }
    // $ANTLR end "decl"

    public class valvar_return : ParserRuleReturnScope
    {
    };

    // $ANTLR start "valvar"
    // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:103:1: valvar : ( oper_vint | oper_vint ',' valvar );
    public BIPASMParser.valvar_return valvar() // throws RecognitionException [1]
    {   
        BIPASMParser.valvar_return retval = new BIPASMParser.valvar_return();
        retval.Start = input.LT(1);
        int valvar_StartIndex = input.Index();
        BIPASMParser.oper_vint_return oper_vint3 = default(BIPASMParser.oper_vint_return);

        BIPASMParser.oper_vint_return oper_vint4 = default(BIPASMParser.oper_vint_return);


        try 
    	{
    	    if ( (state.backtracking > 0) && AlreadyParsedRule(input, 5) ) 
    	    {
    	    	return retval; 
    	    }
            // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:103:8: ( oper_vint | oper_vint ',' valvar )
            int alt3 = 2;
            switch ( input.LA(1) ) 
            {
            case INT:
            	{
                int LA3_1 = input.LA(2);

                if ( (LA3_1 == EOF || LA3_1 == SECTEXT || LA3_1 == ID) )
                {
                    alt3 = 1;
                }
                else if ( (LA3_1 == 73) )
                {
                    alt3 = 2;
                }
                else 
                {
                    if ( state.backtracking > 0 ) {state.failed = true; return retval;}
                    NoViableAltException nvae_d3s1 =
                        new NoViableAltException("", 3, 1, input);

                    throw nvae_d3s1;
                }
                }
                break;
            case INTS:
            	{
                int LA3_2 = input.LA(2);

                if ( (LA3_2 == 73) )
                {
                    alt3 = 2;
                }
                else if ( (LA3_2 == EOF || LA3_2 == SECTEXT || LA3_2 == ID) )
                {
                    alt3 = 1;
                }
                else 
                {
                    if ( state.backtracking > 0 ) {state.failed = true; return retval;}
                    NoViableAltException nvae_d3s2 =
                        new NoViableAltException("", 3, 2, input);

                    throw nvae_d3s2;
                }
                }
                break;
            case HEX:
            	{
                int LA3_3 = input.LA(2);

                if ( (LA3_3 == EOF || LA3_3 == SECTEXT || LA3_3 == ID) )
                {
                    alt3 = 1;
                }
                else if ( (LA3_3 == 73) )
                {
                    alt3 = 2;
                }
                else 
                {
                    if ( state.backtracking > 0 ) {state.failed = true; return retval;}
                    NoViableAltException nvae_d3s3 =
                        new NoViableAltException("", 3, 3, input);

                    throw nvae_d3s3;
                }
                }
                break;
            case BIN:
            	{
                int LA3_4 = input.LA(2);

                if ( (LA3_4 == 73) )
                {
                    alt3 = 2;
                }
                else if ( (LA3_4 == EOF || LA3_4 == SECTEXT || LA3_4 == ID) )
                {
                    alt3 = 1;
                }
                else 
                {
                    if ( state.backtracking > 0 ) {state.failed = true; return retval;}
                    NoViableAltException nvae_d3s4 =
                        new NoViableAltException("", 3, 4, input);

                    throw nvae_d3s4;
                }
                }
                break;
            case CHAR:
            	{
                int LA3_5 = input.LA(2);

                if ( (LA3_5 == 73) )
                {
                    alt3 = 2;
                }
                else if ( (LA3_5 == EOF || LA3_5 == SECTEXT || LA3_5 == ID) )
                {
                    alt3 = 1;
                }
                else 
                {
                    if ( state.backtracking > 0 ) {state.failed = true; return retval;}
                    NoViableAltException nvae_d3s5 =
                        new NoViableAltException("", 3, 5, input);

                    throw nvae_d3s5;
                }
                }
                break;
            	default:
            	    if ( state.backtracking > 0 ) {state.failed = true; return retval;}
            	    NoViableAltException nvae_d3s0 =
            	        new NoViableAltException("", 3, 0, input);

            	    throw nvae_d3s0;
            }

            switch (alt3) 
            {
                case 1 :
                    // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:103:10: oper_vint
                    {
                    	PushFollow(FOLLOW_oper_vint_in_valvar1117);
                    	oper_vint3 = oper_vint();
                    	state.followingStackPointer--;
                    	if (state.failed) return retval;
                    	if ( (state.backtracking==0) )
                    	{
                    	  AddItemVetor(((oper_vint3 != null) ? input.ToString((IToken)(oper_vint3.Start),(IToken)(oper_vint3.Stop)) : null));
                    	}

                    }
                    break;
                case 2 :
                    // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:103:55: oper_vint ',' valvar
                    {
                    	PushFollow(FOLLOW_oper_vint_in_valvar1123);
                    	oper_vint4 = oper_vint();
                    	state.followingStackPointer--;
                    	if (state.failed) return retval;
                    	if ( (state.backtracking==0) )
                    	{
                    	  AddItemVetor(((oper_vint4 != null) ? input.ToString((IToken)(oper_vint4.Start),(IToken)(oper_vint4.Stop)) : null));
                    	}
                    	Match(input,73,FOLLOW_73_in_valvar1126); if (state.failed) return retval;
                    	PushFollow(FOLLOW_valvar_in_valvar1128);
                    	valvar();
                    	state.followingStackPointer--;
                    	if (state.failed) return retval;

                    }
                    break;

            }
            retval.Stop = input.LT(-1);

        }
         catch (RecognitionException e) { throw e; }    finally 
    	{
            if ( state.backtracking > 0 ) 
            {
            	Memoize(input, 5, valvar_StartIndex); 
            }
        }
        return retval;
    }
    // $ANTLR end "valvar"

    public class inst_vint_return : ParserRuleReturnScope
    {
    };

    // $ANTLR start "inst_vint"
    // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:105:1: inst_vint : ( ADDI | SUBI | LDI | ANDI | ORI | XORI | SLL | SRL );
    public BIPASMParser.inst_vint_return inst_vint() // throws RecognitionException [1]
    {   
        BIPASMParser.inst_vint_return retval = new BIPASMParser.inst_vint_return();
        retval.Start = input.LT(1);
        int inst_vint_StartIndex = input.Index();
        try 
    	{
    	    if ( (state.backtracking > 0) && AlreadyParsedRule(input, 6) ) 
    	    {
    	    	return retval; 
    	    }
            // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:105:11: ( ADDI | SUBI | LDI | ANDI | ORI | XORI | SLL | SRL )
            // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:
            {
            	if ( input.LA(1) == ADDI || input.LA(1) == SUBI || input.LA(1) == LDI || input.LA(1) == ANDI || input.LA(1) == ORI || (input.LA(1) >= XORI && input.LA(1) <= SRL) ) 
            	{
            	    input.Consume();
            	    state.errorRecovery = false;state.failed = false;
            	}
            	else 
            	{
            	    if ( state.backtracking > 0 ) {state.failed = true; return retval;}
            	    MismatchedSetException mse = new MismatchedSetException(null,input);
            	    throw mse;
            	}


            }

            retval.Stop = input.LT(-1);

        }
         catch (RecognitionException e) { throw e; }    finally 
    	{
            if ( state.backtracking > 0 ) 
            {
            	Memoize(input, 6, inst_vint_StartIndex); 
            }
        }
        return retval;
    }
    // $ANTLR end "inst_vint"

    public class inst_reg_return : ParserRuleReturnScope
    {
    };

    // $ANTLR start "inst_reg"
    // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:106:1: inst_reg : ( ADD | SUB | LD | STO | AND | OR | XOR | STOV | LDV );
    public BIPASMParser.inst_reg_return inst_reg() // throws RecognitionException [1]
    {   
        BIPASMParser.inst_reg_return retval = new BIPASMParser.inst_reg_return();
        retval.Start = input.LT(1);
        int inst_reg_StartIndex = input.Index();
        try 
    	{
    	    if ( (state.backtracking > 0) && AlreadyParsedRule(input, 7) ) 
    	    {
    	    	return retval; 
    	    }
            // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:106:10: ( ADD | SUB | LD | STO | AND | OR | XOR | STOV | LDV )
            // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:
            {
            	if ( input.LA(1) == ADD || input.LA(1) == SUB || input.LA(1) == LD || input.LA(1) == STO || input.LA(1) == AND || input.LA(1) == OR || input.LA(1) == XOR || (input.LA(1) >= STOV && input.LA(1) <= LDV) ) 
            	{
            	    input.Consume();
            	    state.errorRecovery = false;state.failed = false;
            	}
            	else 
            	{
            	    if ( state.backtracking > 0 ) {state.failed = true; return retval;}
            	    MismatchedSetException mse = new MismatchedSetException(null,input);
            	    throw mse;
            	}


            }

            retval.Stop = input.LT(-1);

        }
         catch (RecognitionException e) { throw e; }    finally 
    	{
            if ( state.backtracking > 0 ) 
            {
            	Memoize(input, 7, inst_reg_StartIndex); 
            }
        }
        return retval;
    }
    // $ANTLR end "inst_reg"

    public class inst_label_return : ParserRuleReturnScope
    {
    };

    // $ANTLR start "inst_label"
    // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:107:1: inst_label : ( BEQ | BNE | BLE | BLT | BGE | BGT | JMP | CALL );
    public BIPASMParser.inst_label_return inst_label() // throws RecognitionException [1]
    {   
        BIPASMParser.inst_label_return retval = new BIPASMParser.inst_label_return();
        retval.Start = input.LT(1);
        int inst_label_StartIndex = input.Index();
        try 
    	{
    	    if ( (state.backtracking > 0) && AlreadyParsedRule(input, 8) ) 
    	    {
    	    	return retval; 
    	    }
            // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:107:13: ( BEQ | BNE | BLE | BLT | BGE | BGT | JMP | CALL )
            // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:
            {
            	if ( (input.LA(1) >= BLE && input.LA(1) <= JMP) || input.LA(1) == CALL ) 
            	{
            	    input.Consume();
            	    state.errorRecovery = false;state.failed = false;
            	}
            	else 
            	{
            	    if ( state.backtracking > 0 ) {state.failed = true; return retval;}
            	    MismatchedSetException mse = new MismatchedSetException(null,input);
            	    throw mse;
            	}


            }

            retval.Stop = input.LT(-1);

        }
         catch (RecognitionException e) { throw e; }    finally 
    	{
            if ( state.backtracking > 0 ) 
            {
            	Memoize(input, 8, inst_label_StartIndex); 
            }
        }
        return retval;
    }
    // $ANTLR end "inst_label"

    public class inst_nulo_return : ParserRuleReturnScope
    {
    };

    // $ANTLR start "inst_nulo"
    // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:108:1: inst_nulo : ( HLT | NOT | RETURN | RETINT );
    public BIPASMParser.inst_nulo_return inst_nulo() // throws RecognitionException [1]
    {   
        BIPASMParser.inst_nulo_return retval = new BIPASMParser.inst_nulo_return();
        retval.Start = input.LT(1);
        int inst_nulo_StartIndex = input.Index();
        try 
    	{
    	    if ( (state.backtracking > 0) && AlreadyParsedRule(input, 9) ) 
    	    {
    	    	return retval; 
    	    }
            // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:108:11: ( HLT | NOT | RETURN | RETINT )
            // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:
            {
            	if ( (input.LA(1) >= HLT && input.LA(1) <= NOT) || (input.LA(1) >= RETURN && input.LA(1) <= RETINT) ) 
            	{
            	    input.Consume();
            	    state.errorRecovery = false;state.failed = false;
            	}
            	else 
            	{
            	    if ( state.backtracking > 0 ) {state.failed = true; return retval;}
            	    MismatchedSetException mse = new MismatchedSetException(null,input);
            	    throw mse;
            	}


            }

            retval.Stop = input.LT(-1);

        }
         catch (RecognitionException e) { throw e; }    finally 
    	{
            if ( state.backtracking > 0 ) 
            {
            	Memoize(input, 9, inst_nulo_StartIndex); 
            }
        }
        return retval;
    }
    // $ANTLR end "inst_nulo"

    public class oper_vint_return : ParserRuleReturnScope
    {
    };

    // $ANTLR start "oper_vint"
    // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:110:1: oper_vint : ( INT | INTS | HEX | BIN | CHAR );
    public BIPASMParser.oper_vint_return oper_vint() // throws RecognitionException [1]
    {   
        BIPASMParser.oper_vint_return retval = new BIPASMParser.oper_vint_return();
        retval.Start = input.LT(1);
        int oper_vint_StartIndex = input.Index();
        try 
    	{
    	    if ( (state.backtracking > 0) && AlreadyParsedRule(input, 10) ) 
    	    {
    	    	return retval; 
    	    }
            // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:110:11: ( INT | INTS | HEX | BIN | CHAR )
            int alt4 = 5;
            switch ( input.LA(1) ) 
            {
            case INT:
            	{
                alt4 = 1;
                }
                break;
            case INTS:
            	{
                alt4 = 2;
                }
                break;
            case HEX:
            	{
                alt4 = 3;
                }
                break;
            case BIN:
            	{
                alt4 = 4;
                }
                break;
            case CHAR:
            	{
                alt4 = 5;
                }
                break;
            	default:
            	    if ( state.backtracking > 0 ) {state.failed = true; return retval;}
            	    NoViableAltException nvae_d4s0 =
            	        new NoViableAltException("", 4, 0, input);

            	    throw nvae_d4s0;
            }

            switch (alt4) 
            {
                case 1 :
                    // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:110:13: INT
                    {
                    	Match(input,INT,FOLLOW_INT_in_oper_vint1269); if (state.failed) return retval;
                    	if ( (state.backtracking==0) )
                    	{
                    	  strTipoOperando = "INT";
                    	}

                    }
                    break;
                case 2 :
                    // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:110:45: INTS
                    {
                    	Match(input,INTS,FOLLOW_INTS_in_oper_vint1274); if (state.failed) return retval;
                    	if ( (state.backtracking==0) )
                    	{
                    	  strTipoOperando = "INT";
                    	}

                    }
                    break;
                case 3 :
                    // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:110:78: HEX
                    {
                    	Match(input,HEX,FOLLOW_HEX_in_oper_vint1279); if (state.failed) return retval;
                    	if ( (state.backtracking==0) )
                    	{
                    	  strTipoOperando = "HEX";
                    	}

                    }
                    break;
                case 4 :
                    // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:110:111: BIN
                    {
                    	Match(input,BIN,FOLLOW_BIN_in_oper_vint1285); if (state.failed) return retval;
                    	if ( (state.backtracking==0) )
                    	{
                    	  strTipoOperando = "BIN";
                    	}

                    }
                    break;
                case 5 :
                    // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:110:144: CHAR
                    {
                    	Match(input,CHAR,FOLLOW_CHAR_in_oper_vint1291); if (state.failed) return retval;
                    	if ( (state.backtracking==0) )
                    	{
                    	  strTipoOperando = "CHAR";
                    	}

                    }
                    break;

            }
            retval.Stop = input.LT(-1);

        }
         catch (RecognitionException e) { throw e; }    finally 
    	{
            if ( state.backtracking > 0 ) 
            {
            	Memoize(input, 10, oper_vint_StartIndex); 
            }
        }
        return retval;
    }
    // $ANTLR end "oper_vint"

    public class oper_reg_return : ParserRuleReturnScope
    {
    };

    // $ANTLR start "oper_reg"
    // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:111:1: oper_reg : ( INT | HEX | ID | REG | CIF );
    public BIPASMParser.oper_reg_return oper_reg() // throws RecognitionException [1]
    {   
        BIPASMParser.oper_reg_return retval = new BIPASMParser.oper_reg_return();
        retval.Start = input.LT(1);
        int oper_reg_StartIndex = input.Index();
        try 
    	{
    	    if ( (state.backtracking > 0) && AlreadyParsedRule(input, 11) ) 
    	    {
    	    	return retval; 
    	    }
            // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:111:10: ( INT | HEX | ID | REG | CIF )
            int alt5 = 5;
            switch ( input.LA(1) ) 
            {
            case INT:
            	{
                alt5 = 1;
                }
                break;
            case HEX:
            	{
                alt5 = 2;
                }
                break;
            case ID:
            	{
                alt5 = 3;
                }
                break;
            case REG:
            	{
                alt5 = 4;
                }
                break;
            case CIF:
            	{
                alt5 = 5;
                }
                break;
            	default:
            	    if ( state.backtracking > 0 ) {state.failed = true; return retval;}
            	    NoViableAltException nvae_d5s0 =
            	        new NoViableAltException("", 5, 0, input);

            	    throw nvae_d5s0;
            }

            switch (alt5) 
            {
                case 1 :
                    // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:111:12: INT
                    {
                    	Match(input,INT,FOLLOW_INT_in_oper_reg1300); if (state.failed) return retval;
                    	if ( (state.backtracking==0) )
                    	{
                    	  strTipoOperando = "INT";
                    	}

                    }
                    break;
                case 2 :
                    // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:111:44: HEX
                    {
                    	Match(input,HEX,FOLLOW_HEX_in_oper_reg1305); if (state.failed) return retval;
                    	if ( (state.backtracking==0) )
                    	{
                    	  strTipoOperando = "HEX";
                    	}

                    }
                    break;
                case 3 :
                    // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:111:77: ID
                    {
                    	Match(input,ID,FOLLOW_ID_in_oper_reg1311); if (state.failed) return retval;
                    	if ( (state.backtracking==0) )
                    	{
                    	  strTipoOperando = "VAR";
                    	}

                    }
                    break;
                case 4 :
                    // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:111:108: REG
                    {
                    	Match(input,REG,FOLLOW_REG_in_oper_reg1316); if (state.failed) return retval;
                    	if ( (state.backtracking==0) )
                    	{
                    	  strTipoOperando = "REG";
                    	}

                    }
                    break;
                case 5 :
                    // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:111:141: CIF
                    {
                    	Match(input,CIF,FOLLOW_CIF_in_oper_reg1322); if (state.failed) return retval;
                    	if ( (state.backtracking==0) )
                    	{
                    	  strTipoOperando = "CIF";
                    	}

                    }
                    break;

            }
            retval.Stop = input.LT(-1);

        }
         catch (RecognitionException e) { throw e; }    finally 
    	{
            if ( state.backtracking > 0 ) 
            {
            	Memoize(input, 11, oper_reg_StartIndex); 
            }
        }
        return retval;
    }
    // $ANTLR end "oper_reg"

    public class oper_label_return : ParserRuleReturnScope
    {
    };

    // $ANTLR start "oper_label"
    // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:112:1: oper_label : ( INT | ID );
    public BIPASMParser.oper_label_return oper_label() // throws RecognitionException [1]
    {   
        BIPASMParser.oper_label_return retval = new BIPASMParser.oper_label_return();
        retval.Start = input.LT(1);
        int oper_label_StartIndex = input.Index();
        try 
    	{
    	    if ( (state.backtracking > 0) && AlreadyParsedRule(input, 12) ) 
    	    {
    	    	return retval; 
    	    }
            // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:112:12: ( INT | ID )
            int alt6 = 2;
            int LA6_0 = input.LA(1);

            if ( (LA6_0 == INT) )
            {
                alt6 = 1;
            }
            else if ( (LA6_0 == ID) )
            {
                alt6 = 2;
            }
            else 
            {
                if ( state.backtracking > 0 ) {state.failed = true; return retval;}
                NoViableAltException nvae_d6s0 =
                    new NoViableAltException("", 6, 0, input);

                throw nvae_d6s0;
            }
            switch (alt6) 
            {
                case 1 :
                    // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:112:14: INT
                    {
                    	Match(input,INT,FOLLOW_INT_in_oper_label1331); if (state.failed) return retval;
                    	if ( (state.backtracking==0) )
                    	{
                    	  strTipoOperando = "INT";
                    	}

                    }
                    break;
                case 2 :
                    // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:112:46: ID
                    {
                    	Match(input,ID,FOLLOW_ID_in_oper_label1336); if (state.failed) return retval;
                    	if ( (state.backtracking==0) )
                    	{
                    	  strTipoOperando = "ROTULO";
                    	}

                    }
                    break;

            }
            retval.Stop = input.LT(-1);

        }
         catch (RecognitionException e) { throw e; }    finally 
    	{
            if ( state.backtracking > 0 ) 
            {
            	Memoize(input, 12, oper_label_StartIndex); 
            }
        }
        return retval;
    }
    // $ANTLR end "oper_label"

    public class oper_nulo_return : ParserRuleReturnScope
    {
    };

    // $ANTLR start "oper_nulo"
    // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:113:1: oper_nulo : ( INT | );
    public BIPASMParser.oper_nulo_return oper_nulo() // throws RecognitionException [1]
    {   
        BIPASMParser.oper_nulo_return retval = new BIPASMParser.oper_nulo_return();
        retval.Start = input.LT(1);
        int oper_nulo_StartIndex = input.Index();
        try 
    	{
    	    if ( (state.backtracking > 0) && AlreadyParsedRule(input, 13) ) 
    	    {
    	    	return retval; 
    	    }
            // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:113:11: ( INT | )
            int alt7 = 2;
            int LA7_0 = input.LA(1);

            if ( (LA7_0 == INT) )
            {
                alt7 = 1;
            }
            else if ( (LA7_0 == EOF || (LA7_0 >= ADD && LA7_0 <= CALL) || LA7_0 == ID) )
            {
                alt7 = 2;
            }
            else 
            {
                if ( state.backtracking > 0 ) {state.failed = true; return retval;}
                NoViableAltException nvae_d7s0 =
                    new NoViableAltException("", 7, 0, input);

                throw nvae_d7s0;
            }
            switch (alt7) 
            {
                case 1 :
                    // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:113:13: INT
                    {
                    	Match(input,INT,FOLLOW_INT_in_oper_nulo1346); if (state.failed) return retval;
                    	if ( (state.backtracking==0) )
                    	{
                    	  strTipoOperando = "INT";
                    	}

                    }
                    break;
                case 2 :
                    // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:113:45: 
                    {
                    }
                    break;

            }
            retval.Stop = input.LT(-1);

        }
         catch (RecognitionException e) { throw e; }    finally 
    	{
            if ( state.backtracking > 0 ) 
            {
            	Memoize(input, 13, oper_nulo_StartIndex); 
            }
        }
        return retval;
    }
    // $ANTLR end "oper_nulo"


    // $ANTLR start "sectionText"
    // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:115:1: sectionText : SECTEXT lista_cmdo ;
    public void sectionText() // throws RecognitionException [1]
    {   
        int sectionText_StartIndex = input.Index();
        try 
    	{
    	    if ( (state.backtracking > 0) && AlreadyParsedRule(input, 14) ) 
    	    {
    	    	return ; 
    	    }
            // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:115:17: ( SECTEXT lista_cmdo )
            // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:115:19: SECTEXT lista_cmdo
            {
            	Match(input,SECTEXT,FOLLOW_SECTEXT_in_sectionText1362); if (state.failed) return ;
            	PushFollow(FOLLOW_lista_cmdo_in_sectionText1364);
            	lista_cmdo();
            	state.followingStackPointer--;
            	if (state.failed) return ;

            }

        }
         catch (RecognitionException e) { throw e; }    finally 
    	{
            if ( state.backtracking > 0 ) 
            {
            	Memoize(input, 14, sectionText_StartIndex); 
            }
        }
        return ;
    }
    // $ANTLR end "sectionText"


    // $ANTLR start "lista_cmdo"
    // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:117:1: lista_cmdo : ( cmdo )+ ;
    public void lista_cmdo() // throws RecognitionException [1]
    {   
        int lista_cmdo_StartIndex = input.Index();
        try 
    	{
    	    if ( (state.backtracking > 0) && AlreadyParsedRule(input, 15) ) 
    	    {
    	    	return ; 
    	    }
            // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:117:12: ( ( cmdo )+ )
            // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:117:14: ( cmdo )+
            {
            	// C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:117:14: ( cmdo )+
            	int cnt8 = 0;
            	do 
            	{
            	    int alt8 = 2;
            	    int LA8_0 = input.LA(1);

            	    if ( ((LA8_0 >= ADD && LA8_0 <= CALL) || LA8_0 == ID) )
            	    {
            	        alt8 = 1;
            	    }


            	    switch (alt8) 
            		{
            			case 1 :
            			    // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:0:0: cmdo
            			    {
            			    	PushFollow(FOLLOW_cmdo_in_lista_cmdo1373);
            			    	cmdo();
            			    	state.followingStackPointer--;
            			    	if (state.failed) return ;

            			    }
            			    break;

            			default:
            			    if ( cnt8 >= 1 ) goto loop8;
            			    if ( state.backtracking > 0 ) {state.failed = true; return ;}
            		            EarlyExitException eee8 =
            		                new EarlyExitException(8, input);
            		            throw eee8;
            	    }
            	    cnt8++;
            	} while (true);

            	loop8:
            		;	// Stops C# compiler whining that label 'loop8' has no statements


            }

        }
         catch (RecognitionException e) { throw e; }    finally 
    	{
            if ( state.backtracking > 0 ) 
            {
            	Memoize(input, 15, lista_cmdo_StartIndex); 
            }
        }
        return ;
    }
    // $ANTLR end "lista_cmdo"


    // $ANTLR start "cmdo"
    // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:119:1: cmdo : ( inst_vint oper_vint | inst_reg oper_reg | inst_label oper_label | inst_nulo oper_nulo | ID ':' );
    public void cmdo() // throws RecognitionException [1]
    {   
        int cmdo_StartIndex = input.Index();
        IToken ID13 = null;
        BIPASMParser.inst_vint_return inst_vint5 = default(BIPASMParser.inst_vint_return);

        BIPASMParser.oper_vint_return oper_vint6 = default(BIPASMParser.oper_vint_return);

        BIPASMParser.inst_reg_return inst_reg7 = default(BIPASMParser.inst_reg_return);

        BIPASMParser.oper_reg_return oper_reg8 = default(BIPASMParser.oper_reg_return);

        BIPASMParser.inst_label_return inst_label9 = default(BIPASMParser.inst_label_return);

        BIPASMParser.oper_label_return oper_label10 = default(BIPASMParser.oper_label_return);

        BIPASMParser.inst_nulo_return inst_nulo11 = default(BIPASMParser.inst_nulo_return);

        BIPASMParser.oper_nulo_return oper_nulo12 = default(BIPASMParser.oper_nulo_return);


         paraphrases.Push("Erro no comando");  
        try 
    	{
    	    if ( (state.backtracking > 0) && AlreadyParsedRule(input, 16) ) 
    	    {
    	    	return ; 
    	    }
            // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:121:3: ( inst_vint oper_vint | inst_reg oper_reg | inst_label oper_label | inst_nulo oper_nulo | ID ':' )
            int alt9 = 5;
            switch ( input.LA(1) ) 
            {
            case ADDI:
            case SUBI:
            case LDI:
            case ANDI:
            case ORI:
            case XORI:
            case SLL:
            case SRL:
            	{
                alt9 = 1;
                }
                break;
            case ADD:
            case SUB:
            case LD:
            case STO:
            case AND:
            case OR:
            case XOR:
            case STOV:
            case LDV:
            	{
                alt9 = 2;
                }
                break;
            case BLE:
            case BGE:
            case BLT:
            case BGT:
            case BEQ:
            case BNE:
            case JMP:
            case CALL:
            	{
                alt9 = 3;
                }
                break;
            case HLT:
            case NOT:
            case RETURN:
            case RETINT:
            	{
                alt9 = 4;
                }
                break;
            case ID:
            	{
                alt9 = 5;
                }
                break;
            	default:
            	    if ( state.backtracking > 0 ) {state.failed = true; return ;}
            	    NoViableAltException nvae_d9s0 =
            	        new NoViableAltException("", 9, 0, input);

            	    throw nvae_d9s0;
            }

            switch (alt9) 
            {
                case 1 :
                    // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:121:5: inst_vint oper_vint
                    {
                    	PushFollow(FOLLOW_inst_vint_in_cmdo1402);
                    	inst_vint5 = inst_vint();
                    	state.followingStackPointer--;
                    	if (state.failed) return ;
                    	PushFollow(FOLLOW_oper_vint_in_cmdo1405);
                    	oper_vint6 = oper_vint();
                    	state.followingStackPointer--;
                    	if (state.failed) return ;
                    	if ( (state.backtracking==0) )
                    	{
                    	  this.AddInstrucao(((inst_vint5 != null) ? input.ToString((IToken)(inst_vint5.Start),(IToken)(inst_vint5.Stop)) : null), ((oper_vint6 != null) ? input.ToString((IToken)(oper_vint6.Start),(IToken)(oper_vint6.Stop)) : null));
                    	}

                    }
                    break;
                case 2 :
                    // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:122:5: inst_reg oper_reg
                    {
                    	PushFollow(FOLLOW_inst_reg_in_cmdo1414);
                    	inst_reg7 = inst_reg();
                    	state.followingStackPointer--;
                    	if (state.failed) return ;
                    	PushFollow(FOLLOW_oper_reg_in_cmdo1418);
                    	oper_reg8 = oper_reg();
                    	state.followingStackPointer--;
                    	if (state.failed) return ;
                    	if ( (state.backtracking==0) )
                    	{
                    	  this.AddInstrucao(((inst_reg7 != null) ? input.ToString((IToken)(inst_reg7.Start),(IToken)(inst_reg7.Stop)) : null),  ((oper_reg8 != null) ? input.ToString((IToken)(oper_reg8.Start),(IToken)(oper_reg8.Stop)) : null));
                    	}

                    }
                    break;
                case 3 :
                    // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:123:5: inst_label oper_label
                    {
                    	PushFollow(FOLLOW_inst_label_in_cmdo1427);
                    	inst_label9 = inst_label();
                    	state.followingStackPointer--;
                    	if (state.failed) return ;
                    	PushFollow(FOLLOW_oper_label_in_cmdo1429);
                    	oper_label10 = oper_label();
                    	state.followingStackPointer--;
                    	if (state.failed) return ;
                    	if ( (state.backtracking==0) )
                    	{
                    	  this.AddInstrucao(((inst_label9 != null) ? input.ToString((IToken)(inst_label9.Start),(IToken)(inst_label9.Stop)) : null),((oper_label10 != null) ? input.ToString((IToken)(oper_label10.Start),(IToken)(oper_label10.Stop)) : null));
                    	}

                    }
                    break;
                case 4 :
                    // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:124:5: inst_nulo oper_nulo
                    {
                    	PushFollow(FOLLOW_inst_nulo_in_cmdo1437);
                    	inst_nulo11 = inst_nulo();
                    	state.followingStackPointer--;
                    	if (state.failed) return ;
                    	PushFollow(FOLLOW_oper_nulo_in_cmdo1440);
                    	oper_nulo12 = oper_nulo();
                    	state.followingStackPointer--;
                    	if (state.failed) return ;
                    	if ( (state.backtracking==0) )
                    	{
                    	  this.AddInstrucao(((inst_nulo11 != null) ? input.ToString((IToken)(inst_nulo11.Start),(IToken)(inst_nulo11.Stop)) : null), ((oper_nulo12 != null) ? input.ToString((IToken)(oper_nulo12.Start),(IToken)(oper_nulo12.Stop)) : null));
                    	}

                    }
                    break;
                case 5 :
                    // C:\\Users\\Paulo\\Desktop\\Montador uBIP\\antlr\\BIPASM.g:125:5: ID ':'
                    {
                    	ID13=(IToken)Match(input,ID,FOLLOW_ID_in_cmdo1449); if (state.failed) return ;
                    	Match(input,72,FOLLOW_72_in_cmdo1450); if (state.failed) return ;
                    	if ( (state.backtracking==0) )
                    	{
                    	  this.AddRotulo(((ID13 != null) ? ID13.Text : null));
                    	}

                    }
                    break;

            }
            if ( (state.backtracking==0) )
            {
               paraphrases.Pop();  
            }
        }
         catch (RecognitionException e) { throw e; }    finally 
    	{
            if ( state.backtracking > 0 ) 
            {
            	Memoize(input, 16, cmdo_StartIndex); 
            }
        }
        return ;
    }
    // $ANTLR end "cmdo"

    // Delegated rules


	private void InitializeCyclicDFAs()
	{
	}

 

    public static readonly BitSet FOLLOW_sectionData_in_programa1036 = new BitSet(new ulong[]{0x0000000040000000UL});
    public static readonly BitSet FOLLOW_sectionText_in_programa1038 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_SECDATA_in_sectionData1061 = new BitSet(new ulong[]{0x0000000000000000UL,0x0000000000000010UL});
    public static readonly BitSet FOLLOW_lista_decl_in_sectionData1063 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_decl_in_lista_decl1074 = new BitSet(new ulong[]{0x0000000000000002UL,0x0000000000000010UL});
    public static readonly BitSet FOLLOW_ID_in_decl1102 = new BitSet(new ulong[]{0x0000000000000000UL,0x0000000000000100UL});
    public static readonly BitSet FOLLOW_72_in_decl1104 = new BitSet(new ulong[]{0xE000000000000000UL,0x0000000000000003UL});
    public static readonly BitSet FOLLOW_valvar_in_decl1108 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_oper_vint_in_valvar1117 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_oper_vint_in_valvar1123 = new BitSet(new ulong[]{0x0000000000000000UL,0x0000000000000200UL});
    public static readonly BitSet FOLLOW_73_in_valvar1126 = new BitSet(new ulong[]{0xE000000000000000UL,0x0000000000000003UL});
    public static readonly BitSet FOLLOW_valvar_in_valvar1128 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_set_in_inst_vint0 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_set_in_inst_reg0 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_set_in_inst_label0 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_set_in_inst_nulo0 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_INT_in_oper_vint1269 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_INTS_in_oper_vint1274 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_HEX_in_oper_vint1279 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_BIN_in_oper_vint1285 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_CHAR_in_oper_vint1291 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_INT_in_oper_reg1300 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_HEX_in_oper_reg1305 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_ID_in_oper_reg1311 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_REG_in_oper_reg1316 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_CIF_in_oper_reg1322 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_INT_in_oper_label1331 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_ID_in_oper_label1336 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_INT_in_oper_nulo1346 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_SECTEXT_in_sectionText1362 = new BitSet(new ulong[]{0x1FFFFFFF00000000UL,0x0000000000000010UL});
    public static readonly BitSet FOLLOW_lista_cmdo_in_sectionText1364 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_cmdo_in_lista_cmdo1373 = new BitSet(new ulong[]{0x1FFFFFFF00000002UL,0x0000000000000010UL});
    public static readonly BitSet FOLLOW_inst_vint_in_cmdo1402 = new BitSet(new ulong[]{0xE000000000000000UL,0x0000000000000003UL});
    public static readonly BitSet FOLLOW_oper_vint_in_cmdo1405 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_inst_reg_in_cmdo1414 = new BitSet(new ulong[]{0x4000000000000000UL,0x000000000000001DUL});
    public static readonly BitSet FOLLOW_oper_reg_in_cmdo1418 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_inst_label_in_cmdo1427 = new BitSet(new ulong[]{0x4000000000000000UL,0x0000000000000010UL});
    public static readonly BitSet FOLLOW_oper_label_in_cmdo1429 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_inst_nulo_in_cmdo1437 = new BitSet(new ulong[]{0x4000000000000000UL});
    public static readonly BitSet FOLLOW_oper_nulo_in_cmdo1440 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_ID_in_cmdo1449 = new BitSet(new ulong[]{0x0000000000000000UL,0x0000000000000100UL});
    public static readonly BitSet FOLLOW_72_in_cmdo1450 = new BitSet(new ulong[]{0x0000000000000002UL});

}
}