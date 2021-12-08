﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MealOrdering.Shared.Dto
{
    public class OrderDto
    {
        public Guid Id { get; set; }
        public DateTime CreateDate { get; set; }
        public Guid CreatedUserId { get; set; }
        public Guid SupplierdId { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
        public DateTime ExpireDate { get; set; }
        public String CreatedUserFullName { get; set; }
        public String SupplierName { get; set; }
    }
}
