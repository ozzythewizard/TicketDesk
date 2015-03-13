﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketDesk.Domain.Model
{
    public class TicketEventNotification
    {
        public TicketEventNotification()
        {
            _isNew = true;
            _isRead = false;
        }

        [Key]
        [Column(Order = 0)]
        public int TicketId { get; set; }

        [Key]
        [Column(Order = 1)]
        [Index]
        public int EventId { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(256)]
        [Index]
        public string SubscriberUserName { get; set; }

        private bool _isNew;
        /// <summary>
        /// Gets or sets a value indicating whether this notification is new.
        /// </summary>
        /// <remarks>
        /// Typically, this would be used to alert a user that chages have happened, and once alerted 
        /// this would be toggled to false to prevent alerting them again.
        /// </remarks>
        /// <value><c>true</c> if this instance is new; otherwise, <c>false</c>.</value>
        public bool IsNew
        {
            get { return _isNew; }
            set
            {
                //when marking as new, read flag implicitly changed to false
                if (value)
                {
                    _isRead = false;
                }
                _isNew = value;
            }
        }

        private bool _isRead;
        /// <summary>
        /// Gets or sets a value indicating whether this notification has been read.
        /// </summary>
        /// <remarks>
        /// Typcially, this would be used to highlight uread items. Once viewed it would be 
        /// toggled to false to prevent it from being highlighted in the future.
        /// </remarks>
        /// <value><c>true</c> if this instance is read; otherwise, <c>false</c>.</value>
        public bool IsRead
        {
            get { return _isRead; }
            set
            {
                //when marking as read, new flag is implicitly chaged to false;
                if (value)
                {
                    IsNew = false;
                }
                _isRead = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether a push notification is pending.
        /// </summary>
        /// <remarks>
        /// Typically would be used to indicate if a push notificaiton is pending. 
        /// This can be used by email or sms systems to indicate that this 
        /// notificaiton is ready to go to the user.
        /// </remarks>
        /// <value><c>true</c> if [push notification pending]; otherwise, <c>false</c>.</value>
        public bool PushNotificationPending { get; set; }

        public virtual TicketEvent TicketEvent { get; set; }

        public virtual TicketSubscriber TicketSubscriber { get; set; }


    }
}
