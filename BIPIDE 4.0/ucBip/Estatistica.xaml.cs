using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.IO;

namespace ucBip
{


    /// <summary>
    /// Interaction logic for Estatistica.xaml
    /// </summary>
    public partial class Estatistica : Window
    {


        public Estatistica()
        {
            InitializeComponent();
        }

        public enum Idioma
        {
            PORTUGUES,
            INGLES
        }

        //public void Exibe(string s)
        //{

        //    label1.Content = s;
        //    this.ShowInTaskbar = false;
        //    this.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterOwner;
        //    this.Show();
        //}

        private void Window_Deactivated(object sender, EventArgs e)
        {
            if (this.IsActive)
                this.Close();
        }

        internal void Exibe(Array v, int[] vetorContaInstrucoes, int totalMemoria, int totalVariavel, int totalPrograma, Contador contaInstrucoes )
        {
            int totalGeral = 0;
            int totalExecutadas = 0;
            int totalTransf = 0;
            int totalArit = 0;
            int totalDesvio = 0;
            int totalLogica = 0;
            int totalDesloca = 0;
            int totalVetor = 0;
            int totalProced = 0;
            int totalControl = 0;

            //conta quantidade geral de instruções
            for (int i = 0; i < v.Length; i++)
                if (vetorContaInstrucoes[i] > 0)
                    totalGeral += vetorContaInstrucoes[i];
			
			//percorre vetor contendo as quantidades e exibe informações
			for (int i = 0; i < v.Length; i++){
                if (vetorContaInstrucoes[i] > 0)
                {
                    string value = v.GetValue(Convert.ToInt32(i)).ToString();
                    //strSaida += value + " : \t" + vetorContaInstrucoes[i] + "\n";

                    ItemsControl it = new ItemsControl();
                    double t = (double)vetorContaInstrucoes[i] ;
                    double t2 = (double)totalGeral;
                    double r = (t / t2)*100;
					//double perc = (double)(vetorContaInstrucoes[i] / totalGeral);
					
                    if (value.ToUpper().Trim() == "RETURN")
                        it.Items.Add(value + " : " + vetorContaInstrucoes[i] + "\t("+ String.Format("{0:0.00}", r) +"%)");
                    else
                        it.Items.Add(value + " : \t  " + vetorContaInstrucoes[i] + "\t(" + String.Format("{0:0.00}", r) + "%)");
                    it.Height = 20.0;
                    listaInstrucoes.Items.Add(it);

                    

                    //string instAtual = v.GetValue(Convert.ToInt32(i)).ToString();

                    //if ((instAtual == "STO") || (instAtual == "LD") || (instAtual == "LDI"))
                    //    totalTransf++;
                    //else if ((instAtual == "ADD") || (instAtual == "ADDI") || (instAtual == "SUB") || (instAtual == "SUBI"))
                    //    totalArit++;
                    //else if ((instAtual == "BEQ") || (instAtual == "BNE") || (instAtual == "BGT") || (instAtual == "BGE") || (instAtual == "BLT") || (instAtual == "BLE") || (instAtual == "JMP"))
                    //    totalDesvio++;
                    //else if ((instAtual == "AND") || (instAtual == "OR") || (instAtual == "XOR") || (instAtual == "ANDI") || (instAtual == "ORI") || (instAtual == "XORI") || (instAtual == "NOT"))
                    //    totalLogica++;
                    //else if ((instAtual == "SLL") || (instAtual == "SRL"))
                    //    totalDesloca++;
                    //else if ((instAtual == "LDV") || (instAtual == "STOV"))
                    //    totalVetor++;
                    //else if ((instAtual == "RETURN") || (instAtual == "CALL"))
                    //    totalProced++;
                    //else if (instAtual == "HLT")
                    //    totalControl++;
                }
			}

            txtTotalInst.Text = totalGeral.ToString();
            txtTotalVariaveis.Text = totalVariavel.ToString();
            txtTotalMemoria.Text = totalMemoria.ToString();
            txtTotalPrograma.Text = totalPrograma.ToString();

            //txtTotalUtilizadas.Text = totalUtilizadas.ToString();

            try
            {
                //txtTotalTransf.Text = totalTransf.ToString();
                //progressTransf.Value = (((double)totalTransf / (double)total) * 100.0);
                //percTransf.Text = Convert.ToInt32(progressTransf.Value).ToString() + "%";

                //txtTotalArit.Text = totalArit.ToString();
                //progressArit.Value = (((double)totalArit / (double)total) * 100.0);
                //percArit.Text = Convert.ToInt32(progressArit.Value).ToString() + "%";

                //txtTotalDesvio.Text = totalDesvio.ToString();
                //progressDesvio.Value = (((double)totalDesvio / (double)total) * 100.0);
                //percDesvio.Text = Convert.ToInt32(progressDesvio.Value).ToString() + "%";


            }
            catch { }


            System.Reflection.PropertyInfo[] properties1 = contaInstrucoes.GetType().GetProperties();
            foreach (System.Reflection.PropertyInfo p in properties1)
            {
                if (p != null)
                {
                    string nome = p.Name;
                    int valor = Convert.ToInt32(p.GetValue(contaInstrucoes, null));
                    if (valor > 0)
                    {
                        totalExecutadas += valor;
                    }
                }
            }

            //label1.Content = strSaida;
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

                        double t = (double)valor;
                        double t2 = (double)totalExecutadas;
                        double r = (t / t2) * 100;

                        if (nome.ToUpper().Trim() == "RETURN")
                            it.Items.Add(nome + " : " + valor + "\t("+ String.Format("{0:0.00}", r) +"%)");
                        else
                            it.Items.Add(nome + " : \t  " + valor + "\t(" + String.Format("{0:0.00}", r) + "%)");
                        
                        it.Height = 20.0;
                        listaExecucao.Items.Add(it);
                        //totalExecutadas += valor;

                    }

