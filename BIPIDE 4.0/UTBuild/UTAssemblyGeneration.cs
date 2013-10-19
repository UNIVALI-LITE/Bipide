﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using br.univali.portugol.integracao.csharp;
using br.univali.portugol.integracao;
using br.univali.portugol.integracao.analise;
using BIPIDE.Classes;
using BIPIDE_4._0;
using br.univali.portugol.integracao.mensagens;

namespace UTBuild
{
    [TestClass]
    public class UTAssemblyGeneration
    {
        private static Portugol _Portugol;
        private static TestContext _TestStore;

        [AssemblyInitialize]
        public static void InicializaTeste(TestContext testStore)
        {
            _TestStore = testStore;
            try
            {
                ServicoIntegracao ServicoIntegracaoPortugol = ServicoIntegracao.getInstance();
                _Portugol = ServicoIntegracaoPortugol.iniciar();
            }
            catch (Exception ex)
            {
                Assert.Fail("Erro ao inicializar CORBA: " + ex.Message);
            }
            

        }
        public String executa(String pSourceCode)
        {

            Codigo iAssembly = new Codigo();
            try
            {
                Programa iProgram = _Portugol.compilar(pSourceCode);

                Tradutor reg = new Tradutor(iProgram);
                iAssembly = reg.Convert(iProgram);
                return AssemblyToString(iAssembly);

            }
            catch (ErroCompilacao ec)
            {
                ResultadoAnalise resultado = ec.resultadoAnalise;

                if (resultado.getNumeroTotalErros() > 0)
                {
                    foreach (ErroSintatico erro in resultado.getErrosSintaticos())
                    {
                        Assert.Fail("Erro Sintatico na linha " + erro.linha + " e coluna " + erro.coluna + ": " + erro.mensagem);
                    }
                    foreach (ErroSemantico erro in resultado.getErrosSemanticos())
                    {
                        Assert.Fail("Erro Semantico na linha " + erro.linha + " e coluna " + erro.coluna + ": " + erro.mensagem);
                    }
                }
            }
            return String.Empty;
        }

        private string AssemblyToString(Codigo iAssembly)
        {
            String iSource = "";

            foreach (InstrucaoASM ins in iAssembly.GetCodigoInstrucaoASM())
            {
                iSource += ins.Instrucao + ins.Operando;
            }
            return iSource;
        }

        [TestMethod]
        public void UTFSe()
        {
            var iReturn = executa("programa{ funcao inicio(){ inteiro x x=1 se(2==2){ x=30 } } }");
            Assert.AreEqual(".datax0.text_INICIOLDI1STOxLDI2SUBI2BNEFIMSE1LDI30STOxFIMSE1HLT0".ToUpper(), iReturn.ToUpper());

            var iReturn2 = executa("programa{ funcao inicio(){ inteiro x x=1 se(2==2){ x=30 } } }");
            Assert.AreEqual(".datax0.text_INICIOLDI1STOxLDI2SUBI2BNEFIMSE1LDI30STOxFIMSE1HLT0".ToUpper(), iReturn2.ToUpper());
        }
        [TestMethod]
        public void UTFSeSenao()
        {
            var iReturn = executa("programa{ funcao inicio(){ inteiro x x=1 se(2==2){ x=30 } } }");
            Assert.AreEqual(".datax0.text_INICIOLDI1STOxLDI2SUBI2BNEFIMSE1LDI30STOxFIMSE1HLT0".ToUpper(), iReturn.ToUpper());

        }
        [TestMethod]
        public void UTFSeSenao2()
        {
            var iReturn = executa("programa{ funcao inicio(){ inteiro x x=1 se(2==2){ x=30 } } }");
            Assert.AreEqual(".datax0.text_INICIOLDI1STOxLDI2SUBI2BNEFIMSE1LDI30STOxFIMSE1HLT0".ToUpper(), iReturn.ToUpper());

        }
        [AssemblyCleanup]
        public static void Cleanup()
        {
            _Portugol.encerrar();
        }
    }
}