using System;
using System.Collections.Generic;
using Ch.Elca.Iiop;
using Ch.Elca.Iiop.Services;
using omg.org.CosNaming;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Remoting.Channels;
using System.Diagnostics;
using System.Threading;
using System.IO;

namespace BIPIDE_4._0.ControlResources
{
    public class CorbaController
    {
        private const int PORTA_PADRAO          = 53787;
        private const int PORTA_PADRAO_CALLBACK = 53788;
        private CorbaInit _CorbaInstace;

        public CorbaInit CorbaInstace
        {
            get { return _CorbaInstace; }
            set { _CorbaInstace = value; }
        }

        public void Start()
        {
            IiopChannel canal = new IiopChannel(PORTA_PADRAO_CALLBACK);
            ChannelServices.RegisterChannel(canal, false);

            _CorbaInstace = CorbaInit.GetInit();
        }

        public Boolean RaiseJavaProcess(String pPath)
        {
            if (File.Exists(pPath))
            {
                ProcessStartInfo construtorProcesso = new ProcessStartInfo();
                construtorProcesso.FileName = "java";
                construtorProcesso.Arguments = "-jar \"" + pPath + "\"";
                construtorProcesso.CreateNoWindow = false;
                construtorProcesso.WindowStyle = ProcessWindowStyle.Hidden;

                Process.Start(construtorProcesso);

                Thread.Sleep(5000);

                return true;
            }
            else
            {
                return false;
            }
        }

        public MarshalByRefObject ResolveProcess(string ComponentName)
        {
            NamingContext iNamingContext = CorbaInstace.GetNameService("localhost", PORTA_PADRAO);
            NameComponent[] iNameComponent = new NameComponent[] { new NameComponent(ComponentName, "") };

            return iNamingContext.resolve(iNameComponent);
        }
    }
}
