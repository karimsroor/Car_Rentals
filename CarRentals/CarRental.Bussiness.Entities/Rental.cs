using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using Core.Common.Core;
using Core.Common.Contracts;

namespace CarRental.Bussiness.Entities
{
    [DataContract]
    public class Rental : EntityBase, IIdentifiableEntity,IAccountOwnedEntity
    {
        [DataMember]
        public int RentalId { get; set; }

        [DataMember]
        public int AccountId { get; set; }

        [DataMember]
        public int CarId { get; set; }

        [DataMember]
        public DateTime DateRented { get; set; }

        [DataMember]
        public DateTime DateDue { get; set; }

        [DataMember]
        public DateTime? DateReturned { get; set; }

        #region IIdentifiableEntity members

        public int EntityId
        {
            get { return RentalId; }
            set { RentalId = value; }
        }

        #endregion

        #region IAccountOwnedEntity members

        int IAccountOwnedEntity.OwnerAccountId
        {
            get { return AccountId; }
        }

        #endregion


    }
}
