using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Entities.Concrete;
using Core.Utilities.Result;
using Core.Utilities.Security.Hashing;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public interface IUserRepository
    {
       
        User GetUserById(int userId);
        void AddUser(User user);
        
    }
    public class UserManager : IUserService
    {
        IUserDal _userDal;
        private readonly IUserRepository _userRepository;

        public UserManager(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public IDataResult<List<OperationClaim>> GetClaims(User user)
        {
            return new SuccesDataResult<List<OperationClaim>>(_userDal.GetClaims(user));
        }

        public IResult Add(User user)
        {
            _userDal.Add(user);
            return new SuccessResult(Massages.UserAdded);
        }

        public IDataResult<User> GetByMail(string email)
        {
            return new SuccesDataResult<User>(_userDal.Get(u => u.Email == email));
        }

        public IDataResult<List<User>> GetAll()
        {
            return new SuccesDataResult<List<User>>(_userDal.GetAll());
        }

        public IResult Delete(User user)
        {
            _userDal.Delete(user);
            return new SuccessResult(Massages.UserDeleted);
        }

        public IResult Update(User user)
        {
            _userDal.Update(user);
            return new SuccessResult(Massages.UserUpdated);
        }

        public IDataResult<User> GetByUserId(int userId)
        {
            return new SuccesDataResult<User>(_userDal.Get(u => u.Id == userId));
        }

        public IResult EditProfil(User user, string password)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);
            var updatedUser = new User
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Status = user.Status
            };
            _userDal.Update(updatedUser);
            return new SuccessResult(Massages.UserUpdated);
        }

        public IDataResult<User> GetUserByEmail(string email)
        {
            return new SuccesDataResult<User>(_userDal.Get(u => u.Email == email));
        }

        public IDataResult<User> GetById(int id)
        {
            if (DateTime.Now.Hour == 00)
            {
                return new ErrorDataResult<User>(Massages.MaintenanceTime);
            }
            return new SuccesDataResult<User>(_userDal.Get(b => b.Id == id));
        }

    }
}




