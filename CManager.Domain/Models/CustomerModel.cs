using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CManager.Domain.Models
{
    public class CustomerModel
    {

        public Guid Id { get; set; }

        [StringLength(50)]
        public required string FirstName { get; set; }
        [StringLength(50)]
        public required string LastName { get; set; }
        [StringLength(50)]
        public required string Email { get; set; }
        [StringLength(20)]
        public required string PhoneNumber { get; set; }

        public required AddressModel Address { get; set; }

    }
}
