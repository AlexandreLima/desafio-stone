using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace MundiPagg.Api.Product.Domain.Utilities
{
    public abstract class AggregateRoot : IAggregateRoot<ObjectId>
    {
        public AggregateRoot()
        {
            this.ID = new ObjectId();
        }

        public ObjectId ID { get; private set; }
    }
}
