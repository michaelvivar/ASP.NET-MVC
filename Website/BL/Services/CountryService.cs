using BL.Dto;
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

        public CountryDto Add(CountryDto country)
        {
            return Db.UniOfWork(uow => uow.Repository<Country, CountryDto>(repo =>
            {
                var entity = MapDtoToEntity(country);
                repo.Add(entity);
                uow.SaveChanges();
                country.Id = entity.Id;
                return country;
            }));
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

    public class CountryValidatorService : BaseService
    {
        public bool CountryNameExists(string name)
        {
            return Db.UniOfWork(uow => uow.Repository<Country, bool>(repo => repo.All().Any(o => o.Name == name)));
        }

        public bool CountryCodeExists(string code)
        {
            return Db.UniOfWork(uow => uow.Repository<Country, bool>(repo => repo.All().Any(o => o.Code == code)));
        }
    }
}
