// Copyright © 2012-2018 Vaughn Vernon. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using Vlingo.Actors;
using Vlingo.Wire.Multicast;

namespace Vlingo.Directory.Client
{
    public interface IDirectoryClient : IStoppable
    {
        void Register(ServiceRegistrationInfo info);

        void Unregister(string serviceName);
    }
    
    // TODO: This is an workaround because C# doesn't allow implementation of default methods in interfaces. Should be fixed with C# 8
    public static class DirectoryClientFactory
    {
        public static string ClientName => "vlingo-directory-client";
        
        public static int DefaultMaxMessageSize = 32767;
        
        public static int DefaultProcessingInterval = 1000;
        
        public static int DefaultProcessingTimeout = 10;

        public static IDirectoryClient Instance(Stage stage, IServiceDiscoveryInterest interest, Group directoryPublisherGroup)
        {
            return Instance(
                stage,
                interest,
                directoryPublisherGroup,
                DefaultMaxMessageSize,
                DefaultProcessingInterval,
                DefaultProcessingTimeout);
        }

        public static IDirectoryClient Instance(
            Stage stage,
            IServiceDiscoveryInterest interest,
            Group directoryPublisherGroup,
            int maxMessageSize,
            long processingInterval,
            int processingTimeout)
        {
            var definition =
                    Definition.Has<DirectoryClientActor>(
                Definition.Parameters(interest, directoryPublisherGroup, maxMessageSize, processingInterval, processingTimeout),
            ClientName);

            return stage.ActorFor<IDirectoryClient>(definition);
        }
    }
}