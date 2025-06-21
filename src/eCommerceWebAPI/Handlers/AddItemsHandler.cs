using AutoMapper;
using MediatR;
using eCommerceWebAPI.Commands;
using eCommerceWebAPI.DTOs;
using eCommerceWebAPI.Entities;
using eCommerceWebAPI.Interface;

namespace eCommerceWebAPI.Handlers
{
    // Handler for adding items to the cart
    public class AddItemsHandler : IRequestHandler<AddCartItemCommand, CartItem>
    {
        private readonly ICartItemRepository _cartItemRepository;
        private readonly IMapper _mapper;

        // Constructor: Injects the cart item repository and mapper dependencies.
        public AddItemsHandler(ICartItemRepository cartItemRepository, IMapper mapper)
        {
            _cartItemRepository = cartItemRepository;
            _mapper = mapper;
        }

        // Handles the AddCartItemCommand by creating a new CartItem entity and saving it to the repository.
        public async Task<CartItem> Handle(AddCartItemCommand request, CancellationToken cancellationToken)
        {
            var item = new CartItem()
            {
                ItemName = request.CartItems.ItemName,
                ItemPrice = request.CartItems.ItemPrice,
                CustomerID = request.Guid,
            };

            return await _cartItemRepository.Createitem(item);
        }
    }
}
