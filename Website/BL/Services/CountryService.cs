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
        public List<CountryDto> GetAll()
        {
            //return Repository<Country>().Get().OrderBy(o => o.Name).Select(o => new CountryDto
            //{
            //    Id = o.Id,
            //    Name = o.Name,
            //    Code = o.Code
            //}).ToList();

            Transaction.Service<UserService>(o =>
            {
                o.Login();
            });

            return (from a in Repository<Country>().Get()
                    orderby a.Name
                    select new CountryDto
                    {
                        Id = a.Id,
                        Name = a.Name,
                        Code = a.Code
                    }).ToList();
        }

        public List<RegionDto> GetRegions(int countryId)
        {
            return (from a in Repository<Country>().Get()
                    join b in Repository<Region>().Get()
                    on a.Id equals b.Id
                    where a.Id == countryId
                    select new RegionDto
                    {
                        Id = b.Id,
                        Name = b.Name,
                        Code = b.Code
                    }).ToList();
        }

        public void Add(CountryDto country)
        {
            var entity = new Country
            {
                Name = country.Name,
                Code = country.Code
            };

            Repository<Country>((repo, uow) =>
            {
                repo.Add(entity);
                //Repository<Region>(r => r.Add(new Region { Name = "Region III", Code = "Region III", Country = entity }));
                uow.SaveChanges();
            });
        }

        public void Delete(int id)
        {
            Repository<Country>(r =>
            {
                var e = r.SingleOrDefault(o => o.Id == id);
                r.Remove(e);
            });
        }
    }
}
