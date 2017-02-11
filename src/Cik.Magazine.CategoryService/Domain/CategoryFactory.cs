﻿using System;
using Akka.Actor;
using Akka.Routing;
using Cik.Magazine.Core;
using Cik.Magazine.Core.Domain;
using Cik.Magazine.Core.Storage.Projections;

namespace Cik.Magazine.CategoryService.Domain
{
    public static class CategoryFactory
    {
        public static IActorRef CategoryAggregate(this IActorRefFactory system, Guid id, int snapshotThreshold = 250)
        {
            var projectionsProps = new ConsistentHashingPool(5).Props(Props.Create<ReadModelProjections>());
            var projections = system.ActorOf(projectionsProps, SystemData.ProjectionsActor.Name);
            var creationParams = new AggregateRootCreationParameters(id, projections, snapshotThreshold);
            return system.ActorOf(Props.Create<Category>(creationParams), "category-service");
        }
    }
}