using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.ApplicationCore.Domain
{
    public class FullName
    {
        [MaxLength(300)]
        [StringLength(250, ErrorMessage = "Longueur doit être <= 250")]
        public string FirstName { get; set; }
        [MaxLength(300)]
        [StringLength(250, ErrorMessage = "Longueur doit être <= 250")]
        public string LastName { get; set; }
        public override string ToString()
        {
            return $"FirstName : {FirstName}, LastName : {LastName}";
        }
    }
}
