using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CManager.Domain.Models
{
    public class AddressModel
    {        
        [StringLength(50)]
        public required string StreetAddress { get; set; }

        [StringLength(50)]
        public required string PostalCode { get; set; }

        [StringLength(50)]
        public required string City { get; set; }
    }
}
