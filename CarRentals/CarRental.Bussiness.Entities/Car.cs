﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Core.Common.Core;
using Core.Common.Contracts;


namespace CarRental.Bussiness.Entities
{   
    [DataContract]
   public  class Car : EntityBase,IIdentifiableEntity
    {
        [DataMember]
        public int CarId { get; set; }

        [DataMember]
        public string Description {get; set;}

        [DataMember]
        public string Color { get; set; }

        [DataMember]
        public int Year { get; set; }

        [DataMember]
        public decimal RentalPrice { get; set; }

        [DataMember]
        public bool  CurrentlyRented { get; set; }

        public int EntityId
        {
            get
            {
                return CarId;
            }
            set
            {
                CarId = value;
            }
        }
    }
}
