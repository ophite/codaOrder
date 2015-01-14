using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebApplication3.Entity
{
    public class Uow : IUow, IDisposable
    {
        #region Properties

        private codaJournal _dbContext;

        #endregion
        #region Methods

        public Uow() : this(new codaJournal()) { }
        public Uow(codaJournal context)
        {
            this._dbContext = context;
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