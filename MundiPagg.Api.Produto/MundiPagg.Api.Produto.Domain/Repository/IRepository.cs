using MongoDB.Bson;
using MundiPagg.Api.Products.Domain.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MundiPagg.Api.Products.Domain.Repository
{
    public interface IRepository<TType, Key> where TType : IAggregateRoot<Key>
    {
        void Add(TType entity);

        void Edit(TType entity);

        void Remove(Key id);

        TType Get(Key id);

        IEnumerable<TType> GetAll(int pagin, int paginRows);
    }
}
