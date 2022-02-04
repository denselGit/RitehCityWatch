using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace CityWatch.Common
{
    [Table("Ticket")]
    public class Ticket : Data
    {
        public int IdTicket {  get; set; }
        public int IdTenant {  get; set; }
        public int IdCreatedBy {  get; set; }
        public DateTime CreateDateTime {  get; set; }
        public ETicketStatus Status { get; set; }
        public ETicketPriority Priority { get; set; }
        public DateTime LastUpdate {  get; set; }
        public int LastUpdateBy {  get; set; }
        public int? IdService { get; set;  }
        public int? IdCategory {  get; set; }
        public int? IdSubCategory {  get; set; }
        public string Title {  get; set; }
        public string Description {  get; set; }
        public int? IdClosedBy {  get; set; }
        public DateTime? CloseDateTime {  get; set; }

        public enum ETicketPriority
        {
            Low = 0,
            Medium = 1,
            High = 2,
            Critical = 3
        }

        public enum ETicketStatus
        {
            Created = 0,
            Reopen,
            InProgress,
            Waiting,
            Closed,
            Rejected
        }
    }
}
