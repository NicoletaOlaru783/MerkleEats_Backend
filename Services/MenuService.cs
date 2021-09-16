using AutoMapper;
using MerkleKitchenApp_V2.Model;
using MerkleKitchenApp_V2.Repository.IRepository;
using MerkleKitchenApp_V2.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MerkleKitchenApp_V2.Services
{
    public class MenuService : IMenuService
    {
        private readonly IMenuRepository _menuRepo;
        private readonly IMapper _mapper;

        public MenuService(IMenuRepository menuRepo, IMapper mapper)
        {
            _menuRepo = menuRepo;
            _mapper = mapper;
        }

        public async Task<List<Menu>> CreateAsync(List<Menu> menu)
        {
            var objDto = new List<Menu>();
            foreach (var obj in menu)
            {
                obj.CreatedAt = DateTime.Now;
                await _menuRepo.CreateAsync(obj);
            }
            return objDto;
        }

        public async Task DeleteAsync(List<Menu> menu)
        {
            foreach (var obj in menu)
            {
                await _menuRepo.DeleteAsync(obj.Id);
            }
        }

        public async Task<IEnumerable<Menu>> ReadAllAsync()
        {
            return await _menuRepo.ReadAllAsync();
        }
    }
}
