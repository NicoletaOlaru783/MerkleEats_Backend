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
    public class TvService : ITvService
    {
        private readonly ITvRepository _tvRepo;
        private readonly IMapper _mapper;

        public TvService(ITvRepository tvRepo, IMapper mapper)
        {
            _tvRepo = tvRepo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TV>> ReadAllAsync()
        {
            var results = await _tvRepo.ReadAllAsync();
            var objDto = new List<TV>();
            foreach (var obj in results)
            {
                objDto.Add(obj);
            }
            return objDto;
        }

        public async Task<Instagram> ReadOneAsync(int id)
        {
            return await _tvRepo.ReadOneAsync(id);
        }

        public async Task<TV> UpdateAsync(TV tv)
        {
            return await _tvRepo.UpdateAsync(tv);
        }

        public async Task<Instagram> UpdateAsync(Instagram instagram)
        {
            return await _tvRepo.UpdateAsync(instagram);
        }
    }
}
