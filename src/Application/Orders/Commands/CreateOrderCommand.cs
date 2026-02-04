using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Application.Orders.Commands
{
    public class CreateOrderCommand : IRequest<Guid>
    {
        [Required]
        public Guid CustomerId { get; set; }

        [Required]
        [MinLength(1, ErrorMessage = "At least one item is required.")]
        public List<CreateOrderItemDto> Items { get; set; } = new();

        [RegularExpression("^(None|Percent10|Percent20|Fixed5|Fixed10)$", ErrorMessage = "Invalid discount strategy.")]
        public string DiscountStrategy { get; set; } = "None";
    }

    public class CreateOrderItemDto
    {
        [Required]
        [MinLength(2)]
        public string ProductName { get; set; } = string.Empty;

        [Range(0.01, 1000000)]
        public decimal UnitPrice { get; set; }

        [Range(1, 1000000)]
        public int Quantity { get; set; }
    }
}
