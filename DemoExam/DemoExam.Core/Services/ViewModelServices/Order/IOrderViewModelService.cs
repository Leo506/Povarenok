﻿using DemoExam.Core.Models;

namespace DemoExam.Core.Services.ViewModelServices.Order;

public interface IOrderViewModelService
{
    IEnumerable<OrderItem> GetProductInOrder();
    IEnumerable<PickupPoint> GetPickupPoints();
    void AddProduct(string productId);
    void RemoveProduct(string productId);
}