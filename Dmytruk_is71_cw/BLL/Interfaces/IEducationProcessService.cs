using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTO;

namespace BLL.Interfaces
{
    public interface IEducationProcessService
    {
        IEnumerable<EducationProcessDTO> Get();
        EducationProcessDTO GetEducation(int idEducation);
        void DeleteEducation(int id);
        void AddSubject(EducationProcessDTO educationProcessDTO);
        void EditEducation(EducationProcessDTO educationProcessDTO);
        string GetStudentName(int idStudent);
        GroupDTO GetStudentGroup(int idStudent);
        SubjectDTO GetStudentSubject(int idEducation);
        string SetSubjectName(int idEducation);
        SubjectDTO GetSubject(int idSubject);
        Dictionary<string, int> GetStudentDetail(int idStudent);
        Dictionary<string, int> GetSubjectDetail(int idSubject);
        string SetGroupName(int idStudent);
        IEnumerable<EducationProcessDTO> GetSubbjectList(int idSubject);
        IEnumerable<EducationProcessDTO> GetStudentList(int idStudent);
        void Dispose();
    }
}
