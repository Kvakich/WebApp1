using App.DAL.Entities;
using App.DAL.Uow;
using AutoMapper;
using App.BLL.BusinessModels;
using App.BLL.DTO;
using App.BLL.Infrastructure;
using App.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.BLL.Services
{
    public class OrderService : IOrderService
    {
        IUnitOfWork Database { get; set; }

        public OrderService(IUnitOfWork uow)
        {
            Database = uow;
        }
        public void MakeOrder(OrderDTO orderDto)
        {
            Phone phone = Database.Phones.Get(orderDto.PhoneId);

            // валидация
            if (phone == null)
                throw new ValidationEx("Телефон не найден", "");
            // применяем скидку
            decimal sum = new Discount(0.1m).GetDiscountedPrice(phone.Price);
            Order order = new Order
            {
                Date = DateTime.Now,
                Address = orderDto.Address,
                PhoneId = phone.Id,
                Sum = sum,
                PhoneNumber = orderDto.PhoneNumber
            };
            Database.Orders.Create(order);
            Database.Save();
        }

        public IEnumerable<PhoneDTO> GetPhones()
        {
            var result = (List<PhoneDTO>)null;

            var phones = Database.Phones.GetAll().ToList();

            Mapper.Initialize(cfg => cfg.CreateMap<Phone, PhoneDTO>());
            result = Mapper.Map<List<Phone>, List<PhoneDTO>>(phones);

            return result;
        }

        public PhoneDTO GetPhone(int? id)
        {
            if (id == null)
                throw new ValidationEx("Не установлено id телефона", "");
            var phone = Database.Phones.Get(id.Value);
            if (phone == null)
                throw new ValidationEx("Телефон не найден", "");
            // применяем автомаппер для проекции Phone на PhoneDTO
            Mapper.Initialize(cfg => cfg.CreateMap<Phone, PhoneDTO>());
            return Mapper.Map<Phone, PhoneDTO>(phone);
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
