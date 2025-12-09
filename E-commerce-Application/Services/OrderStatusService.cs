using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_commerce_Application.DTOs.OrderStatusDTOs;
using E_commerce_Application.Services_Interfaces;
using E_commerce_Core.Interfaces.Unit_Of_Work_Interface;
using Mapster;

namespace E_commerce_Application.Services
{
    public class OrderStatusService : IOrderStatusService
    {
        private readonly IUnitOfWork _uow;

        public OrderStatusService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<IEnumerable<OrderStatusDto>> GetAllAsync()
        {
            var statuses = await _uow.OrderStatuses.GetAllStatusesAsync();

            return statuses.Adapt<IEnumerable<OrderStatusDto>>();
        }
    }

}
