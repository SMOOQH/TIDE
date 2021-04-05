using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace TIDEFloodMonitoring.Models
{
    public class FloodResponse : Error
    {
        public string Context { get; set; }
        public Meta Meta { get; set; }
        public List<Item> Items { get; set; }

        public FloodResponse(string error)
        {
            Message = error;
        }

    }

    public class Error
    {
        public string Message;
    }

    public class Meta
    {
        public string Publisher { get; set; }
        public string Licence { get; set; }
        public string Documentation { get; set; }
        public string Version { get; set; }
        public string Comment { get; set; }
        public List<string> HasFormat { get; set; }
    }

    public class FloodArea
    {
        public string Id { get; set; }
        public string County { get; set; }
        public string Notation { get; set; }
        public string Polygon { get; set; }
        public string RiverOrSea { get; set; }
    }

    public class Item
    {

        public string Id { get; set; }
        public string Description { get; set; }
        public string EaAreaName { get; set; }
        public string EaRegionName { get; set; }
        public FloodArea FloodArea { get; set; }
        public string FloodAreaId { get; set; }
        public bool IsTidal { get; set; }
        public string Message { get; set; }
        public string Severity { get; set; }
        public int SeverityLevel { get; set; }
        public DateTime TimeMessageChanged { get; set; }
        public DateTime TimeRaised { get; set; }
        public DateTime TimeSeverityChanged { get; set; }
    }

}
