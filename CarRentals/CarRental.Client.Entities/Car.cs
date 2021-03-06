﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Common.Core;

namespace CarRental.Client.Entities
{   

   public class Car : ObjectBase
    {
         int _CarId ;
         string _Description ;
         string _Color;
         int _Year ;
         decimal _RentalPrice;
         bool _CurrentlyRented;

         public int CarId 
         {
             get 
             {
                 return CarId;
             }
             set
             {
                 if (_CarId != value)
                 {
                     _CarId = value;
                     OnPropertyChanged(() => CarId);
                 }
             }
         }

         public string Description
         {
             get
             {
                 return _Description;
             }
             set
             {
                 if (_Description != value)
                 {
                     _Description = value;
                     OnPropertyChanged(() => CarId);
                 }
                 
             }
         }

         public string Color
         {
             get 
             {
                 return _Color;
             }
             set 
             {
                 if (_Color != value)
                 {
                     _Color = value;
                     OnPropertyChanged(() => CarId);
                 }
                 
             }
         }

         public int Year 
         {
             get
             {
                 return _Year;
             }
             set
             {
                 if (_Year != value)
                 {
                     _Year = value;
                     OnPropertyChanged(() => CarId);
                 }
                 
             }
         }

         public decimal RentalPrice
         {
             get 
             {
                 return _RentalPrice;
             }
             set 
             {
                 if (_RentalPrice != value)
                 {
                     _RentalPrice = value;
                     OnPropertyChanged(() => CarId);
                 }
                  
             }
         }

         public bool CurrentlyRented
         {
             get
             {
                 return _CurrentlyRented;
             }
             set
             {
                 if (_CurrentlyRented != value)
                 {
                     _CurrentlyRented = value;
                     OnPropertyChanged(() => CarId);
                 }
             }
         }

    }
}
