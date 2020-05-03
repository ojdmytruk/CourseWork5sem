﻿using System;
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
    public class SubjectController : Controller
    {
        ISubjectService subjectService;
        IEducationProcessService educationService;

        public SubjectController() { }

        public SubjectController(ISubjectService subjectService, IEducationProcessService educationService)
        {
            this.subjectService = subjectService;
            this.educationService = educationService;
        }

        [HttpGet]
        public ActionResult ShowSubject(string searchName, string searchSubjectAvg, string searchProgress)
        {
            ViewBag.subjects = subjectService.Get().OrderBy(s => s.SubjectAvg);

            IEnumerable<SubjectDTO> subjectDtos = subjectService.Get().OrderBy(s => s.Name);
            List<double> subjectAvgs = new List<double>();
            foreach (var subject in subjectDtos)
            {
                subjectAvgs.Add(subject.SubjectAvg);
            }
            subjectAvgs = subjectAvgs.Distinct().ToList();
            subjectAvgs.Sort();
            ViewBag.subjectAvgs = subjectAvgs;

            if (!String.IsNullOrEmpty(searchName))
            {
                subjectDtos = subjectService.SearchByName(subjectDtos, searchName);
            }
            if (!String.IsNullOrEmpty(searchSubjectAvg) && (searchSubjectAvg != "Всі"))
            {
                subjectDtos = subjectService.SearchBySubjectAvg(subjectDtos, searchSubjectAvg);
            }
            if (!String.IsNullOrEmpty(searchProgress))
            {
                subjectDtos = subjectService.SearchByProgress(subjectDtos, searchProgress);
            }

            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<SubjectDTO, SubjectViewModel>()).CreateMapper();
            var students = mapper.Map<IEnumerable<SubjectDTO>, IEnumerable<SubjectViewModel>>(subjectDtos);

            return View(students);
        }

        public ActionResult SubjectDetails(int idSubject)
        {
            SubjectDTO subjectDTO = subjectService.GetSubject(idSubject);
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<SubjectDTO, SubjectViewModel>()).CreateMapper();
            var subjectVM = mapper.Map<SubjectDTO, SubjectViewModel>(subjectDTO);

            ViewBag.subjectName = subjectVM.Name;
            ViewBag.subjectAvg = subjectService.GetSubjectAvg(idSubject).ToString("0.00");

            Dictionary<string, int> subjectResult = educationService.GetSubjectDetail(idSubject);
            return View(subjectResult);
        }

        public ActionResult DeleteSubject(int idSubject)
        {
            IEnumerable<EducationProcessDTO> eduList = educationService.GetSubbjectList(idSubject);

            if (eduList.Count() == 0)
            {
                subjectService.DeleteSubject(idSubject);
                ViewBag.message = "Предмет успішно видалено";
                return View("Report");
            }

            if (subjectService.Get().FirstOrDefault(s => s.Id == idSubject) == null)
            {
                ViewBag.message = "Предмет не існує";
                return View("Report");
            }

            if (eduList.Count() != 0)
            {
                ViewBag.message = "Помилка видалення: студенти ще мають оцінки з даного предмету";
                return View("Report");
            }

            ViewBag.message = "Помилка при видаленні";
            return View("Report");
        }

        [HttpGet]
        public ActionResult EditSubject(int idSubject)
        {
            SubjectDTO subjectDTO = subjectService.GetSubject(idSubject);

            EditSubjectViewModel editSubjectVM = new EditSubjectViewModel()
            {
                Id = subjectDTO.Id,
                Name = subjectDTO.Name,
                SubjectAvg = subjectDTO.SubjectAvg
            };

            return View("EditSubject", editSubjectVM);
        }
        [HttpPost]
        public ActionResult EditSubject(EditSubjectViewModel editSubjectVM)
        {
            Validate validate = new Validate();
            if (!(validate.ValidationSubjectName(editSubjectVM.Name)))
            {
                ViewBag.message = "Неправильна назва предмету";
                return View("Report");
            }

            SubjectDTO subjectDTO1 = subjectService.Get().Where(s => s.Name == editSubjectVM.Name).FirstOrDefault();

            if (subjectDTO1 != null)
            {
                ViewBag.message = "Предмет з такою назвою вже існує";
                return View("Report");
            }
            IMapper mapper = new MapperConfiguration(cfg => cfg.CreateMap<EditSubjectViewModel, SubjectDTO>()).CreateMapper();
            SubjectDTO subjectDTO = mapper.Map<EditSubjectViewModel, SubjectDTO>(editSubjectVM);

            subjectService.EditSubject(subjectDTO);
            ViewBag.message = "Предмет оновлено";
            return View("Report");
        }

        [HttpGet]
        public ActionResult CreateSubject()
        {
            SubjectViewModel subjectVM = new SubjectViewModel();
            return View(subjectVM);
        }
        [HttpPost]
        public ActionResult CreateSubject(SubjectViewModel subjectVM)
        {
            if (ModelState.IsValid)
            {
                Validate validate = new Validate();
                if (!(validate.ValidationSubjectName(subjectVM.Name)))
                {
                    ViewBag.message = "Неправильна назва предмету";
                    return View("Report");
                }

                SubjectDTO subjectDTO1 = subjectService.Get().Where(s => s.Name == subjectVM.Name).FirstOrDefault();

                if (subjectDTO1 != null)
                {
                    ViewBag.message = "Предмет з такою назвою вже існує";
                    return View("Report");
                }

                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<SubjectViewModel, SubjectDTO>()).CreateMapper();
                SubjectDTO subjectDTO = mapper.Map<SubjectViewModel, SubjectDTO>(subjectVM);
                subjectService.AddSubject(subjectDTO);

                ViewBag.message = "Предмет додано";
                return View("Report");
            }
            return View(subjectVM);
        }

        public ActionResult Report()
        {
            return View();
        }
    }
}