﻿using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication3.Entity;
using WebApplication3.Entity.Interfaces;
using WebApplication3.Entity.Repositories;

namespace WebApplication3.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDependencyResolver()
        {
            kernel = new StandardKernel();
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            kernel.Bind<IUow>().To<Uow>();
            kernel.Bind<IDocumentRepository>().To<DocumentRepository>();
            kernel.Bind<ICodaJsonRepository>().To<CodaJsonRepository>();
            kernel.Bind<IAccountRepository>().To<AccountRepository>();
        }
    }
}