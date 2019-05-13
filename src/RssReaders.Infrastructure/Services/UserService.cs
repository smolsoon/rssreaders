using System;
using System.Threading.Tasks;
using AutoMapper;
using MongoDB.Bson;
using RssReaders.Core.Model;
using RssReaders.Core.Repositories;
using RssReaders.Infrastructure.DTO;
using RssReaders.Infrastructure.Settings;

namespace RssReaders.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IJwtHandler _jwtHandler;
        public UserService(IUserRepository userRepository, IMapper mapper, IJwtHandler jwtHandler)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _jwtHandler = jwtHandler;
        }
        public async Task<AccountDTO> GetAccountAsync(ObjectId userId)
        {
            var user = await _userRepository.GetAsync(userId);
            return _mapper.Map<AccountDTO>(user);
        }

        public async Task<TokenDTO> LoginAsync(string email, string password)
        {
           var user = await _userRepository.GetAsync(email);
            if(user == null){
                throw new Exception("Invalid credentials.");
            }
            if(user.Password != password){
                throw new Exception("Invalid credentials.");
            }
            
            var jwt = _jwtHandler.CreateToken(user.Id, user.Role);

            return new TokenDTO
            {
                Token = jwt.Token,
                Expires = jwt.Expires,
                Role = user.Role
            };            
        }

        public async Task RegisterAsync(ObjectId userId, string email, string username, string password, string role = "user")
        {
            var user = await _userRepository.GetAsync(email);
            if(user != null)
            {
                throw new Exception($"User with email: '{email}' already exists.");
            }
            user = new User(userId, role, username, email, password);
            await _userRepository.AddAsync(user);
        }
    }
}