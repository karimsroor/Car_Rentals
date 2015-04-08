using CarRental.Bussiness.Managers;
using System;
using System.Timers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using SM = System.ServiceModel;
using CarRental.Bussiness.Entities;
using System.Transactions;
using Core.Common.Core;
using CarRental.Bussiness.Bootstrapper;

namespace CarRental.ServiceHost.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            ObjectBase.Container = MEFLoader.Init();

            GenericPrincipal prinicpal = new GenericPrincipal
                (new GenericIdentity ("karim"),new string[]{"CarRentalGroup"});
            Thread.CurrentPrincipal = prinicpal;

            System.Console.WriteLine("Starting Up the Services......");
            System.Console.WriteLine("");
            SM.ServiceHost hostInventoryManager = new SM.ServiceHost(typeof(InventoryManager));
            SM.ServiceHost hostRentalManager = new SM.ServiceHost(typeof(RentalManager));
            SM.ServiceHost hostAccountManager = new SM.ServiceHost(typeof(AccountManager));

            StartService(hostInventoryManager,"Inventory Service");
            StartService(hostRentalManager, "Rental Service");
            StartService(hostAccountManager, "Account Service");

            System.Timers.Timer timer = new System.Timers.Timer(10000);
            timer.Elapsed += OnTimerElapsed;
            timer.Start();

            System.Console.WriteLine("Reservation timer monitor started");
            
            System.Console.WriteLine("");
            System.Console.WriteLine("Press [Enter] to exit. ");
            System.Console.ReadLine();

            timer.Stop();
            System.Console.WriteLine("Reservation timer monitor started");

            StopService(hostInventoryManager,"InventroyManager");
            StopService(hostRentalManager,"RentalManager");
            StopService(hostAccountManager,"Accountmanager");



        }

            private static void OnTimerElapsed(object sender, ElapsedEventArgs e)
            {
                RentalManager rentalManager = new RentalManager();
                Reservation[] reservations = rentalManager.GetDeadReservations();

                if (reservations != null)
                {
                    foreach (Reservation reservation in reservations)
                    {
                        using (TransactionScope scope = new TransactionScope())
                        {
                            try
                            {
                                rentalManager.CancelReservation(reservation.ReservationId);
                                scope.Complete();
                            }
                            catch (Exception ex)
                            {
                                System.Console.WriteLine("There was an exception when attempting to cancel reservation '{0}'." +"for more details : {1}", reservation.ReservationId,ex.Message);
                            }

                        }
                    }
                }
            }

        
        static void StartService(SM.ServiceHost host, string serviceDescription)
        {
            host.Open();
            System.Console.WriteLine("Service {0} Started", serviceDescription);

            foreach (var endpoint in host.Description.Endpoints)
            {
                System.Console.WriteLine("Listening on End Point:");
                System.Console.WriteLine("Address: {0}",endpoint.Address.Uri.ToString());
                System.Console.WriteLine("Binding: {0}", endpoint.Binding.Name);
                System.Console.WriteLine("Contract: {0}", endpoint.Contract.ConfigurationName);
            }
            System.Console.WriteLine();
        }

        static void StopService(SM.ServiceHost host, string serviceDescription)
        {
            host.Close();
            System.Console.WriteLine("Service {0} has stopped",serviceDescription);
        }
    }
}
