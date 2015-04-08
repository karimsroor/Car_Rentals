﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarRental.DataContracts;
using Core.Common.Contracts;
using Core.Common.Core;
using System.ComponentModel.Composition;

namespace CarRental.Data
{
    [Export(typeof(IDataRepositoryFactory))]
        [PartCreationPolicy(CreationPolicy.NonShared)]
    public class DataRepositoryFactory : IDataRepositoryFactory
    {
        
        public T GetDataRepository<T>() where T : IDataRepository
        {
            return ObjectBase.Container.GetExportedValue<T>();
        }
    }
}
