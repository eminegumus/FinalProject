using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.CCS;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validations;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Entities.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        IProductDal _productdal;
        ICategoryService _categoryService;
        ILogger _logger;
        public ProductManager(IProductDal productdal, ICategoryService categoryService)
        {
            _productdal = productdal;
            _categoryService = categoryService;
            // _logger = logger;
        }
       [SecuredOperation("product.add,admin")]
        [ValidationAspect(typeof(ProductValidator))]
        public IResult Add(Product product)
        {
            //if (product.UnitPrice<=0)
            //{
            //    return new ErrorResult(Messages.UnitPriceInvalid);
            //}
            //if (product.ProductName.Length<=2)
            //{
            //    return new ErrorResult(Messages.ProductNameInvalid);
            //}
            //[ValidationAspect(typeof(ProductValidator))] geldiği için burası iptal ValidationTool.Validate(new ProductValidator(), product);
            //_logger.Log();
            //try
            //{
            IResult result = BusinessRules.Run(CheckIfProductNameExists(product.ProductName), CheckIfProductCountOfCategoryCorrect(product.CategoryId),CheckIfCategoryLimitExcided());
            if (result != null)
            {
                return result;
            }
            _productdal.Add(product);

            return new SuccessResult(Messages.ProductAdded);
            //if (CheckIfProductCountOfCategoryCorrect(product.CategoryId).Success)
            //{
            //    if (CheckIfProductNameExists(product.ProductName).Success)
            //    {
            //        _productdal.Add(product);

            //        return new SuccessResult(Messages.ProductAdded);

            //    }
            //}


            //}
            //catch (Exception)
            //{

            //   _logger?.Log();
            //}
            return new ErrorResult();
        }

        public IDataResult<Product> Get(int productId)
        {
            return new SuccessDataResult<Product>(_productdal.Get(p => p.ProductId == productId), "Ürün listelendi");
        }

        public IDataResult<List<Product>> GetAll()
        {
            if (DateTime.Now.Hour == 22)
            {
                return new ErrorDataResult<List<Product>>(Messages.MaintenanceTime);
            }
            return new SuccessDataResult<List<Product>>(_productdal.GetAll(), Messages.ProductedListed);
        }
        public IDataResult<List<Product>> GetAllByCategory(int id)
        {
            return new SuccessDataResult<List<Product>>(_productdal.GetAll(p => p.CategoryId == id));
        }

        public IDataResult<List<Product>> GetByUnitPrice(decimal min, decimal max)
        {
            return new SuccessDataResult<List<Product>>(_productdal.GetAll(p => p.UnitPrice >= min && p.UnitPrice <= max));
        }

        public IDataResult<List<ProductDetailDto>> GetProductDetails()
        {
            return new SuccessDataResult<List<ProductDetailDto>>(_productdal.GetProductDetails(), "Ürün detayları Listendi.");
        }

        public IResult Update(Product product)
        {

            var productCountBasedOnCategory = _productdal.GetAll(x => x.CategoryId == product.CategoryId).Count();


            if (productCountBasedOnCategory >= 10)
            {
                return new ErrorResult(Messages.ProductCountOfCategoryError);
            }
            throw new NotImplementedException();
        }

        private IResult CheckIfProductCountOfCategoryCorrect(int categoryId)
        {
            var result = _productdal.GetAll(p => p.CategoryId == categoryId).Count();

            if (result >= 100)
            {
                return new ErrorResult(Messages.ProductCountOfCategoryError);
            }

            return new SuccessResult();
        }

        private IResult CheckIfProductNameExists(string productName)
        {
            var result = _productdal.GetAll(p => p.ProductName == productName).Any();
            if (result)
            {
                return new ErrorResult(Messages.ProductNameAlreadyExists);
            }

            return new SuccessResult();
        }

        private IResult CheckIfCategoryLimitExcided()
        {
            var result = _categoryService.GetAll();
            if (result.Data.Count>=150)
            {
                return new ErrorResult(Messages.CategoryLimitExcided);
            }

            return new SuccessResult();
        }
    }
}
