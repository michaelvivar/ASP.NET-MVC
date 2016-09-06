﻿using BL.Dto;
using DL;
using DL.Entities;
using Nelibur.ObjectMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services
{
    public class CountryService : BaseService 
    {
        private Country MapDtoToEntity(CountryDto dto)
        {
            return TinyMapper.Map<Country>(dto);
        }

        private CountryDto MapEntityToDto(Country entity)
        {
            return TinyMapper.Map<CountryDto>(entity);
        }

        public CountryDto Get(int id)
        {
            return Db.UniOfWork(uow => MapEntityToDto(uow.Repository<Country, Country>(repo => repo.SingleOrDefault(o => o.Id == id))));
        }

        public List<CountryDto> Get(int take, int skip)
        {
            return Db.UniOfWork(uow => uow.Repository<Country, List<CountryDto>>(repo => repo.All().OrderBy(o => o.Name).Skip(skip).Take(take).Select(a => MapEntityToDto(a)).ToList()));
        }

        public List<CountryDto> All()
        {
            return Db.UniOfWork(uow => uow.Repository<Country, List<CountryDto>>(repo => repo.All().OrderBy(o => o.Name).Select(a => MapEntityToDto(a)).ToList()));
        }

        public void Add(CountryDto country)
        {
            Db.UniOfWork(uow => uow.Repository<Country>(repo => repo.Add(MapDtoToEntity(country))));
        }

        public void Edit(CountryDto country)
        {
            Db.UniOfWork(uow => uow.Repository<Country>(repo => repo.Update(MapDtoToEntity(country))));
        }

        public void Delete(int id)
        {
            Db.UniOfWork(uow => uow.Repository<Country>(repo => repo.Remove(repo.SingleOrDefault(o => o.Id == id))));
        }
    }
}
