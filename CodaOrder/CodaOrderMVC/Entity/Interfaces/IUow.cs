using iOrder.Entity.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace iOrder.Entity
{
    public interface IUow : IDisposable
    {
        void Save();
        IDocumentRepository DocumentRepository { get; }
        ICodaJsonRepository CodaJsonRepository { get; }
        IAccountRepository AccountRepository { get; }
    }
}