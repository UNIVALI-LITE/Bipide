using System;
using System.IO;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Navigation;

using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Ink;

using System.Collections.ObjectModel;
using System.Windows.Threading;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.RegularExpressions;
using BIPIDE.Classes;


namespace ucBip
{
    #region Classe Contador
    public class Contador
    {

        public int LD { get; set; }
        public int LDI {get; set;}
        public int ADD { get; set; }
        public int ADDI { get; set; }
        public int SUB { get; set; }
        public int SUBI { get; set; }
        public int JMP { get; set; }
        public int STO { get; set; }
        public int BEQ { get; set; }
        public int BNE { get; set; }
        public int BGT { get; set; }
        public int BGE { get; set; }
        public int BLT { get; set; }
        public int BLE { get; set; }
        public int HLT { get; set; }
        public int NOT { get; set; }
        public int AND { get; set; }
        public int ANDI { get; set; }
        public int OR { get; set; }
        public int ORI { get; set; }
        public int XOR { get; set; }
        public int XORI { get; set; }
        public int SLL { get; set; }
        public int SRL { get; set; }
        public int STOV { get; set; }
        public int LDV { get; set; }
        public int RETURN { get; set; }
        public int CALL { get; set; }


    }
    #endregion

    public partial class Simulador
    {
        public enum Idioma
        {
            PORTUGUES,
            INGLES
        }

        Idioma l = Idioma.PORTUGUES;

        #region enum Instrucao
        public enum Instrucao
        {
            LD = 0,
            LDI = 1,
            ADD = 2,
            ADDI = 3,
            SUB = 4,
            SUBI = 5,
            JMP = 6,
            STO = 7,
            BEQ = 8,
            BNE = 9,
            BGT = 10,
            BGE = 11,
            BLT = 12,
            BLE = 13,
            HLT = 14,
            NOT = 15,
            AND = 16,
            ANDI = 17,
            OR = 18,
            ORI = 19,
            XOR = 20,
            XORI = 21,
            SLL = 22,
            SRL = 23,
            STOV = 24,
            LDV = 25,
            RETURN = 26,
            CALL = 27

        }
        #endregion

        //vetor utilizado para contar numero de instrucoes, inicializado com a quantidade de instrucoes utilizadas
        static int countInstrucoes = Enum.GetValues(typeof(Instrucao)).Length;
        int[] vetorContaInstrucoes = new int[countInstrucoes];

        Contador contadorInstrucoes = new Contador();
        

        #region clonador de objeto
        private static T Clone<T>(T target)
        {
            MemoryStream stream = new MemoryStream();
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, target);
            stream.Position = 0;

            return (T)formatter.Deserialize(stream);
        }
        #endregion


        //Variaveis usadas para armazenar valores usados para repetir uma animacao
        public bool isPause = false;
        public bool isStop = false;
        public bool isRepeat = false;
        int PC_ant;
        int posicao_ant;
        int acc_ant;
        int n_ant;
        int z_ant;
        int _indr_ant;
        bool overflow;
        //ListView dados_ant;
        //ListView programa_ant;

        public bool exibeAtualizaPC = true;
        public bool breakpoint = true;

        public int tamanhoProg = 0;

        public List<InstrucaoASM> listaRotulos = new List<InstrucaoASM>();
        public List<InstrucaoASM> listaVariavel = new List<InstrucaoASM>();

        public bool exibeComentario = false;
        
        public string Arquivo = @"Teste.txt";
        public string instrucaoAtual = "";

        private int PROCESSOR = 2;
        private int ACCUMULATOR = 0;
        private int PROGRAMCOUNTER = 0;
        private int status_Z = 0;
        private int status_N = 0;
        private Stack<int> pilhaSubrotina = new Stack<int>();
        private Stack<int> pilhaSubrotinaAnterior = new Stack<int>();
        private int _outport = 0;
        private int _indr = 0;
        private bool[] _inportBits = new bool[16];
        
        private double SPEED = 2;

        /// <summary>
        /// Lista auxiliar utilizada na simulação da instrução
        /// </summary>
        public ArrayList ArrayLinha = new ArrayList();

        //public bool testaFim = false;

        //utilizada para testar se a entrada de dados foi realizada
        bool entradaOK = false;

        public int posicao = 0;

        #region ACC
        int ACC
        { 
            get
            {
                return this.ACCUMULATOR;
            }
            set
            {
                if (value < -32768 | value > 32767)
                {
                    if (!this.isPause)
                    {
                        this.overflow = true;
                    }
                }
                this.ACCUMULATOR = value;
            }
        }
        #endregion

        #region PC
        int _PC
        {
            get
            {
                return this.PROGRAMCOUNTER;
            }
            set
            {
                this.SetEndereco(this.PROGRAMCOUNTER);
                this.PROGRAMCOUNTER = value;
            }
        }
        #endregion

        //animação de Entrada
        Storyboard sbAnimaRealceES = null;

        #region Speed
        public double Speed
        {
            get
            {
                return this.SPEED;
            }
            set
            {
                this.SPEED = value;
            }
        }
        #endregion

        #region tipo processador
        public int Processor
        {
            get
            {
                return this.PROCESSOR;
                //try
                //{
                //    return Convert.ToInt32(this.comboTipo.SelectionBoxItem.ToString()); //this.PROCESSOR;
                //}
                //catch { return 2; }
            }
            set
            {
               this.PROCESSOR = value;
            }

        }
        #endregion

        //==========================================================

        #region getValorVariavel
        public int getValorVariavel(string strVar)
        {
            //verifica se é um registrador
            if (strVar.Contains("$"))
            {
                if (strVar.Replace("$", "").ToLower() == "in_port")
                {
                    return 1024;
                }
                if (strVar.Replace("$", "").ToLower() == "out_port")
                {
                    return 1025;
                }
                if (strVar.Replace("$", "").ToLower() == "indr")
                {
                    return 1073;
                }
                return (-1);
            }
            //memória
            foreach (InstrucaoASM inst in this.listaVariavel)
            {
                if (inst.Instrucao.ToUpper() == strVar.ToUpper())
                    return inst.IndexMemoria;
            }
            return -1;

        }
        #endregion

        #region verifica acesso à memória
        public void verificaAcessoMemoria(int posicao, int index)
        {
            if (posicao+index >= 1024)
            {
                throw new Exception("Violação de acesso à memória ("+(posicao+index).ToString()+").");
            }
            //verifica se a posiçao da memória é de uma variável
            foreach (InstrucaoASM i in this.listaVariavel)
            {
                if (i.IndexMemoria == posicao)
                {
                    if (posicao + index < posicao || posicao + index >= posicao + /*i.Indice)*/i.Tamanho)
                    {
                        throw new Exception("Violação de acesso à memória. Variável "+i.Instrucao+" na posição "+(posicao+index).ToString());
                    }
                }
            }
        }
        #endregion

        //==========================================================

        #region getValorRotulo
        public int getValorRotulo(string strRotulo)
        {

            foreach (InstrucaoASM inst in this.listaRotulos)
            {
                if (inst.Instrucao.ToUpper() == strRotulo.ToUpper())
                    return inst.IndexMemoria;
            }
            return -1;

        }
        #endregion
        
        //==========================================================
        
        #region Verifica Instrução
        /// <summary>
        /// Verifica qual é a instrução e a executa
        /// </summary>
        /// <param name="instrucao">instrução</param>
        /// <param name="operando">operando</param>
        public void verificaInstrucao(string instrucao, string operando)
        {
            showInformacao(exibeComentario);
            //Verifica se operando é uma variável
            int intOperando;

            try
            {
                intOperando = Convert.ToInt32(operando);
            }
            catch
            {
                //não é inteiro
                intOperando = this.getValorVariavel(operando);
                if (intOperando == -1)
                {
                    //nao achou
                    if (instrucao == "LD" || instrucao == "LDI" || instrucao == "ADD" || instrucao == "ADDI" || instrucao == "SUB" || instrucao == "SUBI" || instrucao == "STO")
                        return;//retorn - erro.
                    else
                    {
                        //busca endereço do rotulo
                        intOperando = getValorRotulo(operando);
                        if (intOperando == -1)
                            return; //nao encontrou

                    }

                }
            }

            
                instrucao = instrucao.ToUpper();
                if (instrucao == "LD")
                {
                    if (!isRepeat) contadorInstrucoes.LD++;
                    this.LD(intOperando);
                }
                else if (instrucao == "LDV")
                {
                    if (!isRepeat) contadorInstrucoes.LDV++;
                    this.LDV(intOperando);
                }
                else if (instrucao == "LDI")
                {
                    if (!isRepeat) contadorInstrucoes.LDI++;
                    this.LDI(intOperando);
                }
                else if (instrucao == "ADD")
                {
                    if (!isRepeat) contadorInstrucoes.ADD++;
                    this.ADD(intOperando);
                }
                else if (instrucao == "ADDI")
                {
                    if (!isRepeat) contadorInstrucoes.ADDI++;
                    this.ADDI(intOperando);
                }
                else if (instrucao == "SUB")
                {
                    if (!isRepeat) contadorInstrucoes.SUB++;
                    this.SUB(intOperando);
                }
                else if (instrucao == "SUBI")
                {
                    if (!isRepeat) contadorInstrucoes.SUBI++;
                    this.SUBI(intOperando);
                }
                else if (instrucao == "STO")
                {
                    if (!isRepeat) contadorInstrucoes.STO++;
                    this.STO(intOperando);
                }
                else if (instrucao == "STOV")
                {
                    if (!isRepeat) contadorInstrucoes.STOV++;
                    this.STOV(intOperando);
                }
                else if (instrucao == "BEQ")
                {
                    if (!isRepeat) contadorInstrucoes.BEQ++;
                    this.BEQ(intOperando);
                }
                else if (instrucao == "BNE")
                {
                    if (!isRepeat) contadorInstrucoes.BNE++;
                    this.BNE(intOperando);
                }
                else if (instrucao == "BGT")
                {
                    if (!isRepeat) contadorInstrucoes.BGT++;
                    this.BGT(intOperando);
                }
                else if (instrucao == "BGE")
                {
                    if (!isRepeat) contadorInstrucoes.BGE++;
                    this.BGE(intOperando);
                }
                else if (instrucao == "BLT")
                {
                    if (!isRepeat) contadorInstrucoes.BLT++;
                    this.BLT(intOperando);
                }
                else if (instrucao == "BLE")
                {
                    if (!isRepeat) contadorInstrucoes.BLE++;
                    this.BLE(intOperando);
                }
                else if (instrucao == "JMP")
                {
                    if (!isRepeat) contadorInstrucoes.JMP++;
                    this.JMP(intOperando);
                }
                else if (instrucao == "HLT")
                {
                    if (!isRepeat) contadorInstrucoes.HLT++;
                    this.HLT(intOperando);
                }
                else if (instrucao == "CALL")
                {
                    if (!isRepeat) contadorInstrucoes.CALL++;
                    this.CALL(Convert.ToInt32(intOperando));
                }
                else if (instrucao == "RETURN")
                {
                    if (!isRepeat) contadorInstrucoes.RETURN++;
                    this.RETURN();
                }
                else if (instrucao == "AND")
                {
                    if (!isRepeat) contadorInstrucoes.AND++;
                    this.AND(Convert.ToInt32(intOperando));
                }
                else if (instrucao == "OR")
                {
                    if (!isRepeat) contadorInstrucoes.OR++;
                    this.OR(Convert.ToInt32(intOperando));
                }
                else if (instrucao == "XOR")
                {
                    if (!isRepeat) contadorInstrucoes.XOR++;
                    this.XOR(Convert.ToInt32(intOperando));
                }
                else if (instrucao == "ANDI")
                {
                    if (!isRepeat) contadorInstrucoes.ANDI++;
                    this.ANDI(Convert.ToInt32(intOperando));
                }
                else if (instrucao == "ORI")
                {
                    if (!isRepeat) contadorInstrucoes.ORI++;
                    this.ORI(Convert.ToInt32(intOperando));
                }
                else if (instrucao == "XORI")
                {
                    if (!isRepeat) contadorInstrucoes.XORI++;
                    this.XORI(Convert.ToInt32(intOperando));
                }
                else if (instrucao == "SLL")
                {
                    if (!isRepeat) contadorInstrucoes.SLL++;
                    this.SLL(Convert.ToInt32(intOperando));
                }
                else if (instrucao == "SRL")
                {
                    if (!isRepeat) contadorInstrucoes.SRL++;
                    this.SRL(Convert.ToInt32(intOperando));
                }
                else if (instrucao == "NOT")
                {
                    if (!isRepeat) contadorInstrucoes.NOT++;
                    this.NOT();
                }
                else
                {
                    this.posicao++;
                    //this._PC++;
                    this.PCmore1(false);
                    this.getNovaInstrucao();
                }
                this.Exibe(contadorInstrucoes);
        }
        #endregion

        //==========================================================
        #region Inicializa valores
        /// <summary>
        /// Inicializa valores Iniciais antes da simulação de um programa
        /// </summary>
        public void InicializaValoresIniciais()
        {
            contadorInstrucoes = new Contador();

            this.overflow = false;
            isPause = false;
            isStop = false;
            isRepeat = false;
            this.ArrayLinha.Clear();
            this.status_N = 0;
            this.status_Z = 0;
            this.INDR.Text = "0";
            this.SP.Text = "0";

            this.lblN.Content = 0;
            this.lblZ.Content = 0;
            rAcumulador_In.Background = Brushes.Khaki;
            rAcumulador_out.Background = Brushes.Khaki;
            
            ACCUMULATOR = 0;
            this._outport = 0;
            this._indr = 0;
            this.AccToOutPort();
            this.in_port.Text = "0";
            for (int x = 0; x < 16; x++)
            {
                this._inportBits[x] = false;
            }
            PROGRAMCOUNTER = 0;
            rAcumulador_In.Text = "0";
            rAcumulador_out.Text = "0";
            rImediato.Text = "0";
            rInstrucao.Text = "";
            rLDI.Text = "0";
            rOpcode.Text = "0";
            rOperando.Text = "0";
            rPC.Text = "0";
            rPC_in.Text = "0";
            rPC_operand.Text = "0";
            rPC1.Text = "0";
            rRegistrador.Text = "0";
            this.pilhaSubrotina.Clear();
            try
            {
                this._PC = 0;
            }
            catch { }
            this.ACC = 0;

            this.posicao = 0;
            txtPC.Text = "0";
            txtACC.Text = "0";
            txtZ.Text = "0";
            txtN.Text = "0";

            //Preenche grid Memoria de Programa

            //this.PreencheMemoPrograma();
        }
        #endregion

        //==========================================================
       
        #region CONSTRUTOR
        /// <summary>
        /// Construtor Padrão
        /// </summary>
		public Simulador()
		{
            
			this.InitializeComponent();


            //inicializa valores das variaveis
            this.InicializaValoresIniciais();

            //inicialmente oculta bloco comentario
            showInformacao(false);
            exibeComentario = true;

            //oculta campos necessarios
            this.OcultaCampos();
            this.OcultaAcumulador();
            this.VerificaProcessador();

            //Eventos de elementos com double Click
            Decoder.MouseDown +=new MouseButtonEventHandler(Decoder_MouseDown);
            DescDecoder.MouseDown += new MouseButtonEventHandler(Decoder_MouseDown);

            ula.MouseDown += new MouseButtonEventHandler(ula_MouseDown);
            DescUla.MouseDown += new MouseButtonEventHandler(ula_MouseDown);

            blocoACC.MouseDown += new MouseButtonEventHandler(blocoACC_MouseDown);
            DescAcc.MouseDown += new MouseButtonEventHandler(blocoACC_MouseDown);

            PC.MouseDown += new MouseButtonEventHandler(PC_MouseDown);
            DescPC.MouseDown += new MouseButtonEventHandler(PC_MouseDown);

            //lblZ.MouseDown += new MouseButtonEventHandler(blocoStatus_MouseDown);
            //lblN.MouseDown += new MouseButtonEventHandler(blocoStatus_MouseDown);
            //blocoStatus.MouseDown += new MouseButtonEventHandler(blocoStatus_MouseDown);
            borderStatus.MouseDown += new MouseButtonEventHandler(blocoStatus_MouseDown);

            extSinal.MouseDown += new MouseButtonEventHandler(extSinal_MouseDown);
            DescExtSinal.MouseDown += new MouseButtonEventHandler(extSinal_MouseDown);
            

            pilha.MouseDown += new MouseButtonEventHandler(pilha_MouseDown);
            DescPilha.MouseDown += new MouseButtonEventHandler(pilha_MouseDown);

            es_reg.MouseDown += new MouseButtonEventHandler(es_reg_MouseDown);
            DescES.MouseDown += new MouseButtonEventHandler(es_reg_MouseDown);

            vetor_acces.MouseDown += new MouseButtonEventHandler(vetor_acces_MouseDown);
            DescVetorAccess.MouseDown += new MouseButtonEventHandler(vetor_acces_MouseDown);

            // foi copiado para metodo InicializaValoresIniciais
            //this.lblN.Content = 0;
            //this.lblZ.Content = 0;
            //rAcumulador_In.Background = Brushes.Khaki;
            //rAcumulador_out.Background = Brushes.Khaki;

            //Preenche grid Memoria de Programa
            //this.PreencheMemoPrograma();
            //comboSpeed.Visibility = Visibility.Hidden;
            //btPC.Visibility = Visibility.Hidden;

            this.PreencheGridDados();
            this.PreecheGridPrograma();


            blocoInfo.Visibility = Visibility.Hidden;


        }

        
        #endregion

