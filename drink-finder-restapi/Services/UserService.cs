using AutoMapper;
using drink_finder_restapi.Domain.Models;
using drink_finder_restapi.Domain.Repositories;
using drink_finder_restapi.Domain.Services;
using drink_finder_restapi.Domain.Services.Communication;
using drink_finder_restapi.Migrations;
using drink_finder_restapi.Persistence.Repositories;
using drink_finder_restapi.Resources;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace drink_finder_restapi.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserService(IUserRepository userRepository, ITokenService tokenService, IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<string> authenticate(string username, string password)
        {
            var user = await _userRepository.getByUsernameAsync(username);
            if (user != null && user.Password == password) 
            {
                var token = _tokenService.GenerateToken(user);
                return token; 
            }
            return null;
        }

        public async Task<SaveUserResponse> register(string username, string password)
        {
            try
            {
                var user = await _userRepository.getByUsernameAsync(username);
                if(user != null)
                {
                    return new SaveUserResponse($"Already exists.");
                }
            var newUser = new User { Username = username, Password = password, Role = "User"};
                await _userRepository.registerUser(newUser);
                await _unitOfWork.CompleteAsync();

                return new SaveUserResponse(newUser);
            }
            catch (Exception ex)
            {
                return new SaveUserResponse($"An error occurred when saving the category: {ex.Message}");
            }
        }

        public async Task<Establishment> getEstablishmentByIdAndUserId(int establishmentId)
        {
            var user = _httpContextAccessor.HttpContext?.User;
            int userId = int.Parse(user.FindFirst(ClaimTypes.NameIdentifier).Value);
            return await _userRepository.FindEstablishmentByIdAndUserIdAsync(establishmentId,userId);
        }

        public async Task<PageResource<EstablishmentResource>> getAllEstablishmentsPageAsync(int pageNumber, int pageSize, string query, string cityFilter, string sortBy, string sort)
        {
            var user = _httpContextAccessor.HttpContext?.User;
            int userId = int.Parse(user.FindFirst(ClaimTypes.NameIdentifier).Value);
            var establishments = await _userRepository.getAllEstablishmentsPageAsync(userId, query, cityFilter, sortBy, sort);
            int totalItems = establishments.Count();
            establishments = establishments.Skip((pageNumber - 1) * pageSize).Take(pageSize);
            var resources = _mapper.Map<IEnumerable<Establishment>, IEnumerable<EstablishmentResource>>(establishments);
            var pageResource = new PageResource<EstablishmentResource>
            {
                Items = resources.ToList(),
                TotalItems = totalItems,
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            return pageResource;
        }

        public async Task<SaveEstablishmentResponse> SaveEstablishmentAsync(Establishment establishment)
        {
            try
            {
                var user = _httpContextAccessor.HttpContext?.User;
                int userId = int.Parse(user.FindFirst(ClaimTypes.NameIdentifier).Value);
                establishment.UserId = userId;
                await _userRepository.AddEstablishmentAsync(establishment);
                await _unitOfWork.CompleteAsync();

                return new SaveEstablishmentResponse(establishment);
            }
            catch (Exception ex)
            {
                return new SaveEstablishmentResponse($"An error occurred when saving the category: {ex.Message}");
            }
        }

        public async Task<SaveEstablishmentDrinkResponse> SaveEstablishmentDrinkAsync(EstablishmentDrink establishmentDrink)
        {
            try
            {
                await _userRepository.AddEstablishmentDrinkAsync(establishmentDrink);
                await _unitOfWork.CompleteAsync();

                return new SaveEstablishmentDrinkResponse(establishmentDrink);
            }
            catch (Exception ex)
            {
                return new SaveEstablishmentDrinkResponse($"An error occurred when saving the category: {ex.Message}");
            }
        }

        public async Task<SaveEstablishmentResponse> UpdateEstablishmentAsync(int id, Establishment establishment)
        {
            var user = _httpContextAccessor.HttpContext?.User;
            int userId = int.Parse(user.FindFirst(ClaimTypes.NameIdentifier).Value);

            var existingEstablishment = await _userRepository.FindEstablishmentByIdAndUserIdAsync(id, userId);

            if (existingEstablishment == null)
                return new SaveEstablishmentResponse("Category not found.");

            existingEstablishment.Name = establishment.Name;
            existingEstablishment.Address = establishment.Address;
            existingEstablishment.CityId = establishment.CityId;
            existingEstablishment.City =  establishment.City;

            try
            {
                _userRepository.UpdateEstablishment(existingEstablishment);
                await _unitOfWork.CompleteAsync();

                return new SaveEstablishmentResponse(existingEstablishment);
            }
            catch (Exception ex)
            {
                return new SaveEstablishmentResponse($"An error occurred when updating the category: {ex.Message}");
            }
        }

        public async Task<SaveEstablishmentResponse> DeleteEstablishmentAsync(int id)
        {
            var user = _httpContextAccessor.HttpContext?.User;
            int userId = int.Parse(user.FindFirst(ClaimTypes.NameIdentifier).Value);
            var existingEstablishment = await _userRepository.FindEstablishmentByIdAndUserIdAsync(id, userId);

            if (existingEstablishment == null)
                return new SaveEstablishmentResponse("Category not found.");

            try
            {
                _userRepository.RemoveEstablishment(existingEstablishment);
                await _unitOfWork.CompleteAsync();

                return new SaveEstablishmentResponse(existingEstablishment);
            }
            catch (Exception ex)
            {

                return new SaveEstablishmentResponse($"An error occurred when deleting the category: {ex.Message}");
            }
        }
        public async Task<SaveEstablishmentDrinkResponse> DeleteEstablishmentDrinkAsync(int establishmentId,int drinkId)
        {
            var existingEstablishmentDrink = await _userRepository.FindEstablishmentDrinkByIdsAsync(establishmentId, drinkId);

            if (existingEstablishmentDrink == null)
                return new SaveEstablishmentDrinkResponse("Category not found.");

            try
            {
                _userRepository.RemoveEstablishmentDrink(existingEstablishmentDrink);
                await _unitOfWork.CompleteAsync();

                return new SaveEstablishmentDrinkResponse(existingEstablishmentDrink);
            }
            catch (Exception ex)
            {

                return new SaveEstablishmentDrinkResponse($"An error occurred when deleting the category: {ex.Message}");
            }
        }
    }
}
