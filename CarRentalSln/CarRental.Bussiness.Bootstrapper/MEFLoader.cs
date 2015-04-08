using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition.Hosting;
using CarRental.Bussiness.Entities;
using CarRental.Data;
using CarRental.Data.Data_Repositories;
using Core.Common.Contracts;
using CarRental.Bussiness.Engines;

using System.ComponentModel.Composition.Primitives;
using CarRental.Bussiness;

namespace CarRental.Bussiness.Bootstrapper
{
    public static class MEFLoader
    {
        public static CompositionContainer Init()
        {
            return Init(null);
        }

        public static CompositionContainer Init(ICollection<ComposablePartCatalog> catalogParts)
        {
            AggregateCatalog catalog = new AggregateCatalog();

            catalog.Catalogs.Add(new AssemblyCatalog(typeof(CarRentalEngine).Assembly));
            catalog.Catalogs.Add(new AssemblyCatalog(typeof(AccountRepository).Assembly));

            CompositionContainer container = new CompositionContainer(catalog);

            return container;
        }

    }
}
