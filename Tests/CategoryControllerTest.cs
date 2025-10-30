using M223PunchclockDotnet.Controllers;
using M223PunchclockDotnet.Model;
using M223PunchclockDotnet.Service;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace M223PunchclockDotnet.Tests
{
    [TestClass]
    public class CategoryControllerTest
    {
        private Mock<ICategoryService> _categoryServiceMock = null!;
        private CategoryController _categoryController = null!;

        [TestInitialize]
        public void Setup()
        {
            _categoryServiceMock = new Mock<ICategoryService>();
            _categoryController = new CategoryController(_categoryServiceMock.Object);
        }


        [TestMethod]
        public async Task GetAll_ReturnsOk_WithListOfCategories()
        {
            var categories = new List<Category>
            {
                new Category { Id = 1, Title = "test1" },
                new Category { Id = 2, Title = "test2" }
            };

            _categoryServiceMock
                .Setup(s => s.GetCategoriesAsync())
                .ReturnsAsync(categories);

            var result = await _categoryController.GetAll();

            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            var returnedCategories = okResult.Value as IEnumerable<Category>;
            Assert.AreEqual(2, (returnedCategories as List<Category>)!.Count);
        }

        [TestMethod]
        public async Task GetAll_ReturnsEmptyList_WhenNoCategoriesExist()
        {
            _categoryServiceMock
                .Setup(s => s.GetCategoriesAsync())
                .ReturnsAsync(new List<Category>());

            var result = await _categoryController.GetAll();

            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            var returnedCategories = okResult.Value as List<Category>;
            Assert.AreEqual(0, returnedCategories!.Count);
        }

        [TestMethod]
        public async Task GetById_ReturnsOk_WhenCategoryExists()
        {
            var category = new Category { Id = 2, Title = "test2" };
            _categoryServiceMock
                .Setup(s => s.GetCategoryByIdAsync(1))
                .ReturnsAsync(category);

            var result = await _categoryController.GetById(1);

            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            var returnedCategory = okResult.Value as Category;
            Assert.AreEqual(2, returnedCategory!.Id);
        }     
        
        [TestMethod]
        public async Task GetById_ReturnsNotFound_WhenCategoryExists()
        {
            _categoryServiceMock
                .Setup(s => s.GetCategoryByIdAsync(1))
                .ReturnsAsync((Category)null!);

            var result = await _categoryController.GetById(1);

           Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task Create_ReturnsCreatedAtAction_WhenCategoryIsCreated()
        {
            var newCategory = new Category { Id = 1, Title = "test1" };
            _categoryServiceMock
                .Setup(s => s.AddCategoryAsync(It.IsAny<Category>()))
                .ReturnsAsync(newCategory);

            // Act
            var result = await _categoryController.Create(new Category { Title = "test1" });

            // Assert
            var createdResult = result.Result as CreatedAtActionResult;
            Assert.IsNotNull(createdResult);
            Assert.AreEqual(nameof(_categoryController.GetById), createdResult.ActionName);
            Assert.AreEqual(newCategory, createdResult.Value);
        }
    }
}
