﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TicketDesk.Web.Client.Helpers;
using TicketDesk.Domain.Services;
using System.Configuration;
using System.Web.Mvc;
using TicketDesk.Domain.Models;

namespace TicketDesk.Web.Client.Models
{
    public class TicketCreateViewModel
    {
        
        public SelectListUtility ListUtility { get; private set; }
        public ISecurityService Security { get; private set; }
        public SettingsService Settings { get; private set; }
        public Ticket Ticket { get; set; }

        public TicketCreateViewModel(ISecurityService security, SettingsService settings, Ticket ticket)
        {
            ListUtility = MefHttpApplication.ApplicationContainer.GetExportedValue<SelectListUtility>();
            Security = security;
            Settings = settings;
            Ticket = ticket;
        }

        public bool DisplayOwner
        {
            get
            {
                return Security.IsTdStaff();
            }
        }

        public bool DisplayPriorityList
        {
            get
            {
                bool isAllowed = Security.IsTdStaff();
                if (Security.IsTdSubmitter() && !Security.IsTdStaff())
                {
                    var configValue = ConfigurationManager.AppSettings["AllowSubmitterRoleToEditPriority"];
                    if (configValue != null)
                    {
                        isAllowed = Convert.ToBoolean(configValue);
                    }
                }
                return isAllowed;
            }
        }

        public SelectList PriorityList
        {
            get { return ListUtility.GetPriorityList(true, string.Empty); }

        }

        public SelectList CategoryList
        {
            get { return ListUtility.GetCategoryList(true, string.Empty); }
        }

        public SelectList TicketTypeList
        {
            get { return ListUtility.GetTicketTypeList(true, string.Empty); }
        }

        public SelectList OwnersList
        {
            get { return ListUtility.GetSubmittersList(true, Security.CurrentUserName); }
        }
    }
}