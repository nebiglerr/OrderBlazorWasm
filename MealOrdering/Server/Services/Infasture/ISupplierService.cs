using MealOrdering.Shared.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MealOrdering.Server.Services.Infasture
{
    public interface ISupplierService
    {
        public Task<List<SuppliersDto>> GetSuppliers();

        public Task<SuppliersDto> CreateSupplier(SuppliersDto Order);

        public Task<SuppliersDto> UpdateSupplier(SuppliersDto Order);

        public Task DeleteSupplier(Guid SupplierId);

        public Task<SuppliersDto> GetSupplierById(Guid Id);
    }
}