                    //string nome = p.Name;
                    //string h = p.GetValue(contaInstrucoes, null).ToString();
                }

            }

            ExportToExcel(listaExecucao, "D:\\temp\\a.xls");

            txtTotalExec.Text = totalExecutadas.ToString();
            #endregion

            this.ShowInTaskbar = false;
            this.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterOwner;
            this.Show();

            
            
            
        }

        public static void ExportToExcel(ListBox lst, string excel_file)
        {
            //int cols;
            ////open file
            //StreamWriter wr = new StreamWriter(excel_file);

            ////write rows to excel file
            //for (int i = 0; i < (lst.Items.Count - 1); i++)
            //{
            //    wr.Write(lst.Items[i].ToString() + "\t");

            //    wr.WriteLine();
            //}

            ////close file
            //wr.Close();
        }


        internal void ChangeLanguage(string lang)
        {
            if (lang == Idioma.INGLES.ToString())
            {

                this.Title = "Counter Instructions";
                this.lblTotalMemoria.Content = "Occupied positions in Data Memory:";
                this.lblTotalVariaveis.Content = "Number of variables used:";
                this.lblTotalPrograma.Content = "Occupied positions in Program Memory:";
                this.lblTotalInst.Content = "Used instructions:";
                this.lblTotalExec.Content = "Executed instructions:";
                this.imgHelp1.ToolTip = this.imgHelp2.ToolTip = "The value will be updated after the execution of the program.";

            }
            else
            {
                this.Title = "Contador de Instruções";
                this.lblTotalMemoria.Content = "Posições Ocupadas na Memória de Dados:";
                this.lblTotalVariaveis.Content = "Quantidade de Variáveis Utilizadas:";
                this.lblTotalPrograma.Content = "Posições Ocupadas na Memória de Programa:";
                this.lblTotalInst.Content = "Instruções Utilizadas:";
                this.lblTotalExec.Content = "Instruções Executadas:";
                this.imgHelp1.ToolTip = this.imgHelp2.ToolTip = "O valor será atualizado após a execução do programa.";

            }
        }
    }
}
