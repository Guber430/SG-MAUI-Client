using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace SG_MAUI_Client.Models
{
    public class Athlete
    {
        public int ID { get; set; }

        [Display(Name = "Athlete")]
        public string Summary
        {
            get
            {
                return FormalName + " - " + ACode;
            }
        }

        [Display(Name = "Athlete")]
        public string FullName
        {
            get
            {
                return FirstName
                    + (string.IsNullOrEmpty(MiddleName) ? " " :
                        (" " + (char?)MiddleName[0] + ". ").ToUpper())
                    + LastName;
            }
        }
        [Display(Name = "Athlete")]
        public string FormalName
        {
            get
            {
                return LastName + ", " + FirstName
                    + (string.IsNullOrEmpty(MiddleName) ? "" :
                        (" " + (char?)MiddleName[0] + ".").ToUpper());
            }
        }
        [Display(Name = "ID Code")]
        public string ACode
        {
            get
            {
                return "A:" + AthleteCode.ToString().PadLeft(7, '0');
            }
        }
        public string Age
        {
            get
            {
                DateTime today = DateTime.Today;
                int a = today.Year - DOB.Year
                    - ((today.Month < DOB.Month || (today.Month == DOB.Month && today.Day < DOB.Day) ? 1 : 0));
                return a.ToString(); /*Note: You could add .PadLeft(3) but spaces disappear in a web page. */
            }
        }

        public string FirstName { get; set; } = "";
        public string? MiddleName { get; set; }
        public string LastName { get; set; } = "";
        public string AthleteCode { get; set; } = "0000000";
        public DateTime DOB { get; set; }
        public int Height { get; set; }
        public double Weight { get; set; }
        public string Gender { get; set; } = "";
        public string Affiliation { get; set; } = "";
        public Byte[]? RowVersion { get; set; }

        public int ContingentID { get; set; } //Foreign Key

        public int SportID { get; set; } //Foreign Key

        public Contingent? Contingent { get; set; }
        public Sport? Sport { get; set; }
    }
}
