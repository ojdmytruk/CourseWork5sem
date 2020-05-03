using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL.Interfaces;
using BLL.DTO;
using WEB.Models;
using AutoMapper;
using WEB.Models.EditModels;
using WEB.Validation;

namespace WEB.Controllers
{
    public class GroupController : Controller
    {
        IStudentService studentService;
        IGroupService groupService;

        public GroupController() { }

        public GroupController(IStudentService studentService, IGroupService groupService)
        {
            this.studentService = studentService;
            this.groupService = groupService;
        }

        
        public ActionResult ShowGroups()
        {
            IEnumerable<GroupDTO> groupDtos = groupService.Get().OrderBy(g => g.Name);
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<GroupDTO, GroupViewModel>()).CreateMapper();
            var groups = mapper.Map<IEnumerable<GroupDTO>, List<GroupViewModel>>(groupDtos);

            return View(groups);
        }

        
        public ActionResult GroupDetails(int idGroup)
        {
            IEnumerable<StudentDTO> studentDtos = studentService.GetGroupList(idGroup);
            List<StudentDTO> studentList = new List<StudentDTO>();

            foreach (StudentDTO student in studentDtos)
            {
                student.StudentAvg = studentService.GetStudentAvg(student.Id);
                studentList.Add(student);
            }
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<StudentDTO, StudentViewModel>()).CreateMapper();
            var students = mapper.Map<List<StudentDTO>, List<StudentViewModel>>(studentList);

            ViewBag.groupName = groupService.GetGroup(idGroup).Name;
            return View(students);
        }

        
        public ActionResult DeleteGroup(int idGroup)
        {
            IEnumerable<StudentDTO> studentList = studentService.GetGroupList(idGroup);

            if (groupService.Get().FirstOrDefault(r => r.Id == idGroup) == null)
            {
                ViewBag.message = "Такої групи не існує";
                return View("Report");
            }

            if (studentList.Count() != 0)
            {
                ViewBag.message = "Помилка видалення: список студентів групи не пустий";
                return View("Report");
            }

            groupService.DeleteGroup(idGroup);
            ViewBag.message = "Групу успішно видалено";
            return View("Report");
        }

        
        public ActionResult EditGroup(int idGroup)
        {
            GroupDTO groupDTO = groupService.GetGroup(idGroup);

            EditGroupViewModel editGroupVM = new EditGroupViewModel()
            {
                Id = groupDTO.Id,
                Name = groupDTO.Name
            };

            return View("EditGroup", editGroupVM);
        }

        [HttpPost]
        public ActionResult EditGroup(EditGroupViewModel editGroupVM)
        {
            if (ModelState.IsValid)
            {
                Validate validate = new Validate();
                if (!(validate.ValidationGroupName(editGroupVM.Name)))
                {
                    ViewBag.message = "Неправильна назва групи";
                    return View("Report");
                }

                GroupDTO groupDTO = groupService.GetGroup(editGroupVM.Id);

                if (groupService.Get().ToList().Contains(groupService.Get().Where(g => g.Name == editGroupVM.Name).FirstOrDefault()))
                {
                    ViewBag.message = "Група з таким ім'ям вже існує";
                    return View("Report");
                }

                IMapper mapper = new MapperConfiguration(cfg => cfg.CreateMap<EditGroupViewModel, GroupDTO>()).CreateMapper();
                groupDTO = mapper.Map<EditGroupViewModel, GroupDTO>(editGroupVM);

                groupService.EditGroup(groupDTO);

                IEnumerable<GroupDTO> groupDtos = groupService.Get();
                var mapperGroups = new MapperConfiguration(cfg => cfg.CreateMap<GroupDTO, GroupViewModel>()).CreateMapper();
                var groups = mapperGroups.Map<IEnumerable<GroupDTO>, List<GroupViewModel>>(groupDtos);
                return View("ShowGroups", groups);
            }

            return View(editGroupVM);
        }

        
        [HttpGet]
        public ActionResult CreateGroup()
        {
            GroupViewModel groupVM = new GroupViewModel();
            return View(groupVM);
        }
        [HttpPost]
        public ActionResult CreateGroup(GroupViewModel groupVM)
        {
            if (ModelState.IsValid)
            {
                Validate validate = new Validate();
                if (!(validate.ValidationGroupName(groupVM.Name)))
                {
                    ViewBag.message = "Неправильна назва групи";
                    return View("Report");
                }

                GroupDTO groupDTO1 = groupService.Get().Where(g => g.Name == groupVM.Name).FirstOrDefault();

                if (groupDTO1 == null)
                {
                    ViewBag.message = "Група з таким ім'ям вже існує";
                    return View("Report");
                }

                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<GroupViewModel, GroupDTO>()).CreateMapper();
                GroupDTO groupDTO = mapper.Map<GroupViewModel, GroupDTO>(groupVM);
                groupService.AddGroup(groupDTO);

                IEnumerable<GroupDTO> groupDtos = groupService.Get();
                var mapperGroups = new MapperConfiguration(cfg => cfg.CreateMap<GroupDTO, GroupViewModel>()).CreateMapper();
                var groups = mapperGroups.Map<IEnumerable<GroupDTO>, List<GroupViewModel>>(groupDtos);
                return View("ShowGroups", groups);
            }

            return View(groupVM);
        }

        
        public ActionResult Report()
        {
            return View();
        }


    }
}