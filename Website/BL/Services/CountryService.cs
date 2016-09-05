using BL.Dto;
using DL;
using DL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services
{
    public class CountryService : BaseService 
    {
        public CountryDto Get(int id)
        {
            var country = Repository<Country>().SingleOrDefault(o => o.Id == id);
            return new CountryDto
            {
                Id = country.Id,
                Name = country.Name,
                Code = country.Code
            };
        }

        public List<CountryDto> Get(int take, int skip)
        {
            return (from a in Repository<Country>().Get().Skip(skip).Take(take)
                    orderby a.Name
                    select new CountryDto
                    {
                        Id = a.Id,
                        Name = a.Name,
                        Code = a.Code
                    }).ToList();
        }

        public List<CountryDto> GetAll()
        {
            return (from a in Repository<Country>().Get()
                    orderby a.Name
                    select new CountryDto
                    {
                        Id = a.Id,
                        Name = a.Name,
                        Code = a.Code
                    }).ToList();
        }

        public void Add(CountryDto country)
        {
            Repository<Country>((repo, uow) =>
            {
                repo.Add(new Country
                {
                    Name = country.Name,
                    Code = country.Code
                });
                uow.SaveChanges();
            });
        }

        public void Update(CountryDto country)
        {
            Repository<Country>((repo, uow) =>
            {
                repo.Update(new Country
                {
                    Id = country.Id,
                    Name = country.Name,
                    Code = country.Code
                });
                uow.SaveChanges();
            });
        }

        public void Delete(int id)
        {
            Repository<Country>((r, u) =>
            {
                var e = r.SingleOrDefault(o => o.Id == id);
                r.Remove(e);
                u.SaveChanges();
            });
        }
    }
}
