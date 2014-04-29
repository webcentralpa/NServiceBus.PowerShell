﻿namespace NServiceBus.PowerShell
{
    using System;
    using System.Management.Automation;
    

    [Cmdlet(VerbsLifecycle.Install, "NServiceBusRavenDB", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.Medium)]
    public class InstallRavenDB : CmdletBase
    {
        [Parameter(HelpMessage = "Port number to be used, default is 8080", ValueFromPipelineByPropertyName = true)]
        public int Port { get; set; }

        [Parameter(HelpMessage = "Path to install RavenDB into, default is %ProgramFiles%\\NServiceBus.Persistence.v4", ValueFromPipelineByPropertyName = true)]
        public string Path { get; set; }

        protected override void ProcessRecord()
        {
            if (ShouldProcess(Environment.MachineName))
            {
                new RavenDBSetup(Host).Install(Port, Path);
            }
        }
    }

    [Cmdlet(VerbsDiagnostic.Test, "NServiceBusRavenDBInstallation")]
    public class ValidateRavenDB : CmdletBase
    {
        [Parameter(HelpMessage = "Port number to be used, default is 8080", ValueFromPipelineByPropertyName = true)]
        public int Port { get; set; }

        protected override void ProcessRecord()
        {
            var isGood = new RavenDBSetup(Host).Check(Port);

            WriteObject(isGood);
        }
    }
}
