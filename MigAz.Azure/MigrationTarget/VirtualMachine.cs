﻿using MigAz.Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigAz.Azure.MigrationTarget
{
    public class VirtualMachine : IMigrationTarget
    {
        private AvailabilitySet _TargetAvailabilitySet = null;
        private string _TargetName = String.Empty;
        private AzureContext _AzureContext;
        private String _TargetSize = String.Empty;
        private List<NetworkInterface> _NetworkInterfaces = new List<NetworkInterface>();
        private List<Disk> _DataDisks = new List<Disk>();

        private VirtualMachine() { }

        public VirtualMachine(AzureContext azureContext, Asm.VirtualMachine virtualMachine)
        {
            this._AzureContext = azureContext;
            this.Source = virtualMachine;
            this.TargetName = virtualMachine.RoleName;
            this._TargetSize = virtualMachine.RoleSize;
            this.OSVirtualHardDisk = new Disk(virtualMachine.OSVirtualHardDisk);
            this.OSVirtualHardDiskOS = virtualMachine.OSVirtualHardDiskOS;
        }

        public VirtualMachine(AzureContext azureContext, Arm.VirtualMachine virtualMachine)
        {
            this._AzureContext = azureContext;
            this.Source = virtualMachine;
            this.TargetName = virtualMachine.Name;
            this._TargetSize = virtualMachine.VmSize;
            this.OSVirtualHardDisk = new Disk(virtualMachine.OSVirtualHardDisk);
            this.OSVirtualHardDiskOS = virtualMachine.OSVirtualHardDiskOS;
        }

        public AvailabilitySet ParentAvailabilitySet // todo now russell
        {
            get;set;
        }

        public Disk OSVirtualHardDisk // todo now russell
        {
            get; set;
        }

        public List<Disk> DataDisks // todo now russell
        {
            get { return _DataDisks; }
        }

        public IVirtualMachine Source
        { get; set; }

        public List<NetworkInterface> NetworkInterfaces
        {
            get { return _NetworkInterfaces; }
        }


        public String TargetSize
        {
            get { return _TargetSize; }
            set { _TargetSize = value.Trim(); }
        }

    
        public string TargetName
        {
            get { return _TargetName; }
            set { _TargetName = value; }
        }

        public string OSVirtualHardDiskOS
        {
            get; set;
        }

        public override string ToString()
        {
            return this.TargetName + _AzureContext.SettingsProvider.VirtualMachineSuffix;
        }



        public AvailabilitySet TargetAvailabilitySet
        {
            get { return _TargetAvailabilitySet; }
            set { _TargetAvailabilitySet = value; }
        }

        public IMigrationVirtualNetwork TargetVirtualNetwork { get; set; }
        public IMigrationSubnet TargetSubnet { get; set; }
    }
}
