using Checkout.Com.PaymentGateway.Services.Encryption;
using Xunit;
using Moq;
using Microsoft.Extensions.Options;
using Checkout.Com.PaymentGateway.Models.Options;

namespace Checkout.Com.PaymentGateway.Tests.Unit
{
    public class AesServiceTests
    {
        [Fact]
        public void EncryptAndDecryptWorks()
        {
            //Arrange
            var optionsMock = new Mock<IOptions<EncryptionOptions>>();
            optionsMock.SetupGet(x => x.Value).Returns(new EncryptionOptions() { IV = "5D5F9A8EB43FDBB9", Key = "AD784C94ABFE8176" });
            var sut = new AesService(optionsMock.Object);

            //Act
            var want = "sample string";
            var encrypted = sut.Encrypt(want);
            var got = sut.Decrypt(encrypted);

            //Assert
            Assert.Equal(want, got);
        }
    }
}
