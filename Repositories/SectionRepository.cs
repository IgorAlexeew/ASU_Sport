using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASUSport.Models;
using ASUSport.Repositories.Impl;
using ASUSport.Helpers;
using ASUSport.DTO;

namespace ASUSport.Repositories
{
    public class SectionRepository : ISectionRepository
    {
        private ApplicationContext db;

        public SectionRepository(ApplicationContext context)
        {
            db = context;
        }

        /// <inheritdoc/>
        public Response AddSection(SectionDTO data)
        {
            var sportObject = db.SportObjects.First(s => s.Id == data.SportObject);

            if (sportObject == null)
            {
                return new Response()
                {
                    Status = false,
                    Type = "SportObjectNotFound",
                    Message = "Спортивный объект не найден"
                };
            }

            var section = new Section()
            {
                SportObject = sportObject,
                Name = data.Name,
                Duration = data.Duration
            };

            db.Sections.Add(section);
            db.SaveChanges();

            return new Response()
            {
                Status = true,
                Type = "success",
                Message = "OK"
            };
        }

        /// <inheritdoc/>
        public List<object> GetSections(string name, string sportobject)
        {
            var sections = db.Sections.Select(s => s).ToList();

            if (name != null)
            {
                sections = sections.Where(s => s.Name == name).ToList();
            }

            if (sportobject != null)
            {
                var obj = db.SportObjects.First(s => s.Id == int.Parse(sportobject));

                sections = sections.Where(s => s.SportObject == obj).ToList();
            }

            var result = new List<object>();

            foreach (var section in sections)
            {
                result.Add(
                    new {section.Id, section.Name, sportobject = section.SportObject.Name, section.Duration }    
                );
            }

            return result;
        }
    }
}
