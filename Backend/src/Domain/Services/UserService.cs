﻿using Application.IServices;
using Application.Models;
using Domain.IRepositories;
using static Domain.Exceptions.Exceptions;

namespace Domain.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<UserModel> CreateUser(UserModel user)
        {
            return await _userRepository.Create(user);
        }

        public async Task<List<UserModel>> GetAllUsers()
        {
            return await _userRepository.GetAll();
        }

        public async Task<UserModel> GetUserById(int id)
        {
            return await _userRepository.GetById(id)
                ?? throw new NotFoundException($"User not found by this ID: {id}");
        }
    }
}
