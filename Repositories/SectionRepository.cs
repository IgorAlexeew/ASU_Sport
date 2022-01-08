using System.Collections.Generic;
using System.Linq;
using ASUSport.Models;
using ASUSport.Repositories.Impl;
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
            //db.SaveChanges();

            return new Response()
            {
                Status = true,
                Type = "success",
                Message = "OK"
            };
        }

        public Response DeleteSection(int id)
        {
            var section = db.Sections.FirstOrDefault(s => s.Id == id);

            db.Sections.Remove(section);
            db.SaveChanges();
            
            return new Response()
            {
                Status = true,
                Type = "success",
                Message = "OK"
            };
        }

        /// <inheritdoc/>
        public List<SectionInfoDTO> GetSections(string name, string sportobject)
        {
            var sections = db.Sections.Select(s => s).ToList();

            if (name != null)
                sections = sections.Where(s => s.Name == name).ToList();

            if (sportobject != null)
            {
                var obj = db.SportObjects.First(s => s.Id == int.Parse(sportobject));

                sections = sections.Where(s => s.SportObject == obj).ToList();
            }

            var result = new List<SectionInfoDTO>();

            foreach (var section in sections)
            {
                var sect = new SectionInfoDTO()
                {
                    Id = section.Id,
                    Name = section.Name,
                    Duration = section.Duration,
                    SportObjectId = section.SportObject.Id,
                };
                
                result.Add(sect);
            }

            return result;
        }

        /// <inheritdoc/>
        public Response UpdateSection(UpdateSectionDTO data)
        {
            var section = db.Sections.FirstOrDefault(s => s.Id == (int)data.Id);

            if (data.SportObject != null)
            {
                section.SportObjectId = (int) data.SportObject;
            }

            if (data.Name != null)
            {
                section.Name = data.Name;
            }

            if (data.Duration != null)
            {
                section.Duration = (int) data.Duration;
            }

            db.Sections.Update(section);
            //db.SaveChanges();

            return new Response()
            {
                Status = true,
                Type = "success",
                Message = "OK"
            };
        }

        /// <inheritdoc/>
        public Response UpdateTable(List<UpdateSectionDTO> data)
        {
            foreach (var section in data)
            {
                if (section.Id != null)
                    UpdateSection(section);

                else
                {
                    var sportObject = db.SportObjects.FirstOrDefault(s => s.Id == (int)section.SportObject);
                    
                    var newSection = new Section()
                    {
                        Name = section.Name,
                        SportObject = sportObject,
                        SportObjectId = (int)section.SportObject,
                        Duration = (int)section.Duration,
                    };

                    db.Sections.Add(newSection);
                }
            }

            db.SaveChanges();

            return new Response()
            {
                Status = true,
                Type = "success",
                Message = "OK"
            };
        }

        /// <inheritdoc/>
        public int GetNumberOfEntities()
        {
            return db.Sections.Count();
        }
    }
}
