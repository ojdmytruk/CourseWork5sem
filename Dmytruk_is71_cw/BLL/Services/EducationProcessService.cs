using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTO;
using DAL.Entities;
using DAL.Interfaces;
using BLL.Infrastructure;
using BLL.Interfaces;
using AutoMapper;


namespace BLL.Services
{
    public class EducationProcessService : IEducationProcessService
    {
        IUnitOfWork Database { get; set; }

        public EducationProcessService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public IEnumerable<EducationProcessDTO> Get()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<EducationProcess, EducationProcessDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<EducationProcess>, List<EducationProcessDTO>>(Database.EducationsRepository.Get());
        }

        public EducationProcessDTO GetEducation(int id)
        {
            var education = Database.EducationsRepository.FindById(id);
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<EducationProcess, EducationProcessDTO>()).CreateMapper();
            return mapper.Map<EducationProcess, EducationProcessDTO>(education);
        }

        public void AddSubject(EducationProcessDTO educationDTO)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<EducationProcessDTO, EducationProcess>()).CreateMapper();
            Database.EducationsRepository.Create(mapper.Map<EducationProcessDTO, EducationProcess>(educationDTO));
        }

        public void DeleteEducation(int id)
        {
            EducationProcess education = Database.EducationsRepository.FindById(id);
            Database.EducationsRepository.Remove(education);
            Database.SaveChanges();
        }

        public void EditEducation(EducationProcessDTO educationDTO)
        {
            IMapper mapper = new MapperConfiguration(cfg => cfg.CreateMap<EducationProcessDTO, EducationProcess>()).CreateMapper();
            EducationProcess education = Database.EducationsRepository.FindById(educationDTO.Id);
            education = mapper.Map<EducationProcessDTO, EducationProcess>(educationDTO);

            Database.EducationsRepository.Update(education);
            Database.SaveChanges();
        }

        public string GetStudentName(int idStudent)
        {
            Student student = Database.StudentsRepository.FindById(idStudent);
            return student.Name;
        }

        public GroupDTO GetStudentGroup(int idStudent)
        {
            Student student = Database.StudentsRepository.FindById(idStudent);
            Group group = Database.GroupsRepository.Get().Where(o => o.Id == student.IdGroup).FirstOrDefault();
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Group, GroupDTO>()).CreateMapper();
            return mapper.Map<Group, GroupDTO>(group);
        }

        public string SetGroupName(int idStudent)
        {
            EducationProcess education = Database.EducationsRepository.Get().Where(e => e.IdStudent == idStudent).FirstOrDefault();
            GroupDTO groupDTO = GetStudentGroup(education.IdStudent);
            education.GroupName = groupDTO.Name;
            return groupDTO.Name;
        }

        public SubjectDTO GetStudentSubject(int idEducation)
        {
            EducationProcess education = Database.EducationsRepository.FindById(idEducation);
            Subject subject = Database.SubjectsRepository.Get().Where(s => s.Id == education.IdSubject).FirstOrDefault();
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Subject, SubjectDTO>()).CreateMapper();
            return mapper.Map<Subject, SubjectDTO>(subject);
        }

        public string SetSubjectName(int idEducation)
        {
            EducationProcess education = Database.EducationsRepository.FindById(idEducation);
            SubjectDTO subjectDTO = GetStudentSubject(education.Id);
            return subjectDTO.Name;
        }

        public SubjectDTO GetSubject(int idSubject)
        {
            var subject = Database.SubjectsRepository.FindById(idSubject);
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Subject, SubjectDTO>()).CreateMapper();
            return mapper.Map<Subject, SubjectDTO>(subject);
        }

        public Dictionary<string, int> GetStudentDetail(int idStudent)
        {
            IEnumerable<EducationProcess> educations = Database.EducationsRepository.Get().Where(o => o.IdStudent == idStudent);
            Dictionary<string, int> subjectResult = new Dictionary<string, int>(); 

            foreach (EducationProcess education in educations)
            {
                subjectResult.Add(GetSubject(education.IdSubject).Name, education.SubjectResult);
            }

            return subjectResult;
        }

        public Dictionary<string, int> GetSubjectDetail(int idSubject)
        {
            IEnumerable<EducationProcess> educations = Database.EducationsRepository.Get().Where(o => o.IdSubject == idSubject);
            Dictionary<string, int> subjectResult = new Dictionary<string, int>();

            foreach (EducationProcess education in educations)
            {
                subjectResult.Add(GetStudentName(education.IdStudent), education.SubjectResult);
            }

            return subjectResult;
        }

        public IEnumerable<EducationProcessDTO> GetSubbjectList(int idSubject)
        {
            IEnumerable<EducationProcess> educations = Database.EducationsRepository.Get().Where(s => s.IdSubject == idSubject);
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<EducationProcess, EducationProcessDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<EducationProcess>, IEnumerable<EducationProcessDTO>>(educations);
        }

        public IEnumerable<EducationProcessDTO> GetStudentList(int idStudent)
        {
            IEnumerable<EducationProcess> educations = Database.EducationsRepository.Get().Where(s => s.IdStudent == idStudent);
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<EducationProcess, EducationProcessDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<EducationProcess>, IEnumerable<EducationProcessDTO>>(educations);
        }

        public void Dispose()
        {
            Database.Dispose();
        }


    }
}
