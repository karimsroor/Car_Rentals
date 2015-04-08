﻿using Core.Common.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition;
using System.ServiceModel;
using CarRental.Bussiness.Entities;
using Core.Common.Contracts;
using System.Threading;
using CarRental.Common;

namespace CarRental.Bussiness.Managers
{
    public class ManagerBase
    {
        protected string _LoginName;
        protected Account _AuthorizationAccount;
        public ManagerBase()
        {
            OperationContext context = OperationContext.Current;
            if (context != null)
            {
                try
                {
                    _LoginName = OperationContext.Current.IncomingMessageHeaders.GetHeader<string>("String", "System");
                    if (_LoginName.IndexOf(@"\") > -1) _LoginName = string.Empty;
                }
                catch
                {
                    _LoginName = string.Empty;
                }
            }

            if (ObjectBase.Container != null)
                ObjectBase.Container.SatisfyImportsOnce(this);

            if (!string.IsNullOrWhiteSpace(_LoginName))
                _AuthorizationAccount = LoadAuthorizationValidationAccount(_LoginName);
        //    OperationContext context = OperationContext.Current;
        //    if (context != null)
        //        _LoginName = context.IncomingMessageHeaders.GetHeader<string>("string","system");
        //    if (_LoginName.IndexOf(@"\") > 1) _LoginName = string.Empty;


        //    if (ObjectBase.Container != null)
        //        ObjectBase.Container.SatisfyImportsOnce(this);

        //    if (!string.IsNullOrWhiteSpace(_LoginName))
        //        _AuthorizationAccount = LoadAuthorizationValidationAccount(_LoginName);

        }

        protected virtual Account LoadAuthorizationValidationAccount(string loginName)
        {
            return null;
        }

        protected void ValidateAuthorization(IAccountOwnedEntity entity)
        {
            if (!Thread.CurrentPrincipal.IsInRole(Security.CarRentalAdminRole))
            {
                if (_LoginName != string.Empty && entity.OwnerAccountId != _AuthorizationAccount.AccountId)
                {
                    AuthorizationValidationException ex = new AuthorizationValidationException("Attempt to access a secure record with improper user authorization validation.");
                    throw new FaultException<AuthorizationValidationException>(ex, ex.Message);
                }
            }
        }
        protected T ExecuteFaultHandledOperation<T>(Func<T> codeToExecute)
        {
            try
            {
                return codeToExecute.Invoke();
            }
            catch (FaultException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }


        protected void ExecuteFaultHandledOperation(Action codeToExecute)
        {
            try
            {
                 codeToExecute.Invoke();
            }
            catch (FaultException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }
    }
}
