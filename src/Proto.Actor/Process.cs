﻿// -----------------------------------------------------------------------
//  <copyright file="ActorRef.cs" company="Asynkron HB">
//      Copyright (C) 2015-2016 Asynkron HB All rights reserved
//  </copyright>
// -----------------------------------------------------------------------

namespace Proto
{
    public abstract class Process
    {
        public abstract void SendUserMessage(PID pid, object message, PID sender);

        public void Stop(PID pid)
        {
            SendSystemMessage(pid, new Stop());
        }

        public abstract void SendSystemMessage(PID pid, SystemMessage sys);
    }

    public class LocalActorRef : Process
    {
        public LocalActorRef(IMailbox mailbox)
        {
            Mailbox = mailbox;
        }

        public IMailbox Mailbox { get; }

        public override void SendUserMessage(PID pid, object message, PID sender)
        {
            if (sender != null)
            {
                Mailbox.PostUserMessage(new MessageSender(message, sender));
                return;
            }

            Mailbox.PostUserMessage(message);
        }

        public override void SendSystemMessage(PID pid, SystemMessage sys)
        {
            Mailbox.PostSystemMessage(sys);
        }
    }
}