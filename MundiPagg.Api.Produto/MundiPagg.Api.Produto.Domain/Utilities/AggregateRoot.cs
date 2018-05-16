using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace MundiPagg.Api.Products.Domain.Utilities
{
    public abstract class AggregateRoot : IAggregateRoot<Guid>
    {
        public AggregateRoot() => this.Id = Guid.NewGuid();

        [BsonId]
        public Guid Id { get; private set; }
    }
}