        //==========================================================

        #region PreencheGridDados
        /// <summary>
        /// Preenche Grid
        /// </summary>
        public void PreencheGridDados()
        {
            #region Preenche grid Memoria de Dados
            
            GridView view = new GridView();

            GridViewColumn c = new GridViewColumn();
            c.Width = 85;
            c.Header = "Endereço";
            c.DisplayMemberBinding = new Binding("[0]");
            view.Columns.Add(c);

            GridViewColumn c2 = new GridViewColumn();
            c2.Width = 85;
            c2.Header = "Valor";
            c2.DisplayMemberBinding = new Binding("[1]");
            view.Columns.Add(c2);

            GridViewColumn c3 = new GridViewColumn();
            c3.Width = 85;
            c3.Header = "Variável";
            c3.DisplayMemberBinding = new Binding("[2]");
            view.Columns.Add(c3);

            gridMemoDados.View = view;

            for (int i = 0; i < 1024; i++)
            {
                ListViewItem item = new ListViewItem();
                item.Content = new String[] { i.ToString(), "", "" };
                gridMemoDados.Items.Add(item);
            }
            #endregion

        }
        #endregion

        //==========================================================

        #region PreecheGridPrograma
        public void PreecheGridPrograma(){
            //memoria de programa
            gridMemoPrograma.Items.Clear();

            GridView view2 = new GridView();

            GridViewColumn col = new GridViewColumn();
            col.Width = 85;
            col.Header = "Rótulo";
            col.DisplayMemberBinding = new Binding("[0]");
            view2.Columns.Add(col);


            GridViewColumn col2 = new GridViewColumn();
            col2.Width = 85;
            col2.Header = "Endereço";
            col2.DisplayMemberBinding = new Binding("[1]");
            view2.Columns.Add(col2);

            GridViewColumn col3 = new GridViewColumn();
            col3.Width = 85;
            col3.Header = "Instrução";
            col3.DisplayMemberBinding = new Binding("[2]");
            view2.Columns.Add(col3);

            gridMemoPrograma.View = view2;
        }
        #endregion
        
        //==========================================================
        
