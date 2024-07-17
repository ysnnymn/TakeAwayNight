﻿using AutoMapper;
using MongoDB.Driver;
using TakeAwayNight.Catalog.Dtos.ProductDtos;
using TakeAwayNight.Catalog.Entities;
using TakeAwayNight.Catalog.Settings;

namespace TakeAwayNight.Catalog.Services.ProductServices
{
    public class ProductService:IProductService
    {
        private readonly IMongoCollection<Product> _productCollection;
        private readonly IMapper _mapper;

        public ProductService(IMapper mapper, IDataBaseSettings _dataBaseSettings)
        {
            var client = new MongoClient(_dataBaseSettings.ConnectionString);
            var database = client.GetDatabase(_dataBaseSettings.DatabaseName);
            _productCollection = database.GetCollection<Product>(_dataBaseSettings.ProductCollectionName);
            _mapper = mapper;
        }

        public async Task CreateProductAsync(CreateProductDto CreateProductDto)
        {
            var value = _mapper.Map<Product>(CreateProductDto);
            await _productCollection.InsertOneAsync(value);
        }

        public async Task DeleteProductAsync(string id)
        {
            await _productCollection.DeleteOneAsync(x => x.ProductId == id);
        }

        public async Task<List<ResultProductDto>> GetAllProductAsync()
        {
            var values = await _productCollection.Find(x => true).ToListAsync();
            return _mapper.Map<List<ResultProductDto>>(values);
        }

        public async Task<GetByIdProductDto> GetByIdProductAsync(string id)
        {
            var values = await _productCollection.Find(x => x.ProductId == id).FirstOrDefaultAsync();
            return _mapper.Map<GetByIdProductDto>(values);
        }

        public async Task UpdateProductAsync(UpdateProductDto updateProductDto)
        {
            var values = _mapper.Map<Product>(updateProductDto);
            await _productCollection.FindOneAndReplaceAsync(x => x.ProductId == updateProductDto.ProductId, values);
        }
    }
}
