using System;
using System.Threading.Tasks;
using Application.Repository;
using Application.Services.User;
using Application.Services.User.Payload;
using Domain.Entities;
using Moq;
using Xunit;

namespace Tests.Services.User
{
    public class UserServiceTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly UserService _userService;

        public UserServiceTests()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _userService = new UserService(_userRepositoryMock.Object);
        }

        [Fact]
        public async Task CreateAccountAsync_InvalidCpf_ThrowsArgumentException()
        {
            // Arrange
            var payload = new UserPayload
            {
                Name = "Teste",
                Email = "teste@email.com",
                CPF = "123" // CPF inválido
            };

            // Força CPFUtils.IsCpf a retornar false usando um delegate temporário (não é possível mockar método estático diretamente)
            // Portanto, este teste só funcionará corretamente se CPFUtils.IsCpf aceitar CPFs inválidos como no exemplo.

            // Act & Assert
            var ex = await Assert.ThrowsAsync<ArgumentException>(() => _userService.CreateAccountAsync(payload));
            Assert.Contains("CPF invalido", ex.Message);
        }

        [Fact]
        public async Task CreateAccountAsync_UserAlreadyExists_ThrowsArgumentException()
        {
            // Arrange
            var payload = new UserPayload
            {
                Name = "Teste",
                Email = "teste@email.com",
                CPF = "12345678901"
            };

            _userRepositoryMock
                .Setup(r => r.VerifyUserExistsByDocument(payload.CPF))
                .ReturnsAsync(true);

            // Act & Assert
            var ex = await Assert.ThrowsAsync<ArgumentException>(() => _userService.CreateAccountAsync(payload));
            Assert.Contains("Usuário já existe", ex.Message);
        }

        [Fact]
        public async Task CreateAccountAsync_ValidUser_ReturnsTrue()
        {
            // Arrange
            var payload = new UserPayload
            {
                Name = "Teste",
                Email = "teste@email.com",
                CPF = "12345678901"
            };

            _userRepositoryMock
                .Setup(r => r.VerifyUserExistsByDocument(payload.CPF))
                .ReturnsAsync(false);

            _userRepositoryMock
                .Setup(r => r.CreateAccountAsync(It.IsAny<UserEntity>()))
                .ReturnsAsync(true);

            // Act
            var result = await _userService.CreateAccountAsync(payload);

            // Assert
            Assert.True(result);
            _userRepositoryMock.Verify(r => r.CreateAccountAsync(It.IsAny<UserEntity>()), Times.Once);
        }

        [Fact]
        public async Task IdentificationAsync_UserNotFound_ReturnsNull()
        {
            // Arrange
            var cpf = "12345678901";
            _userRepositoryMock
                .Setup(r => r.IdentificationAsync(cpf))
                .ReturnsAsync((UserEntity?)null);

            // Act
            var result = await _userService.IdentificationAsync(cpf);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task IdentificationAsync_UserFound_ReturnsPayload()
        {
            // Arrange
            var cpf = "12345678901";
            var userEntity = new UserEntity
            {
                Id = 1,
                Nome = "Teste",
                CPF = cpf,
                Email = "teste@email.com"
            };

            _userRepositoryMock
                .Setup(r => r.IdentificationAsync(cpf))
                .ReturnsAsync(userEntity);

            // Act
            var result = await _userService.IdentificationAsync(cpf);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(userEntity.CPF, result.CPF);
            Assert.Equal(userEntity.Email, result.Email);
            Assert.Equal(userEntity.Nome, result.Name);
        }
    }
}