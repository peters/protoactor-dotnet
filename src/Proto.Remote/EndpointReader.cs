﻿// -----------------------------------------------------------------------
//  <copyright file="EndpointReader.cs" company="Asynkron HB">
//      Copyright (C) 2015-2016 Asynkron HB All rights reserved
//  </copyright>
// -----------------------------------------------------------------------

using System.Threading.Tasks;
using Grpc.Core;
using Grpc.Core.Utils;

namespace Proto.Remote
{
    public class EndpointReader : Remoting.RemotingBase
    {
        public override async Task Receive(IAsyncStreamReader<MessageBatch> requestStream, IServerStreamWriter<Unit> responseStream, ServerCallContext context)
        {
            await requestStream.ForEachAsync(batch =>
            {
                foreach (var envelope in batch.Envelopes)
                {
                    var target = envelope.Target;
                    var sender = envelope.Sender;
                    var message = Serialization.Deserialize(envelope.TypeName, envelope.MessageData);
                    target.Request(message, sender);
                }

                return Actor.Done;
            });
        }
    }
}