        #region Exibe Informação dos componentes do Processador
        void PC_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DoubleClick("PC", e);
        }

        void blocoACC_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DoubleClick("ACC", e);
        }

        void ula_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DoubleClick("ULA", e);
        }

        void Decoder_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DoubleClick("DECODIFICADOR", e);
        }
        void blocoStatus_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DoubleClick("STATUS", e);
        }
        void vetor_acces_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DoubleClick("VETOR", e);
        }

        void extSinal_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DoubleClick("EXTENSÃO DE SINAL", e);
        }

        void es_reg_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DoubleClick("ENTRADA E SAÍDA", e);
        }

        void pilha_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DoubleClick("PILHA", e);
        }


        //implementa double click dos elementos
        public void DoubleClick(string item, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                blocoInfo.Visibility = Visibility.Visible;

                txtInfoTitle.Text = item;

                switch (item)
                {
                    case "STATUS":
                        
                        txtInfoMessage.Text = @"O registrador STATUS armazena informações de acordo com o resultado da operação realizada na ULA. É utilizado em todas as operações de desvio condicional.
                            
Toda instrução de comparação e desvio condicional deve ser precedida por uma instrução de subtração (SUB ou SUBI). 

O registrador STATUS possui dois flags (Z e N) que são ativados dependendo do resultado da operação anterior: 

Z indica se o resultado da última operação da ULA foi igual a zero ou não.

N indica se o último resultado da ULA foi um número negativo ou não.";
                        break;
                    case "DECODIFICADOR":
                        txtInfoMessage.Text = @"O Decodificador é o componente da unidade de controle responsável por decodificar as instruções da memória de programa a fim de definir qual a instrução que será executada pela ULA.";
                        break;
                    case "ULA":
                        txtInfoMessage.Text = @"A Unidade lógica e aritmética (ULA) é a unidade central do processador, que executa as operações aritméticas e lógicas que ocorrem no processador.
                            
A ULA executa as principais operações lógicas e aritméticas do computador. Ela soma, subtrai, determina se um número é positivo ou negativo ou se é zero. 

Além de executar funções aritméticas, uma ULA deve ser capaz de determinar se uma quantidade é menor ou maior que outra e quando quantidades são iguais.";
                        break;
                    case "ACC":
                        txtInfoMessage.Text = @"O Acumulador (Accumulator) é o registrador da Unidade Central de Processamento que atua como memória auxiliar durante cálculos e movimentações de dados dentro do sistema.

Sem o acumulador seria necessário armazenar o resultado de cada cálculo na memória de dados para depois buscá-lo novamente. O acesso a este valor na memória seria mais lento que acessá-lo do Acumulador, o qual normalmente esta ligado diretamente à Unidade Lógica Aritmética (ULA).

Nos processadores BIP, o Acumulador é utilizado em todas as operações aritméticas executadas.

Por exemplo: na expressão 1+2+3, os valores 1 e 2 são somados e o resultado é armazenado no Acumulador. Na próxima instrução, o valor do Acumulador é carregado e somado com o valor 3.";
                        break;
                    case "PC":
                        txtInfoMessage.Text = @"O Contador de Programa (Program Counter) é o registrador da Unidade Central de Processamento que indica qual é a posição atual na sequência de execução de um computador. Na arquitetura dos Processadores BIP, ele armazena o endereço da próxima instrução a ser executada. Em outras arquiteturas ele pode indicar o endereço da instrução sendo executada.

O Contador de Programa é automaticamente incrementado depois de cada instrução de forma que as instruções são normalmente executadas sequencialmente a partir da memória. Entretanto, certas instruções como estruturas de desvio condicional e incondicional interrompem a sequência ao modificar manualmente o valor do contador de programa.

Um exemplo de instrução que altera manualmente o valor do PC nos processadores BIP é a instrução JMP (jump)";
                        break;
                    case "EXTENSÃO DE SINAL":
                        txtInfoMessage.Text = @"Extensao de sinal...";
                        break;
                    case "VETOR":
                        txtInfoMessage.Text = @"O módulo de Manipulação de Vetores é composto por um registrador (INDR) e um somador.
 
Este módulo é responsável por calcular o endereço efetivo de memória através da soma do operando nas instruções LDV e STOV com o conteúdo do registrador INDR";
                        break;
                    case "PILHA":
                        txtInfoMessage.Text = @"A Pilha é utilizada para controle do suporte a chamadas de procedimentos.

Quanto ocorre uma chamada de procedimento, o endereço seguinte a instrução CALL é armazenado no topo da pilha de suporte a procedimentos.

Para retorno do procedimento, o valor armazenado no topo da pilha é recuperado pela instrução RETURN";
                        break;
                    case "ENTRADA E SAÍDA":
                        txtInfoMessage.Text = @"Entrada e saida...";
                        break;
                    default:
                        break;
                }
            }
        }
        #endregion

        //==========================================================

        #region PreencheMemoPrograma
        /// <summary>
        /// Preenche memória de Programa a partir de um arquivo texto
        /// </summary>
        public void PreencheMemoPrograma()
        {
            gridMemoPrograma.Items.Clear();

            //le arquivo
            string vArquivo = this.Arquivo;
            if (File.Exists(vArquivo))
                {
                StreamReader reader = new StreamReader(vArquivo);
                ArrayList ArrayLinha = new ArrayList();

                string linha = "";
                string[] a;
                int posicao = 0;
                while (!reader.EndOfStream)
                {
                    //ArrayLinha.Add(reader.ReadLine());
                    linha = reader.ReadLine().ToString();
                    a = linha.Split(' ');

                    ListViewItem item2 = new ListViewItem();
                    //item2.Content = new String[] { a[0], a[1] };
                    item2.Content = new String[] {"", posicao.ToString(), linha };
                    gridMemoPrograma.Items.Add(item2);
                    posicao++;
                }
                reader.Close();
            }
        }
        #endregion

        //==========================================================
        public void setProcessador(int value)
        {
            this.Processor = value;
            this.VerificaProcessador();
        }
        #region VerificaProcessador
        /// <summary>
        /// Verifica o modelo de processador e exibe os componentes específicos de cada modelo
        /// </summary>
        public void VerificaProcessador()
        {
            
            if (this.Processor == 1)
            {
                this.status.Visibility = Visibility.Hidden;
                this.linhaBIP2.Visibility = Visibility.Hidden;
                this.blocoBIP2.Visibility = Visibility.Hidden; 
                this.linhaBIP2_2.Visibility = Visibility.Hidden;
                this.linhaStatus1.Visibility = Visibility.Hidden;
                this.linhaStatus2.Visibility = Visibility.Hidden;
                this.linhaStatus3.Visibility = Visibility.Hidden;
                this.linhaStatus4.Visibility = Visibility.Hidden;
                this.linhaStatus5.Visibility = Visibility.Hidden;
                this.setaStatus1.Visibility = Visibility.Hidden;
                this.setaStatus2.Visibility = Visibility.Hidden;
                this.setaStatus3.Visibility = Visibility.Hidden;
                this.setaStatus4.Visibility = Visibility.Hidden;
                this.setaStatus5.Visibility = Visibility.Hidden;
                this.lblZ.Visibility = Visibility.Hidden;
                this.lblN.Visibility = Visibility.Hidden;
                this.ES_grid.Visibility = Visibility.Hidden;
                this.DescBip.Content = "BIP I";
                DescUla.Content = "+ -";
                DescUla.FontSize = 22;
                this.pilha_grid.Visibility = Visibility.Hidden;
                this.es_grid2.Visibility = Visibility.Hidden;
                this.vetor_grid.Visibility = Visibility.Hidden;
				
				this.OrGate.Visibility = Visibility.Hidden;
				this.linhaInterrupcao1.Visibility = Visibility.Hidden;
				this.linhaInterrupcao2.Visibility = Visibility.Hidden;
				this.linhaInterrupcao3.Visibility = Visibility.Hidden;
				this.linhaInterrupcao4.Visibility = Visibility.Hidden;
				this.InterruptBlock.Visibility = Visibility.Hidden;
				this.DescInterrupt.Visibility = Visibility.Hidden;
				this.blocoSelPC.Visibility = Visibility.Hidden;
            }
            else if (this.Processor == 2)
            {
                this.status.Visibility = Visibility.Visible;
                this.linhaBIP2.Visibility = Visibility.Visible;
                this.blocoBIP2.Visibility = Visibility.Visible;
                this.linhaBIP2_2.Visibility = Visibility.Visible;
                this.linhaStatus1.Visibility = Visibility.Visible;
                this.linhaStatus2.Visibility = Visibility.Visible;
                this.linhaStatus3.Visibility = Visibility.Visible;
                this.linhaStatus4.Visibility = Visibility.Visible;
                this.linhaStatus5.Visibility = Visibility.Visible;
                this.setaStatus1.Visibility = Visibility.Visible;
                this.setaStatus2.Visibility = Visibility.Visible;
                this.setaStatus3.Visibility = Visibility.Visible;
                this.setaStatus4.Visibility = Visibility.Visible;
                this.setaStatus5.Visibility = Visibility.Visible;
                this.lblZ.Visibility = Visibility.Visible;
                this.lblN.Visibility = Visibility.Visible;
                this.ES_grid.Visibility = Visibility.Hidden;
                this.DescBip.Content = "BIP II";
                DescUla.Content = "+ -";
                DescUla.FontSize = 22;
                this.pilha_grid.Visibility = Visibility.Hidden;
                this.es_grid2.Visibility = Visibility.Hidden;
                this.vetor_grid.Visibility = Visibility.Hidden;
				this.OrGate.Visibility = Visibility.Hidden;
				this.linhaInterrupcao1.Visibility = Visibility.Hidden;
				this.linhaInterrupcao2.Visibility = Visibility.Hidden;
				this.linhaInterrupcao3.Visibility = Visibility.Hidden;
				this.linhaInterrupcao4.Visibility = Visibility.Hidden;
				this.InterruptBlock.Visibility = Visibility.Hidden;
				this.DescInterrupt.Visibility = Visibility.Hidden;
				this.blocoSelPC.Visibility = Visibility.Hidden;
            }
            else if (this.Processor == 3)
            {
                this.DescBip.Content = "BIP III";
                DescUla.Content = "ULA";
                DescUla.FontSize = 18;
                this.status.Visibility = Visibility.Visible;
                this.linhaBIP2.Visibility = Visibility.Visible;
                this.blocoBIP2.Visibility = Visibility.Visible;
                this.linhaBIP2_2.Visibility = Visibility.Visible;
                this.linhaStatus1.Visibility = Visibility.Visible;
                this.linhaStatus2.Visibility = Visibility.Visible;
                this.linhaStatus3.Visibility = Visibility.Visible;
                this.linhaStatus4.Visibility = Visibility.Visible;
                this.linhaStatus5.Visibility = Visibility.Visible;
                this.setaStatus1.Visibility = Visibility.Visible;
                this.setaStatus2.Visibility = Visibility.Visible;
                this.setaStatus3.Visibility = Visibility.Visible;
                this.setaStatus4.Visibility = Visibility.Visible;
                this.setaStatus5.Visibility = Visibility.Visible;
                this.lblZ.Visibility = Visibility.Visible;
                this.lblN.Visibility = Visibility.Visible;
                this.ES_grid.Visibility = Visibility.Hidden;
                this.pilha_grid.Visibility = Visibility.Hidden;
                this.es_grid2.Visibility = Visibility.Hidden;
                this.vetor_grid.Visibility = Visibility.Hidden;
				this.OrGate.Visibility = Visibility.Hidden;
				this.linhaInterrupcao1.Visibility = Visibility.Hidden;
				this.linhaInterrupcao2.Visibility = Visibility.Hidden;
				this.linhaInterrupcao3.Visibility = Visibility.Hidden;
				this.linhaInterrupcao4.Visibility = Visibility.Hidden;
				this.InterruptBlock.Visibility = Visibility.Hidden;
				this.DescInterrupt.Visibility = Visibility.Hidden;
				this.blocoSelPC.Visibility = Visibility.Hidden;
            }
            else if (this.Processor == 4)
            {
                this.status.Visibility = Visibility.Visible;
                this.linhaBIP2.Visibility = Visibility.Visible;
                this.blocoBIP2.Visibility = Visibility.Visible;
                this.linhaBIP2_2.Visibility = Visibility.Visible;
                this.linhaStatus1.Visibility = Visibility.Visible;
                this.linhaStatus2.Visibility = Visibility.Visible;
                this.linhaStatus3.Visibility = Visibility.Visible;
                this.linhaStatus4.Visibility = Visibility.Visible;
                this.linhaStatus5.Visibility = Visibility.Visible;
                this.setaStatus1.Visibility = Visibility.Visible;
                this.setaStatus2.Visibility = Visibility.Visible;
                this.setaStatus3.Visibility = Visibility.Visible;
                this.setaStatus4.Visibility = Visibility.Visible;
                this.setaStatus5.Visibility = Visibility.Visible;
                this.lblZ.Visibility = Visibility.Visible;
                this.lblN.Visibility = Visibility.Visible;
                this.ES_grid.Visibility = Visibility.Visible;
                this.DescBip.Content = "BIP IV";
                DescUla.Content = "ULA";
                DescUla.FontSize = 18;
                this.pilha_grid.Visibility = Visibility.Visible;
                this.es_grid2.Visibility = Visibility.Visible;
                this.vetor_grid.Visibility = Visibility.Visible;
				this.OrGate.Visibility = Visibility.Hidden;
				this.linhaInterrupcao1.Visibility = Visibility.Hidden;
				this.linhaInterrupcao2.Visibility = Visibility.Hidden;
				this.linhaInterrupcao3.Visibility = Visibility.Hidden;
				this.linhaInterrupcao4.Visibility = Visibility.Hidden;
				this.InterruptBlock.Visibility = Visibility.Hidden;
				this.DescInterrupt.Visibility = Visibility.Hidden;
				this.blocoSelPC.Visibility = Visibility.Hidden;

            } 
			else if (this.Processor == 5)
			{
				this.status.Visibility = Visibility.Visible;
                this.linhaBIP2.Visibility = Visibility.Visible;
                this.blocoBIP2.Visibility = Visibility.Visible;
                this.linhaBIP2_2.Visibility = Visibility.Visible;
                this.linhaStatus1.Visibility = Visibility.Visible;
                this.linhaStatus2.Visibility = Visibility.Visible;
                this.linhaStatus3.Visibility = Visibility.Visible;
                this.linhaStatus4.Visibility = Visibility.Visible;
                this.linhaStatus5.Visibility = Visibility.Visible;
                this.setaStatus1.Visibility = Visibility.Visible;
                this.setaStatus2.Visibility = Visibility.Visible;
                this.setaStatus3.Visibility = Visibility.Visible;
                this.setaStatus4.Visibility = Visibility.Visible;
                this.setaStatus5.Visibility = Visibility.Visible;
                this.lblZ.Visibility = Visibility.Visible;
                this.lblN.Visibility = Visibility.Visible;
                this.ES_grid.Visibility = Visibility.Visible;
                this.DescBip.Content = "µBIP";
                DescUla.Content = "ULA";
                DescUla.FontSize = 18;
                this.pilha_grid.Visibility = Visibility.Visible;
                this.es_grid2.Visibility = Visibility.Visible;
                this.vetor_grid.Visibility = Visibility.Visible;
				this.OrGate.Visibility = Visibility.Visible;
				this.linhaInterrupcao1.Visibility = Visibility.Visible;
				this.linhaInterrupcao2.Visibility = Visibility.Visible;
				this.linhaInterrupcao3.Visibility = Visibility.Visible;
				this.linhaInterrupcao4.Visibility = Visibility.Visible;
				this.InterruptBlock.Visibility = Visibility.Visible;
				this.DescInterrupt.Visibility = Visibility.Visible;
				this.blocoSelPC.Visibility = Visibility.Visible;
			}
        }
        #endregion

        //==========================================================
        #region Oculta Campos
        /// <summary>
        /// Oculta algums elementos do processador
        /// </summary>
        public void OcultaCampos()
        {
            //this.rAcumulador_out.Visibility = Visibility.Hidden;
            this.rAcumulador_In.Visibility = Visibility.Hidden;
            this.rImediato.Visibility = Visibility.Hidden;
            //this.rInstrucao.Visibility = Visibility.Hidden;
            //this.rOpcode.Visibility = Visibility.Hidden;
            //this.rOperando.Visibility = Visibility.Hidden;
            this.rRegistrador.Visibility = Visibility.Hidden;

            this.rLDI.Visibility = Visibility.Hidden;
            
            //this.acc.Visibility = Visibility.Hidden;
            

            //PC
            //this.rPC.Visibility = Visibility.Hidden;
            this.rPC_in.Visibility = Visibility.Hidden;
            this.rPC1.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// Oculta elemento Acumulador
        /// </summary>
        public void OcultaAcumulador()
        {
            this.rAcumulador_out.Visibility = Visibility.Hidden;
        }
        #endregion

        //==========================================================
        #region // Altera Cor de Fundo
        /// <summary>
        /// Altera cor de fundo do WPF de acordo com o tema
        /// </summary>
        /// <param name="R"></param>
        /// <param name="G"></param>
        /// <param name="B"></param>
        /// <param name="corDoFundo"> cor de fundo ao redor do processador</param>
        public void CorFundo(int R, int G, int B, Color corDoFundo)
        {
            /*
            byte bR = Convert.ToByte(R);
            byte bG = Convert.ToByte(G);
            byte bB = Convert.ToByte(B);

            //Cor fundo do processador
            LinearGradientBrush linearGrad = new LinearGradientBrush();
            GradientStopCollection gradiente = new GradientStopCollection();
            gradiente.Add(new GradientStop(Color.FromRgb( bR,bB,bG), 0));
            gradiente.Add(new GradientStop(Color.FromRgb((byte)(bR - 24), (byte)(bG - 8), (byte)(bB + 12)), 0));

            linearGrad.GradientStops = gradiente;

            this.fundo.Fill = linearGrad;
            this.blocoValorReg.Fill = linearGrad;
            this.fundoComentario.Fill = linearGrad;


            //cor de fundo do wpf ao redor do processador
            LinearGradientBrush linearGrad2 = new LinearGradientBrush();
            GradientStopCollection gradiente2 = new GradientStopCollection();
            gradiente2.Add(new GradientStop(corDoFundo,0));
            linearGrad2.GradientStops = gradiente2;
            this.LayoutRoot.Background = linearGrad2;
            */
        }

        #endregion

        //==========================================================
        #region //HLT
        //=======================================================
        /// <summary>
        /// HLT - Halts : Desabilita a atualização do PC
        /// Nao utilizado na simulação,
        /// </summary>
        /// <param name="value">operando imediato</param>
        public void HLT(int value)
        {
            this.SetEndereco(this.PROGRAMCOUNTER);
            this.instrucaoAtual = Instrucao.HLT.ToString();
            setInformacao(Instrucao.HLT);
            this.SetFim();

        }
        #endregion

        //==========================================================
        #region //LDI
        //==========================================================
        /// <summary>
        /// LDI - Load Immediate : Carrega o valor de um operando imediato para o acumulador ACC
        /// </summary>
        /// <param name="value">operando imediato</param>
        public void LDI(int value)
        {
            this.instrucaoAtual = Instrucao.LDI.ToString();

            //this.OcultaCampos();
            //this.OcultaAcumulador();
            rInstrucao.Text = "LDI " + value.ToString();
            rOpcode.Text = "LDI";

            //Busca animacao
            Storyboard sbAnimaLoad = (Storyboard)FindResource("LDI");
            //if(this.a2 == false)

            sbAnimaLoad.Completed -= new EventHandler(sbAnimaLoad_Completed);
            sbAnimaLoad.Completed += new EventHandler(sbAnimaLoad_Completed);
            

            sbAnimaLoad.SpeedRatio = this.Speed;
            sbAnimaLoad.AccelerationRatio = 0.1;

            this.rImediato.Text = value.ToString();
            rImediato.Background = Brushes.Khaki;
            //rLDI.Visibility = Visibility.Visible;
            
            sbAnimaLoad.Begin(this,true);
            this.ACC = value;

            this.PCmore1(false);

            setInformacao(Instrucao.LDI);
            //blocoComentario.Text = "Carregando valor imediato " + value.ToString() + " para o Acumulador";
        }

        void sbAnimaLoad_Completed(object sender, EventArgs e)
        {
            
            this.txtACC.Text = this.ACC.ToString();
            this.getNovaInstrucao();
            //this.ADDI(5);
            
        }
        #endregion

        //==========================================================
        #region //SUB
        //==========================================================
        /// <summary>
        /// SUB : Subtract : O Valor do registrador ACC é subtraído pelo valor armazenado 
        /// na posição de memória indicado pelo campo operand e o resultado é armazenado no ACC.
        /// </summary>
        public void SUB(int position)
        {
            this.instrucaoAtual = Instrucao.SUB.ToString();

            rOperando.Background = Brushes.PaleGreen;
            rInstrucao.Text = "SUB " + position;
            rOpcode.Text = "SUB";
            rOperando.Text = position.ToString();


            this.lblMore.Content = "-";

            int Valor = this.getValorMemoria(position);

            this.rRegistrador.Text = Valor.ToString();
            rRegistrador.Background = Brushes.Khaki;
            this.rAcumulador_out.Text = this.ACC.ToString();
            rAcumulador_out.Background = Brushes.Khaki;
            rAcumulador_In.Background = Brushes.Khaki; 
            this.rAcumulador_In.Text = (this.ACC - Valor).ToString();
            this.ACC = (this.ACC - Valor);

            //seta registrador Status
            this.atualizaStatus();
            if (this.Processor >= 2)
            {
                this.lblN.Visibility = Visibility.Visible;
                this.lblN.Content = this.status_N.ToString();
            }
           
            Storyboard sbAnimaSUB = (Storyboard)FindResource("SUB_ADD");

            sbAnimaSUB.Completed -= new EventHandler(sbAnimaSUB_ADD_Completed);
            sbAnimaSUB.Completed += new EventHandler(sbAnimaSUB_ADD_Completed);

            sbAnimaSUB.SpeedRatio = this.Speed + 0.5; 
            sbAnimaSUB.Begin(this,true);

            this.PCmore1(false);

            setInformacao(Instrucao.SUB);
        }

        //void sbAnimaSUB_Completed(object sender, EventArgs e)
        //{
        //    this.getNovaInstrucao();
        //}
        #endregion

        #region atualiza os status
        void atualizaStatus()
        {
            if (this.ACC == 0)
                this.status_Z = 1;
            else
                this.status_Z = 0;
            if (this.Processor >= 2)
            {
                this.lblZ.Visibility = Visibility.Visible;
                this.lblZ.Content = this.status_Z.ToString();
            }
            if (this.ACC < 0)
                this.status_N = 1;
            else
                this.status_N = 0;
            this.lblN.Content = this.status_N.ToString();
        }
        #endregion

        //==========================================================
        #region //SUBI
        //==========================================================
        /// <summary>
        /// SUBI - Subtract Immediate : O valor do registrador ACC é subtraído pelo valor do operando imediato 
        /// e o resultado é armazenado no ACC
        /// </summary>
        /// <param name="value">operando imediato</param>
        public void SUBI(int value)
        {

            this.instrucaoAtual = Instrucao.SUBI.ToString();

            rInstrucao.Text = "SUBI " + value;
            rOpcode.Text = "SUBI";

            //value = Convert.ToInt32(this.txtTeste.Text);
            this.lblMore.Content = "-";

            this.rImediato.Text = value.ToString();
            this.rAcumulador_out.Text = this.ACC.ToString();
            this.rAcumulador_In.Text = (this.ACC - value).ToString();
            this.ACC = (this.ACC - value);

            rImediato.Background = Brushes.Khaki;
            rAcumulador_In.Background = Brushes.Khaki;
            rAcumulador_out.Background = Brushes.Khaki;

            //seta registrador Status
            this.atualizaStatus();
            if (this.Processor >= 2)
            {
                this.lblN.Visibility = Visibility.Visible;
                this.lblN.Content = this.status_N.ToString();
            }
            //

            Storyboard sbAnimaSUBI = (Storyboard)FindResource("SUBI_ADDI");

            sbAnimaSUBI.Completed -= new EventHandler(sbAnimaSUBI_ADDI_Completed);
            sbAnimaSUBI.Completed += new EventHandler(sbAnimaSUBI_ADDI_Completed);

            sbAnimaSUBI.SpeedRatio = this.Speed;
            sbAnimaSUBI.Begin(this,true);

            this.PCmore1(false);

            setInformacao(Instrucao.SUBI);
        }

        void sbAnimaSUBI_ADDI_Completed(object sender, EventArgs e)
        {
            this.txtACC.Text = this.ACC.ToString();
            this.txtZ.Text = this.status_Z.ToString();
            this.txtN.Text = this.status_N.ToString();
            this.getNovaInstrucao();
        }
        #endregion

        //==========================================================
        #region //ADD
        //==========================================================
        /// <summary>
        /// ADD : O valor do registrador ACC é somado com o valor armazenado na posição  
        /// de memória indicado pelo campo operand e o resultado armazenado no ACC.
        /// </summary>
        public void ADD(int position)
        {
            this.instrucaoAtual = Instrucao.ADD.ToString();
            rOperando.Background = Brushes.PaleGreen;
            rInstrucao.Text = "ADD " + position;
            rOpcode.Text = "ADD";
            rOperando.Text = position.ToString();
            this.lblMore.Content = "+";
            int Valor = this.getValorMemoria(position);
            this.rRegistrador.Text = Valor.ToString();
            rRegistrador.Background = Brushes.Khaki;
            //this.rRegistrador.Text = 2.ToString();
            this.rAcumulador_out.Text = this.ACC.ToString();
            this.rAcumulador_In.Text = (this.ACC + Valor).ToString();
            this.ACC = (this.ACC + Valor);

            //seta registrador Status
            this.atualizaStatus();
            if (this.Processor >= 2)
            {
                this.lblN.Visibility = Visibility.Visible;
                this.lblN.Content = this.status_N.ToString();
            }

            rAcumulador_out.Background = Brushes.Khaki;
            rAcumulador_In.Background = Brushes.Khaki; 

            Storyboard sbAnimaADD = (Storyboard)FindResource("SUB_ADD");

            sbAnimaADD.Completed -= new EventHandler(sbAnimaSUB_ADD_Completed);
            sbAnimaADD.Completed += new EventHandler(sbAnimaSUB_ADD_Completed);

            sbAnimaADD.SpeedRatio = this.Speed + 0.5;
            
            sbAnimaADD.Begin(this,true);

            this.PCmore1(false);

            setInformacao(Instrucao.ADD);
        }

        void sbAnimaSUB_ADD_Completed(object sender, EventArgs e)
        {
            this.txtZ.Text = this.status_Z.ToString();
            this.txtN.Text = this.status_N.ToString();
            this.txtACC.Text = this.ACC.ToString();
            this.getNovaInstrucao();
            
        }
        #endregion

        //==========================================================
        #region //ADDI
        //==========================================================
        /// <summary>
        ///ADDI - Add Immediate : O valor do registrador ACC é adicionado com o valor do operando imediato 
        ///e o resultado é armazenado no ACC
        /// </summary>
        /// <param name="value">operando imediato</param>
        public void ADDI(int value)
        {
            this.instrucaoAtual = Instrucao.ADDI.ToString();

            try
            {
                rInstrucao.Text = "ADDI " + value;
                rOpcode.Text = "ADDI";
                

                //value = Convert.ToInt32(this.txtTeste.Text);
                this.lblMore.Content = "+";

                this.rImediato.Text = value.ToString();
                
                this.rAcumulador_out.Text = this.ACC.ToString();
                this.rAcumulador_In.Text = (this.ACC + value).ToString();
                this.ACC = (this.ACC + value);

                //seta registrador Status
                this.atualizaStatus();
                if (this.Processor >= 2)
                {
                    this.lblN.Visibility = Visibility.Visible;
                    this.lblN.Content = this.status_N.ToString();
                }

                rImediato.Background = Brushes.Khaki;
                rAcumulador_In.Background = Brushes.Khaki;
                rAcumulador_out.Background = Brushes.Khaki;

                Storyboard sbAnimaADDI = (Storyboard)FindResource("SUBI_ADDI");

                sbAnimaADDI.Completed -= new EventHandler(sbAnimaSUBI_ADDI_Completed);
                sbAnimaADDI.Completed += new EventHandler(sbAnimaSUBI_ADDI_Completed);


                sbAnimaADDI.SpeedRatio = this.Speed;
                sbAnimaADDI.Begin(this,true);

                this.PCmore1(false);
                setInformacao(Instrucao.ADDI);
            }
            catch { }
        }

        //void sbAnimaADDI_Completed(object sender, EventArgs e)
        //{
        //    this.getNovaInstrucao();
        //}
        #endregion

        //==========================================================
        #region AND
        //==========================================================
        /// <summary>
        /// ADD : Operação de 'E' entre ACC e o valor armazenado na posição  
        /// de memória indicado pelo campo operand é efetuado e o resultado armazenado no ACC.
        /// </summary>
        public void AND(int position)
        {
            this.instrucaoAtual = Instrucao.AND.ToString();
            rOperando.Background = Brushes.PaleGreen;
            rInstrucao.Text = "AND " + position;
            rOpcode.Text = "AND";
            rOperando.Text = position.ToString();
            this.lblMore.Content = "&";
            int Valor = this.getValorMemoria(position);
            this.rRegistrador.Text = Valor.ToString();
            rRegistrador.Background = Brushes.Khaki;
            //this.rRegistrador.Text = 2.ToString();
            this.rAcumulador_out.Text = this.ACC.ToString();
            this.rAcumulador_In.Text = (this.ACC & Valor).ToString();
            this.ACC = (this.ACC & Valor);

            //seta registrador Status
            this.atualizaStatus();
            if (this.Processor >= 2)
            {
                this.lblN.Visibility = Visibility.Visible;
                this.lblN.Content = this.status_N.ToString();
            }

            rAcumulador_out.Background = Brushes.Khaki;
            rAcumulador_In.Background = Brushes.Khaki;

            Storyboard sbAnimaAND = (Storyboard)FindResource("SUB_ADD");

            sbAnimaAND.Completed -= new EventHandler(sbAnimaSUB_ADD_Completed);
            sbAnimaAND.Completed += new EventHandler(sbAnimaSUB_ADD_Completed);

            sbAnimaAND.SpeedRatio = this.Speed + 0.5;

            sbAnimaAND.Begin(this,true);

            this.PCmore1(false);

            setInformacao(Instrucao.AND);
        }

        #endregion

        //==========================================================
        #region OR
        //==========================================================
        /// <summary>
        /// ADD : Operação de 'OU' entre ACC e o valor armazenado na posição  
        /// de memória indicado pelo campo operand é efetuado e o resultado armazenado no ACC.
        /// </summary>
        public void OR(int position)
        {
            this.instrucaoAtual = Instrucao.OR.ToString();
            rOperando.Background = Brushes.PaleGreen;
            rInstrucao.Text = "OR " + position;
            rOpcode.Text = "OR";
            rOperando.Text = position.ToString();
            this.lblMore.Content = "|";
            int Valor = this.getValorMemoria(position);
            this.rRegistrador.Text = Valor.ToString();
            rRegistrador.Background = Brushes.Khaki;
            //this.rRegistrador.Text = 2.ToString();
            this.rAcumulador_out.Text = this.ACC.ToString();
            this.rAcumulador_In.Text = (this.ACC | Valor).ToString();
            this.ACC = (this.ACC | Valor);

            //seta registrador Status
            this.atualizaStatus();
            if (this.Processor >= 2)
            {
                this.lblN.Visibility = Visibility.Visible;
                this.lblN.Content = this.status_N.ToString();
            }

            rAcumulador_out.Background = Brushes.Khaki;
            rAcumulador_In.Background = Brushes.Khaki;

            Storyboard sbAnimaOR = (Storyboard)FindResource("SUB_ADD");

            sbAnimaOR.Completed -= new EventHandler(sbAnimaSUB_ADD_Completed);
            sbAnimaOR.Completed += new EventHandler(sbAnimaSUB_ADD_Completed);

            sbAnimaOR.SpeedRatio = this.Speed + 0.5;

            sbAnimaOR.Begin(this,true);

            this.PCmore1(false);

            setInformacao(Instrucao.OR);
        }

        #endregion

        //==========================================================
        #region XOR
        //==========================================================
        /// <summary>
        /// ADD : Operação de 'OU Exclusiva' entre ACC e o valor armazenado na posição  
        /// de memória indicado pelo campo operand é efetuado e o resultado armazenado no ACC.
        /// </summary>
        public void XOR(int position)
        {
            this.instrucaoAtual = Instrucao.XOR.ToString();
            rOperando.Background = Brushes.PaleGreen;
            rInstrucao.Text = "XOR " + position;
            rOpcode.Text = "XOR";
            rOperando.Text = position.ToString();
            this.lblMore.Content = "^";
            int Valor = this.getValorMemoria(position);
            this.rRegistrador.Text = Valor.ToString();
            rRegistrador.Background = Brushes.Khaki;
            //this.rRegistrador.Text = 2.ToString();
            this.rAcumulador_out.Text = this.ACC.ToString();
            this.rAcumulador_In.Text = (this.ACC ^ Valor).ToString();
            this.ACC = (this.ACC ^ Valor);

            //seta registrador Status
            this.atualizaStatus();
            if (this.Processor >= 2)
            {
                this.lblN.Visibility = Visibility.Visible;
                this.lblN.Content = this.status_N.ToString();
            }

            rAcumulador_out.Background = Brushes.Khaki;
            rAcumulador_In.Background = Brushes.Khaki;

            Storyboard sbAnimaXOR = (Storyboard)FindResource("SUB_ADD");

            sbAnimaXOR.Completed -= new EventHandler(sbAnimaSUB_ADD_Completed);
            sbAnimaXOR.Completed += new EventHandler(sbAnimaSUB_ADD_Completed);

            sbAnimaXOR.SpeedRatio = this.Speed + 0.5;

            sbAnimaXOR.Begin(this,true);

            this.PCmore1(false);

            setInformacao(Instrucao.XOR);
        }

        #endregion

        //==========================================================
        #region NOT
        //==========================================================
        /// <summary>
        /// ADD : Operação de 'NOT' no ACC. O resultado armazenado no ACC.
        /// </summary>
        public void NOT()
        {
            this.instrucaoAtual = Instrucao.NOT.ToString();
            rOperando.Background = Brushes.PaleGreen;
            rInstrucao.Text = "NOT";
            rOpcode.Text = "NOT";
            this.lblMore.Content = "!";
            rRegistrador.Background = Brushes.Khaki;
            //this.rRegistrador.Text = 2.ToString();
            this.rAcumulador_out.Text = this.ACC.ToString();
            this.rAcumulador_In.Text = (~this.ACC).ToString();
            this.ACC = (~this.ACC);

            //seta registrador Status
            this.atualizaStatus();
            if (this.Processor >= 2)
            {
                this.lblN.Visibility = Visibility.Visible;
                this.lblN.Content = this.status_N.ToString();
            }

            rAcumulador_out.Background = Brushes.Khaki;
            rAcumulador_In.Background = Brushes.Khaki;

            Storyboard sbAnimaNOT = (Storyboard)FindResource("NOT");

            sbAnimaNOT.Completed -= new EventHandler(sbAnimaNOT_Completed);
            sbAnimaNOT.Completed += new EventHandler(sbAnimaNOT_Completed);

            sbAnimaNOT.SpeedRatio = this.Speed + 0.5;

            sbAnimaNOT.Begin(this,true);

            this.PCmore1(false);

            setInformacao(Instrucao.NOT);
        }
        void sbAnimaNOT_Completed(object sender, EventArgs e)
        {
            this.txtACC.Text = this.ACC.ToString();
            this.txtZ.Text = this.status_Z.ToString();
            this.txtN.Text = this.status_N.ToString();
            this.getNovaInstrucao();
        }
        #endregion

        //==========================================================
        #region //ANDI
        //==========================================================
        /// <summary>
        ///ANDI - AND Immediate : Operação de 'E' lógica entre o acc e o valor do operando imediato 
        ///e o resultado é armazenado no ACC
        /// </summary>
        /// <param name="value">operando imediato</param>
        public void ANDI(int value)
        {
            this.instrucaoAtual = Instrucao.ANDI.ToString();

            try
            {
                rInstrucao.Text = "ANDI " + value;
                rOpcode.Text = "ANDI";


                //value = Convert.ToInt32(this.txtTeste.Text);
                this.lblMore.Content = "&";

                this.rImediato.Text = value.ToString();

                this.rAcumulador_out.Text = this.ACC.ToString();
                this.rAcumulador_In.Text = (this.ACC & value).ToString();
                this.ACC = (this.ACC & value);

                //seta registrador Status
                this.atualizaStatus();
                if (this.Processor >= 2)
                {
                    this.lblN.Visibility = Visibility.Visible;
                    this.lblN.Content = this.status_N.ToString();
                }

                rImediato.Background = Brushes.Khaki;
                rAcumulador_In.Background = Brushes.Khaki;
                rAcumulador_out.Background = Brushes.Khaki;

                Storyboard sbAnimaANDI = (Storyboard)FindResource("SUBI_ADDI");

                sbAnimaANDI.Completed -= new EventHandler(sbAnimaSUBI_ADDI_Completed);
                sbAnimaANDI.Completed += new EventHandler(sbAnimaSUBI_ADDI_Completed);


                sbAnimaANDI.SpeedRatio = this.Speed;
                sbAnimaANDI.Begin(this,true);

                this.PCmore1(false);
                setInformacao(Instrucao.ANDI);
            }
            catch { }
        }

        //void sbAnimaADDI_Completed(object sender, EventArgs e)
        //{
        //    this.getNovaInstrucao();
        //}
        #endregion

        //==========================================================
        #region //ORI
        //==========================================================
        /// <summary>
        ///ORI - OR Immediate : Operação de OU entre o ACC e o valor do operando imediato 
        ///e o resultado é armazenado no ACC
        /// </summary>
        /// <param name="value">operando imediato</param>
        public void ORI(int value)
        {
            this.instrucaoAtual = Instrucao.ORI.ToString();

            try
            {
                rInstrucao.Text = "ORI " + value;
                rOpcode.Text = "ORI";


                //value = Convert.ToInt32(this.txtTeste.Text);
                this.lblMore.Content = "|";

                this.rImediato.Text = value.ToString();

                this.rAcumulador_out.Text = this.ACC.ToString();
                this.rAcumulador_In.Text = (this.ACC | value).ToString();
                this.ACC = (this.ACC | value);

                //seta registrador Status
                this.atualizaStatus();
                if (this.Processor >= 2)
                {
                    this.lblN.Visibility = Visibility.Visible;
                    this.lblN.Content = this.status_N.ToString();
                }

                rImediato.Background = Brushes.Khaki;
                rAcumulador_In.Background = Brushes.Khaki;
                rAcumulador_out.Background = Brushes.Khaki;

                Storyboard sbAnimaORI = (Storyboard)FindResource("SUBI_ADDI");

                sbAnimaORI.Completed -= new EventHandler(sbAnimaSUBI_ADDI_Completed);
                sbAnimaORI.Completed += new EventHandler(sbAnimaSUBI_ADDI_Completed);


                sbAnimaORI.SpeedRatio = this.Speed;
                sbAnimaORI.Begin(this,true);

                this.PCmore1(false);
                setInformacao(Instrucao.ORI);
            }
            catch { }
        }

        //void sbAnimaADDI_Completed(object sender, EventArgs e)
        //{
        //    this.getNovaInstrucao();
        //}
        #endregion

        //==========================================================
        #region //XORI
        //==========================================================
        /// <summary>
        ///XORI - XOR Immediate : Operação de OU exclusiva entre ACC e o valor do operando imediato 
        ///e o resultado é armazenado no ACC
        /// </summary>
        /// <param name="value">operando imediato</param>
        public void XORI(int value)
        {
            this.instrucaoAtual = Instrucao.XORI.ToString();

            try
            {
                rInstrucao.Text = "XORI " + value;
                rOpcode.Text = "XORI";


                //value = Convert.ToInt32(this.txtTeste.Text);
                this.lblMore.Content = "^";

                this.rImediato.Text = value.ToString();

                this.rAcumulador_out.Text = this.ACC.ToString();
                this.rAcumulador_In.Text = (this.ACC ^ value).ToString();
                this.ACC = (this.ACC ^ value);

                //seta registrador Status
                this.atualizaStatus();
                if (this.Processor >= 2)
                {
                    this.lblN.Visibility = Visibility.Visible;
                    this.lblN.Content = this.status_N.ToString();
                }

                rImediato.Background = Brushes.Khaki;
                rAcumulador_In.Background = Brushes.Khaki;
                rAcumulador_out.Background = Brushes.Khaki;

                Storyboard sbAnimaXORI = (Storyboard)FindResource("SUBI_ADDI");

                sbAnimaXORI.Completed -= new EventHandler(sbAnimaSUBI_ADDI_Completed);
                sbAnimaXORI.Completed += new EventHandler(sbAnimaSUBI_ADDI_Completed);


                sbAnimaXORI.SpeedRatio = this.Speed;
                sbAnimaXORI.Begin(this,true);

                this.PCmore1(false);
                setInformacao(Instrucao.XORI);
            }
            catch { }
        }

        #endregion

        //==========================================================
        #region //SLL
        //==========================================================
        /// <summary>
        ///SLL - SLL : Operação de deslocamento do acc pelo valor do operando imediato 
        ///e o resultado é armazenado no ACC
        /// </summary>
        /// <param name="value">operando imediato</param>
        public void SLL(int value)
        {
            this.instrucaoAtual = Instrucao.SLL.ToString();

            try
            {
                rInstrucao.Text = "SLL " + value;
                rOpcode.Text = "SLL";


                //value = Convert.ToInt32(this.txtTeste.Text);
                this.lblMore.Content = "<<";

                this.rImediato.Text = value.ToString();

                this.rAcumulador_out.Text = this.ACC.ToString();
                this.rAcumulador_In.Text = (this.ACC << value).ToString();
                this.ACC = (this.ACC << value);

                //seta registrador Status
                this.atualizaStatus();
                if (this.Processor >= 2)
                {
                    this.lblN.Visibility = Visibility.Visible;
                    this.lblN.Content = this.status_N.ToString();
                }

                rImediato.Background = Brushes.Khaki;
                rAcumulador_In.Background = Brushes.Khaki;
                rAcumulador_out.Background = Brushes.Khaki;

                Storyboard sbAnimaSLL= (Storyboard)FindResource("SUBI_ADDI");

                sbAnimaSLL.Completed -= new EventHandler(sbAnimaSUBI_ADDI_Completed);
                sbAnimaSLL.Completed += new EventHandler(sbAnimaSUBI_ADDI_Completed);


                sbAnimaSLL.SpeedRatio = this.Speed;
                sbAnimaSLL.Begin(this,true);

                this.PCmore1(false);
                setInformacao(Instrucao.SLL);
            }
            catch { }
        }

        #endregion

        //==========================================================
        #region //SRL
        //==========================================================
        /// <summary>
        ///SLL - SLL : Operação de deslocamento do acc pelo valor do operando imediato 
        ///e o resultado é armazenado no ACC
        /// </summary>
        /// <param name="value">operando imediato</param>
        public void SRL(int value)
        {
            this.instrucaoAtual = Instrucao.SLL.ToString();

            try
            {
                rInstrucao.Text = "SRL " + value;
                rOpcode.Text = "SRL";


                //value = Convert.ToInt32(this.txtTeste.Text);
                this.lblMore.Content = ">>";

                this.rImediato.Text = value.ToString();

                this.rAcumulador_out.Text = this.ACC.ToString();
                this.rAcumulador_In.Text = (this.ACC >> value).ToString();
                this.ACC = (this.ACC >> value);

                //seta registrador Status
                this.atualizaStatus();
                if (this.Processor >= 2)
                {
                    this.lblN.Visibility = Visibility.Visible;
                    this.lblN.Content = this.status_N.ToString();
                }

                rImediato.Background = Brushes.Khaki;
                rAcumulador_In.Background = Brushes.Khaki;
                rAcumulador_out.Background = Brushes.Khaki;

                Storyboard sbAnimaSRL = (Storyboard)FindResource("SUBI_ADDI");

                sbAnimaSRL.Completed -= new EventHandler(sbAnimaSUBI_ADDI_Completed);
                sbAnimaSRL.Completed += new EventHandler(sbAnimaSUBI_ADDI_Completed);


                sbAnimaSRL.SpeedRatio = this.Speed;
                sbAnimaSRL.Begin(this,true);

                this.PCmore1(false);
                setInformacao(Instrucao.SRL);
            }
            catch { }
        }

        #endregion

        //==========================================================
        #region //LD
        //==========================================================
        /// <summary>
        /// LD - Load : Carrega um valor armazenado em uma posição de memória indicado pelo operand 
        /// para o registrador ACC
        /// </summary>
        public void LD(int position)
        {
            string resource = "LD";
            if (position == 1024 && !this.entradaOK && this.breakpoint)
            {
                this.DestacaGridPrograma(this._PC.ToString());
                this.SetEndereco(this._PC);
                sbAnimaRealceES = (Storyboard)FindResource("realce_entrada");
                sbAnimaRealceES.Completed -= new EventHandler(sbAnimaRealceEntrada_Completed);
                sbAnimaRealceES.Completed += new EventHandler(sbAnimaRealceEntrada_Completed);

                sbAnimaRealceES.SpeedRatio = this.Speed;
                sbAnimaRealceES.DecelerationRatio = 0.2;

                this.Pause();
                sbAnimaRealceES.Begin(this,true);

                this.Pause();
                this.entradaOK = true;
                this.RequestPausePrograma();
                return;
            }
            this.setEntrada();
            this.entradaOK = false;
            this.instrucaoAtual = Instrucao.LD.ToString();
            if (position == 1024)
                resource = "LD_INPORT";
            this.rImediato.Text = "imediato";
            this.rInstrucao.Text = "LD "+ position;
            rOperando.Text = position.ToString();
            rOperando.Background = Brushes.PaleGreen;
            rOpcode.Text = "LD ";
            this.rLDI.Text = "ldi";
            this.rRegistrador.Text = "registrador";

            this.rAcumulador_In.Text = this.ACC.ToString();
            
            int Valor = this.getValorMemoria(position);
            this.rRegistrador.Text = Valor.ToString();
            rRegistrador.Background = Brushes.Khaki;
            this.ACC = Valor;
            this.rAcumulador_out.Text = this.ACC.ToString();

            Storyboard sbAnimaLD = (Storyboard)FindResource(resource);

            sbAnimaLD.Completed -= new EventHandler(sbAnimaLD_Completed);
            sbAnimaLD.Completed += new EventHandler(sbAnimaLD_Completed);

            sbAnimaLD.SpeedRatio = this.Speed;
            sbAnimaLD.DecelerationRatio = 0.2;
            sbAnimaLD.Begin(this,true);

            this.PCmore1(false);

            setInformacao(Instrucao.LD);
            //blocoComentario.Text = "Buscando valor do endereço " + position.ToString() + " da memória de Dados e armazenando no acumulador";

        }

        void sbAnimaLD_Completed(object sender, EventArgs e)
        {
            this.txtACC.Text = this.ACC.ToString();
            this.getNovaInstrucao();
        }
        void sbAnimaRealceEntrada_Completed(object sender, EventArgs e)
        {
            if (isPause)
                sbAnimaRealceES.Begin(this,true);
        }
        #endregion

        //==========================================================
        #region //LDV
        //==========================================================
        /// <summary>
        /// LDV - Load : Carrega um valor armazenado em uma posição de memória indicado pelo operand somado ao registrador INDR
        /// para o registrador ACC
        /// </summary>
        public void LDV(int position)
        {
            try
            {
                this.verificaAcessoMemoria(position, this._indr);
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message, "Violação de acesso!");
                this.Pause();
                this.RequestPausePrograma();
                return;
            }
            string resource = "LDV";
            this.setEntrada();
            this.instrucaoAtual = Instrucao.LDV.ToString();
            this.rImediato.Text = "imediato";
            position += this._indr;
            this.rInstrucao.Text = "LDV " + position;
            rOperando.Text = position.ToString();
            rOperando.Background = Brushes.PaleGreen;
            rOpcode.Text = "LDV ";
            this.rLDI.Text = "ldi";
            this.rRegistrador.Text = "registrador";

            this.rAcumulador_In.Text = this.ACC.ToString();


            int Valor = this.getValorMemoria(position);
            this.rRegistrador.Text = Valor.ToString();
            rRegistrador.Background = Brushes.Khaki;
            this.ACC = Valor;
            this.rAcumulador_out.Text = this.ACC.ToString();

            Storyboard sbAnimaLDV = (Storyboard)FindResource(resource);

            sbAnimaLDV.Completed -= new EventHandler(sbAnimaLDV_Completed);
            sbAnimaLDV.Completed += new EventHandler(sbAnimaLDV_Completed);

            sbAnimaLDV.SpeedRatio = this.Speed;
            sbAnimaLDV.DecelerationRatio = 0.2;
            sbAnimaLDV.Begin(this,true);

            this.PCmore1(false);

            setInformacao(Instrucao.LDV);
            //blocoComentario.Text = "Buscando valor do endereço " + position.ToString() + " da memória de Dados e armazenando no acumulador";

        }

        void sbAnimaLDV_Completed(object sender, EventArgs e)
        {
            this.txtACC.Text = this.ACC.ToString();
            this.getNovaInstrucao();
        }
        #endregion

        //==========================================================
        #region //STO
        //==========================================================
        /// <summary>
        /// STO - Store : Armazena o conteúdo do ACC em uma posição da memória indicada pelo operand
        /// </summary>
        public void STO(int position)
        {
            string resource = "STO";
            if (position == 1025)
                resource = "STO_OUTPORT";
            if (position == 1073)
                resource = "STO_INDR";
            this.instrucaoAtual = Instrucao.STO.ToString();

            rAcumulador_out.Background = Brushes.Khaki;
            this.rAcumulador_out.Text = this.ACC.ToString();

            rOperando.Text = position.ToString();
            rOperando.Background = Brushes.PaleGreen;
            rOpcode.Text = "STO";
            rInstrucao.Text = "STO " + position;

            Storyboard sbAnimaSTO = (Storyboard)FindResource(resource);

            sbAnimaSTO.Completed -= new EventHandler(sbAnimaSTO_Completed);
            sbAnimaSTO.Completed += new EventHandler(sbAnimaSTO_Completed);
            
            sbAnimaSTO.SpeedRatio = this.Speed;
            sbAnimaSTO.Begin(this,true);


            
            //reposiciona scroll
            gridMemoDados.SelectedIndex = position;

            gridMemoDados.ScrollIntoView(gridMemoDados.SelectedItem);
            //ListViewItem lvi = (ListViewItem)gridMemoDados.ItemContainerGenerator.ContainerFromIndex(gridMemoDados.SelectedIndex);
            //lvi.Focus();

            //altera cor de fundo do item selecionado

            foreach (ListViewItem list in gridMemoDados.Items)
            {
                list.Background = Brushes.Transparent;

            }
            if (gridMemoDados.SelectedItem != null)
            {
                ((ListViewItem)(gridMemoDados.SelectedItem)).Background = Brushes.SandyBrown;
            }
            //------------------------------------------------  COLOCAR ESSE TRECHO NO FIM DA ANIMACAO PARA ARMAZENAR DADO AO FINAL DA ANIMAÇÃO DE STO
            //THIS.STOAUX = POSITION
            //NO TRECHO ABAIXO SUBSTITUIR POSITION POR STOAUX
            //salva em variavel auxiliar
            if (position < 1024)
            {
                //armazena no endereço de memória
                ListViewItem a = (ListViewItem)gridMemoDados.Items[position];
                string var = ((string[])(a.Content))[2].ToString();

                ((ListViewItem)(gridMemoDados.Items.GetItemAt(position))).Content = new String[] { position.ToString(), (this.ACC.ToString()).ToString(), var };// this.ACC.ToString();

            }
            else
            {
                //armazena no registrador
                switch (position)
                {
                    case 1025:
                    this._outport = this.ACC;
                    break;
                    case 1073:
                    this._indr = this.ACC;
                    this.rINDR.Text = "+"+this._indr.ToString();
                    break;
                }
            }
            //-----------------------------------------------
            
            this.PCmore1(false);
            setInformacao(Instrucao.STO);
        }



        void sbAnimaSTO_Completed(object sender, EventArgs e)
        {
            this.INDR.Text = _indr.ToString();
            this.txtACC.Text = this.ACC.ToString();
            this.AccToOutPort();
//            this.LimpaCorGridDados();
            this.getNovaInstrucao();
        }
        #endregion

        //==========================================================
        #region //STOV
        //==========================================================
        /// <summary>
        /// STOV - Store : Armazena o conteúdo do ACC em uma posição da memória indicada pelo operand + registrador INDR
        /// </summary>
        public void STOV(int position)
        {
            try
            {
                this.verificaAcessoMemoria(position, this._indr);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Violação de acesso!");
                this.Pause();
                this.RequestPausePrograma();
                return;
            }
            string resource = "STOV";
            this.instrucaoAtual = Instrucao.STOV.ToString();

            rAcumulador_out.Background = Brushes.Khaki;
            this.rAcumulador_out.Text = this.ACC.ToString();
            position += this._indr;
            
            rOperando.Text = position.ToString();
            rOperando.Background = Brushes.PaleGreen;
            rOpcode.Text = "STOV";
            rInstrucao.Text = "STOV " + position;

            Storyboard sbAnimaSTOV = (Storyboard)FindResource(resource);

            sbAnimaSTOV.Completed -= new EventHandler(sbAnimaSTOV_Completed);
            sbAnimaSTOV.Completed += new EventHandler(sbAnimaSTOV_Completed);

            sbAnimaSTOV.SpeedRatio = this.Speed;
            sbAnimaSTOV.Begin(this,true);



            //reposiciona scroll
            gridMemoDados.SelectedIndex = position;

            gridMemoDados.ScrollIntoView(gridMemoDados.SelectedItem);
            //ListViewItem lvi = (ListViewItem)gridMemoDados.ItemContainerGenerator.ContainerFromIndex(gridMemoDados.SelectedIndex);
            //lvi.Focus();

            //altera cor de fundo do item selecionado

            foreach (ListViewItem list in gridMemoDados.Items)
            {
                list.Background = Brushes.Transparent;

            }
            if (gridMemoDados.SelectedItem != null)
            {
                ((ListViewItem)(gridMemoDados.SelectedItem)).Background = Brushes.SandyBrown;
            }
            //------------------------------------------------  COLOCAR ESSE TRECHO NO FIM DA ANIMACAO PARA ARMAZENAR DADO AO FINAL DA ANIMAÇÃO DE STO
            //THIS.STOAUX = POSITION
            //NO TRECHO ABAIXO SUBSTITUIR POSITION POR STOAUX
            //salva em variavel auxiliar
            if (position < 1024)
            {
                //armazena no endereço de memória
                ListViewItem a = (ListViewItem)gridMemoDados.Items[position];
                string var = ((string[])(a.Content))[2].ToString();

                ((ListViewItem)(gridMemoDados.Items.GetItemAt(position))).Content = new String[] { position.ToString(), (this.ACC.ToString()).ToString(), var };// this.ACC.ToString();

            }
            else
            {
                //tratar violação de memória
                
            }
            //-----------------------------------------------

            this.PCmore1(false);
            setInformacao(Instrucao.STOV);
        }



        void sbAnimaSTOV_Completed(object sender, EventArgs e)
        {
            this.txtACC.Text = this.ACC.ToString();
            this.AccToOutPort();
            //            this.LimpaCorGridDados();
            this.getNovaInstrucao();
        }
        #endregion

        //==========================================================
        #region //BEQ
        //==========================================================
        /// <summary>
        /// BEQ - Branch Equal : Atualiza o valor do PC com o valor do campo operand caso o resultado da operação anterior na ULA foi zero
        /// </summary>
        public void BEQ(int operand)
        {
            this.instrucaoAtual = Instrucao.BEQ.ToString();
            if (this.lblZ.Content.ToString() == "1")
            {
                this.PCOperand(operand.ToString());
            }
            else
            {
                //Pc +1
                this.PCmore1(true);
            }
            setInformacao(Instrucao.BEQ);
            
        }
        #endregion

        //==========================================================
        #region //BNE
        //==========================================================
        /// <summary>
        /// BNE - Branch Non-Equal : Atualiza o valor do PC com o valor do campo operand caso o resultado da operação anterior na ULA tenha sido diferente de zero.
        /// </summary>
        public void BNE(int operand)
        {
            this.instrucaoAtual = Instrucao.BNE.ToString();
            if (this.lblZ.Content.ToString() == "0")
            {
                this.PCOperand(operand.ToString());
            }
            else
            {
                //PC + 1
                this.PCmore1(true);
            }
            setInformacao(Instrucao.BNE);
           
        }
        #endregion

        //==========================================================
        #region //BGT
        //==========================================================
        /// <summary>
        /// BGT - Branch Greater Than : Atualiza o valor do PC com o valor do campo operand caso o resultado da operação anterior na ULA tenha sido maior que zero.
        /// </summary>
        public void BGT(int operand)
        {
            this.instrucaoAtual = Instrucao.BGT.ToString();
            if (this.lblZ.Content.ToString() == "0" && this.lblN.Content.ToString() == "0" )
            {
                this.PCOperand(operand.ToString());
            }
            else
            {
                //PC + 1
                this.PCmore1(true);
            }
            setInformacao(Instrucao.BGT);
            
        }
        #endregion

        //==========================================================
        #region //BGE
        //==========================================================
        /// <summary>
        /// BGE - Branch Greater Equal : Atualiza o valor do PC com o valor do campo operand caso o resultado da operação anterior na ULA tenha sido maior ou igual zero.
        /// </summary>
        public void BGE(int operand)
        {
            this.instrucaoAtual = Instrucao.BGE.ToString();
            if ( this.lblN.Content.ToString() == "0")
            {
                this.PCOperand(operand.ToString());
            }
            else
            {
                //PC + 1
                this.PCmore1(true);
            }
            setInformacao(Instrucao.BGE);
            
        }
        #endregion

        //==========================================================
        #region //BLT
        //==========================================================
        /// <summary>
        /// BLT - Branch Less Than : Atualiza o valor do PC com o valor do campo operand caso o resultado da operação anterior na ULA tenha sido menor que zero.
        /// </summary>
        public void BLT(int operand)
        {
            this.instrucaoAtual = Instrucao.BLT.ToString();
            if (this.lblN.Content.ToString() == "1")
            {
                this.PCOperand(operand.ToString());
            }
            else
            {
                //PC + 1
                this.PCmore1(true);
            }

            setInformacao(Instrucao.BLT);
            
        }
        #endregion

        //==========================================================
        #region //BLE
        //==========================================================
        /// <summary>
        /// BLE - Branch Less Equal : Atualiza o valor do PC com o valor do campo operand caso o resultado da operação anterior na ULA tenha sido menor ou igual a zero.
        /// </summary>
        public void BLE(int operand)
        {
            this.instrucaoAtual = Instrucao.BLE.ToString();
            if (this.lblZ.Content.ToString() == "1" || this.lblN.Content.ToString() == "1")
            {
                this.PCOperand(operand.ToString());
            }
            else
            {
                //PC + 1
                this.PCmore1(true);
            }
            setInformacao(Instrucao.BLE);
            
        }
        #endregion

        //==========================================================
        #region //JMP
        //==========================================================
        /// <summary>
        /// JMP - Jump - Atualiza o valor do PC com o valor do campo operand, ou seja, realiza um desvio incondicional
        /// </summary>
        public void JMP(int operand)
        {
            this.instrucaoAtual = Instrucao.JMP.ToString();

            this.PCOperand(operand.ToString());
            
            setInformacao(Instrucao.JMP);
        }
        #endregion

        //==========================================================
        #region PCmore1
        /// <summary>
        /// Executa animação de atualização do PC, incrementado-o em 1 unidade
        /// </summary>
        /// <param name="desvio">indica se atualizaçao ocorreu durante uma instrução de desvio </param>
        public void PCmore1(bool desvio){
            this.rPC1.Text = "1";
            this.rPC.Text = this._PC.ToString() ;
            try
            {
                this._PC += 1;
            }
            catch { }


            if (exibeAtualizaPC || desvio)
            {
                this.rPC_in.Text = this._PC.ToString();
                //this.ACC = Convert.ToInt32(this.rRegistrador.Text);
                //this.rAcumulador_out.Text = this.ACC.ToString();

                Storyboard sbAnimaPC = (Storyboard)FindResource("PC+1");
                sbAnimaPC.Completed -= new EventHandler(sbAnimaPC_Completed);
                sbAnimaPC.Completed -= new EventHandler(sbAnimaPCmore1_Completed);
                if (desvio)
                    sbAnimaPC.Completed += new EventHandler(sbAnimaPC_Completed);
                else
                    sbAnimaPC.Completed += new EventHandler(sbAnimaPCmore1_Completed);

                sbAnimaPC.SpeedRatio = this.Speed;
                sbAnimaPC.Begin(this,true);
            }
            else
            {
                this.rPC_in.Text = (this._PC - 1).ToString();
                this.txtPC.Text = (this._PC - 1).ToString();
            }
             
        }
        void sbAnimaPCmore1_Completed(object sender, EventArgs e)
        {
            this.txtPC.Text = this._PC.ToString();
        }
        #endregion

        //==========================================================
        #region PCOperand
        /// <summary>
        /// Executa animação de uma instrução de Desvio
        /// </summary>
        /// <param name="operand">operando - endereço da proxima instrução a ser executada</param>
        public void PCOperand(string operand)
        {

            this.rPC_in.Text = this._PC.ToString();
            this.rPC_operand.Text = operand.ToString();

            rInstrucao.Text =  this.instrucaoAtual + " " + operand.ToString();
            rOpcode.Text = this.instrucaoAtual;

            rPC.Text = this._PC.ToString();
            this.rPC_in.Text = (this._PC + 1).ToString();
            //this.rPC.Text = this._PC.ToString();
            try
            {
                this._PC = Convert.ToInt32(operand);
            }
            catch
            {
                //buscar endereço na lista de rotulos
                
            }
            this.rPC1.Text = "1";
           
            //this.rPC_in.Text = this._PC.ToString();
            ////this.ACC = Convert.ToInt32(this.rRegistrador.Text);
            ////this.rAcumulador_out.Text = this.ACC.ToString();

            Storyboard sbAnimaPC = (Storyboard)FindResource("PC_operand");

            sbAnimaPC.Completed -= new EventHandler(sbAnimaPC_Completed);
            sbAnimaPC.Completed += new EventHandler(sbAnimaPC_Completed);

            sbAnimaPC.SpeedRatio = this.Speed;
            sbAnimaPC.AccelerationRatio = 0.1;
            sbAnimaPC.Begin(this,true);
            
        }
        void sbAnimaPC_Completed(object sender, EventArgs e)
        {
            this.txtPC.Text = this._PC.ToString();
            this.getNovaInstrucao();
        }
        #endregion

        //==========================================================
        #region ACC ot OUT_PORT
        void AccToOutPort()
        {
            ArrayList ligado = new ArrayList();
            ArrayList desligado = new ArrayList();
            ligado.Add(this.out_port_1_on);
            ligado.Add(this.out_port_2_on);
            ligado.Add(this.out_port_3_on);
            ligado.Add(this.out_port_4_on);
            ligado.Add(this.out_port_5_on);
            ligado.Add(this.out_port_6_on);
            ligado.Add(this.out_port_7_on);
            ligado.Add(this.out_port_8_on);
            ligado.Add(this.out_port_9_on);
            ligado.Add(this.out_port_10_on);
            ligado.Add(this.out_port_11_on);
            ligado.Add(this.out_port_12_on);
            ligado.Add(this.out_port_13_on);
            ligado.Add(this.out_port_14_on);
            ligado.Add(this.out_port_15_on);
            ligado.Add(this.out_port_16_on);
            desligado.Add(this.out_port_1_off);
            desligado.Add(this.out_port_2_off);
            desligado.Add(this.out_port_3_off);
            desligado.Add(this.out_port_4_off);
            desligado.Add(this.out_port_5_off);
            desligado.Add(this.out_port_6_off);
            desligado.Add(this.out_port_7_off);
            desligado.Add(this.out_port_8_off);
            desligado.Add(this.out_port_9_off);
            desligado.Add(this.out_port_10_off);
            desligado.Add(this.out_port_11_off);
            desligado.Add(this.out_port_12_off);
            desligado.Add(this.out_port_13_off);
            desligado.Add(this.out_port_14_off);
            desligado.Add(this.out_port_15_off);
            desligado.Add(this.out_port_16_off);
            this.out_port.Text = this._outport.ToString();
            string binario = BIP.Montador.Funcoes.IntToBin(this._outport, 16);
            for (int x = 0; x < binario.Length;x++ )
            {
                if (binario.Substring(x, 1) == "1")
                {
                    ((System.Windows.Shapes.Ellipse)ligado[x]).Visibility = Visibility.Visible;
                    ((System.Windows.Shapes.Ellipse)desligado[x]).Visibility = Visibility.Hidden;
                }
                else
                {
                    ((System.Windows.Shapes.Ellipse)desligado[x]).Visibility = Visibility.Visible;
                    ((System.Windows.Shapes.Ellipse)ligado[x]).Visibility = Visibility.Hidden;
                }
            }
        }
        #endregion

        //==========================================================
        #region CALL
        void CALL(int operand)
        {
            this.instrucaoAtual = Instrucao.CALL.ToString();
            this.pilhaSubrotina.Push(this._PC + 1);
            this.rPC_in.Text = this._PC.ToString();
            this.rPC_operand.Text = operand.ToString();

            rInstrucao.Text = this.instrucaoAtual + " " + operand.ToString();
            rOpcode.Text = this.instrucaoAtual;

            rPC.Text = this._PC.ToString();
            this.rPC_in.Text = (this._PC + 1).ToString();
            //this.rPC.Text = this._PC.ToString();
            try
            {
                this._PC = Convert.ToInt32(operand);
            }
            catch
            {
                //buscar endereço na lista de rotulos

            }
            this.rPC1.Text = "1";

            //this.rPC_in.Text = this._PC.ToString();
            ////this.ACC = Convert.ToInt32(this.rRegistrador.Text);
            ////this.rAcumulador_out.Text = this.ACC.ToString();

            Storyboard sbAnimaPC = (Storyboard)FindResource("CALL");

            sbAnimaPC.Completed -= new EventHandler(sbAnimaCALL_Completed);
            sbAnimaPC.Completed += new EventHandler(sbAnimaCALL_Completed);

            sbAnimaPC.SpeedRatio = this.Speed;
            sbAnimaPC.AccelerationRatio = 0.1;
            sbAnimaPC.Begin(this,true);
            this.setInformacao(Instrucao.CALL);
            
        }
        void sbAnimaCALL_Completed(object sender, EventArgs e)
        {
            this.SP.Text = "0";
            if (this.pilhaSubrotina.Count > 0)
            {
                this.SP.Text = this.pilhaSubrotina.Peek().ToString();
            }
            this.txtPC.Text = this._PC.ToString();
            this.getNovaInstrucao();
        }
        #endregion

        //==========================================================
        #region RETURN
        void RETURN()
        {
            if (pilhaSubrotina.Count == 0)
                pilhaSubrotina.Push(0);
            string operand = pilhaSubrotina.Pop().ToString();
            this.instrucaoAtual = Instrucao.RETURN.ToString();
            this.rPC_in.Text = this._PC.ToString();
            this.rPC_operand.Text = operand.ToString();

            rInstrucao.Text = this.instrucaoAtual;
            rOpcode.Text = this.instrucaoAtual;

            rPC.Text = this._PC.ToString();
            this.rPC_in.Text = (this._PC + 1).ToString();
            //this.rPC.Text = this._PC.ToString();
            try
            {
                this._PC = Convert.ToInt32(operand);
            }
            catch
            {
                //buscar endereço na lista de rotulos

            }
            this.rPC1.Text = "1";

            //this.rPC_in.Text = this._PC.ToString();
            ////this.ACC = Convert.ToInt32(this.rRegistrador.Text);
            ////this.rAcumulador_out.Text = this.ACC.ToString();

            Storyboard sbAnimaPC = (Storyboard)FindResource("RETURN");

            sbAnimaPC.Completed -= new EventHandler(sbAnimaRETURN_Completed);
            sbAnimaPC.Completed += new EventHandler(sbAnimaRETURN_Completed);

            sbAnimaPC.SpeedRatio = this.Speed;
            sbAnimaPC.AccelerationRatio = 0.1;
            sbAnimaPC.Begin(this,true);
            this.setInformacao(Instrucao.CALL);
        }
        void sbAnimaRETURN_Completed(object sender, EventArgs e)
        {
            this.SP.Text = "0";
            if (this.pilhaSubrotina.Count > 0)
            {
                this.SP.Text = this.pilhaSubrotina.Peek().ToString();
            }
            this.txtPC.Text = this._PC.ToString();
            this.getNovaInstrucao();
        }

        #endregion

        //==========================================================
        #region liga_desliga botões de entrada
        void botoesEntrada()
        {
            #region adiciona botoes na lista
            List<Viewbox> botoes = new List<Viewbox>();
            List<System.Windows.Shapes.Path> ledsChave = new List<System.Windows.Shapes.Path>();
            botoes.Add(this.chave_1);
            botoes.Add(this.chave_2);
            botoes.Add(this.chave_3);
            botoes.Add(this.chave_4);
            botoes.Add(this.chave_5);
            botoes.Add(this.chave_6);
            botoes.Add(this.chave_7);
            botoes.Add(this.chave_8);
            botoes.Add(this.chave_9);
            botoes.Add(this.chave_10);
            botoes.Add(this.chave_11);
            botoes.Add(this.chave_12);
            botoes.Add(this.chave_13);
            botoes.Add(this.chave_14);
            botoes.Add(this.chave_15);
            botoes.Add(this.chave_16);
            ledsChave.Add(this.led_chave_1);
            ledsChave.Add(this.led_chave_2);
            ledsChave.Add(this.led_chave_3);
            ledsChave.Add(this.led_chave_4);
            ledsChave.Add(this.led_chave_5);
            ledsChave.Add(this.led_chave_6);
            ledsChave.Add(this.led_chave_7);
            ledsChave.Add(this.led_chave_8);
            ledsChave.Add(this.led_chave_9);
            ledsChave.Add(this.led_chave_10);
            ledsChave.Add(this.led_chave_11);
            ledsChave.Add(this.led_chave_12);
            ledsChave.Add(this.led_chave_13);
            ledsChave.Add(this.led_chave_14);
            ledsChave.Add(this.led_chave_15);
            ledsChave.Add(this.led_chave_16);
            #endregion
            for (int x = 0; x < 16; x++)
            {
                int y_scale = 1;
                if (this._inportBits[x])
                {
                    y_scale = -1;
                    try
                    {
                        ledsChave[x].Visibility = Visibility.Visible;
                    }
                    catch { }
                }
                else
                {
                    try
                    {
                        ledsChave[x].Visibility = Visibility.Hidden;
                    }
                    catch { }
                }
                try
                {
                    botoes[x].RenderTransform = new ScaleTransform(1, y_scale);
                }
                catch { }
            }
        }

        void botaoEntradaPress(int x)
        {
            this._inportBits[x - 1] = !this._inportBits[x - 1];
            this.botoesEntrada();
            //seta o valor do campo
            string bin = "";
            for (int i = 0; i < 16; i++)
            {
                if (this._inportBits[i])
                {
                    bin += "1";
                }
                else
                {
                    bin += "0";
                }
            }
            this.in_port.Text = BIP.Montador.Funcoes.BinToInt(bin, false).ToString();
        }

        void setEntrada()
        {
            int x=0;
            if (this.in_port.Text != "" && this.in_port != null)
                x = int.Parse(this.in_port.Text);
            string bin = BIP.Montador.Funcoes.IntToBin(x, 16);
            for (int i = 0; i < 16; i++)
            {
                this._inportBits[i] = bin.Substring(i, 1) == "1";
            }
            this.botoesEntrada();
        }
        #endregion

        //==========================================================
        #region
        /// <summary>
        /// Busca valor da memória de dados
        /// </summary>
        /// <param name="position">Endereço na memória de dados - numero da linha com inicio em 0</param>
        /// <returns></returns>
        public int getValorMemoria(int position)
        {
            if (position < gridMemoDados.Items.Count)
            {
                ListViewItem item = (ListViewItem)(gridMemoDados.Items.GetItemAt(position));
                string end = ((string[])(item.Content))[0].ToString();
                string valor = ((string[])(item.Content))[1].ToString();

                gridMemoDados.SelectedIndex = position;

                //reposiciona scroll
                gridMemoDados.ScrollIntoView(gridMemoDados.SelectedItem);

                //ListViewItem lvi = (ListViewItem)gridMemoDados.ItemContainerGenerator.ContainerFromIndex(gridMemoDados.SelectedIndex);
                //lvi.Focus();

                //modifica cor dos item
                foreach (ListViewItem list in gridMemoDados.Items)
                {
                    list.Background = Brushes.Transparent;
                }
                ((ListViewItem)(gridMemoDados.SelectedItem)).Background = Brushes.SandyBrown;


                try
                {
                    return Convert.ToInt32(valor);
                }
                catch {
                    return 0;
                }
            }
            else
            {
                //registradores
                switch (position){
                    case 1024:
                        if (this.in_port.Text == "")
                            this.in_port.Text = "0";
                        return Convert.ToInt32(this.in_port.Text);
                    case 1025:
                        return Convert.ToInt32(this.out_port.Text);
                    case 1073:
                        return 0;
                }
                return 0;
            }
        }
        #endregion

        //==========================================================

        #region LimpaCorGridDados
        /// <summary>
        /// Desmarca todos os itens selecionados da Memória de Dados, setando backgroud como transparente e deselecionando itens
        /// </summary>
        public void LimpaCorGridDados(){
            foreach (ListViewItem list in gridMemoDados.Items)
            {
                //altera cor mas mantem item selecionado
                list.Background = Brushes.Transparent;
                
            }
            //des-seleciona todos os itens.
            gridMemoDados.SelectedItem = null;
        }
        #endregion

        //==========================================================        
        /// <summary>
        /// Evento clique da seleção do Processador (BIP I ou BIP II)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.VerificaProcessador();
        }

        //==========================================================
        #region BOTOES DO WPF OCULTOS
        //Eventos dos Botoes contidos no WPF que iniciam cada uma das animacoes. Estes botoes estão Ocultos.

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.LDI(Convert.ToInt32(this.txtTeste.Text));
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            this.SUBI(Convert.ToInt32(this.txtTeste.Text));
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            this.ADDI(Convert.ToInt32(this.txtTeste.Text));
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            this.SUB(Convert.ToInt32(this.txtTeste.Text));
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            this.ADD(Convert.ToInt32(this.txtTeste.Text));
        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            this.LD(Convert.ToInt32(this.txtTeste.Text));
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            this.PCmore1(false);
        }

        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            this.BEQ(Convert.ToInt32(this.txtTeste.Text));
        }

        private void Button_Click_9(object sender, RoutedEventArgs e)
        {
            this.STO(Convert.ToInt32(this.txtTeste.Text));
        }

        private void Button_Click_10(object sender, RoutedEventArgs e)
        {
            this.BNE(Convert.ToInt32(this.txtTeste.Text));
        }

        private void Button_Click_11(object sender, RoutedEventArgs e)
        {
            this.BGT(Convert.ToInt32(this.txtTeste.Text));
        }

        private void Button_Click_12(object sender, RoutedEventArgs e)
        {
            this.BGE(Convert.ToInt32(this.txtTeste.Text));
        }

        private void Button_Click_13(object sender, RoutedEventArgs e)
        {
            this.BLT(Convert.ToInt32(this.txtTeste.Text));
        }

        private void Button_Click_14(object sender, RoutedEventArgs e)
        {
            this.BLE(Convert.ToInt32(this.txtTeste.Text));
        }

        private void Button_Click_15(object sender, RoutedEventArgs e)
        {
            this.JMP(Convert.ToInt32(this.txtTeste.Text));
        }
        #endregion

        //==========================================================
        /// <summary>
        /// PERCORRE MEMORIA DE PROGRAMA E PREENCHE ARRAY DE INSTRUÇOES
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            //foreach (ListViewItem item in gridMemoPrograma.Items)
            //{
            //    string posicao = ((string[])(item.Content))[0].ToString();
            //    string valor = ((string[])(item.Content))[1].ToString();
            //    this.PreencheArray(posicao, valor);

            //}


            //PREENCHE ARRAY DE INSTRUÇAO A PARTIR DE ARQUIVO
            //string vArquivo = @"Teste.txt";
            //if (File.Exists(vArquivo))
            //{
            //    StreamReader reader = new StreamReader(vArquivo);
            //    //ArrayList ArrayLinha = new ArrayList();

                
            //    while (!reader.EndOfStream)
            //    {
            //        this.PreencheArray(reader.ReadLine());
            //   }
            //    reader.Close();
            //}
        }

        //==========================================================
        /// <summary>
        /// Preenche Array auxiliar com instrução atual da memória de programa
        /// </summary>
        /// <param name="posicao"></param>
        /// <param name="value"></param>
        public void PreencheArray(string posicao, string value)
        {
            this.ArrayLinha.Add(new String[] { posicao, value });
        }

        //==========================================================
        /// <summary>
        /// Busca proxima instrução a ser executada na Memória de Programa
        /// </summary>
        public void getNovaInstrucao()
        {
            if (overflow)
            {
                overflow = false;
                MessageBox.Show("Valor " + ACC + " não pode ser representado por 16 bits.", "Valor inválido");
                this.overflow = false;
                this.Stop();
                this.InicializaValoresIniciais();
                this.RequestFimPrograma();
                return;
            }
            if (!isPause && !isStop)
            {
                //armazena estado anterior para repetir animação da instruçao
                PC_ant = this._PC;
                pilhaSubrotinaAnterior = Clone(pilhaSubrotina);
                posicao_ant = this.posicao;
                acc_ant = this.ACC;
                n_ant = this.status_N;
                z_ant = this.status_Z;
                _indr_ant = _indr;
                //dados_ant = gridMemoDados;
                //programa_ant = gridMemoPrograma;

                this.LimpaCorGridDados();
                try
                {
                    string end = this._PC.ToString(); // ((string[])(this.ArrayLinha[this.posicao]))[0].ToString();

                    this.DestacaGridPrograma(end);
                    string linha = ((string[])(this.ArrayLinha[this._PC]))[1].ToString();
                    //linha = this.ArrayLinha[this.posicao].ToString();
                    string[] a = linha.Split(' ');
                    this.verificaInstrucao(a[0], a[1]);
                    this.posicao++;
                }
                catch { }
            }
            
        }

        //==========================================================
        /// <summary>
        /// Inicia simulação do programa carregado na memória de programa
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, RoutedEventArgs e)
        {

            this.ExecutaSimulacao();
            
        }

        public void Stop()
        {
            isPause = true;
            this.entradaOK = false;
            this.InicializaValoresIniciais();
        }

        public void ExecutaSimulacao()
        {

            this.InicializaValoresIniciais();

            foreach (ListViewItem item in gridMemoPrograma.Items)
            {
                string posicao = ((string[])(item.Content))[1].ToString();
                string valor = ((string[])(item.Content))[2].ToString();
                this.PreencheArray(posicao, valor);
            }
            this.getNovaInstrucao();
        }
        
        //==========================================================
        /// <summary>
        /// DESTACA INSTRUÇAO ATUAL NA MEMORIA DE PROGRAMA
        /// </summary>
        /// <param name="pos">posicão da instrução na memória de programa - Indice da Linha iniciando em 0</param>
        public void DestacaGridPrograma(string pos)
        {
            int posicao = Convert.ToInt32(pos);
            posicao = this._PC;
            //reposiciona scroll
            gridMemoPrograma.SelectedIndex = posicao;

            //ListViewItem lvi = (ListViewItem)gridMemoPrograma.ItemContainerGenerator.ContainerFromIndex(posicao);
            //((ListViewItem)gridMemoPrograma.Items[posicao]).Focus();
            gridMemoPrograma.ScrollIntoView(gridMemoPrograma.SelectedItem);
            
            foreach (ListViewItem list in gridMemoPrograma.Items)
            {
                list.Background = Brushes.Transparent;
            }
            ((ListViewItem)(gridMemoPrograma.SelectedItem)).Background = Brushes.SeaGreen;
            this.gridMemoPrograma.UpdateLayout();
        }

        //==========================================================
        // Possibilita chamar método da Interface para exibir Popup
        
        public delegate void ShowMessageHandler(String strTitulo, String strConteudo, String strImagem);

        public event ShowMessageHandler RequestShowMessage;
        
        protected void VerificaClickBotao(object sender, RoutedEventArgs e)
        {
            if (this.RequestShowMessage != null)
                this.RequestShowMessage("titulo", "conteudo", "imagem");
        }

        //==========================================================
        
        /// <summary>
        /// EXECUTA UMA ÚNICA INSTRUÇÃO
        /// </summary>
        /// <param name="instrucao">nome da instrução</param>
        /// <param name="operando">valor do operando</param>
        public void ExecutaInstrucao(string instrucao, string operando)
        {
            exibeAtualizaPC = false;
            exibeComentario = true;
            instrucao = instrucao.Trim().ToUpper();
            operando = operando.Trim().ToUpper();

            if (instrucao == "LD")
            {
                this.LD(Convert.ToInt32(operando));
            }
            else if (instrucao == "LDI")
            {
                this.LDI(Convert.ToInt32(operando));
            }
            else if (instrucao == "ADD")
            {
                this.ADD(Convert.ToInt32(operando));
            }
            else if (instrucao == "ADDI")
            {
                this.ADDI(Convert.ToInt32(operando));
            }
            else if (instrucao == "SUB")
            {
                this.SUB(Convert.ToInt32(operando));
            }
            else if (instrucao == "SUBI")
            {
                this.SUBI(Convert.ToInt32(operando));
            }
            else if (instrucao == "STO")
            {
                this.STO(Convert.ToInt32(operando));
            }
            else if (instrucao == "BEQ")
            {
                this.BEQ(Convert.ToInt32(operando));
            }
            else if (instrucao == "BNE")
            {
                this.BNE(Convert.ToInt32(operando));
            }
            else if (instrucao == "BGT")
            {
                this.BGT(Convert.ToInt32(operando));
            }
            else if (instrucao == "BGE")
            {
                this.BGE(Convert.ToInt32(operando));
            }
            else if (instrucao == "BLT")
            {
                this.BLT(Convert.ToInt32(operando));
            }
            else if (instrucao == "BLE")
            {
                this.BLE(Convert.ToInt32(operando));
            }
            else if (instrucao == "JMP")
            {
                this.JMP(Convert.ToInt32(operando));
            }
            else if (instrucao == "PC+1")
            {
                exibeAtualizaPC = true;
                this.PCmore1(false);
            }
            else if (instrucao == "HLT")
            {
                this.HLT(Convert.ToInt32(operando));
            }
            else if (instrucao == "CALL")
            {
                this.CALL(Convert.ToInt32(operando));
            }
            else if (instrucao == "RETURN")
            {
                this.RETURN();
            }
            else if (instrucao == "AND")
            {
                this.AND(Convert.ToInt32(operando));
            }
        }
        
        //==========================================================
        /// <summary>
        /// Preenche bloco de comentarios com informações referente a instrução sendo executada
        /// </summary>
        /// <param name="inst">instrução sendo executada</param>
        public void setInformacao(Instrucao inst) {
            
            switch (inst){
                    
                case Instrucao.LD:
                    //lbComentario.Text = "LD\r\nCarregando valor armazenado em uma posição da memória e armazenando no registrador Acumulador";
                    blocoComentario.Text = "LD\r\nCarregando valor armazenado em uma posição da memória e armazenando no registrador Acumulador";
                    break;
                case Instrucao.LDI:
                    blocoComentario.Text = "LDI\r\nCarregando o valor de um operando imediato para o registrador Acumulador"; 
                    break;
                case Instrucao.STO:
                    blocoComentario.Text = "STO\r\nArmazenando o conteúdo do Acumulador em uma posição da memória";
                    break;
                case Instrucao.ADD:
                    blocoComentario.Text = "ADD\r\nCarregando valor armazenado em uma posição da memória e somando com o valor armazenado no Acumulador. O resultado é novamente armazenado no Acumulador ";
                    break;
                case Instrucao.ADDI:
                    blocoComentario.Text = "ADDI\r\nSomando o valor de um operando imediato com o valor armazenado no Acumulador.\r\nO resultado é novamente armazenado no Acumulador";
                    break;
                case Instrucao.SUB:
                    blocoComentario.Text = "SUB\r\nSubtraindo o valor armazenado em uma posição da memória do valor armazenado no Acumulador. O resultado é novamente armazenado no Acumulador ";
                    break;
                case Instrucao.SUBI:
                    blocoComentario.Text = "SUBI\r\nSubtraindo o valor de um operando imediato do valor armazenado no Acumulador.\r\nO resultado é novamente armazenado no Acumulador";
                    break;
                case Instrucao.JMP:
                    blocoComentario.Text = "JMP\r\nAtualiza o valor do registrador PC com o endereço de uma instrução para realizar um desvio incondicional.";
                    break;
                case Instrucao.BEQ:
                    blocoComentario.Text = "BEQ\r\nAtualiza o valor do registrador PC caso o resultado da operação anterior na ULA foi zero.";
                    break;
                case Instrucao.BGE:
                    blocoComentario.Text = "BGE\r\nAtualiza o valor do registrador PC caso o resultado da operação anterior na ULA tenha sido maior ou igual zero.";
                    break;
                case Instrucao.BGT:
                    blocoComentario.Text = "BGT\r\nAtualiza o valor do registrador PC caso o resultado da operação anterior na ULA tenha sido maior que zero.";
                    break;
                case Instrucao.BLE:
                    blocoComentario.Text = "BLE\r\nAtualiza o valor do registrador PC caso o resultado da operação anterior na ULA tenha sido menor ou igual a zero.";
                    break;
                case Instrucao.BLT:
                    blocoComentario.Text = "BLT\r\nAtualiza o valor do registrador PC caso o resultado da operação anterior na ULA tenha sido menor que zero";
                    break;
                case Instrucao.BNE:
                    blocoComentario.Text = "BNE\r\nAtualiza o valor do registrador PC caso o resultado da operação anterior na ULA tenha sido diferente de zero.";
                    break;
                case Instrucao.HLT:
                    blocoComentario.Text = "HLT\r\nDesabilita a atualização do PC, interropendo a execução do programa.";
                    break;
                case Instrucao.CALL:
                    blocoComentario.Text = "CALL\r\nAtualiza o valor do registrador PC com o endereço de uma instrução e armazena o endereço da próxima instrução na Pilha.";
                    break;
                case Instrucao.RETURN:
                    blocoComentario.Text = "RETURN\r\nAtualiza o valor do registrador PC com o endereço do topo da Pilha.";
                    break;
                case Instrucao.LDV:
                    blocoComentario.Text = "LDV\r\nCarregando valor armazenado em uma posição da memória, definida pelo operando somado ao conteúdo do registrador INDR, e armazenando no registrador Acumulador";
                    break;
                case Instrucao.STOV:
                    blocoComentario.Text = "STOV\r\nArmazenando o conteúdo do Acumulador em uma posição da memória definida pelo operando somado ao conteúdo do registrador INDR";
                    break;
                case Instrucao.AND:
                    blocoComentario.Text = "AND\r\nCarregando valor armazenado em uma posição da memória e executando operação de 'E' lógica com o valor armazenado no Acumulador. O resultado é novamente armazenado no Acumulador ";
                    break;
                case Instrucao.OR:
                    blocoComentario.Text = "OR\r\nCarregando valor armazenado em uma posição da memória e executando operação de 'OU' lógica com o valor armazenado no Acumulador. O resultado é novamente armazenado no Acumulador ";
                    break;
                case Instrucao.XOR:
                    blocoComentario.Text = "XOR\r\nCarregando valor armazenado em uma posição da memória e executando operação lógica de 'OU' exclusiva com o valor armazenado no Acumulador. O resultado é novamente armazenado no Acumulador ";
                    break;
                case Instrucao.NOT:
                    blocoComentario.Text = "NOT\r\nExecutando operação de 'NOT' lógica com o valor armazenado no Acumulador. O resultado é novamente armazenado no Acumulador ";
                    break;
                case Instrucao.ANDI:
                    blocoComentario.Text = "ANDI\r\nEfetuando 'E' lógico entre o valor de um operando imediato com o valor armazenado no Acumulador.\r\nO resultado é novamente armazenado no Acumulador";
                    break;
                case Instrucao.ORI:
                    blocoComentario.Text = "ORI\r\nEfetuando 'OU' lógico entre o valor de um operando imediato com o valor armazenado no Acumulador.\r\nO resultado é novamente armazenado no Acumulador";
                    break;
                case Instrucao.XORI:
                    blocoComentario.Text = "XORI\r\nEfetuando 'OU' exclusivo entre o valor de um operando imediato com o valor armazenado no Acumulador.\r\nO resultado é novamente armazenado no Acumulador";
                    break;
                case Instrucao.SLL:
                    blocoComentario.Text = "SLL\r\nEfetuando deslocamento binário à esquerda do valor armazenado no Acumulador pelo valor do operando imediato.\r\nO resultado é novamente armazenado no Acumulador";
                    break;
                case Instrucao.SRL:
                    blocoComentario.Text = "SRL\r\nEfetuando deslocamento binário à direita do valor armazenado no Acumulador pelo valor do operando imediato.\r\nO resultado é novamente armazenado no Acumulador";
                    break;
                default:
                    blocoComentario.Text = "";
                    
                    break;
            }
        }
        
        //==========================================================
        /// <summary>
        /// Exibe bloco de informações descrevendo ações ocorridas durante a simulação
        /// </summary>
        /// <param name="exibe">indica se o bloco de infomações será exibido ou nao</param>
        public void showInformacao(bool exibe)
        {
            exibeComentario = exibe;
            if (!exibe)
            {
                gridComentario.Visibility = Visibility.Hidden;
                //blocoComentario.Visibility = Visibility.Hidden;
                //glassComentario.Visibility = Visibility.Hidden;
                //fundoComentario.Visibility = Visibility.Hidden;
            }
            else
            {
                gridComentario.Visibility = Visibility.Visible;
                //blocoComentario.Visibility = Visibility.Visible;
                //glassComentario.Visibility = Visibility.Visible;
                //fundoComentario.Visibility = Visibility.Visible;
            }
        }
        //==========================================================
        /// <summary>
        /// Exibe bloco de Registradores
        /// </summary>
        /// <param name="exibe">indica se bloco de registradores será exibido</param>
        public void showRegistradores(bool exibe)
        {
            exibeComentario = exibe;
            if (!exibe)
            {
                blocoRegistradores.Visibility = Visibility.Hidden;
            }
            else
            {
                blocoRegistradores.Visibility = Visibility.Visible;
            }
        }
        //==========================================================
        /// <summary>
        /// Exibe ou nao atualização do PC
        /// </summary>
        /// <param name="exibe"></param>
        public void showAtualizaPC(bool exibe)
        {
            exibeComentario = exibe;
            if (!exibe)
            {
                exibeAtualizaPC = false;
            }
            else
            {
                exibeAtualizaPC = true;
            }
        }
        
        //==========================================================
        /// <summary>
        /// Abri um arquivo texto para simulação, carrega as instruções para Memória de programa e inicializa variaveis de controle da animação
        /// </summary>
        /// <returns></returns>
        public bool AbrirArquivo()
        {
            // Configure open file dialog box
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.FileName = "Document"; // Default file name
            dlg.DefaultExt = ".txt"; // Default file extension
            dlg.Filter = "Text documents (.txt)|*.txt"; // Filter files by extension

            // Show open file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            // Process open file dialog box results
            if (result == true)
            {
                // Open document
                this.Arquivo = dlg.FileName;
                this.InicializaValoresIniciais();
                
                this.PreencheMemoPrograma();    //
                
                gridMemoDados.Items.Clear(); 
                this.PreencheGridDados();
                
                return true;
            }
            else
            {
                return false;
            }

        }
        //==========================================================
        
        /// <summary>
        /// Insere variaávei no grid memoria de dados
        /// </summary>
        /// <param name="intPos">posicao a ser inserido</param>
        /// <param name="strValor">valor da variável</param>
        /// <param name="nomeVariavel">nome da variável</param>
        private void SetMemoriaDadosGrid(int intPos, string strValor, string nomeVariavel)
        {
            ((ListViewItem)(gridMemoDados.Items.GetItemAt(intPos))).Content = new String[] { intPos.ToString(), strValor, nomeVariavel };

            //ListViewItem item2 = new ListViewItem();
            //item2.Content = new String[] { intPos.ToString(), strValor };
            //gridMemoDados.Items.Add(item2);

        }
        
        //==========================================================
        public void LimpaMemoriaDados()
        {
            for (int i = 0; i < 1024; i++)
            {
                this.SetMemoriaDadosGrid(i, "", "");
            }
        }
        public void LimpaMemoriaPrograma()
        {
            gridMemoPrograma.Items.Clear();
            for (int i = 0; i < 1024; i++)
            {
                this.SetMemoriaDadosGrid(i, "", "");
            }
        }
        /// <summary>
        /// Preenche memoria de Dados
        /// </summary>
        /// <param name="lista"></param>
        public void SetMemoriaDados(List<InstrucaoASM> lista){

            for (int i = 0; i < 1024; i++)
            {
                this.SetMemoriaDadosGrid(i, "", "");
            }

            this.listaVariavel = lista;

            lista = lista.OrderBy(l => l.IndexMemoria).ToList();
            //gridMemoDados.Items.Clear();
            foreach (InstrucaoASM item in lista)
            {
                this.SetMemoriaDadosGrid(item.IndexMemoria, item.Operando, item.Instrucao);
            }
            //seta os endereços de e/s
            this._outport = 0;
            this._indr = 0;
            this.AccToOutPort();
            this.in_port.Text = "0";
        }
        //==========================================================

        /// <summary>
        /// Preenche grid memória de programa
        /// </summary>
        /// <param name="strRotulo"></param>
        /// <param name="intPos"></param>
        /// <param name="strValor"></param>
        private void SetMemoriaProgramaGrid(string strRotulo, int intPos, string strValor)
        {
            ListViewItem item2 = new ListViewItem();
            item2.Content = new String[] { strRotulo, intPos.ToString(), strValor };
            gridMemoPrograma.Items.Add(item2);
        }
        //==========================================================

        /// <summary>
        /// Preenchememória de programa
        /// </summary>
        /// <param name="lista"></param>
        public void SetMemoriaPrograma(List<InstrucaoASM> lista)
        {
            
            this.tamanhoProg = lista.Count;
            gridMemoPrograma.Items.Clear();
            lista = lista.OrderBy(l => l.IndexMemoria).ToList();
            foreach (InstrucaoASM item in lista)
            {
                this.SetMemoriaProgramaGrid( "", item.IndexMemoria, item.Instrucao + " " + item.Operando);
               
            }

            //adiciona uma instrucao sem nada no fim do grid - solucao temporaria para evitar erro de indice
            //ListViewItem item2 = new ListViewItem();
            //item2.Content = new String[] { "", gridMemoPrograma.Items.Count.ToString(), "" };
            //gridMemoPrograma.Items.Add(item2);

        }

        //==========================================================
        /// <summary>
        /// Preenche rótulos no grid memória de programa
        /// </summary>
        /// <param name="lista">lista de instruçoes assembly</param>
        public void SetRotulosPrograma(List<InstrucaoASM> lista)
        {
            
            this.listaRotulos = lista;
            
            lista = lista.OrderBy(l => l.IndexMemoria).ToList();
            foreach (InstrucaoASM item in lista)
            {
                int index = item.IndexMemoria; //traz a posicao da memoria do rotulo... buscar este indice no grid e preencher rotulo

                ListViewItem a = (ListViewItem)gridMemoPrograma.Items[index];

                string posicao = ((string[])(a.Content))[1].ToString();
                string instrucao = ((string[])(a.Content))[2].ToString();


                ((ListViewItem)(gridMemoPrograma.Items.GetItemAt(index))).Content = new String[] { item.Instrucao, posicao, instrucao };
            }
        }
        //==========================================================
        
        
        //delegate para setar os endereços das linhas dos programas
        public delegate void SetEnderecoPrograma(int str);

        public event SetEnderecoPrograma RequestEnderecoPrograma;

        protected void SetEndereco(int strEndereco)
        {
            if (this.RequestEnderecoPrograma != null)
                this.RequestEnderecoPrograma(strEndereco);
        }

        //==========================================================
        //delegate para setar informar que a simulação terminou e habilitar botoes especificos
        public delegate void SetFimPrograma();
        //delegate para setar a simulação como pausada
        public delegate void SetPausePrograma();

        public event SetFimPrograma RequestFimPrograma;
        public event SetPausePrograma RequestPausePrograma;

        protected void SetFim()
        {
            if (this.RequestFimPrograma != null)            
                this.RequestFimPrograma();
        }

        //==========================================================
        
        /// <summary>
        /// Pausa simulação
        /// </summary>
        public void Pause()
        {
            isPause = true;
        }
        //==========================================================

        /// <summary>
        /// Reinicia simulação
        /// </summary>
        public void Restart()
        {
            isPause = false;
            isStop = false;
            this.getNovaInstrucao();
        }
        //==========================================================

        /// <summary>
        /// Simula a proxima instrução
        /// </summary>
        public void PassoProx()
        {
            isPause = false;
            this.getNovaInstrucao();
            isPause = true; ;
            isRepeat = false;
        }
        //==========================================================
        
        /// <summary>
        /// Repete simulação da instrução
        /// </summary>
        public void RepetePasso()
        {
            isRepeat = true;
            this.pilhaSubrotina = this.pilhaSubrotinaAnterior;
            this.SP.Text = "0";
            if (this.pilhaSubrotina.Count > 0)
                this.SP.Text = this.pilhaSubrotina.Peek().ToString();
            this._PC = PC_ant;
            this.posicao = posicao_ant;
            this.ACC = acc_ant;
            this.status_N = n_ant;
            this.status_Z = z_ant;
            this._indr = this._indr_ant;
            this.INDR.Text = this._indr.ToString();
            //gridMemoPrograma = programa_ant;
            //gridMemoDados = dados_ant;
            
            this.PassoProx();
        }
        //==========================================================

        #region Velocidade
        public void Velocidade(int p)
        {

            switch (p){
                case 1:
                        this.Speed = Convert.ToDouble(0.5);
                        break;
                case 2:
                    this.Speed = Convert.ToDouble(1);
                    break;
                case 3:
                    this.Speed = Convert.ToDouble(2);
                    break;
                case 4:
                    this.Speed = Convert.ToDouble(3);
                    break;
                case 5:
                    this.Speed = Convert.ToDouble(5);
                    break;
                case 6:
                    this.Speed = Convert.ToDouble(10);
                    break;
                case 7:
                    this.Speed = Convert.ToDouble(30);
                    break;
                case 8:
                    this.Speed = Convert.ToDouble(80);
                    break;
                case 9:
                    this.Speed = Convert.ToDouble(300);
                    break;
                case 10:
                    this.Speed = Convert.ToDouble(1000);
                    break;
                default:
                    this.Speed = Convert.ToDouble(2);
                    break;
            }
        }
        #endregion

        //==========================================================
        private void aspecto(object sender, RoutedEventArgs e)
        {
            if (vBox.Stretch == Stretch.Fill)
                vBox.Stretch = Stretch.Uniform;
            else
                vBox.Stretch = Stretch.Fill;
        }

        //==========================================================
        private void Button_Click_16(object sender, RoutedEventArgs e)
        {
            blocoInfo.Visibility = Visibility.Hidden;
        }

        private void BipI_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void chave_1_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void chave_1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.botaoEntradaPress(1);
        }

        private void chave_2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.botaoEntradaPress(2);
        }

        private void chave_3_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.botaoEntradaPress(3);
        }

        private void chave_4_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.botaoEntradaPress(4);
        }

        private void chave_5_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.botaoEntradaPress(5);
        }

        private void chave_6_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.botaoEntradaPress(6);
        }

        private void chave_7_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.botaoEntradaPress(7);
        }

        private void chave_8_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.botaoEntradaPress(8);
        }

        private void chave_9_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.botaoEntradaPress(9);
        }

        private void chave_10_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.botaoEntradaPress(10);
        }

        private void chave_11_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.botaoEntradaPress(11);
        }

        private void chave_12_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.botaoEntradaPress(12);
        }

        private void chave_13_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.botaoEntradaPress(13);
        }

        private void chave_14_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.botaoEntradaPress(14);
        }

        private void chave_15_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.botaoEntradaPress(15);
        }

        private void chave_16_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.botaoEntradaPress(16);
        }

        private void in_port_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
        	// TODO: Add event handler implementation here.
            
        }

        private void in_port_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.D0:
                case Key.D1:
                case Key.D2:
                case Key.D3:
                case Key.D4:
                case Key.D5:
                case Key.D6:
                case Key.D7:
                case Key.D8:
                case Key.D9:
                case Key.NumPad0:
                case Key.NumPad1:
                case Key.NumPad2:
                case Key.NumPad3:
                case Key.NumPad4:
                case Key.NumPad5:
                case Key.NumPad6:
                case Key.NumPad7:
                case Key.NumPad8:
                case Key.NumPad9:
                case Key.OemMinus:
                    e.Handled = false;
                    return;
            }
            e.Handled = true; 
        }

        private void in_port_TextChanged(object sender, TextChangedEventArgs e)
        {
            #region verifica mudanças
            if (in_port.Text == "" || in_port.Text == null)
            {
                return;
            }
            try
            {
                int i = int.Parse(in_port.Text);
            }
            catch
            {
                if (e.Changes.Count > 0)
                {
                    if (e.Changes.ToList()[0].AddedLength > 0)
                    {
                        in_port.Text = in_port.Text.Remove(e.Changes.ToList()[0].Offset, e.Changes.ToList()[0].AddedLength);
                    }
                    else
                    {
                        in_port.Text = "";
                    }
                }
                else
                {
                    in_port.Text = "0";
                }
            }
            #endregion
            this.setEntrada();
        }

        private void in_port_TextInput(object sender, TextCompositionEventArgs e)
        {
        }
        //==========================================================


        public void LimpaEstatistica()
        {
            contadorInstrucoes = new Contador();
            
        }
        /// <summary>
        /// Conta a quantidade de instruções no programa assembly
        /// </summary>
        public void ExibeEstatistica()
        {

            //inicializa em zero vetor com contagem de instruções
            for (int i = 0; i < vetorContaInstrucoes.Length; i++)
                vetorContaInstrucoes[i] = 0;

            //cria vetor com elementos no Enum Instrucao
            Array v = Enum.GetValues(typeof(Instrucao));

            //Percorre memoria decimal programa
            foreach (ListViewItem item in gridMemoPrograma.Items)
            {
                try
                {
                    //busca cada instrução e separa o mnemonico
                    string instrucao = ((string[])(item.Content))[2].ToString();
                    string[] a = instrucao.Split(' ');
                    string inst = a[0];
                    string val = a[1];

                    //Percorre vetor de Instruções (Enum)
                    for (int i = 0; i < v.Length; i++)
                    {
                        //Quando a instrução atual é igual à instução do vetor, incrementa posicao correspondente
                        string value = v.GetValue(Convert.ToInt32(i)).ToString();
                        if (inst == value)
                            vetorContaInstrucoes[i]++;
                    }
                }
                catch { /*excecoes gerais*/ }
            }

            //string strSaida = "";
            ////percorre vetor contendo as quantidades de instruções e monta string de saída
            //for (int i = 0; i < v.Length; i++)
            //{
            //    if (vetorContaInstrucoes[i] > 0)
            //    {
            //        string value = v.GetValue(Convert.ToInt32(i)).ToString();
            //        strSaida += value + " : \t" + vetorContaInstrucoes[i] + "\n";
            //    }
            //}

            
            //quantidade de posicoes de memoria ocupadas - posicoes de memoria diferentes de vazio
            int totalMemoria = 0;
            //quantidade de posicoes ocupadas na memoria de programa
            int totalPrograma = 0;
            //quantidade de variaveis utilizadas
            int totalVariavel = 0;




            foreach (ListViewItem item in gridMemoPrograma.Items)
            {
                try
                {
                    string strVar = ((string[])(item.Content))[2].ToString();

                    if (!String.IsNullOrEmpty(strVar))
                        totalPrograma++;
                }
                catch { /*excecoes gerais*/ }
            }

            foreach (ListViewItem item in gridMemoDados.Items)
            {
                try
                {
                    string str = ((string[])(item.Content))[1].ToString();
                    if (!String.IsNullOrEmpty(str))
                        totalMemoria++;

                    string strVar = ((string[])(item.Content))[2].ToString();

                    if (!String.IsNullOrEmpty(strVar))
                        totalVariavel++;
                }
                catch { /*excecoes gerais*/ }
            }


            Estatistica es = new Estatistica();
            es.ChangeLanguage(this.l.ToString());
            es.Exibe(v, vetorContaInstrucoes, totalMemoria, totalVariavel, totalPrograma, contadorInstrucoes);
            
            //this.Exibe(contadorInstrucoes);

        }

        public void Exibe(Contador contaInstrucoes)
        {
            listaExecucao.Items.Clear();
            #region Contador de execução executadas
            System.Reflection.PropertyInfo[] properties = contaInstrucoes.GetType().GetProperties();
            foreach (System.Reflection.PropertyInfo p in properties)
            {
                if (p != null)
                {
                    string nome = p.Name;
                    int valor = Convert.ToInt32(p.GetValue(contaInstrucoes, null));
                    if (valor > 0)
                    {
                        ItemsControl it = new ItemsControl();
                        if (nome.ToUpper().Trim() == "RETURN")
                            it.Items.Add(nome + " : " + valor);
                        else
                            it.Items.Add(nome + " : \t  " + valor);
                        it.Height = 20.0;
                        listaExecucao.Items.Add(it);
                        ////totalExecutadas += valor;

                    }

                    //string nome = p.Name;
                    //string h = p.GetValue(contaInstrucoes, null).ToString();
                }

            }
           // txtTotalExec.Text = totalExecutadas.ToString();
            #endregion
        }
        //---------------------------------------------------------------

        public void ChangeLanguage(string lang)
        {


            

            if (lang == Idioma.INGLES.ToString())
            {
                
                DescDecoder.Content = "Decoder";
                DescControle.Content = "Control";
                DescCaminhoDados.Content = "Datapath";
                DescPilha.Content = "Stack";
                DescExtSinal.Content = "Signal Ext.";
                if (this.Processor == 1 || this.Processor == 2)
                    DescUla.Content = "+ -";
                else
                    DescUla.Content = "ALU";
                DescUlaPC.Content = "+";
                DescES.Content = "I/O";
                DescVetorAccess.Content = "Vector";
                lblAcumulador.Content = "Accumulator";

                ((GridView)gridMemoPrograma.View).Columns[0].Header = "Label";
                ((GridView)gridMemoPrograma.View).Columns[1].Header = "Address";
                ((GridView)gridMemoPrograma.View).Columns[2].Header = "Instruction";

                ((GridView)gridMemoDados.View).Columns[0].Header = "Address";
                ((GridView)gridMemoDados.View).Columns[1].Header = "Value";
                ((GridView)gridMemoDados.View).Columns[2].Header = "Variable";

                this.l = Idioma.INGLES;
                                
            }
            else
            {
                DescDecoder.Content = "Decodificador";
                DescControle.Content = "Controle";
                DescCaminhoDados.Content = "Caminho de Dados";
                DescPilha.Content = "Pilha";
                DescExtSinal.Content = "Ext. Sinal";
                if (this.Processor == 1 || this.Processor == 2)
                    DescUla.Content = "+ -";
                else
                    DescUla.Content = "ULA";
                DescUlaPC.Content = "+";
                DescES.Content = "E/S";
                DescVetorAccess.Content = "Vetor";
                lblAcumulador.Content = "Acumulador";

                ((GridView)gridMemoPrograma.View).Columns[0].Header = "Rótulo";
                ((GridView)gridMemoPrograma.View).Columns[1].Header = "Endereço";
                ((GridView)gridMemoPrograma.View).Columns[2].Header = "Instrução";

                ((GridView)gridMemoDados.View).Columns[0].Header = "Endereço";
                ((GridView)gridMemoDados.View).Columns[1].Header = "Valor";
                ((GridView)gridMemoDados.View).Columns[2].Header = "Variável";

                this.l = Idioma.PORTUGUES;
            }
        }

        public void SetTheme(string tema)
        {
            if (tema == "BLACK")
            {
                var bc = new BrushConverter();
                BipI.Background = (Brush)bc.ConvertFrom("#FFE0E8EB");
            }
            else
            {
                var bc = new BrushConverter();
                BipI.Background = (Brush)bc.ConvertFrom("#FFDFEDFF");
                
            }
             
        }
    }

    


}