﻿// -----------------------------------------------------------------------
//  <copyright file="Futures.cs" company="Asynkron HB">
//      Copyright (C) 2015-2016 Asynkron HB All rights reserved
//  </copyright>
// -----------------------------------------------------------------------

using System.Threading.Tasks;

namespace Proto
{
    //TODO: remove this, just use a FutureProcess instead..
    public class FutureActor<T> : IActor
    {
        private readonly TaskCompletionSource<T> _tcs;

        public FutureActor(TaskCompletionSource<T> tcs)
        {
            _tcs = tcs;
        }

        public Task ReceiveAsync(IContext context)
        {
            var msg = context.Message;
            if (msg is AutoReceiveMessage || msg is SystemMessage)
            {
                return Actor.Done;
            }
            if (msg is T)
            {
                _tcs.TrySetResult((T) msg);
                context.Self.Stop();
            }

            return Actor.Done;
        }
    }
}