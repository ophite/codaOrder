using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApplication3.Entity.Interfaces;

namespace WebApplication3.Entity
{
    public interface IUow : IDisposable
    {
        void Save();
        IDocumentRepository DocumentRepository { get; }
        ICodaJsonRepository CodaJsonRepository { get; }
        IAccountRepository AccountRepository { get; }
    }
}