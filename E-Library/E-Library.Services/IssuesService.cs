using E_Library.Models;
using E_Library.Models.EntityModels;
using E_Library.Repositories.Abstractions;
using E_Library.Services.Abstractions;
using E_Library.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_Library.Services
{
    public class IssuesService : Service<Issue>, IIssuesService
    {
        IIssuesRepository _issuesRepository;
        public IssuesService(IIssuesRepository issuesRepository) : base(issuesRepository)
        {
            _issuesRepository = issuesRepository;
        }

        public override Result Create(Issue entity)
        {
            //unique code
            var result = new Result();
            var codeResult = _issuesRepository.Get(x => x.IssueNo == entity.IssueNo);
            if (codeResult.Any())
            {
                result.IsSucced = false;
                result.ErrorMessages.Add("Book Issue  already Exists with same Issue No ..!");
            }
            if (result.ErrorMessages.Any())
            {
                return result;
            }

            bool isSuccess = _issuesRepository.Create(entity);
            if (isSuccess)
            {
                result.IsSucced = true;
            }
            result.ErrorMessages.Add("Author Not Added");
            return result;
        }
        public override Result Update(Issue entity)
        {
            var result = new Result();

            bool isSuccess = _issuesRepository.Update(entity);
            if (isSuccess)
            {
                result.IsSucced = true;
            }
            result.ErrorMessages.Add("Issues not Updated");

            return result;
        }
        public override Result Remove(Issue entity)
        {
            var result = new Result();
            bool isSuccess = _issuesRepository.Remove(entity);
            if (isSuccess)
            {
                result.IsSucced = true;
            }
            result.ErrorMessages.Add("Issues not Removed");
            return result;
        }
    }
}
