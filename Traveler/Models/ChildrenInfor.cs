using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Traveler.Models
{
    public class ChildrenInfor
    {
        public string CodeTicket { get; set; }
        public string Gender { get; set; }
        public double PriceTicket { get; set; }


        public ChildrenInfor(string codeTicket, string gender, double priceTicket)
        {
            this.CodeTicket = codeTicket;
            this.Gender = gender;
            this.PriceTicket = priceTicket;
        }

        public ChildrenInfor()
        {
            this.CodeTicket = this.GenerateRandomString(20);
            this.Gender = "";
            this.PriceTicket = 0;
        }

        private string GenerateRandomString(int length)
        {
            const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            Random random = new Random();

            StringBuilder sb = new StringBuilder(length);
            for (int i = 0; i < length; i++)
            {
                int index = random.Next(chars.Length);
                sb.Append(chars[index]);
            }

            return sb.ToString();
        }


    }
}