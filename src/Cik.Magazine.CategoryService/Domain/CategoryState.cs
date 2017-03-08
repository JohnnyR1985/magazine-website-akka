﻿using Cik.Magazine.Shared;
using Cik.Magazine.Shared.Domain;
using Cik.Magazine.Shared.Messages.Category;

namespace Cik.Magazine.CategoryService.Domain
{
    internal class CategoryState
    {
        public string Name { get; private set; }
        internal IEventSink EventSink { get; set; }

        public void Handle(ICommand command)
        {
            ((dynamic) this).Handle((dynamic) command);
        }

        public void Mutate(IEvent @event)
        {
            ((dynamic) this).Apply((dynamic) @event);
        }

        public void Handle(CreateCategory message)
        {
            EventSink.Publish(new CategoryCreated(message.AggregateId, message.Name, message.ParentId));
        }

        public void Handle(UpdateCategory message)
        {
            EventSink.Publish(new CategoryUpdated(message.AggregateId, message.Name));
        }

        public void Handle(DeleteCategory message)
        {
            EventSink.Publish(new CategoryDeleted(message.AggregateId));
        }

        public void Apply(CategoryCreated message)
        {
            Name = message.Name;
        }

        public void Apply(CategoryUpdated message)
        {
            Name = message.Name;
        }

        public void Apply(CategoryDeleted message)
        {
        }

        public override string ToString()
        {
            return string.Join(", ", Name);
        }
    }
}