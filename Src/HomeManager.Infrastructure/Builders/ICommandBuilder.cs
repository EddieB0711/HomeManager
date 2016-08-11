﻿namespace HomeManager.Infrastructure.Builders
{
    public interface ICommandBuilder<in TCommand, out TAggregate>
    {
        TAggregate Create(TCommand command, params object[] valueObjects);
    }
}
