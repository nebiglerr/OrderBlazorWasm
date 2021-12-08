using MealOrdering.Server.Services.Infasture;
using MealOrdering.Shared.Dto;
using MealOrdering.Shared.ResponseModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MealOrdering.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SupplierController : ControllerBase
    {

        private readonly ISupplierService supplierService;

        public SupplierController(ISupplierService SupplierService)
        {
            supplierService = SupplierService;
        }



        [HttpGet("SupplierById/{Id}")]
        public async Task<ServiceResponse<SuppliersDto>> GetSupplierById(Guid Id)
        {
            return new ServiceResponse<SuppliersDto>()
            {
                Value = await supplierService.GetSupplierById(Id)
            };
        }


        [HttpGet("Suppliers")]
        public async Task<ServiceResponse<List<SuppliersDto>>> GetSuppliers()
        {
            return new ServiceResponse<List<SuppliersDto>>()
            {
                Value = await supplierService.GetSuppliers()
            };
        }


        [HttpPost("CreateSupplier")]
        public async Task<ServiceResponse<SuppliersDto>> CreateSupplier(SuppliersDto Supplier)
        {
            return new ServiceResponse<SuppliersDto>()
            {
                Value = await supplierService.CreateSupplier(Supplier)
            };
        }


        [HttpPost("UpdateSupplier")]
        public async Task<ServiceResponse<SuppliersDto>> UpdateSupplier(SuppliersDto Supplier)
        {
            return new ServiceResponse<SuppliersDto>()
            {
                Value = await supplierService.UpdateSupplier(Supplier)
            };
        }


        [HttpPost("DeleteSupplier")]
        public async Task<BaseResponse> DeleteSupplier([FromBody] Guid SupplierId)
        {
            await supplierService.DeleteSupplier(SupplierId);
            return new BaseResponse();
        }
    }
}
