using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Antlr.Runtime;
using BIP.Montador.Analizer;
using System.IO;

namespace BIP.Montador
{
    public class Montador
    {
        #region PROPRIEDADES
        /// <summary>Lista de Erros do Ultimo Código Montado</summary>
        public List<Erro> ListaErros = new List<Erro>();
        /// <summary>Resultado do código montado</summary>
        public Programa Programa = new Programa();
        /// <summary>Configuracoes do Montador</summary>
        public Configuracoes Configuracoes = new Configuracoes();
        /// <summary>
        /// Indica que ocorreu um erro durante a última análise, isto pode impedir o funcionamento de outros metodos.
        /// </summary>
        public bool HasError
        {
            get
            {
                return this.ListaErros.Where(w => w.Tipo == Erro.eTipo.Error).Count() > 0;
            }
        }

        #endregion
        //============================================================
        #region METODOS
        /// <summary>
        /// Executa o montador
        /// </summary>
        /// <param name="strCodigo">Codigo Fonte ASM</param>
        /// <returns></returns>
        public bool Executa(string strCodigo)
        {
            //strCodigo = strCodigo.ToLower();
            try
            {
                InstrASM.StartNewAnaliser();
                Variavel.StartNewAnaliser();

                ListaErros.Clear();
                ICharStream input = new ANTLRStringStream(strCodigo);
                BIPASMLexer lex = new BIPASMLexer(input);
                CommonTokenStream tokens = new CommonTokenStream(lex);
                BIPASMParser parser = new BIPASMParser(tokens);
                //Mesagens de Erros
                lex.MessageRequest += new BIPASMLexer.HandlerMsgRequest(lexparser_MessageRequest);
                parser.MessageRequest += new BIPASMParser.HandlerMsgRequest(lexparser_MessageRequest);
                //Inicia análise
                parser.programa();
                //Atualiza objeto do Resultado
                this.Programa = parser.Programa;

                this.CheckVariaveis();
                this.CheckRotulos();
                this.CheckOperandos();

                return !this.HasError;
            }
            catch (RecognitionException e)
            {
                ListaErros.Add(new Erro(e.Line, "Comando não encontrado ou inválido: " + e.Token.Text, Erro.eTipo.Error));
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
        //---------------------------------------------------------------------
        private void lexparser_MessageRequest(string _msg, int _linha)
        {
            this.ListaErros.Add(new Erro(_linha, _msg, Erro.eTipo.Error));
        }
        //-----------------------------------------------------------
        /// <summary>
        /// Verifica a validade das variaveis e atualiza os enderecos auxiliares das instrucoes com variaveis como ADD
        /// </summary>
        private void CheckVariaveis()
        {
            
            if (this.Programa.Variaveis.Count == 0) return;

            //Armazena as variaveis Utilizadas
            List<string> VariaveisUtilizadas = new List<string>();

            //Indica se houve o uso de uma variavel não declarada
            bool blnGeraExcecao = false;

            //Verifica se existe variaveis declaradas com o mesmo nome
            foreach (Variavel item in this.Programa.Variaveis)
            {
                List<Variavel> varIguais = this.Programa.Variaveis.Where(w => w.Nome.ToLower() == item.Nome.ToLower()).ToList();
                if (varIguais.Count() > 1) 
                {
                    ListaErros.Add(new Erro(item.EndArquivo, "Variável " + item.Nome+ " já declarada!", Erro.eTipo.Error));
                    blnGeraExcecao = true;
                }
            }

            //Checa se há variaveis usadas não declaradas
            foreach (InstrASM item in this.Programa.Instrucoes)
            {
                if (item.TipoOperando == eOperando.VAR)
                {
                    try
                    {
                        var variavel = this.Programa.Variaveis.Where(w => w.Nome.ToLower() == item.Operando.ToLower()).Single();
                        VariaveisUtilizadas.Add(item.Operando.ToLower());
                    }
                    catch
                    {//Se lançar um exceção, então não existe a variavel utilizada.
                        ListaErros.Add(new Erro(item.EndArquivo, "Variável " + item.Operando + " não declarada!", Erro.eTipo.Error) );
                        blnGeraExcecao = true;
                    }
                }
            }

            foreach (BIP.Montador.Variavel objVar in this.Programa.Variaveis)
            {
                string aux = "0";
                for (int x = 1; x < objVar.TamanhoVariavel; x++)
                {
                    aux += ",0";

                }
                objVar.Operando = aux;
            }

            if (blnGeraExcecao)
                throw new Exception("Variável não declarada!");

            List<Variavel> lstVarParaRemover = new List<Variavel>();
            //Checa se há variaveis declaradas e não usadas
            foreach (Variavel item in this.Programa.Variaveis)
            {
                if (!VariaveisUtilizadas.Contains(item.Nome.ToLower()))
                {
                    ListaErros.Add(new Erro(item.EndArquivo, "Variável " + item.Nome + " declarada mas não utilizada.", Erro.eTipo.Warning));
                    //Remove a variavel da lista
                    if (this.Configuracoes.RemoverVariaveisNaoUtilizadas)
                    {
                        lstVarParaRemover.Add(item);
                    }
                }
            }
            //Remove as Marcadas para remover
            foreach (Variavel item in lstVarParaRemover)
            {
                this.Programa.Variaveis.Remove(item);
            }

            //Reordena as posições das variaveis na memoria
            if (this.Configuracoes.RemoverVariaveisNaoUtilizadas)
            {
                int intCont = Arquitetura.PrimeiraPosicaoMemDados;
                foreach (Variavel item in this.Programa.Variaveis)
                {
                    item.EndMemoria = intCont;
                    intCont = intCont + (item.TamanhoVariavel * Arquitetura.QtdWordVariavel); //*2 Comparar com o ArchC
                }
            }

            //Adiciona o endereco da variavel nas instrucoes que utilizam-as
            foreach (InstrASM item in this.Programa.Instrucoes)
            {
                if (item.TipoOperando == eOperando.VAR)
                {
                    try
                    {
                        var variavel = this.Programa.Variaveis.Where(w => w.Nome.ToLower() == item.Operando.ToLower()).Single();
                        item.EndRotVar = variavel.EndMemoria;
                    }
                    catch
                    {
                    }
                }
            }
        }
        //---------------------------------------------------------------------
        /// <summary>
        /// Verifica a validade dos rotulos e atualiza os enderecos auxiliares das instrucoes com rotulos como BEQ.
        /// </summary>
        //---------------------------------------------------------------------
        private void CheckRotulos()
        {
            if (this.Programa.Instrucoes.Count == 0) return;

            //Busca todas os rotulos
            List<InstrASM> Rotulos = this.Programa.Instrucoes.Where(w => w.Instrucao == eInstrucao.Rotulo).ToList();
            //Armazena os rotulos utilizados
            List<string> RotulosUtilizadas = new List<string>();


            //Indica se houve o uso de um rotulo não declarado
            bool blnGeraExcecao = false;

            //Verifica se existe rotulos declarados com o mesmo nome
            foreach (InstrASM item in Rotulos)
            {
                List<InstrASM> rotIguais = Rotulos.Where(w => w.Auxiliar.ToUpper() == item.Auxiliar.ToUpper()).ToList();
                if (rotIguais.Count() > 1)
                {
                    ListaErros.Add(new Erro(item.EndArquivo, "Rótulo " + item.Auxiliar + " já declarado!", Erro.eTipo.Error));
                    blnGeraExcecao = true;
                }
            }

            //Checa se há rotulos usados não declarados
            foreach (InstrASM item in this.Programa.Instrucoes)
            {
                if (item.TipoOperando == eOperando.ROTULO)
                {
                    try
                    {
                        var rotulo = Rotulos.Where(w => w.Auxiliar.ToUpper() == item.Operando.ToUpper()).Single();
                        RotulosUtilizadas.Add(item.Operando.ToUpper());
                    }
                    catch
                    {//Se lançar um exceção, então não existe o rotulo utilizado.
                        ListaErros.Add(new Erro(item.EndArquivo, "Rótulo " + item.Operando + " não declarado!",Erro.eTipo.Error));
                        blnGeraExcecao = true;
                    }
                }
            }

            if (blnGeraExcecao)
                throw new Exception("Rótulo não declarado!");

            //Checa se há rotulos declarados e não usados
            foreach (InstrASM item in Rotulos)
            {
                if (!RotulosUtilizadas.Contains(item.Auxiliar.ToUpper()))
                {
                    ListaErros.Add(new Erro(item.EndArquivo, "Rótulo " + item.Auxiliar + " declarado mas não utilizado.", Erro.eTipo.Warning));
                    //Remove o rotulo da lista
                    this.Programa.Instrucoes.Remove(item);
                }
            }

            //Adiciona o endereco do rotulo nas instrucoes que utilizam, ex. BEQ, JMP...
            foreach (InstrASM item in this.Programa.Instrucoes)
            {
                if (item.TipoOperando == eOperando.ROTULO)
                {
                    try
                    {
                        var rotulo = Rotulos.Where(w => w.Auxiliar.ToUpper() == item.Operando.ToUpper()).Single();
                        item.EndRotVar = rotulo.EndMemoria;
                    }
                    catch
                    {
                    }
                }
            }
        }
        /// <summary>
        /// Verifica se os valores atribuidos aos operandos são válidos
        /// </summary>
        private void CheckOperandos()
        {
            foreach (InstrASM item in this.Programa.Instrucoes)
            {
                if ((item.TipoOperando == eOperando.BIN) ||
                    (item.TipoOperando == eOperando.CIF) ||
                    (item.TipoOperando == eOperando.HEX) ||
                    (item.TipoOperando == eOperando.INT))
                {
                    //Verifica se o valor apos convertido para binário permanece o mesmo
                    int intValor = item.GetOperandoToInt();
                    string strBin = Funcoes.IntToBin(intValor, BIP.Montador.Arquitetura.BitsCampoOperando);
                    int intConvertido = Funcoes.BinToInt(strBin, Arquitetura.GetTipoOperando(item.Instrucao) == eTipoOperando.Imediato);
                    if (intValor != intConvertido)
                    {
                        this.ListaErros.Add(new Erro(item.EndArquivo, "Valor do operando (" + item.Operando + ") fora da faixa permitida. (Imediatos: -1024 a 1023; Endereços: 0 a 2047)", Erro.eTipo.Error));
                        throw new Exception("Valor fora do permitido");
                    }
                }
                else
                    if (item.TipoOperando == eOperando.REG)
                    {
                        item.EndRotVar = BIP.Montador.Arquitetura.GetEndRegistrador(item.Operando);
                    }
            }
        }
        //---------------------------------------------------------------------
        public eArquitetura GetArquiteturaSuportada()
        {
            eArquitetura arquituraSuportada = eArquitetura.BIP_I;
            foreach (InstrASM instrucao in this.Programa.Instrucoes)
            {
                if ((instrucao.GetClasseInstrucao() == eClasseInstrucao.Manipulacao_de_Vetor) ||
                    (instrucao.GetClasseInstrucao() == eClasseInstrucao.Suporte_a_Procedimentos))
                    arquituraSuportada = eArquitetura.BIP_IV;
                else
                    if ((instrucao.GetClasseInstrucao() == eClasseInstrucao.Deslocamento_Logico) ||
                        (instrucao.GetClasseInstrucao() == eClasseInstrucao.Logica_Booleana))
                        arquituraSuportada = eArquitetura.BIP_III;
                    if (((instrucao.GetClasseInstrucao() == eClasseInstrucao.Desvio_Condicional) ||
                         (instrucao.GetClasseInstrucao() == eClasseInstrucao.Desvio_Incondicional)) &&
                         (arquituraSuportada != eArquitetura.uBIP))
                        arquituraSuportada = eArquitetura.BIP_II;
            }
            return arquituraSuportada;
        }
        //---------------------------------------------------------------------
        /// <summary>
        /// Converte a memória de programa para o formato da ROM do modelo VHDL
        /// </summary>
        /// <param name="strPakageName">Nome do Pacote</param>
        /// <param name="blnIncludeSourceAsComment">Inclui o código ASM como comentário alinhado do arquivo VHDL.</param>
        public string GetVHDLROM(string strPakageName, bool blnIncludeSourceAsComment)
        {
            //Se há erro, retorna uma string vazia
            if (this.HasError) return "";

            StringBuilder sbInstr = new StringBuilder();
            int intInstrucoes = 0;
            int intTotalInstr = this.Programa.Instrucoes.Where(w => w.Instrucao != eInstrucao.Rotulo).Count();
            string strEspaco = "        ";

            foreach (InstrASM item in this.Programa.Instrucoes)
            {
                string strIns = item.ToVHDL();
                if (item.Instrucao!=eInstrucao.Rotulo)
                {
                    intInstrucoes++;

                    string strLinha = strEspaco + strIns;
                    if (intInstrucoes != intTotalInstr)
                        strLinha +=  ",";
                    else
                        strLinha += " ";

                    if (blnIncludeSourceAsComment)
                        sbInstr.AppendLine(strLinha + " -- " + Funcoes.IntToHex(intInstrucoes,4) + ": "+ item.ToString());
                    else
                        sbInstr.AppendLine(strLinha);
                    
                }
                else
                    if (blnIncludeSourceAsComment)
                        sbInstr.AppendLine(strEspaco + "       " + "  -- " + item.ToString());
            }
           
            #region CABECALHO
            StringBuilder sbCabecalho = new StringBuilder();
            sbCabecalho.AppendLine("library ieee;");
            sbCabecalho.AppendLine("USE ieee.std_logic_1164.all;");
            sbCabecalho.AppendLine("USE ieee.std_logic_unsigned.all;");
            sbCabecalho.AppendLine("USE ieee.std_logic_arith.all;");
            sbCabecalho.AppendLine("library ubip;");
            sbCabecalho.AppendLine("use ubip.defines.all;");
            sbCabecalho.AppendLine("library testbench;");
            sbCabecalho.AppendLine("--==============================================================");
            sbCabecalho.AppendLine("package "+ strPakageName +" is");
            sbCabecalho.AppendLine("   CONSTANT memory : memory_type (0 TO " + (intInstrucoes - 1).ToString() + " ) := (");
            #endregion

            #region FINALIZA
            StringBuilder sbFinal = new StringBuilder();
            sbFinal.Append(sbCabecalho);
            sbFinal.Append(sbInstr.ToString());
            sbFinal.AppendLine(");");
            sbFinal.AppendLine(string.Format("end package {0};", strPakageName));
            #endregion


            return sbFinal.ToString();
        }
        //---------------------------------------------------------------------
        /// <summary>
        /// Converte a memória de programa para o formato MIF (Memory Initialization File)
        /// </summary>
        /// <param name="blnIncludeSourceAsComment">Inclui o código ASM como comentário alinhado do arquivo VHDL.</param>
        public string GetMifROM(bool blnIncludeSourceAsComment)
        {
            //Se há erro, retorna uma string vazia
            if (this.HasError) return "";

            StringBuilder sbInstr = new StringBuilder();
            int intInstrucoes = 0;

            foreach (InstrASM item in this.Programa.Instrucoes)
            {
                if (item.Instrucao != eInstrucao.Rotulo)
                {
                    string strIns = item.ToMif();
                    if (strIns != "")
                    {
                        string strLinha = strIns;
                        if (blnIncludeSourceAsComment)
                            sbInstr.AppendLine(strLinha + " -- " + item.ToString());
                        else
                            sbInstr.AppendLine(strLinha);
                        intInstrucoes++;
                    }
                    else
                        if (blnIncludeSourceAsComment)
                            sbInstr.AppendLine("          -- " + item.ToString());
                }
            }

            #region CABECALHO
            StringBuilder sbCabecalho = new StringBuilder();
            sbCabecalho.AppendLine(string.Format("WIDTH={0};", Arquitetura.BitsCampoOpCode+Arquitetura.BitsCampoOperando));
            sbCabecalho.AppendLine(string.Format("DEPTH={0};", intInstrucoes));
            sbCabecalho.AppendLine("ADDRESS_RADIX=HEX;");
            sbCabecalho.AppendLine("DATA_RADIX=HEX;");
            sbCabecalho.AppendLine("CONTENT BEGIN");
            #endregion

            #region FINALIZA
            StringBuilder sbFinal = new StringBuilder();
            sbFinal.Append(sbCabecalho);
            sbFinal.Append(sbInstr.ToString());
            sbFinal.AppendLine("END;");
            #endregion
            
            return sbFinal.ToString();
        }
        //---------------------------------------------------------------------
        /// <summary>
        /// Converte a memória de dados para o formato MIF (Memory Initialization File)
        /// </summary>
        /// <param name="blnIncludeSourceAsComment">Inclui o código ASM como comentário alinhado do arquivo VHDL.</param>
        public string GetMifRAM(bool blnIncludeSourceAsComment)
        {
            //Se há erro, retorna uma string vazia
            if (this.HasError) return "";

            StringBuilder sbInstr = new StringBuilder();
            int intInstrucoes = 0;

            foreach (Variavel item in this.Programa.Variaveis)
            {
                List<string> lstValores = item.ToMif(blnIncludeSourceAsComment);
                foreach (string itemVar in lstValores)
                {
                    sbInstr.AppendLine(itemVar);
                    intInstrucoes++;
                }
            }

            #region CABECALHO
            StringBuilder sbCabecalho = new StringBuilder();
            sbCabecalho.AppendLine(string.Format("WIDTH={0};", Arquitetura.BitsCampoOpCode + Arquitetura.BitsCampoOperando));
            sbCabecalho.AppendLine(string.Format("DEPTH={0};", intInstrucoes));
            sbCabecalho.AppendLine("ADDRESS_RADIX=HEX;");
            sbCabecalho.AppendLine("DATA_RADIX=HEX;");
            sbCabecalho.AppendLine("CONTENT BEGIN");
            #endregion

            #region FINALIZA
            StringBuilder sbFinal = new StringBuilder();
            sbFinal.Append(sbCabecalho);
            sbFinal.Append(sbInstr.ToString());
            sbFinal.AppendLine("END;");
            #endregion

            return sbFinal.ToString();
            
        }
        //---------------------------------------------------------------------
        /// <summary>
        /// Converte o programa para Hexadecimal
        /// </summary>
        /// <param name="blnIncludeSourceAsComment">Inclui o código ASM como comentário alinhado do arquivo VHDL.</param>
        public string ToHex(bool blnIncludeSourceAsComment)
        {
            //Se há erro, retorna uma string vazia
            if (this.HasError) return "";

            StringBuilder sbInstr = new StringBuilder();
            int intInstrucoes = 0;

            sbInstr.AppendLine(".data");
            foreach (Variavel item in this.Programa.Variaveis)
            {
                List<string> lstValores = item.ToHex(true, blnIncludeSourceAsComment);
                foreach (string itemVar in lstValores)
                {
                    sbInstr.AppendLine(itemVar);
                    intInstrucoes++;
                }
            }
            sbInstr.AppendLine(".text");
            foreach (InstrASM item in this.Programa.Instrucoes)
            {
                if (item.Instrucao != eInstrucao.Rotulo)
                {
                    string strIns = item.ToHex(true);
                    if (strIns != "")
                    {
                        string strLinha = strIns;
                        if (blnIncludeSourceAsComment)
                            sbInstr.AppendLine(strLinha + " -- " + item.ToString());
                        else
                            sbInstr.AppendLine(strLinha);
                        intInstrucoes++;
                    }
                }
            }
            return sbInstr.ToString();
        }
        //---------------------------------------------------------------------
        /// <summary>
        /// Converte o programa para Binário
        /// </summary>
        /// <param name="blnIncludeSourceAsComment">Inclui o código ASM como comentário alinhado do arquivo VHDL.</param>
        public string ToBin(bool blnIncludeSourceAsComment)
        {
            //Se há erro, retorna uma string vazia
            if (this.HasError) return "";

            StringBuilder sbInstr = new StringBuilder();
            int intInstrucoes = 0;

            sbInstr.AppendLine(".data");
            foreach (Variavel item in this.Programa.Variaveis)
            {
                List<string> lstValores = item.ToBin(true, blnIncludeSourceAsComment);
                foreach (string itemVar in lstValores)
                {
                    sbInstr.AppendLine(itemVar);
                    intInstrucoes++;
                }
            }
            sbInstr.AppendLine(".text");
            foreach (InstrASM item in this.Programa.Instrucoes)
            {
                if (item.Instrucao != eInstrucao.Rotulo)
                {
                    string strIns = Funcoes.IntToHex(item.EndMemoria,4)+ ":" + item.ToBin();
                    if (strIns != "")
                    {
                        string strLinha = strIns;
                        if (blnIncludeSourceAsComment)
                            sbInstr.AppendLine(strLinha + " -- " + item.ToString());
                        else
                            sbInstr.AppendLine(strLinha);
                        intInstrucoes++;
                    }
                    else
                        if (blnIncludeSourceAsComment)
                            sbInstr.AppendLine("          -- " + item.ToString());
                }
            }
            return sbInstr.ToString();
        }
        //---------------------------------------------------------------------
        /// <summary>
        /// Converte o programa para String (ASM)
        /// </summary>
        public override string ToString()
        {
            //Se há erro, retorna uma string vazia
            if (this.HasError) return "";

            StringBuilder sbInstr = new StringBuilder();
            int intInstrucoes = 0;

            sbInstr.AppendLine(".data");
            foreach (Variavel item in this.Programa.Variaveis)
            {
                sbInstr.AppendLine(item.ToString());
            }
            sbInstr.AppendLine(".text");
            foreach (InstrASM item in this.Programa.Instrucoes)
            {
                string strIns = item.ToString();
                if (strIns != "")
                {
                    string strLinha = strIns;
                    sbInstr.AppendLine(strLinha);
                    intInstrucoes++;
                }
            }
            return sbInstr.ToString();
        }
        #endregion
    }
}
