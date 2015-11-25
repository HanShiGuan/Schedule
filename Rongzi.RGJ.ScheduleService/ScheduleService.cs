using System.ServiceProcess;
using Common.Logging;
using Quartz;
using Quartz.Impl;

namespace Rongzi.RGJ.ScheduleService
{
    public partial class ScheduleService : ServiceBase
    {
        private readonly ILog logger;
        private IScheduler scheduler;

        public ScheduleService()
        {
            InitializeComponent();
            logger = LogManager.GetLogger(GetType());
            ISchedulerFactory schedulerFactory = new StdSchedulerFactory();
            scheduler = schedulerFactory.GetScheduler();
        }

        protected override void OnStart(string[] args)
        {
            scheduler.Start();
            logger.Info("Schedule Start \r");
        }

        protected override void OnStop()
        {
            scheduler.Shutdown();
            logger.Info("Schedule Stop \r");
        }

        protected override void OnPause()
        {
            scheduler.PauseAll();
        }

        protected override void OnContinue()
        {
            scheduler.ResumeAll();
        }
    }
}
