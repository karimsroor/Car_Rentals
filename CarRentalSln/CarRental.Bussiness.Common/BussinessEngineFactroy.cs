using Core.Common.Contracts;
using Core.Common.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Bussiness.Common
{
    public class BussinessEngineFactroy : IBusinessEngineFactory
    {
        public T GetBusinessEngine<T>() where T : IBusinessEngine
        {
            return ObjectBase.Container.GetExportedValue<T>();  
        }
    }
}
