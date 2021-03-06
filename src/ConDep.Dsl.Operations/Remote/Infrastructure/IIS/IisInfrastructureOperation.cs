﻿using System.Collections.Generic;
using ConDep.Dsl.Validation;

namespace ConDep.Dsl.Operations.Infrastructure.IIS
{
    public class IisInfrastructureOperation : RemoteCompositeOperation
    {
        private readonly List<string> _featuresToAdd = new List<string>();
        private readonly List<string> _featuresToRemove = new List<string>();

        public override string Name
        {
            get { return "IIS Installer"; }
        }

        public override bool IsValid(Notification notification)
        {
            return true;
        }

        public override void Configure(IOfferRemoteComposition server)
        {
            server.Configure
                .Windows(win =>
                {
                    win.InstallFeature("Web-Server");
                    win.InstallFeature("Web-WebServer");

                    foreach (var feature in _featuresToAdd)
                    {
                        win.InstallFeature(feature);
                    }

                    foreach (var feature in _featuresToRemove)
                    {
                        win.UninstallFeature(feature);
                    }
                });
        }

        public void AddRoleService(string roleService)
        {
            _featuresToAdd.Add(roleService);
        }

        public void RemoveRoleService(string roleService)
        {
            _featuresToRemove.Add(roleService);
        }

    }
}