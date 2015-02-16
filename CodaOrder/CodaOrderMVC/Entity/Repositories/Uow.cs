using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApplication3.Entity.Interfaces;
using WebApplication3.Entity.Repositories;
using WebApplication3.Models;

namespace WebApplication3.Entity
{
    public class Uow : IUow, IDisposable
    {
        #region Properties

        private codaJournal _dbContext;
        private IdentityContext _dbIdentityContext;

        #endregion
        #region Methods

        public Uow() : this(new codaJournal(), new IdentityContext()) { }
        public Uow(codaJournal context, IdentityContext identityContext)
        {
            this._dbContext = context;
            this._dbIdentityContext = identityContext;
            // Do NOT enable proxied entities, else serialization fails.
            //if false it will not get the associated certification and skills when we
            ////get the applicants
            //this._dbContext.Configuration.ProxyCreationEnabled = false;

            //// Load navigation properties explicitly (avoid serialization trouble)
            //this._dbContext.Configuration.LazyLoadingEnabled = false;

            //// Because Web API will perform validation, we don't need/want EF to do so
            //this._dbContext.Configuration.ValidateOnSaveEnabled = false;

            //DbContext.Configuration.AutoDetectChangesEnabled = false;
            // We won't use this performance tweak because we don't need 
            // the extra performance and, when autodetect is false,
            // we'd have to be careful. We're not being that careful.
        }

        #endregion
        #region IUow

        private IDocumentRepository _documentRepository;
        public IDocumentRepository DocumentRepository
        {
            get
            {
                if (_documentRepository == null)
                    _documentRepository = new DocumentRepository(_dbContext);

                return _documentRepository;
            }
        }

        private ICodaJsonRepository _codaJsonRepository;
        public ICodaJsonRepository CodaJsonRepository
        {
            get
            {
                if (_codaJsonRepository == null)
                    _codaJsonRepository = new CodaJsonRepository(_dbContext);

                return _codaJsonRepository;
            }
        }

        private IAccountRepository _accountRepository;
        public IAccountRepository AccountRepository
        {
            get
            {
                if (_accountRepository == null)
                    _accountRepository = new AccountRepository(_dbIdentityContext);

                return _accountRepository;
            }
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        #endregion
        #region IDisposable

        private bool _isDisposed;

        public void Disposing(bool isDisposing)
        {
            if (!this._isDisposed)
            {
                if (isDisposing)
                {
                    if (_dbContext != null)
                        _dbContext.Dispose();
                }
            }

            _isDisposed = true;
        }

        public void Dispose()
        {
            Disposing(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}