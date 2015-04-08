using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Common.Core;
using Core.Common.Contracts;
using System.ComponentModel.Composition;

namespace CarRental.Bussiness.Engines
{
    [Export(typeof(IBusinessEngineFactory))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class BussinessEngineFactory : IBusinessEngineFactory
    {
        T IBusinessEngineFactory.GetBusinessEngine<T>()
        {
            return ObjectBase.Container.GetExportedValue<T>();
        }
    }
}
