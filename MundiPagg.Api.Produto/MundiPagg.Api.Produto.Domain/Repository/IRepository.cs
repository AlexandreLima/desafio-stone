using MongoDB.Bson;
using MundiPagg.Api.Product.Domain.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MundiPagg.Api.Product.Domain.Repository
{
    public interface IRepository<TType, Key> where TType : IAggregateRoot<Key>
    {
        void Add(TType entity);

        void Edit(TType entity);

        void Remove(Key id);

        void Get(Key id);

        void GetAll(int pagin, int paginRows);
    }
}
