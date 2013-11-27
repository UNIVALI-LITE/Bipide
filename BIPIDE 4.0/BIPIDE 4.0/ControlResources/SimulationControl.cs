using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Ribbon;

namespace BIPIDE_4._0
{
    public class SimulationControl
    {
        public enum SimulationStatus
        {
            ssRunning     = 1,
            ssPaused      = 2,
            ssStopped     = 3
        }

        public enum SimulationControls
        {
            scStart       = 1,
            scPause       = 2,
            scRepeat      = 3,
            scNext        = 4,
            scContinue    = 5,
            scStop        = 6
        }

        public enum Processors
        {
            psBipI   = 1,
            psBipII  = 2,
            psBipIII = 3,
            psBipIV  = 4,
            psUBip   = 5
        }

        private SimulationStatus _Status;
        public SimulationStatus Status
        {
            get { return _Status; }
        }

        private SimulationControls _Control;
        public SimulationControls Control
        {
            get { return _Control; }
            set 
            {
                switch (value)
                {
                    case SimulationControls.scStart :
                        _Status = SimulationStatus.ssRunning;
                        break;

                    case SimulationControls.scPause :
                        _Status = SimulationStatus.ssPaused;
                        break;

                    case SimulationControls.scRepeat :
                        _Status = SimulationStatus.ssPaused;
                        break;

                    case SimulationControls.scNext :
                        _Status = SimulationStatus.ssPaused;
                        break;

                    case SimulationControls.scContinue :
                        _Status = SimulationStatus.ssRunning;
                        break;

                    case SimulationControls.scStop :
                        _Status = SimulationStatus.ssStopped;
                        break;

                    default :
                        _Status = SimulationStatus.ssStopped;
                        break;
                }

                _RibbonButtonStart.IsEnabled    = (_Status == SimulationStatus.ssStopped) ;
                _RibbonButtonPause.IsEnabled    = (_Status == SimulationStatus.ssRunning) ;
                _RibbonButtonRepeat.IsEnabled   = (_Status == SimulationStatus.ssPaused ) ;
                _RibbonButtonNext.IsEnabled     = (_Status == SimulationStatus.ssPaused ) ;     
                _RibbonButtonContinue.IsEnabled = (_Status == SimulationStatus.ssPaused ) ; 
                _RibbonButtonStop.IsEnabled     = (_Status == SimulationStatus.ssRunning) ||
                                                  (_Status == SimulationStatus.ssPaused ) ;     

                _Control = value; 
            }
        }

        private RibbonButton _RibbonButtonStart;
        public RibbonButton RibbonButtonStart
        {
            set { _RibbonButtonStart = value; }
        }

        private RibbonButton _RibbonButtonPause;
        public RibbonButton RibbonButtonPause
        {
            set { _RibbonButtonPause = value; }
        }

        private RibbonButton _RibbonButtonRepeat;
        public RibbonButton RibbonButtonRepeat
        {
            set { _RibbonButtonRepeat = value; }
        }

        private RibbonButton _RibbonButtonNext;
        public RibbonButton RibbonButtonNext
        {
            set { _RibbonButtonNext = value; }
        }

        private RibbonButton _RibbonButtonContinue;
        public RibbonButton RibbonButtonContinue
        {
            set { _RibbonButtonContinue = value; }
        }

        private RibbonButton _RibbonButtonStop;
        public RibbonButton RibbonButtonStop
        {
            set { _RibbonButtonStop = value; }
        }

        public SimulationControl()
        {
            Control    = SimulationControls.scStop;
        }

        public SimulationControl( RibbonButton pRibbonButtonStart,
                                  RibbonButton pRibbonButtonPause,
                                  RibbonButton pRibbonButtonRepeat,
                                  RibbonButton pRibbonButtonNext,
                                  RibbonButton pRibbonButtonContinue,
                                  RibbonButton pRibbonButtonStop )
        {
            _RibbonButtonStart      = pRibbonButtonStart;
            _RibbonButtonPause      = pRibbonButtonPause;
            _RibbonButtonRepeat     = pRibbonButtonRepeat;
            _RibbonButtonNext       = pRibbonButtonNext;
            _RibbonButtonContinue   = pRibbonButtonContinue;
            _RibbonButtonStop       = pRibbonButtonStop;

            Control = SimulationControls.scStop;
        }
    }
}
