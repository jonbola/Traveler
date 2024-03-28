using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Traveler.FileExporter.OptionExporter;

namespace Traveler.FileExporter
{
    public class TourData : DataCollector
    {

        private DataCollector collector;
        private int id;
        private string tourName, desBegin, desEnd, maxCus, dateBegin, dateEnd, price, hotel, meeting, tourGuide, description;
        protected bool status;

        public TourData() { }

        public TourData(int id, string tourName, string desBegin, string desEnd, string maxCus, string dateBegin, string dateEnd, string price, string hotel, string meeting, string tourGuide, string description)
        {
            this.id = id;
            this.tourName = tourName;
            this.desBegin = desBegin;
            this.desEnd = desEnd;
            this.maxCus = maxCus;
            this.dateBegin = dateBegin;
            this.dateEnd = dateEnd;
            this.price = price;
            this.hotel = hotel;
            this.meeting = meeting;
            this.tourGuide = tourGuide;
            this.description = description;
        }

        public int GetId()
        {
            return id;
        }

        public void SetId(int value)
        {
            id = value;
        }

        public string GetTourName()
        {
            return tourName;
        }

        public void SetTourName(string value)
        {
            tourName = value;
        }

        public string GetDestinationBegin()
        {
            return desBegin;
        }

        public void SetDestinationBegin(string value)
        {
            desBegin = value;
        }

        public string GetDestinationEnd()
        {
            return desEnd;
        }

        public void SetDestinationEnd(string value)
        {
            desEnd = value;
        }

        public string GetMaxCutomer()
        {
            return maxCus;
        }

        public void SetMaxCustomer(string value)
        {
            maxCus = value;
        }

        public string GetDateBegin()
        {
            return dateBegin;
        }

        public void SetDateBegin(string value)
        {
            dateBegin = value;
        }

        public string GetDateEnd()
        {
            return dateEnd;
        }

        public void SetDateEnd(string value)
        {
            dateEnd = value;
        }

        public string GetPrice()
        {
            return price;
        }

        public void SetPrice(string value)
        {
            price = value;
        }

        public string GetHotel()
        {
            return hotel;
        }

        public void SetHotel(string value)
        {
            hotel = value;
        }

        public string GetMeetingLocation()
        {
            return meeting;
        }

        public void SetMeetingLocation(string value)
        {
            meeting = value;
        }

        public string GetTourGuide()
        {
            return tourGuide;
        }

        public void SetTourGuide(string value)
        {
            tourGuide = value;
        }

        public string GetDescription()
        {
            return description;
        }

        public void SetDescription(string value)
        {
            description = value;
        }

        public virtual void SetStatus(bool value)
        {
            status = value;
        }

        public void SetOption(DataCollector option)
        {
            collector = option;
        }

        public void ExportData(TourData tourData)
        {
            collector.ExportData(tourData);
        }
    }
}